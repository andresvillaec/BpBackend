using MediatR;

namespace Account.Application.UseCases.Movement.Commands.Delete;

public record DeleteMovementCommand(int Id) : IRequest;
