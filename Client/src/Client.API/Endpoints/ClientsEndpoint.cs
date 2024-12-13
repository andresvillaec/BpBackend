using Carter;
using Client.Application.UseCases.Commands.Create;
using Client.Application.UseCases.Commands.Delete;
using Client.Application.UseCases.Commands.Update;
using Client.Application.UseCases.Queries.Get;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Endpoints;

public class ClientsEndpoint : CarterModule
{
    public ClientsEndpoint()
        : base("/api/clients")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("{id:int}", async (int id, ISender sender) =>
        {
            return Results.Ok(await sender.Send(new GetClientQuery(id)));
        });

        app.MapPost("", async (CreateClientCommand command, IValidator<CreateClientCommand> validator, ISender sender) =>
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            await sender.Send(command);

            return Results.Created();
        });

        app.MapDelete("{id:int}", async (int id, ISender sender) =>
        {
            await sender.Send(new DeleteClientCommand(id));
            return Results.NoContent();
        });

        app.MapPut("{id:int}", async (int id, [FromBody] UpdateClientRequest payload, IValidator<UpdateClientCommand> validator, ISender sender) =>
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

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            await sender.Send(command);
            return Results.NoContent();
        });
    }
}
