using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_AppointmentSystem.API.ApiResponse;
using Web_AppointmentSystem.BUSINESS.DTOs.TokenDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

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
                    Data = null,
                    ErrorMessage = null
                });
            }
            catch (NullReferenceException ex)
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
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
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
            catch (NullReferenceException ex)
            {
                return BadRequest(new ApiResponse<TokenResponseDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
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
    }
}
