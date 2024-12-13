using Client.Application.UseCases.Queries.Get;
using Client.Domain.Enums;
using MediatR;

namespace Client.Application.UseCases.Commands.Create;

public record CreateClientCommand(
    string Name,
    Genders? Gender,
    int? Age,
    string DocumentNumber,
    string Address,
    string Phone,
    string Password
    ) : IRequest<ClientResponse>;