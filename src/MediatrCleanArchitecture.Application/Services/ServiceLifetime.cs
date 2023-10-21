using MediatrCleanArchitecture.Application.Interfaces;

namespace MediatrCleanArchitecture.Application.Services;

internal class ServiceLifetime : IServiceLifetime
{
    private readonly IDefaultRegistration _defaultRegistration;
    private readonly IInstancePerDependency _instancePerDependency;
    private readonly IInstancePerLifetimeScope _instancePerLifetimeScope;
    private readonly ISingleInstance _singleInstance;
    private readonly IExtendedServiceLifetime _extendedServiceLifetime;

    public ServiceLifetime(IDefaultRegistration defaultRegistration,
        IInstancePerDependency instancePerDependency,
        IInstancePerLifetimeScope instancePerLifetimeScope,
        ISingleInstance singleInstance,
        IExtendedServiceLifetime extendedServiceLifetime)
    {
        _defaultRegistration = defaultRegistration;
        _instancePerDependency = instancePerDependency;
        _instancePerLifetimeScope = instancePerLifetimeScope;
        _singleInstance = singleInstance;
        _extendedServiceLifetime = extendedServiceLifetime;
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

        var extendedLifetimeResult = _extendedServiceLifetime.GetResult();

        return new
        {
            ExtendedLifetime = extendedLifetimeResult,
            ServiceLifetime = new
            {
                DefaultRegistration = _defaultRegistration.Count,
                InstancePerDependency = _instancePerDependency.Count,
                InstancePerLifetimeScope = _instancePerLifetimeScope.Count,
                SingleInstance = _singleInstance.Count
            }
        };
    }
}
