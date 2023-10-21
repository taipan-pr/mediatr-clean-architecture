using MediatR;
using MediatrCleanArchitecture.Application.Queries.GetAllEmployee;
using MediatrCleanArchitecture.Application.Queries.GetEmployeeById;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCleanArchitecture.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ISender _sender;

    public EmployeeController(ILogger logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IResult> Get(CancellationToken cancellationToken)
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(EmployeeController), nameof(Get));
        var employees = await _sender.Send(new GetAllEmployeeRequest(), cancellationToken);
        return Results.Ok(employees);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IResult> GetById(string id, CancellationToken cancellationToken)
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(EmployeeController), nameof(GetById));
        var employee = await _sender.Send(new GetEmployeeByIdRequest
            {
                Id = id
            },
            cancellationToken);
        return employee is null ? Results.NotFound() : Results.Ok(employee);
    }
}
