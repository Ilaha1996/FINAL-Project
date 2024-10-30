using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Web_AppointmentSystem.BUSINESS.DTOs.ServiceDTOs;

public record ServiceCreateDto(string Name, string Description, bool IsDeleted, decimal Price, int Duration, IFormFile Image);

public class ServiceCreateDtoValidator : AbstractValidator<ServiceCreateDto>
{
    public ServiceCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Can not be empty!")
            .NotNull().WithMessage("Can not be null")
            .MinimumLength(1).WithMessage("Minimum length must be 1")
            .MaximumLength(50).WithMessage("Maximum length must be 200");

        RuleFor(x => x.Description)
            .NotNull().When(x => !x.IsDeleted).WithMessage("If movie is active description can not be null!")
            .MaximumLength(550).WithMessage("Maximum length must be 800");

        RuleFor(x => x.IsDeleted).NotNull();

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .ScalePrecision(2, 18).WithMessage("Price must have at most 18 digits and 2 decimal places");

        RuleFor(x => x.Duration)
            .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes")
            .LessThanOrEqualTo(120).WithMessage("Duration must not exceed 120 minutes (2 hours)");

        RuleFor(x => x.Image)
        .Must(x => x.ContentType == "image/png" || x.ContentType == "image/jpeg")
        .WithMessage("Image content type must be png or jpeg")
        .Must(x => x.Length < 2 * 1024 * 1024)
        .WithMessage("Image length must be less than 2 MB");
    }
}



