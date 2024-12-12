using Account.Domain.Enums;
using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Update;

public record UpdateMovementCommand(
    int Id,
    DateTime Timestamp,
    AccountTypes AccountType,
    decimal Amount,
    decimal Balance,
    string AccountNumber
) : IRequest;

public record UpdateMovementRequest(
    DateTime Timestamp,
    AccountTypes AccountType,
    decimal Amount,
    decimal Balance,
    string AccountNumber
);