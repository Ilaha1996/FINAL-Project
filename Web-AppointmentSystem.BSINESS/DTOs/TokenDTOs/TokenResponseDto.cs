using FluentValidation;

namespace Web_AppointmentSystem.BUSINESS.DTOs.TokenDTOs;

public record TokenResponseDto(string AccessToken, DateTime ExpireDate);

public class TokenResponseDtoValidator : AbstractValidator<TokenResponseDto>
{
    public TokenResponseDtoValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("AccessToken is required.");

        RuleFor(x => x.ExpireDate)
            .GreaterThan(DateTime.Now).WithMessage("ExpireDate must be in the future.");
    }
}
