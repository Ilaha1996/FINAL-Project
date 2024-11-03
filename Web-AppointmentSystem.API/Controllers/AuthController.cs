using Microsoft.AspNetCore.Mvc;
using Web_AppointmentSystem.API.ApiResponse;
using Web_AppointmentSystem.BUSINESS.DTOs.TokenDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;
using Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;

namespace Web_AppointmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            try
            {
                await _authService.Register(dto);
                return Ok(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = "Registration successful. Please check your email to confirm.",
                    ErrorMessage = null
                });
            }
            catch (EntityAlreadyExistException ex)
            {
                return Conflict(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (UserRegistrationException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = "An unexpected error occurred.",
                    Data = null
                });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            TokenResponseDto data = null;
            try
            {
                data = await _authService.Login(dto);
                return Ok(new ApiResponse<TokenResponseDto>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = data,
                    ErrorMessage = null
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<TokenResponseDto>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TokenResponseDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authService.Logout();
                return Ok(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = "Logout successful.",
                    ErrorMessage = null
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = "An unexpected error occurred during logout.",
                    Data = null
                });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            try
            {
                await _authService.ForgotPassword(dto);
                return Ok(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = "Password reset email sent successfully.",
                    ErrorMessage = null
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = "An unexpected error occurred while attempting password reset.",
                    Data = null
                });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            try
            {
                await _authService.ResetPassword(dto);
                return Ok(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = "Password reset successful.",
                    ErrorMessage = null
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = "An unexpected error occurred while resetting password.",
                    Data = null
                });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            try
            {
                await _authService.ChangePassword(dto);
                return Ok(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = "Password change successful.",
                    ErrorMessage = null
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = "An unexpected error occurred while changing password.",
                    Data = null
                });
            }
        }
    }
}

//[HttpGet]
//public async Task<IActionResult> CreateRole()
//{
//    IdentityRole role2 = new IdentityRole("Admin");
//    IdentityRole role3 = new IdentityRole("Member");

//    await _roleManager.CreateAsync(role2);
//    await _roleManager.CreateAsync(role3);

//    return Ok();
//}

//[HttpGet]
//public async Task<IActionResult> CreateAdmin()
//{
//    AppUser appUser = new AppUser()
//    {
//        UserName = "Ilaha",
//        Email = "ilahahasanova@yahoo.com",
//        Fullname = "Ilaha Hasanova"
//    };

//    IdentityResult result = await _userManager.CreateAsync(appUser, "Salam123!");

//    if (result.Succeeded)
//    {
//        return Ok("Admin user created successfully.");
//    }
//    else
//    {
//        return BadRequest(result.Errors);
//    }
//}

//[HttpGet]
//public async Task<IActionResult> AddRole()
//{
//    AppUser appUser = await _userManager.FindByNameAsync("Aghalar");

//    await _userManager.AddToRoleAsync(appUser, "Member");
//    return Ok();

//}