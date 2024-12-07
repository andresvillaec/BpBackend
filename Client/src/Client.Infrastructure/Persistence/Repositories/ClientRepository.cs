using Client.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Client.Infrastructure.Persistence.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ClientContext _context;

    public ClientRepository(ClientContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Domain.Entities.Client> GetByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<Domain.Entities.Client> AddAsync(Domain.Entities.Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();

        return client;
    }

    public async Task UpdateAsync(Domain.Entities.Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}
