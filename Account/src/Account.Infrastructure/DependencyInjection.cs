using Account.Application.Data;
using Account.Domain.Interfaces;
using Account.Infrastructure.Persistence;
using Account.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                ));

            services.AddScoped<IApplicantionDbContext>(sp =>
               sp.GetRequiredService<AccountContext>());

            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<AccountContext>());

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMovementRepository, MovementRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

            return services;
        }
    }
}
