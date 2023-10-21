namespace MediatrCleanArchitecture.Application.Interfaces;

public interface IExtendedServiceLifetime
{
    void Increment();
    dynamic GetResult();
}
