using Catalog.Application.Interfaces.Persistence;
using Catalog.Infrastructure.Bus;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
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
            services.AddSingleton<TestConsumer>(sp => 
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return new TestConsumer(config);
            });         
            return services;
        }

        public static void Configue(IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<TestConsumer>()?.Consume();
        }
       
    }
}
