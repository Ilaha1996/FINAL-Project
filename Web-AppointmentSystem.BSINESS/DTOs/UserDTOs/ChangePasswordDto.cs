using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.UserDTOs
{
    public record ChangePasswordDto(string Token,string CurrentPassword, string NewPassword, string ConfirmNewPassword);

    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.")
                .MinimumLength(6).WithMessage("Current password must be at least 8 characters.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 8 characters.")
                .NotEqual(x => x.CurrentPassword).WithMessage("New password cannot be the same as the current password.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Confirm new password is required.")
                .Equal(x => x.NewPassword).WithMessage("Confirm new password must match the new password.");
        }
    }
}
