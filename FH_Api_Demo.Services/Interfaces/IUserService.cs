using FH_Api_Demo.Services.ViewModels;

namespace FH_Api_Demo.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponseVM<object>> RegisterUserAsync(SignUpVM signupVM);
}
