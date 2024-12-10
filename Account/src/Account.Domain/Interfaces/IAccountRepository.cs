namespace Account.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Entities.Account>> GetAllAsync();
        Task<Entities.Account> GetByIdAsync(int id);
        Task<Entities.Account> AddAsync(Entities.Account account);
        Task UpdateAsync(Entities.Account account);
        Task DeleteAsync(int id);
    }
}
