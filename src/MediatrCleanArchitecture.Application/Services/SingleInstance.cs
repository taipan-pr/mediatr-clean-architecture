using MediatrCleanArchitecture.Application.Interfaces;

namespace MediatrCleanArchitecture.Application.Services;

internal class SingleInstance : ISingleInstance
{
    public int Count { get; private set; }
    public void Increment() => Count++;
}
