using Gesd.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gesd.Features.Contrats.Repositories;
using Gesd.Data.Settings;
using Gesd.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gesd.Data
{
    public static class DataApplicationExtension
    {
        public static IServiceCollection AddDataConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlSettings = configuration.GetSection(nameof(SQLSettings)).Get<SQLSettings>();
            var connectionstring = $"Server={sqlSettings.Server},{sqlSettings.Port};Initial Catalog={sqlSettings.Database};Persist Security Info=False;User ID={sqlSettings.UserName};Password={sqlSettings.Password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            services.AddDbContext<GesdContext>(options =>
            {
                options.UseSqlServer(connectionstring);
            });

            var fichierSettings = configuration.GetSection(nameof(FileSettings)).Get<FileSettings>();
            services.AddSingleton(fichierSettings);

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IEncryptedFileRepository, EncryptedFileRepository>();
            services.AddScoped<IBlobRepository, BlobRepository>();
            services.AddScoped<IKeyStoreRepository, KeyStoreRepository>();
            return services;
        }
    }
}
