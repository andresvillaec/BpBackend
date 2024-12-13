using Account.Domain.Enums;
using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Create;

public record CreateMovementCommand(
    decimal Amount,
    string AccountNumber
) : IRequest;
