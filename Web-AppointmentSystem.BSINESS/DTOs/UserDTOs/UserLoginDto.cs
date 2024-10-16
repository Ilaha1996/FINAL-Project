using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs;
public record UserLoginDto(string Username, string Password, bool RememberMe);

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50).MinimumLength(2);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
    }
}

