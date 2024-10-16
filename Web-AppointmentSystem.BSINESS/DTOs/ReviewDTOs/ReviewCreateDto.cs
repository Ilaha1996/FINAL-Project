using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;

public record ReviewCreateDto(string? Comment, int Rating, bool IsDeleted, int AppointmentId);

public class ReviewCreateDtoValidator : AbstractValidator<ReviewCreateDto>
{
    public ReviewCreateDtoValidator()
    {
        RuleFor(x => x.AppointmentId)
            .GreaterThan(0).WithMessage("AppointmentId must be greater than 0.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.Comment)
            .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Comment)); 

        RuleFor(x => x.IsDeleted)
            .NotNull().WithMessage("IsDeleted must be provided.");
    }
}
