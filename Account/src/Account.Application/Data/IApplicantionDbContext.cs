using Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.Data;

public interface IApplicantionDbContext
{
    DbSet<Domain.Entities.Account> Accounts { get; set; }

    DbSet<Movement> Movements { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
