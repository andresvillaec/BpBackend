using MediatR;

namespace Account.Application.UseCases.BankAccount.Commands.Update;

public record UpdateAccountCommand(
    int Id,
    string Number,
    decimal OpeningDeposit,
    decimal Balance,
    bool Status,
    int ClientId
) : IRequest;


public record UpdateAccountRequest(
    string Number,
    decimal OpeningDeposit,
    decimal Balance,
    bool Status,
    int ClientId
);