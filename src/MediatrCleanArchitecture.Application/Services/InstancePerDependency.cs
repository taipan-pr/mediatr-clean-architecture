using MediatrCleanArchitecture.Application.Interfaces;

namespace MediatrCleanArchitecture.Application.Services;

internal class InstancePerDependency : IInstancePerDependency
{
    public int Count { get; private set; }
    public void Increment() => Count++;
}
