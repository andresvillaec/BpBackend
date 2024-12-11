using Client.Domain.Enums;
using MediatR;

namespace Client.Application.Client.Commands.Update
{
    public record UpdateClientCommand(
        int Id,
        string Name,
        Genders? Gender,
        int? Age,
        string DocumentNumber,
        string Address,
        string Phone,
        string Password,
        bool Status) 
        : IRequest;


    public record UpdateClientRequest(
        string Name,
        Genders? Gender,
        int? Age,
        string DocumentNumber,
        string Address,
        string Phone,
        string Password,
        bool Status);
}
