namespace MediatrCleanArchitecture.Application.Interfaces;

public interface ISingleInstance
{
    int Count { get; }
    void Increment();
}
