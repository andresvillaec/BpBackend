using FluentValidation;

namespace Account.Application.UseCases.BankAccount.Commands.Update;

public sealed class UpdateAccountValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .MaximumLength(25);

        RuleFor(x => x.OpeningDeposit)
            .GreaterThan(0);

        RuleFor(x => x.Balance)
            .GreaterThan(0);

        RuleFor(x => x.ClientId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
