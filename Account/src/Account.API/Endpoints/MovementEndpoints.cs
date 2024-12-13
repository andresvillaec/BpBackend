using Account.Application.UseCases.Movement.Commands.Create;
using Account.Application.UseCases.Movement.Commands.Delete;
using Account.Application.UseCases.Movement.Commands.Update;
using Account.Application.UseCases.Movement.Queries.Get;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Endpoints;

public class MovementEndpoints : CarterModule
{
    private const string BasePath = "/api/movements";

    public MovementEndpoints()
        : base(BasePath)
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

            var response = await sender.Send(command);

            return Results.Created($"{BasePath}/{response.Id}", response);
        });

        app.MapGet("{id:int}", async (int id, ISender sender) =>
        {
            var response = await sender.Send(new GetMovementQuery(id));
            return Results.Ok(response);
        });

        app.MapPut("{id:int}", async (int id, [FromBody] UpdateMovementRequest payload, 
            IValidator<UpdateMovementCommand> validator, 
            ISender sender) =>
        {
            var command = new UpdateMovementCommand(id, payload.Amount, payload.AccountNumber);
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            await sender.Send(command);
            return Results.NoContent();
        });

        app.MapDelete("{id:int}", async (int id, ISender sender) =>
        {
            await sender.Send(new DeleteMovementCommand(id));
            return Results.NoContent();
        });
    }
}
