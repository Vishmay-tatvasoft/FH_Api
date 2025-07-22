using FH_Api_Demo.Repositories.Interfaces;
using FH_Api_Demo.Repositories.Models;
using FH_Api_Demo.Services.Helpers;
using FH_Api_Demo.Services.Interfaces;
using FH_Api_Demo.Services.ViewModels;
using Isopoh.Cryptography.Argon2;

namespace FH_Api_Demo.Services.Implementations;

public class UserService(IGenericRepository<FhUser> userGR, IUserRepository userRepository) : IUserService
{
    #region Configuration Settings
    private readonly IGenericRepository<FhUser> _userGR = userGR;
    private readonly IUserRepository _userRepository = userRepository;
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
}
