using Abstraction.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services
            , IConfiguration config)
        {
            services.AddDbContext<MyDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("MyDbContext"));
            });

            services.AddScoped<IMyDbContext, MyDbContext>();
            return services;
        }
    }
}
