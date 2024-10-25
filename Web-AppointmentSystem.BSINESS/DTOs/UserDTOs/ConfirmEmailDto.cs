using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs;

public record ConfirmEmailDto(string Email, string Token);

public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
{
    public ConfirmEmailDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(50).WithMessage("Email must be 50 characters or fewer.")
            .MinimumLength(5).WithMessage("Email must be at least 5 characters.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.")
            .Length(20, 200).WithMessage("Token must be between 20 and 200 characters.");
    }
}
