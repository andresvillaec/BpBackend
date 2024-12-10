using Account.Domain.Entities;

namespace Account.Domain.Interfaces;

public interface IReportRepository
{
    Task<IEnumerable<Movement>> GenerateMovementsReport(DateTime startDate, DateTime endDate, string accountNumber);
}
