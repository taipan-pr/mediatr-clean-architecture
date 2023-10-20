using MediatrCleanArchitecture.Domain.Entities;

namespace MediatrCleanArchitecture.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee?> GetEmployeeById(string id);
}
