using Web_AppointmentSystem.BUSINESS.DTOs.TokenDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.BUSINESS.Services.Interfaces;

public interface IAuthService
{
    Task Register(UserRegisterDto dto);
    Task<TokenResponseDto> Login(UserLoginDto dto);
    List<AppUser> GetAllUsers();
}
