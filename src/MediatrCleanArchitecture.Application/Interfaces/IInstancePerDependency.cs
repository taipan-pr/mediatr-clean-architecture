namespace MediatrCleanArchitecture.Application.Interfaces;

public interface IInstancePerDependency
{
    int Count { get; }
    void Increment();
}
