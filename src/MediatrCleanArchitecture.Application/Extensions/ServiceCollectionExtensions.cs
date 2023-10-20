using MediatrCleanArchitecture.Application.Interfaces;
using MediatrCleanArchitecture.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MediatrCleanArchitecture.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();

        return services;
    }
}
