using Client.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Client.Infrastructure.Persistence;

public class ClientContext : DbContext, IApplicantionDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ClientContext(DbContextOptions options, IPublisher publisher)
        : base(options) 
    {
        _publisher = publisher;
    }

    public DbSet<Domain.Entities.Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientContext).Assembly);
    }
}
