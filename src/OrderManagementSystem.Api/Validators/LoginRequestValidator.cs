using FluentValidation;
using OrderManagementSystem.Api.DTO;

namespace OrderManagementSystem.Api.Validators;

/// <summary>
/// Валідація вхідного DTO реєстрації.
/// Правила зібрані в окремому класі, щоб контролер залишався простим.
/// </summary>
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email є обов'язковим.")
            .EmailAddress().WithMessage("Email має некоректний формат.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль є обов'язковим.")
            .MinimumLength(8).WithMessage("Пароль має містити щонайменше 8 символів.");
    }
}
