using Carter;
using Client.Application.UseCases.Commands.Create;
using Client.Application.UseCases.Commands.Delete;
using Client.Application.UseCases.Commands.Update;
using Client.Application.UseCases.Queries.Get;
using Client.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Endpoints;

public class Clients : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("clients/{id:int}", async (int id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetClientQuery(id)));
            }
            catch (ClientNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapPost("clients", async (CreateClientCommand command, IValidator<CreateClientCommand> validator, ISender sender) =>
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

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

        app.MapPut("clients/{id:int}", async (int id, [FromBody] UpdateClientRequest payload, IValidator<UpdateClientCommand> validator, ISender sender) =>
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

                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

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
