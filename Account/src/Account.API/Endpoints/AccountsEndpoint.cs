﻿using Account.Application.UseCases.BankAccount.Commands.Create;
using Account.Application.UseCases.BankAccount.Commands.Delete;
using Account.Application.UseCases.BankAccount.Commands.Update;
using Account.Application.UseCases.BankAccount.Queries.Get;
using Account.Domain.Exceptions;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        app.MapGet("accounts/{id:int}", async (int id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetAccountQuery(id)));
            }
            catch (AccountNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapPut("accounts/{id:int}", async (int id, [FromBody] UpdateAccountRequest payload, IValidator<UpdateAccountCommand> validator, ISender sender) =>
        {
            try
            {
                var command = new UpdateAccountCommand(
                    id,
                    payload.Number,
                    payload.OpeningDeposit,
                    payload.Balance,
                    payload.Status,
                    payload.ClientId
                    );

                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                await sender.Send(command);
                return Results.NoContent();
            }
            catch (AccountNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapDelete("accounts/{id:int}", async (int id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteAccountCommand(id));
                return Results.NoContent();
            }
            catch (AccountNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });
    }
}