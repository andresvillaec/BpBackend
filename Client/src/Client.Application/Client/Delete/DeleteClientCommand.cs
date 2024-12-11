using MediatR;

namespace Client.Application.Client.Delete;

public record DeleteClientCommand(int Id) : IRequest;