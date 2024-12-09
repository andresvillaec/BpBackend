using Client.Domain.Interfaces;
using Client.Infrastructure.Persistence;
using Client.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClientContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                ));

            services.AddScoped<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
