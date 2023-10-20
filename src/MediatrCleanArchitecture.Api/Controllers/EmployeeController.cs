using MediatrCleanArchitecture.Application.Interfaces;
using MediatrCleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCleanArchitecture.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IEmployeeService _employeeService;

    public EmployeeController(ILogger logger, IEmployeeService employeeService)
    {
        _logger = logger;
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IEnumerable<Employee>> Get()
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(EmployeeController), nameof(Get));
        var employees = await _employeeService.GetAllEmployees();
        return employees;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IResult> GetById(string id)
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(EmployeeController), nameof(GetById));
        var employee = await _employeeService.GetEmployeeById(id);
        return employee is null ? Results.NotFound() : Results.Ok(employee);
    }
}
