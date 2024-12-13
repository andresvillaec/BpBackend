namespace Account.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Entities.Account>> GetAllAsync();
        Task<Entities.Account> GetByIdAsync(int id);
        Task<Entities.Account> GetByAccountNumberAsync(string accountNumber);
        Task<Entities.Account> AddAsync(Entities.Account account);
        Task UpdateAsync(Entities.Account account);
        Task DeleteAsync(int id);
        Task<decimal> GetBalance(string accountNumber);
        Task<bool> ExistsAccountNumber(string accountNumber);
    }
}
