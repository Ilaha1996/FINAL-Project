using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.AppointmentDTOs;

public record AppointmentCreateDto(string UserId, int ServiceId, int TimeSlotId,
                                   bool IsConfirmed, string? Notes, bool IsDeleted);

public class AppointmentCreateDtoValidator : AbstractValidator<AppointmentCreateDto>
{
    public AppointmentCreateDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty")
            .NotNull().WithMessage("UserId cannot be null");           

        RuleFor(x => x.ServiceId)
            .GreaterThan(0).WithMessage("ServiceId must be greater than 0");

        RuleFor(x => x.TimeSlotId)
            .GreaterThan(0).WithMessage("TimeSlotId must be greater than 0");

        RuleFor(x => x.IsConfirmed)
            .NotNull().WithMessage("IsConfirmed must be provided");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters");

        RuleFor(x => x.IsDeleted)
            .NotNull().WithMessage("IsDeleted must be provided");
    }
}

