using Account.Domain.Enums;
using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Create;

public record CreateMovementCommand(
    DateTime Timestamp,
    AccountTypes AccountType,
    decimal Amount,
    decimal Balance,
    string AccountNumber
) : IRequest;
