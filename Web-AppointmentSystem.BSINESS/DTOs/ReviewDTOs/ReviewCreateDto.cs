using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;

public record ReviewCreateDto(string? Comment, DateTime CreatedDate,string? UserId, bool IsDeleted = false);

public class ReviewCreateDtoValidator : AbstractValidator<ReviewCreateDto>
{
    public ReviewCreateDtoValidator()
    {

        RuleFor(x => x.UserId)
          .NotEmpty().WithMessage("UserId cannot be empty");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment cannot be empty.")
            .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.");

        RuleFor(x => x.CreatedDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedDate cannot be in the future.");
    }
}
