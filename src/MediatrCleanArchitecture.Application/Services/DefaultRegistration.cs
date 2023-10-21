using MediatrCleanArchitecture.Application.Interfaces;

namespace MediatrCleanArchitecture.Application.Services;

public class DefaultRegistration : IDefaultRegistration
{
    public int Count { get; private set; }
    public void Increment() => Count++;
}
