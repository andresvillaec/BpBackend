using MediatR;

namespace Account.Application.Handlers;

public record ClientExistsQuery(int ClientId) : IRequest<bool>;
