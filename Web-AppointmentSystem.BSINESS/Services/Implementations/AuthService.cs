using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_AppointmentSystem.BUSINESS.DTOs.TokenDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.BUSINESS.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
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

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id),
            new Claim(ClaimTypes.Name, appUser.UserName),
            new Claim("Fullname", appUser.Fullname)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var secretKey = _configuration.GetSection("JWT:secretKey").Value;
        var audience = _configuration.GetSection("JWT:audience").Value;
        var issuer = _configuration.GetSection("JWT:issuer").Value;
        var expiredDate = DateTime.UtcNow.AddMinutes(40);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            claims: claims,
            audience: audience,
            issuer: issuer,
            expires: expiredDate,
            notBefore: DateTime.UtcNow
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return new TokenResponseDto(token, expiredDate);
    }
    public async Task Register(UserRegisterDto dto)
    {
        AppUser appUser = new AppUser()
        {
            Email = dto.Email,
            Fullname = dto.Fullname,
            UserName = dto.Username
        };

        var result = await _userManager.CreateAsync(appUser, dto.Password);

        if (!result.Succeeded)
        {
            throw new UserRegistrationException("User registration failed");
        }
    }
    public List<AppUser> GetAllUsers()
    {
        var users = _userManager.Users.ToList();
        return users;
    }
}
