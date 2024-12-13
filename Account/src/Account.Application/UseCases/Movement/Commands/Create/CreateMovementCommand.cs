using Account.Application.UseCases.Movement.Queries.Get;
using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Create;

public record CreateMovementCommand(
    decimal Amount,
    string AccountNumber
) : IRequest<MovementResponse>;
