using MediatrCleanArchitecture.Application.Interfaces;
using MediatrCleanArchitecture.Infrastructure.Database;
using MediatrCleanArchitecture.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatrCleanArchitecture.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PostgresDataContext>(
            opt => opt.UseNpgsql(configuration.GetConnectionString(PostgresDataContext.ConnectionStringName)));

        services.AddScoped<IDbContext, PostgresDataContext>();
        return services;
    }

    public static IServiceCollection ConfigureInfrastructureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PostgresOptions>()
            .BindConfiguration(PostgresOptions.ConfigurationName)
            .PostConfigure(opt =>
            {
                opt.ConnectionString = configuration.GetConnectionString(PostgresDataContext.ConnectionStringName);
            });

        return services;
    }
}
