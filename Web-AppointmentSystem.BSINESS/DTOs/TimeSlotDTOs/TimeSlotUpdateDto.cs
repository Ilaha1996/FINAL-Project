using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.TimeSlotDTOs;

public record TimeSlotUpdateDto(DateTime Date, TimeSpan StartTime, bool IsAvailable, bool IsDeleted);

public class TimeSlotUpdateDtoValidator : AbstractValidator<TimeSlotCreateDto>
{
    public TimeSlotUpdateDtoValidator()
    {
        RuleFor(x => x.Date)
                  .NotEmpty().WithMessage("Date must be provided.")
                  .GreaterThan(DateTime.Now).WithMessage("Date cannot be in the past.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("StartTime must be provided.");

        RuleFor(x => x.IsAvailable)
            .NotNull().WithMessage("IsAvailable must be provided.");

        RuleFor(x => x.IsDeleted)
            .NotNull().WithMessage("IsDeleted must be provided.");
    }
}