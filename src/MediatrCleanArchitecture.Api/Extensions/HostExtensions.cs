using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Exceptions;

namespace MediatrCleanArchitecture.Api.Extensions;

public static class HostExtensions
{
    public static IHostBuilder UseAppConfigurations(this IHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            var environment = context.HostingEnvironment.EnvironmentName;
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
            config.AddEnvironmentVariables();
            config.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
            config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
        });
        return host;
    }

    public static IHostBuilder UseSeriLog(this IHostBuilder host)
    {
        host.UseSerilog((context, services, config) =>
        {
            if (!string.IsNullOrWhiteSpace(context.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
            {
                config.WriteTo.ApplicationInsights(new TelemetryConfiguration
                {
                    ConnectionString = context.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
                }, TelemetryConverter.Traces);
            }

            if (!string.IsNullOrWhiteSpace(context.Configuration["Seq:HostName"]) &&
                !string.IsNullOrWhiteSpace(context.Configuration["Seq:ApiKey"]))
            {
                config.WriteTo.Seq(
                    context.Configuration["Seq:HostName"]!,
                    apiKey: context.Configuration["Seq:ApiKey"]);
            }

            config
#if DEBUG
                .Enrich.WithProperty("Environment", "Local")
#else
                .Enrich.WithProperty("Environment", context.Configuration["ASPNETCORE_ENVIRONMENT"])
#endif
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMachineName()
                .Enrich.WithClientIp()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services);
        });
        return host;
    }
}
