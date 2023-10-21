using MediatrCleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCleanArchitecture.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceLifetimeController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IServiceLifetime _serviceLifetime;

    public ServiceLifetimeController(ILogger logger, IServiceLifetime serviceLifetime)
    {
        _logger = logger;
        _serviceLifetime = serviceLifetime;
    }

    [HttpPost]
    public IResult Increment()
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(ServiceLifetimeController), nameof(Increment));
        _serviceLifetime.Increment();
        return Results.Ok();
    }

    [HttpGet]
    public IResult Get()
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(ServiceLifetimeController), nameof(Get));
        var result = _serviceLifetime.GetResult();
        return Results.Ok(result);
    }
}
