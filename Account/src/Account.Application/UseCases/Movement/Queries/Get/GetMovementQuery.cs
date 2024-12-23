﻿using MediatR;

namespace Account.Application.UseCases.Movement.Queries.Get;

public record GetMovementQuery(int Id) : IRequest<MovementResponse>
{
}

public record MovementResponse(
    int Id,
    string AccountNumber,
    decimal Amount,
    decimal Balance,
    DateTime Timestamp);
