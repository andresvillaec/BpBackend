using Carter;
using Client.Application.Client.Commands.Create;
using Client.Application.Client.Commands.Delete;
using Client.Application.Client.Commands.Update;
using Client.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        app.MapPut("clients/{id:int}", async (int id, [FromBody] UpdateClientRequest payload, ISender sender) =>
        {
            try
            {
                var command = new UpdateClientCommand(
                    id,
                    payload.Name,
                    payload.Gender,
                    payload.Age,
                    payload.DocumentNumber,
                    payload.Address,
                    payload.Phone,
                    payload.Password,
                    payload.Status
                    );

                await sender.Send(command);
                return Results.NoContent();
            }
            catch (ClientNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

    }
}
