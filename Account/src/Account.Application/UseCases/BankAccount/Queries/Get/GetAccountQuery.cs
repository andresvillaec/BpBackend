using Account.Domain.Enums;
using MediatR;

namespace Account.Application.UseCases.BankAccount.Queries.Get;

public record GetAccountQuery(int Id) : IRequest<AccountResponse>
{
}

public record AccountResponse(
    int Id,
    string Number,
    AccountTypes AccountTypes,
    decimal OpeningDeposit,
    decimal Balance,
    bool Status,
    int ClientId
);
