using Carter;
using Client.Application.Client.Create;
using MediatR;

namespace Client.API.Endpoints;

public class Clients : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("clients", async (CreateClientCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Ok();
        });
    }
}
