using System.Diagnostics;
using MediatR;
using Serilog.Context;

namespace MediatrCleanArchitecture.Application.Mediatr.PipelineBehaviors;

internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        LogContext.PushProperty("Request", request, true);
        try
        {
            _logger.Information("Start handling {Type}", typeof(TRequest).Name);
            return await next();
        }
        finally
        {
            stopwatch.Stop();
            _logger.Information("Completed handling {Type} - {Elapsed}", typeof(TRequest).Name, stopwatch.Elapsed);
        }
    }
}
