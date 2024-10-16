using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs;

public record UserRegisterDto(string Fullname, string Username, string Email, string Password, string ConfirmPassword);

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.Fullname).NotEmpty().MaximumLength(50).MinimumLength(2);
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50).MinimumLength(2);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50)
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");   

        RuleFor(x => x).Custom((x, context) =>
        {
            if (x.Password != x.ConfirmPassword)
            {
                context.AddFailure("ConfirmPassword", "ConfirmPassword and Password must be the same.");
            }
        });
    }
}

