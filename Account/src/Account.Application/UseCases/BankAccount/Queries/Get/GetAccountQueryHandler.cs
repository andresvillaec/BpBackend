using Account.Application.Data;
using Account.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.UseCases.BankAccount.Queries.Get;

internal sealed class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountResponse>
{
    private readonly IApplicantionDbContext _context;

    public GetAccountQueryHandler(IApplicantionDbContext context)
    {
        _context = context;
    }

    public async Task<AccountResponse> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var client = await _context
            .Accounts
            .Where(c => c.Id == request.Id)
            .Select(p => new AccountResponse(
                p.Id,
                p.Number,
                p.AccountType,
                p.OpeningDeposit,
                p.Balance,
                p.Status,
                p.ClientId
                    )
            ).FirstOrDefaultAsync();

        if (client == null)
        {
            throw new AccountNotFoundException(request.Id);
        }

        return client;
    }
}
