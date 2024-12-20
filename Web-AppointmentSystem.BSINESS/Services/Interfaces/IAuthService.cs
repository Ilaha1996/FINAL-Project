﻿using Web_AppointmentSystem.BUSINESS.DTOs.TokenDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.BUSINESS.Services.Interfaces;

public interface IAuthService
{
    Task<string> Register(UserRegisterDto dto);
    Task<TokenResponseDto> Login(UserLoginDto dto);
    Task ConfirmEmail(string email, string token);
    Task ForgotPassword(ForgotPasswordDto dto);
    Task Logout();
    Task ResetPassword(ResetPasswordDto dto);
    Task ChangePassword(ChangePasswordDto dto);
}
