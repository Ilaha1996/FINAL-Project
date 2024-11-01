using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs
{
    public record ResetPasswordDto(string Email, string Token, string Password, string ConfirmPassword);

    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(50).WithMessage("Email must be 50 characters or fewer.")
                .MinimumLength(5).WithMessage("Email must be at least 5 characters.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required.")
                .Length(20, 200).WithMessage("Token must be between 20 and 200 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 8 characters.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.Password).WithMessage("Confirm password must match the password.");
        }
    }
}
