using Account.Application.UseCases.Movement.Commands.Create;
using Account.Application.UseCases.Movement.Commands.Delete;
using Account.Application.UseCases.Movement.Commands.Update;
using Account.Application.UseCases.Movement.Queries.Get;
using Account.Domain.Exceptions;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Endpoints;

public class MovementEndpoints : CarterModule
{
    public MovementEndpoints()
        : base("/api/movements")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", async (CreateMovementCommand command, IValidator<CreateMovementCommand> validator, ISender sender) =>
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapGet("{id:int}", async (int id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetMovementQuery(id)));
            }
            catch (MovementNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapPut("{id:int}", async (int id, [FromBody] UpdateMovementRequest payload, IValidator<UpdateMovementCommand> validator, ISender sender) =>
        {
            try
            {
                var command = new UpdateMovementCommand(
                    id,
                    payload.Timestamp,
                    payload.AccountType,
                    payload.Amount,
                    payload.Balance,
                    payload.AccountNumber
                    );

                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                await sender.Send(command);
                return Results.NoContent();
            }
            catch (MovementNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapDelete("{id:int}", async (int id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteMovementCommand(id));
                return Results.NoContent();
            }
            catch (MovementNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });
    }
}
