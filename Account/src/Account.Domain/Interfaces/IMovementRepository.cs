using Account.Domain.Entities;

namespace Account.Domain.Interfaces;

public interface IMovementRepository
{
    Task<IEnumerable<Movement>> GetAllAsync();
    Task<Movement> GetByIdAsync(int id);
    Task<Movement> AddAsync(Movement movement);
    Task UpdateAsync(Movement movement);
    Task DeleteAsync(int id);
    Task<decimal> SumMovements(string accountNumber, int movementId);
    Task<decimal> GetOpeningDeposit(string accountNumber);
}
