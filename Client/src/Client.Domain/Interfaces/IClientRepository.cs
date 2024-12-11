namespace Client.Domain.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<Entities.Client>> GetAllAsync();
    Task<Entities.Client> GetByIdAsync(int id);
    Task AddAsync(Entities.Client client);
    Task UpdateAsync(Entities.Client client);
    Task DeleteAsync(int id);
}
