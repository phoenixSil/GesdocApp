using Gesd.Application;
using Gesd.Data;
using Gesd.Features;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gesd.Ioc
{
    public static class IocApplicationExtensions
    {
        public static IServiceCollection AddIocConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(logging =>
            {
                logging.AddConsole(); // Ajoute la sortie console pour la journalisation
            });

            services.AddApplicationServiceConfiguration(configuration);
            services.AddDataConfiguration(configuration);
            services.AddFeaturesConfiguration(configuration);
            return services;
        }
    }
}
