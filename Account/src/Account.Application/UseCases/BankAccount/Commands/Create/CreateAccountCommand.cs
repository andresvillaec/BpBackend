﻿using Account.Application.UseCases.BankAccount.Queries.Get;
using Account.Domain.Enums;
using MediatR;

namespace Account.Application.UseCases.BankAccount.Commands.Create
{
    public record CreateAccountCommand(
        string Number,
        AccountTypes AccountType,
        decimal OpeningDeposit,
        decimal Balance,
        int ClientId
    ) : IRequest<AccountResponse>;
}
