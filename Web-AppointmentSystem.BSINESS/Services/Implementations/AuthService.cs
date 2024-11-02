using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_AppointmentSystem.BUSINESS.DTOs.TokenDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs;
using Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;
using Web_AppointmentSystem.BUSINESS.Services.ExternalService.Interface;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.BUSINESS.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly string _secretKey;
    private readonly string _audience;
    private readonly string _issuer;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _emailService = emailService;

        // Caching configuration values to avoid repeated retrieval
        _secretKey = _configuration["JWT:secretKey"];
        _audience = _configuration["JWT:audience"];
        _issuer = _configuration["JWT:issuer"];
    }

    public async Task<TokenResponseDto> Login(UserLoginDto dto)
    {
        var appUser = await _userManager.FindByNameAsync(dto.Username);
        if (appUser == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials: User not found.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, dto.RememberMe);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid credentials: Incorrect password.");
        }

        var roles = await _userManager.GetRolesAsync(appUser);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id),
            new Claim(ClaimTypes.Name, appUser.UserName),
            new Claim("Fullname", appUser.Fullname)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = GenerateJwtToken(claims);
        return new TokenResponseDto(token, DateTime.UtcNow.AddMinutes(40));
    }

    public async Task<string> Register(UserRegisterDto dto)
    {
        AppUser appUser = null;

        appUser = await _userManager.FindByNameAsync(dto.Username);
        if (appUser != null)
        {
            throw new EntityAlreadyExistException("User already exists with this username.");

        }

        appUser = await _userManager.FindByEmailAsync(dto.Email);
        if (appUser != null)
        {
            throw new EntityAlreadyExistException("Email already exists");
        }

        appUser = new AppUser
        {
            Email = dto.Email,
            Fullname = dto.Fullname,
            UserName = dto.Username
        };

        var result = await _userManager.CreateAsync(appUser, dto.Password);
        if (!result.Succeeded)
        {
            throw new UserRegistrationException("User registration failed.");
        }

        await _userManager.AddToRoleAsync(appUser, "Member");

        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
        var confirmationLink = $"{_configuration["AppUrl"]}/api/account/confirmEmail?token={Uri.EscapeDataString(emailConfirmationToken)}&email={Uri.EscapeDataString(dto.Email)}";
        await _emailService.SendMailAsync(dto.Email, "Confirm your email",$"Please confirm your email by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>");

        return emailConfirmationToken;
    }

    public List<AppUser> GetAllUsers()
    {
        return _userManager.Users.ToList();
    }

    public async Task ConfirmEmail(string email, string token)
    {
        var appUser = await _userManager.FindByEmailAsync(email);
        if (appUser == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials: User not found.");
        }

        var result = await _userManager.ConfirmEmailAsync(appUser, token);
        if (!result.Succeeded)
        {
            throw new UserRegistrationException("User email confirmation failed.");
        }
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();      
    }

    private string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(40),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    public async Task ForgotPassword(string email)
    {
        AppUser appUser = null;
        appUser = await _userManager.FindByEmailAsync(email);

        if (appUser == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials: User not found.");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
        var resetLink = $"{_configuration["AppUrl"]}/auth/ResetPassword?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";
        await _emailService.SendMailAsync(email, "Password Reset", $"Click here to reset your password: <a href='{resetLink}'>Reset Password</a>");

    }
    public async Task<string> ResetPassword(ResetPasswordDto dto)
    {
        var appUser = await _userManager.FindByEmailAsync(dto.Email);
        if (appUser == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials: User not found.");
        }

        var result = await _userManager.ResetPasswordAsync(appUser, dto.Token, dto.Password);
        
        if (!result.Succeeded)
        {
           
            throw new InvalidOperationException("Password reset failed");
        }

        return "Password reset successful.";
    }
    public async Task ChangePassword(ChangePasswordDto dto)
    {
        AppUser appUser = null;
        if (appUser == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        bool isSamePassword = await _userManager.CheckPasswordAsync(appUser, dto.NewPassword);
        if (isSamePassword)
        {
            throw new InvalidOperationException("New password must be different from the current password.");
        }

        var result = await _userManager.ChangePasswordAsync(appUser, dto.CurrentPassword, dto.NewPassword);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Password change failed");
        }
    }

}
