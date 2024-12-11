using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Client.Infrastructure.Persistence.Configurations;

internal class ClientConfiguration : IEntityTypeConfiguration<Domain.Entities.Client>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Client> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
