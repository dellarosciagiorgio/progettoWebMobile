using Abstraction.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services
            , IConfiguration config)
        {
            services.AddDbContext<MyDbContext>(opt =>
            {
                //opt.UseLazyLoadingProxies();
                opt.UseSqlServer(config.GetConnectionString("MyDbContext"));
            });

            services.AddScoped<IMyDbContext, MyDbContext>();
            return services;
        }
    }
}
