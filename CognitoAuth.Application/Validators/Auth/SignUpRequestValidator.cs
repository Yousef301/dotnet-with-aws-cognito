using CognitoAuth.Application.DTOs.Auth;
using FluentValidation;

namespace CognitoAuth.Application.Validators.Auth;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is not valid");

        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage("Password is required");

        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
}