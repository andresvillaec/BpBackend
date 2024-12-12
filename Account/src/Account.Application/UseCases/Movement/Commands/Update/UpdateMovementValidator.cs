using FluentValidation;

namespace Account.Application.UseCases.Movement.Commands.Update;

public sealed class UpdateMovementValidator : AbstractValidator<UpdateMovementCommand>
{
    public UpdateMovementValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThan(0);

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
            .NotEmpty();
    }
}
