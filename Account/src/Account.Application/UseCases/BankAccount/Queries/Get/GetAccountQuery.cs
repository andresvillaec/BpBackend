using MediatR;

namespace Account.Application.UseCases.BankAccount.Queries.Get;

public record GetAccountQuery(int Id) : IRequest<AccountResponse>
{
}

public record AccountResponse(
    int Id,
    string Number,
    decimal OpeningDeposit,
    decimal Balance,
    bool Status,
    int ClientId
);
