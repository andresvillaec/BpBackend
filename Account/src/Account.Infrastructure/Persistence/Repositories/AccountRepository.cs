using Account.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Account.Infrastructure.Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AccountContext _context;

    public AccountRepository(AccountContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Domain.Entities.Account> GetByIdAsync(int id)
    {
        return await _context.Accounts.FindAsync(id);
    }

    public async Task<Domain.Entities.Account> AddAsync(Domain.Entities.Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        return account;
    }

    public async Task UpdateAsync(Domain.Entities.Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account != null)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<decimal> GetBalance(string accountNumber)
    {
        return await _context.Accounts
            .Where(MatchAccountNumber(accountNumber))
            .Select(a => a.Balance)
            .FirstOrDefaultAsync();
    }

    public async Task<Domain.Entities.Account> GetByAccountNumberAsync(string accountNumber)
    {
        return await _context.Accounts.FirstOrDefaultAsync(MatchAccountNumber(accountNumber));
    }

    public async Task<bool> ExistsAccountNumber(string accountNumber)
    {
        return await _context.Accounts.AnyAsync(MatchAccountNumber(accountNumber));
    }

    private Expression<Func<Domain.Entities.Account, bool>> MatchAccountNumber(string accountNumber)
    {
        return a => a.Number == accountNumber;
    }
}
