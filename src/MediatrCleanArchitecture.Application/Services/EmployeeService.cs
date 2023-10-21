using MediatrCleanArchitecture.Application.Interfaces;
using MediatrCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatrCleanArchitecture.Application.Services;

internal class EmployeeService : IEmployeeService
{
    private readonly IDbContext _dbContext;
    private readonly ILogger _logger;

    public EmployeeService(IDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        _logger.Debug("Calling from - {Action}", nameof(GetAllEmployees));
        var employees = await _dbContext.Employees.ToArrayAsync();
        return employees;
    }

    public async Task<Employee?> GetEmployeeById(string id)
    {
        _logger.Debug("Calling from - {Action}", nameof(GetEmployeeById));
        var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
        return employee;
    }
}
