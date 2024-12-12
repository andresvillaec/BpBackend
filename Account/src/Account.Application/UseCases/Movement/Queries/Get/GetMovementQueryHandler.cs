using Account.Application.Data;
using Account.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.UseCases.Movement.Queries.Get;

internal sealed class GetMovementQueryHandler : IRequestHandler<GetMovementQuery, MovementResponse>
{
    private readonly IApplicantionDbContext _context;

    public GetMovementQueryHandler(IApplicantionDbContext context)
    {
        _context = context;
    }

    public async Task<MovementResponse> Handle(GetMovementQuery request, CancellationToken cancellationToken)
    {
        var movement = await _context
            .Movements
            .Where(c => c.Id == request.Id)
            .Select(p => new MovementResponse(
                p.Id,
                p.Timestamp,
                p.AccountType,
                p.Amount,
                p.Balance,
                p.AccountNumber
                )
            ).FirstOrDefaultAsync();

        if (movement == null)
        {
            throw new MovementNotFoundException(request.Id);
        }

        return movement;
    }
}
