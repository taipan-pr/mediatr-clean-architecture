using System.Reflection;
using Autofac;
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
    }
}
