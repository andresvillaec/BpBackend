using FluentValidation;

namespace Account.Application.UseCases.BankAccount.Commands.Create;

public sealed class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .MaximumLength(25);

        RuleFor(x => x.OpeningDeposit)
            .GreaterThan(0);

        RuleFor(x => x.Balance)
            .GreaterThan(0);

        RuleFor(x => x.AccountType)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.ClientId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
