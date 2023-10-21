using System.Net;
using MediatrCleanArchitecture.Application.Interfaces;

namespace MediatrCleanArchitecture.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly IJsonSerializer _jsonSerializer;

    public ExceptionMiddleware(RequestDelegate next, ILogger logger, IJsonSerializer jsonSerializer)
    {
        _logger = logger;
        _jsonSerializer = jsonSerializer;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var messages = GetMessages(ex, new());
            if(ex.StackTrace != null)
            {
                messages?.Add(ex.StackTrace);
            }

            _logger
                .ForContext("Message", messages, true)
                .Error(ex, "Exception {Type}", ex.GetType().Name);

            var exceptionResponse = new
            {
                Type = ex.GetType().Name,
                ex.Message
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var json = _jsonSerializer.Serialize(exceptionResponse);
            await context.Response.WriteAsync(json);
        }
    }

    private static List<string>? GetMessages(Exception? ex, List<string> messages)
    {
        if(ex is null) return default;
        if(ex.InnerException is not null) GetMessages(ex.InnerException, messages);
        messages.Add(ex.Message);
        return messages;
    }
}
