using FH_Api_Demo.Services.Helpers;
using FH_Api_Demo.Services.Interfaces;
using FH_Api_Demo.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FH_Api_Demo.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService) : ControllerBase
{
    #region Configuration Settings
    private readonly IUserService _userService = userService;
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

}
