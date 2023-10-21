namespace MediatrCleanArchitecture.Application.Interfaces;

public interface IServiceLifetime
{
    void Increment();
    dynamic GetResult();
}
