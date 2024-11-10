using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.AppointmentDTOs;

public record AppointmentCreateDto(string UserId, int ServiceId, DateTime Date, TimeSpan StartTime, string? Notes, bool IsDeleted = false);

public class AppointmentCreateDtoValidator : AbstractValidator<AppointmentCreateDto>
{
    public AppointmentCreateDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty")
            .NotNull().WithMessage("UserId cannot be null");

        RuleFor(x => x.ServiceId)
            .GreaterThan(0).WithMessage("ServiceId must be greater than 0");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date cannot be empty")
            .GreaterThanOrEqualTo(DateTime.Today.AddDays(1)).WithMessage("Date must be from tomorrow onwards");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("StartTime cannot be empty")
            .Must(time => time >= new TimeSpan(9, 0, 0) && time <= new TimeSpan(18, 0, 0))
            .WithMessage("StartTime must be between 09:00 and 18:00");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters");

        RuleFor(x => x.IsDeleted)
            .NotNull().WithMessage("IsDeleted must be provided");
    }
}
