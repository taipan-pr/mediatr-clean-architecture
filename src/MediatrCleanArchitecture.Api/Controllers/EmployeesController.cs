using MediatR;
using MediatrCleanArchitecture.Application.Commands.CreateEmployee;
using MediatrCleanArchitecture.Application.Queries.GetAllEmployee;
using MediatrCleanArchitecture.Application.Queries.GetEmployeeById;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCleanArchitecture.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ISender _sender;

    public EmployeesController(ILogger logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IResult> Get(CancellationToken cancellationToken)
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(EmployeesController), nameof(Get));
        var employees = await _sender.Send(new GetAllEmployeeRequest(), cancellationToken);
        return Results.Ok(employees);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IResult> GetById(string id, CancellationToken cancellationToken)
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(EmployeesController), nameof(GetById));
        var employee = await _sender.Send(new GetEmployeeByIdRequest
            {
                Id = id
            },
            cancellationToken);
        return employee is null ? Results.NotFound() : Results.Ok(employee);
    }

    [HttpPost]
    public async Task<IResult> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(EmployeesController), nameof(Create));
        var result = await _sender.Send(request, cancellationToken);
        return Results.Ok(result);
    }
}
