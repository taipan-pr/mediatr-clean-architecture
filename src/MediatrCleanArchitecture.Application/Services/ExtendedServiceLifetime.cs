using MediatrCleanArchitecture.Application.Interfaces;

namespace MediatrCleanArchitecture.Application.Services;

internal class ExtendedServiceLifetime : IExtendedServiceLifetime
{
    private readonly IDefaultRegistration _defaultRegistration;
    private readonly IInstancePerDependency _instancePerDependency;
    private readonly IInstancePerLifetimeScope _instancePerLifetimeScope;
    private readonly ISingleInstance _singleInstance;

    public ExtendedServiceLifetime(IDefaultRegistration defaultRegistration,
        IInstancePerDependency instancePerDependency,
        IInstancePerLifetimeScope instancePerLifetimeScope,
        ISingleInstance singleInstance)
    {
        _defaultRegistration = defaultRegistration;
        _instancePerDependency = instancePerDependency;
        _instancePerLifetimeScope = instancePerLifetimeScope;
        _singleInstance = singleInstance;
    }

    public void Increment()
    {
        _defaultRegistration.Increment();
        _instancePerDependency.Increment();
        _instancePerLifetimeScope.Increment();
        _singleInstance.Increment();
    }

    public dynamic GetResult()
    {
        Increment();

        return new
        {
            DefaultRegistration = _defaultRegistration.Count,
            InstancePerDependency = _instancePerDependency.Count,
            InstancePerLifetimeScope = _instancePerLifetimeScope.Count,
            SingleInstance = _singleInstance.Count
        };
    }
}
