using Basket.Application.Interfaces.Persistence;
using Basket.Infrastructure.Persistence;
using Basket.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Infrastructure
{
    public static class InfrastructureServiceResolver
    {
        public static IServiceCollection InfrastructureServiceRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BasketContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BasketConnectionString")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
