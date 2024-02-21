using Gesd.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gesd.Features.Contrats.Repositories;

namespace Gesd.Data
{
    public static class DataApplicationExtension
    {
        public static IServiceCollection AddDataConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            return services;
        }
    }
}
