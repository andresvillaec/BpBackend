using Microsoft.EntityFrameworkCore;

namespace Client.Infrastructure.Persistence;

public class ClientContext : DbContext
{
    public ClientContext(DbContextOptions<ClientContext> options) : base(options) { }

    public DbSet<Domain.Entities.Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
