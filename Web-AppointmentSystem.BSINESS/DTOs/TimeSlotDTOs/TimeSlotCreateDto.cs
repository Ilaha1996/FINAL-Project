using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.TimeSlotDTOs;

public record TimeSlotCreateDto(DateTime Date, TimeSpan StartTime, TimeSpan EndTime, bool IsAvailable, bool IsDeleted);

public class TimeSlotCreateDtoValidator : AbstractValidator<TimeSlotCreateDto>
{
    public TimeSlotCreateDtoValidator()
    {
        RuleFor(x => x.Date)
                 .NotEmpty().WithMessage("Date must be provided.")
                 .GreaterThan(DateTime.Now).WithMessage("Date cannot be in the past.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("StartTime must be provided.")
            .LessThan(x => x.EndTime).WithMessage("StartTime must be earlier than EndTime.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("EndTime must be provided.")
            .GreaterThan(x => x.StartTime).WithMessage("EndTime must be later than StartTime.");

        RuleFor(x => x.IsAvailable)
            .NotNull().WithMessage("IsAvailable must be provided.");

        RuleFor(x => x.IsDeleted)
            .NotNull().WithMessage("IsDeleted must be provided.");
    }
}