using Gesd.Application;
using Gesd.Data;
using Gesd.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Ioc
{
    public static  class IocApplicationExtensions
    {
        public static IServiceCollection AddIocConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServiceConfiguration(configuration);
            services.AddDataConfiguration(configuration);
            services.AddFeaturesConfiguration(configuration);
            return services;
        }
    }
}
