using Microsoft.EntityFrameworkCore;
using Account.Domain.Interfaces;
using Account.Domain.Entities;

namespace Account.Infrastructure.Persistence.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AccountContext _context;

    public ReportRepository(AccountContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movement>> GenerateMovementsReport(DateTime startDate, DateTime endDate, string accountNumber)
    {
        var start = startDate.Date;
        var end = endDate.Date.AddDays(1).AddSeconds(-1);
        return await _context.Movements
            .Where(m => m.Account.Number == accountNumber && m.Timestamp >= start && m.Timestamp <= end)
            .ToListAsync();
    }
}
