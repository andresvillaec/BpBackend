using Account.Application.Data;
using Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.Persistence;

public class AccountContext : DbContext, IApplicantionDbContext, IUnitOfWork
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

    public DbSet<Domain.Entities.Account> Accounts { get; set; }
    public DbSet<Movement> Movements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Entities.Account>()
            .HasIndex(a => a.Number)
            .IsUnique();

        modelBuilder.Entity<Domain.Entities.Account>()
        .HasMany(e => e.Movements)
        .WithOne(e => e.Account)
        .HasForeignKey(e => e.AccountId)
        .HasPrincipalKey(e => e.Id);
    }
}
