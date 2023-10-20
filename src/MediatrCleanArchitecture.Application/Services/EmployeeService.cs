using MediatrCleanArchitecture.Application.Interfaces;
using MediatrCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatrCleanArchitecture.Application.Services;

internal class EmployeeService : IEmployeeService
{
    private readonly IDbContext _dbContext;

    public EmployeeService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        var employees = await _dbContext.Employees.ToArrayAsync();
        return employees;
    }

    public async Task<Employee?> GetEmployeeById(string id)
    {
        var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
        return employee;
    }
}
