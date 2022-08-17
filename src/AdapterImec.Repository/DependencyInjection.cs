using AdapterImec.Domain.Abstract;
using AdapterImec.Domain.Exceptions;
using AdapterImec.Repository.Repositories;
using AdapterImec.Services;
using AdapterImec.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AdapterImec.Repository
{
    public static class DependencyInjection
    {
        private const string AdapterImecConnectionString = "ConnectionString:AdapterImec";

        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration[AdapterImecConnectionString];

            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ConfigurationException($"{AdapterImecConnectionString} is not set.");
            }

            var assemblyName = typeof(DependencyInjection).Assembly.GetName().Name;

            // Set legacy mode so that we can write DateTime with UTC to comlumns with the 'timestamp without time zone' property
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddDbContextPool<DataContext>(options =>
            {
                options.UseNpgsql(connectionString, o => o.MigrationsAssembly(assemblyName));
            }, 1024);

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();
            services.AddSingleton<IImecTokenService, ImecTokenService>();
            services.AddTransient<IGetImecRequestsService, GetImecRequestsService>();
        }
    }
}
