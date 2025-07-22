using ApiAuthentication.Entity.ViewModels;
using FH_Api_Demo.Services.ViewModels;

namespace FH_Api_Demo.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponseVM<object>> RegisterUserAsync(SignUpVM signupVM);
    Task<ApiResponseVM<object>> ValidateCredentialsAsync(LoginVM loginVM);
    Task<ApiResponseVM<object>> ForgotPasswordAsync(string email, string username);
    Task<ApiResponseVM<object>> ResetPasswordAsync(ResetPassVM resetPassVM);
    Task<ApiResponseVM<object>> RefreshTokenAsync(RefreshTokenVM refreshTokenVM);
    Task<ApiResponseVM<object>> GetUserByUserNameAsync(string userName);
}
