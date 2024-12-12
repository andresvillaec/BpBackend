using Account.Domain.Enums;
using MediatR;

namespace Account.Application.UseCases.Movement.Queries.Get;

public record GetMovementQuery(int Id) : IRequest<MovementResponse>
{
}

public record MovementResponse(
    int Id,
    DateTime Timestamp,
    AccountTypes AccountType,
    decimal Amount,
    decimal Balance,
    string AccountNumber
);
