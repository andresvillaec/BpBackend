using Account.Application.UseCases.BankAccount.Commands.Create;
using Account.Application.UseCases.Movement.Commands.Create;
using Carter;
using FluentValidation;
using MediatR;

namespace Account.API.Endpoints;

public class MovementEndpoints : CarterModule
{
    public MovementEndpoints()
        :base("/api/movements")
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
    }
}
