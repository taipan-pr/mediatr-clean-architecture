using System.Reflection;
using Autofac;
using MediatrCleanArchitecture.Application.Interfaces;
using MediatrCleanArchitecture.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace MediatrCleanArchitecture.Infrastructure;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register all implementations within the current assembly
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AsSelf()
            .AsImplementedInterfaces();

        // Register application module
        builder.RegisterModule<Application.AutofacModule>();

        // Conditional resolution
        builder.Register<IEmailService>(
            (componentContext, parameters) =>
            {
                var config = componentContext.Resolve<IConfiguration>();
                var environment = config["ASPNETCORE_ENVIRONMENT"]?.ToLowerInvariant();
                return string.IsNullOrWhiteSpace(environment) || environment == "development"
                    ? componentContext.Resolve<SendGridEmailService>()
                    : componentContext.Resolve<MailGunEmailService>();
            });
    }
}
