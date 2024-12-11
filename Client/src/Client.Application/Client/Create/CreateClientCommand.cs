using Client.Domain.Enums;
using MediatR;

namespace Client.Application.Client.Create;

public record CreateClientCommand(
    string Name,
    Genders? Gender,
    int? Age,
    string DocumentNumber,
    string Address,
    string Phone,
    string Password,
    bool Status) : IRequest;