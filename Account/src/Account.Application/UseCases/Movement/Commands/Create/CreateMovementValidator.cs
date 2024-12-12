using FluentValidation;

namespace Account.Application.UseCases.Movement.Commands.Create;

public sealed class CreateMovementValidator : AbstractValidator<CreateMovementCommand>
{
    public CreateMovementValidator()
    {
        RuleFor(x => x.Timestamp)
            .NotNull();

        RuleFor(x => x.AccountType)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.Amount)
            .NotEqual(0);

        RuleFor(x => x.Balance)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.AccountNumber)
            .NotEmpty()
            .GreaterThan(0);
    }
}
