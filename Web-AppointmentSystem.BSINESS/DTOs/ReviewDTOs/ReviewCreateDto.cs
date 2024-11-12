using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;

public record ReviewCreateDto(string Comment, int Rating, DateTime CreatedDate, bool IsDeleted = false);

public class ReviewCreateDtoValidator : AbstractValidator<ReviewCreateDto>
{
    public ReviewCreateDtoValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment cannot be empty.")
            .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.");

        RuleFor(x => x.CreatedDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedDate cannot be in the future.");
    }
}
