using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;

public record ReviewUpdateDto(string Comment, DateTime CreatedDate, string UserId, bool IsDeleted = false);

public class ReviewUpdateDtoValidator : AbstractValidator<ReviewUpdateDto>
{
    public ReviewUpdateDtoValidator()
    {

        RuleFor(x => x.UserId)
         .NotEmpty().WithMessage("UserId cannot be empty")
         .NotNull().WithMessage("UserId cannot be null");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment cannot be empty.")
            .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.");

        RuleFor(x => x.CreatedDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedDate cannot be in the future.");
    }
}


