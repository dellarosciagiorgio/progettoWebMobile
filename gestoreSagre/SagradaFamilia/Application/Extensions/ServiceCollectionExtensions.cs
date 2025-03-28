using Application.Abstraction.Services;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));
            services.AddScoped<IAcquirenteService, AcquirenteService>();
            services.AddScoped<IBigliettoService, BigliettoService>();
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ISagraService, SagraService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<ILoginService, LoginService>();
            return services;
        }
    }
}
