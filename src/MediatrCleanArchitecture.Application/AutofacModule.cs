using System.Reflection;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using MediatrCleanArchitecture.Application.Mediatr.PipelineBehaviors;
using MediatrCleanArchitecture.Application.Services;
using Module = Autofac.Module;

namespace MediatrCleanArchitecture.Application;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register all implementations within the current assembly
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Except<SingleInstance>(e => e.SingleInstance().AsImplementedInterfaces())
            .Except<InstancePerLifetimeScope>(e => e.InstancePerLifetimeScope().AsImplementedInterfaces())
            .Except<InstancePerDependency>(e => e.InstancePerDependency().AsImplementedInterfaces())
            .AsImplementedInterfaces();

        var configuration = MediatRConfigurationBuilder
            .Create(Assembly.GetExecutingAssembly())
            .WithAllOpenGenericHandlerTypesRegistered()
            .WithRegistrationScope(RegistrationScope.Scoped)
            .WithCustomPipelineBehaviors(new[]
            {
                typeof(LoggingBehavior<,>),
                typeof(ValidationBehavior<,>)
            })
            .Build();
        builder.RegisterMediatR(configuration);
    }
}
