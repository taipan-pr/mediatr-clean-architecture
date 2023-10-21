using System.Diagnostics;

namespace MediatrCleanArchitecture.Api.Middlewares;

public class TimerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public TimerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();
        _logger.Information("Time elapsed: {Elapsed} ms", stopwatch.Elapsed.TotalMilliseconds);
    }
}
