using CognitoAuth.Application.DTOs.Auth;
using FluentValidation;

namespace CognitoAuth.Application.Validators.Auth;

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is not valid");

        RuleFor(r => r.ConfirmationCode)
            .NotEmpty()
            .WithMessage("Confirmation code is required");
    }
}