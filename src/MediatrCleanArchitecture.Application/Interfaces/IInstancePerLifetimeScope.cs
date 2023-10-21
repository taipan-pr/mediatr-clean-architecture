namespace MediatrCleanArchitecture.Application.Interfaces;

public interface IInstancePerLifetimeScope
{
    int Count { get; }
    void Increment();
}
