using ApiAuthentication.Entity.ViewModels;
using ApiAuthentication.Services.Interfaces;
using FH_Api_Demo.Services.Helpers;
using FH_Api_Demo.Services.Interfaces;
using FH_Api_Demo.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FH_Api_Demo.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService, IJwtTokenService jwtTokenService) : ControllerBase
{
    #region Configuration Settings
    private readonly IUserService _userService = userService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    #endregion

    #region Remote validation
    [HttpGet("check-user")]
    public async Task<IActionResult> GetUserByUserName(string userName)
    {
        ApiResponseVM<object> response = await _userService.GetUserByUserNameAsync(userName);
        if (response.StatusCode == 200)
        {
            return Ok(response);
        }
        else if (response.StatusCode == 404)
        {
            return NotFound(response);
        }
        else
        {
            return BadRequest(response);
        }
    }
    #endregion

    #region Register New User
    [HttpPost("Signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpVM signupVM)
    {
        if (signupVM == null)
        {
            return BadRequest(MessageHelper.INVALID_SIGNUP_REQUEST);
        }
        ApiResponseVM<object> response = await _userService.RegisterUserAsync(signupVM);
        if (response.StatusCode == 201)
        {
            return Ok(response);
        }
        else if (response.StatusCode == 409)
        {
            return Conflict(response); // User already exists
        }
        else
        {
            return BadRequest(response);
        }
    }
    #endregion

    #region Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
    {
        if (loginVM == null)
        {
            return BadRequest("Invalid login request");
        }
        ApiResponseVM<object> response = await _userService.ValidateCredentialsAsync(loginVM);
        if (response.StatusCode == 200)
        {
            TokenResponseVM tokenResponse = (TokenResponseVM)response.Data!;
            DateTime expirationTime = tokenResponse.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(7);
            SetCookie("DemoAccessToken", tokenResponse.AccessToken, expirationTime);
            SetCookie("DemoRefreshToken", tokenResponse.RefreshToken, expirationTime);
            return Ok(response);
        }
        else
        {
            return Unauthorized(response);
        }
    }

    #endregion

    #region Validate Refresh Token
    [HttpGet("validate-refresh-token")]
    public async Task<IActionResult> ValidateToken()
    {
        var refreshToken = Request.Cookies["DemoRefreshToken"]!;
        if (refreshToken == null)
        {
            return Unauthorized(new ApiResponseVM<object>(401, "No token present", null));
        }
        bool rememberMe = Convert.ToBoolean(_jwtTokenService.GetClaimValue(refreshToken, "RememberMe"));

        if (rememberMe)
        {
            RefreshTokenVM refreshTokenVM = new()
            {
                RefreshToken = refreshToken,
                RememberMe = rememberMe,
            };
            ApiResponseVM<object> response = await _userService.RefreshTokenAsync(refreshTokenVM);
            if (response.StatusCode == 200)
            {
                TokenResponseVM tokenResponse = (TokenResponseVM)response.Data!;
                DateTime expirationTime = tokenResponse.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(7);
                SetCookie("DemoAccessToken", tokenResponse.AccessToken, expirationTime);
                SetCookie("DemoRefreshToken", tokenResponse.RefreshToken, expirationTime);
                return Ok(response);
            }
            else if (response.StatusCode == 400)
            {
                return BadRequest(response);
            }
            else
            {
                return Unauthorized(response);
            }
        }

        return Unauthorized(new ApiResponseVM<object>(401, "Invalid or expired refresh token", null));
    }
    #endregion

    #region Forgot Password
    [HttpGet("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email, string username)
    {
        ApiResponseVM<object> response = await _userService.ForgotPasswordAsync(email, username);
        if (response.StatusCode == 200)
        {
            return Ok(response);
        }
        else if (response.StatusCode == 400)
        {
            return BadRequest(response);
        }
        else
        {
            return NotFound(response);
        }

    }
    #endregion

    #region Reset Password
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPassVM resetPassVM)
    {
        if (resetPassVM == null || resetPassVM.Token == string.Empty)
        {
            return BadRequest(new ApiResponseVM<object>(400, MessageHelper.INVALID_LINK, null));
        }
        ApiResponseVM<object> response = await _userService.ResetPasswordAsync(resetPassVM);
        if (response.StatusCode == 200)
        {
            return Ok(response);
        }
        else if (response.StatusCode == 410)
        {
            Response.StatusCode = response.StatusCode;
            return Content(response.Message);
        }
        else
        {
            return NotFound(response);
        }
    }
    #endregion

    #region Logout
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        RemoveCookie("DemoAccessToken");
        RemoveCookie("DemoRefreshToken");
        return Ok(new ApiResponseVM<object>(200, "Logged out successfully", null));
    }
    #endregion

    private void SetCookie(string name, string value, DateTime expiryTime)
    {
        Response.Cookies.Append(name, value, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = expiryTime
        });
    }

    private void RemoveCookie(string name)
    {
        Response.Cookies.Delete(name, new CookieOptions
        {
            Path = "/",
            Secure = true,
            SameSite = SameSiteMode.None
        });
    }
}
