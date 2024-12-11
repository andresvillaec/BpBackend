using Microsoft.EntityFrameworkCore;

namespace Client.Application.Data;

public interface IApplicantionDbContext
{
    DbSet<Domain.Entities.Client> Clients { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
