using Account.Application.Data;
using Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Account.Infrastructure.Persistence;

public class AccountContext : DbContext, IApplicantionDbContext, IUnitOfWork
{
    private IDbContextTransaction _currentTransaction;

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

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
            return;

        _currentTransaction = await Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await SaveChangesAsync();
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
            }
        }
        finally
        {
            // Dispose of the transaction
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
}
