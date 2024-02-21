using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Features
{
    public static class FeaturesApplicationExtension
    {
        public static IServiceCollection AddFeaturesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
