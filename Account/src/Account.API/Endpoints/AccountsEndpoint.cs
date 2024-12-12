using Account.Application.UseCases.BankAccount.Commands.Create;
using Carter;
using FluentValidation;
using MediatR;

namespace Account.API.Endpoints;

public class AccountsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("accounts", async (CreateAccountCommand command, IValidator<CreateAccountCommand> validator, ISender sender) =>
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
