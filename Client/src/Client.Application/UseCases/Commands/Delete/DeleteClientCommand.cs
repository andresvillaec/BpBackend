using MediatR;

namespace Client.Application.UseCases.Commands.Delete;

public record DeleteClientCommand(int Id) : IRequest;