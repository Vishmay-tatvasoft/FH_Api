using ApiAuthentication.Entity.ViewModels;
using ApiAuthentication.Services.Interfaces;
using FH_Api_Demo.Repositories.Interfaces;
using FH_Api_Demo.Repositories.Models;
using FH_Api_Demo.Services.Helpers;
using FH_Api_Demo.Services.Interfaces;
using FH_Api_Demo.Services.ViewModels;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Caching.Memory;

namespace FH_Api_Demo.Services.Implementations;

public class UserService(IUserRepository userRepository, IJwtTokenService jwtTokenService, IMailService mailService, IMemoryCache memoryCache, IGenericRepository<FhUser> userGR) : IUserService
{
    #region Configuration Settings
    private readonly IGenericRepository<FhUser> _userGR = userGR;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IMailService _mailService = mailService;
    private readonly IMemoryCache _memoryCache = memoryCache;

    #endregion

    #region GetUserByUserNameAsync
    public async Task<ApiResponseVM<object>> GetUserByUserNameAsync(string userName)
    {
        FhUser? user = await _userRepository.GetUserByUsername(userName);
        if (user == null)
        {
            return new ApiResponseVM<object>(404, MessageHelper.USER_NOT_FOUND, null);
        }
        else
        {
            return new ApiResponseVM<object>(200, MessageHelper.USER_FOUND, user);
        }
    }
    #endregion


    #region Register User Async
    public async Task<ApiResponseVM<object>> RegisterUserAsync(SignUpVM signupVM)
    {
        #region Validating SignUp Viewmodel
        if (string.IsNullOrEmpty(signupVM.UserName) || string.IsNullOrEmpty(signupVM.FirstName) || string.IsNullOrEmpty(signupVM.LastName) || string.IsNullOrEmpty(signupVM.RoleId))
        {
            return new ApiResponseVM<object>(400, MessageHelper.INVALID_SIGNUP_REQUEST, null);
        }
        #endregion

        #region Checking Existing User
        FhUser? existingUser = await _userRepository.GetUserByUsername(signupVM.UserName);
        if (existingUser != null)
        {
            return new ApiResponseVM<object>(409, MessageHelper.USER_ALREADY_EXISTS, null);
        }
        #endregion

        #region OTP And Hashed OTP Using Argon2
        string otp = OTPGenerator.GenerateAlphaNumericOtp();
        string hashedOtp = Argon2.Hash(otp);
        #endregion

        #region Adding User To Database
        FhUser newUser = new()
        {
            EmailAddress = signupVM.EmailAddress,
            Password = hashedOtp,
            FirstName = signupVM.FirstName,
            LastName = signupVM.LastName,
            UserName = signupVM.UserName,
            RoleId = signupVM.RoleId,
            UserType = signupVM.RoleId == "User" ? "U" : "R",
            UserId = ""
        };

        await _userGR.AddRecord(newUser);
        await _userGR.SaveChangesAsync();
        #endregion

        return new ApiResponseVM<object>(201, MessageHelper.USER_REGISTERED_SUCCESSFULLY, newUser);
    }
    #endregion

    #region Validate User Credentials
    public async Task<ApiResponseVM<object>> ValidateCredentialsAsync(LoginVM loginVM)
    {
        FhUser? user = await _userRepository.GetUserByUsername(loginVM.UserName);

        if (user == null)
        {
            return new ApiResponseVM<object>(404, MessageHelper.USER_NOT_EXIST, null);
        }
        else if (!Argon2.Verify(user.Password, loginVM.Password)) // encrypted password verification here
        {
            return new ApiResponseVM<object>(401, MessageHelper.INVALID_CREDENTIALS, null);
        }
        else
        {
            string accessToken = _jwtTokenService.GenerateJwtToken(loginVM.UserName, user.UserId.ToString(), loginVM.RememberMe);
            string refreshToken = _jwtTokenService.GenerateRefreshTokenJwt(loginVM.UserName, user.UserId.ToString(), loginVM.RememberMe);
            return new ApiResponseVM<object>(200, string.Concat(MessageHelper.LOGIN, MessageHelper.SUCCESSFULLY), new TokenResponseVM { UserName = loginVM.UserName, RememberMe = loginVM.RememberMe, AccessToken = accessToken, RefreshToken = refreshToken });
        }
    }
    #endregion

