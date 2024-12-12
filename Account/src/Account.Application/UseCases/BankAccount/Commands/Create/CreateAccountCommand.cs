using MediatR;

namespace Account.Application.UseCases.BankAccount.Commands.Create
{
    public record CreateAccountCommand(
        string Number,
        decimal OpeningDeposit,
        decimal Balance,
        bool Status,
        int ClientId
    ) : IRequest;
}
