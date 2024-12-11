using Carter;
using Client.Application.Client.Create;
using Client.Application.Client.Delete;
using Client.Domain.Exceptions;
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

        app.MapDelete("clients/{id:int}", async (int id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteClientCommand(id));
                return Results.NoContent();
            }
            catch (ClientNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

    }
}
