using MediatrCleanArchitecture.Infrastructure.Database;
using MediatrCleanArchitecture.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatrCleanArchitecture.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
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

    public static IServiceCollection AddPostgresDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {
        services.AddDbContext<PostgresDataContext>(options);
        return services;
    }
}
