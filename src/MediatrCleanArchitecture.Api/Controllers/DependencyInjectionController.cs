using MediatrCleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCleanArchitecture.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DependencyInjectionController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IServiceLifetime _serviceLifetime;
    private readonly IEmailService _emailService;

    public DependencyInjectionController(ILogger logger, IServiceLifetime serviceLifetime, IEmailService emailService)
    {
        _logger = logger;
        _serviceLifetime = serviceLifetime;
        _emailService = emailService;
    }

    [HttpPost]
    public IResult Increment()
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(DependencyInjectionController), nameof(Increment));
        _serviceLifetime.Increment();
        return Results.Ok();
    }

    [HttpGet]
    public IResult Get()
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(DependencyInjectionController), nameof(Get));
        var result = _serviceLifetime.GetResult();
        return Results.Ok(result);
    }

    [HttpPost]
    [Route("SendEmail")]
    public async Task<IResult> SendEmail()
    {
        _logger.Information("Hello world, from {Controller}.{Action}", nameof(DependencyInjectionController), nameof(SendEmail));
        var result = await _emailService.Send();
        return Results.Ok(result);
    }
}
