using MediatrCleanArchitecture.Application.Interfaces;

namespace MediatrCleanArchitecture.Application.Services;

internal class InstancePerLifetimeScope : IInstancePerLifetimeScope
{
    public int Count { get; private set; }
    public void Increment() => Count++;
}
