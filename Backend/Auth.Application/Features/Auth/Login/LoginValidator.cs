using FluentValidation;

namespace Auth.Application.Features.Auth.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(c => c.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}