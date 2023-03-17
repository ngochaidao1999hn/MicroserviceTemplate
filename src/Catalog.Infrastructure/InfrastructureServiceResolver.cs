using Catalog.Application.Interfaces.Persistence;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure
{
    public static class InfrastructureServiceResolver
    {
        public static IServiceCollection InfrastructureServiceRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CatalogConnectionString")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
