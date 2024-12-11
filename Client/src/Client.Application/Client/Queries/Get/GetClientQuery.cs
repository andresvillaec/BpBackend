using Client.Domain.Enums;
using MediatR;

namespace Client.Application.Client.Queries.Get;

public record GetClientQuery(int Id) : IRequest<ClientResponse>
{
}

public record ClientResponse(
    int Id,
    string Name,
    Genders? Gender,
    int? Age,
    string DocumentNumber,
    string Address,
    string Phone,
    string Password,
    bool Status
    );
