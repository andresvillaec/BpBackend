using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Update;

public record UpdateMovementCommand(
    int Id,
    decimal Amount,
    string AccountNumber
) : IRequest;

public record UpdateMovementRequest(
    decimal Amount,
    string AccountNumber
);