    #region Refresh Token
    public async Task<ApiResponseVM<object>> RefreshTokenAsync(RefreshTokenVM refreshTokenVM)
    {
        if (string.IsNullOrEmpty(refreshTokenVM.RefreshToken))
        {
            return new ApiResponseVM<object>(400, MessageHelper.REFRESH_TOKEN_REQUIRED, null);
        }
        if (_jwtTokenService.IsRefreshTokenValid(refreshTokenVM.RefreshToken))
        {
            string userID = _jwtTokenService.GetClaimValue(refreshTokenVM.RefreshToken, "UserID");
            FhUser? user = await _userGR.GetRecordById(userID);
            if (user != null)
            {
                string newAccessToken = _jwtTokenService.GenerateJwtToken(user.UserName!, user.UserId.ToString(), refreshTokenVM.RememberMe);
                string newRefreshToken = _jwtTokenService.GenerateRefreshTokenJwt(user.UserName!, user.UserId.ToString(), refreshTokenVM.RememberMe);
                return new ApiResponseVM<object>(200, MessageHelper.TOKEN_REFRESHED, new TokenResponseVM { UserName = user.UserName, RememberMe = refreshTokenVM.RememberMe, AccessToken = newAccessToken, RefreshToken = newRefreshToken });
            }
            return new ApiResponseVM<object>(401, MessageHelper.USER_NOT_EXIST, null);
        }
        return new ApiResponseVM<object>(401, MessageHelper.INVALID_REFRESH_TOKEN, null);
    }
    #endregion

    #region Forgot Password
    public async Task<ApiResponseVM<object>> ForgotPasswordAsync(string email, string username)
    {
        FhUser? user = await _userRepository.GetUserByUsername(username);
        if (user == null || string.IsNullOrEmpty(user.EmailAddress))
        {
            return new ApiResponseVM<object>(404, MessageHelper.USER_NOT_EXIST, null);
        }
        else if (user.EmailAddress != email.ToLower())
        {
            return new ApiResponseVM<object>(400, MessageHelper.INVALID_EMAIL, null);
        }
        else
        {
            string resetToken = _jwtTokenService.GenerateJwtToken(user.UserName, user.UserId, false);
            string cacheKey = $"reset_token:{resetToken}";
            _memoryCache.Set(cacheKey, user.EmailAddress.ToLower(), TimeSpan.FromMinutes(15)); // auto-expiry
            await _mailService.SendResetPasswordLink(user.EmailAddress, user.UserName, resetToken);
            return new ApiResponseVM<object>(200, MessageHelper.RESET_LINK_SENT, null);
        }
    }
    #endregion

    #region Reset Password
    public async Task<ApiResponseVM<object>> ResetPasswordAsync(ResetPassVM resetPassVM)
    {
        string cacheKey = $"reset_token:{resetPassVM.Token}";

        if (_memoryCache.TryGetValue(cacheKey, out string? email))
        {
            if (_jwtTokenService.IsRefreshTokenValid(resetPassVM.Token))
            {
                string userID = _jwtTokenService.GetClaimValue(resetPassVM.Token, "UserID");
                FhUser? user = await userGR.GetRecordById(userID);
                if (user != null)
                {
                    user.Password = Argon2.Hash(resetPassVM.Password);
                    _userGR.UpdateRecord(user);
                    await _userGR.SaveChangesAsync();
                    _memoryCache.Remove(cacheKey); //allow for one time only
                    await _mailService.SendResetPasswordMessage(user.EmailAddress, user.UserName);
                    return new ApiResponseVM<object>(200, string.Join(" ", MessageHelper.PASSWORD, MessageHelper.RESET, MessageHelper.SUCCESSFULLY), null);
                }
                else
                {
                    return new ApiResponseVM<object>(404, MessageHelper.USER_NOT_EXIST, null);
                }
            }
        }
        return new ApiResponseVM<object>(410, MessageHelper.INVALID_LINK, null);
    }


    #endregion


}
