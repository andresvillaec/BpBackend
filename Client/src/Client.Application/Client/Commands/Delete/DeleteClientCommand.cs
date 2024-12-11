using MediatR;

namespace Client.Application.Client.Commands.Delete;

public record DeleteClientCommand(int Id) : IRequest;