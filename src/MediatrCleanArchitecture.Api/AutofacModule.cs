using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace MediatrCleanArchitecture.Api;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register all implementations within the current assembly
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

        // Register infrastructure module
        builder.RegisterModule<Infrastructure.AutofacModule>();
    }
}
