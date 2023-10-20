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
}
