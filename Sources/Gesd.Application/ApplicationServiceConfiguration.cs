using Gesd.Application.Services.Fichiers;
using Gesd.Features.Contrats.Services.Fichiers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gesd.Application
{
    public static class ApplicationServiceConfiguration
    {
        public static IServiceCollection AddApplicationServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFichierService, FichierService>();
            return services;
        }
    }
}
