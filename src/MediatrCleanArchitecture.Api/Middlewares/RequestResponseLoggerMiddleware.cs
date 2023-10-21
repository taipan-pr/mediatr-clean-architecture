using System.Net;
using System.Text;

namespace MediatrCleanArchitecture.Api.Middlewares;

public class RequestResponseLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestResponseLoggerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await using var request = new MemoryStream();
        var requestJson = await GetRequestBodyAsync(context, request);
        await using var response = new MemoryStream();
        var originalBody = context.Response.Body;
        context.Response.Body = response;
        await _next(context);
        response.Seek(0, SeekOrigin.Begin);
        var responseJson = await new StreamReader(context.Response.Body).ReadToEndAsync();
        response.Seek(0, SeekOrigin.Begin);
        await response.CopyToAsync(originalBody);

        var route = context.GetRouteData();
        if(route.Values.TryGetValue("controller", out var controller) && route.Values.TryGetValue("action", out var action))
        {
            _logger
                .ForContext("Protocol", context.Request.Protocol)
                .ForContext("PathBase", context.Request.PathBase)
                .ForContext("Path", context.Request.Path)
                .ForContext("Method", context.Request.Method)
                .ForContext("ContentType", context.Request.ContentType)
                .ForContext("ContentLength", context.Request.ContentLength)
                .ForContext("Request", requestJson, true)
                .ForContext("Response", string.IsNullOrWhiteSpace(responseJson) ? "null" : responseJson, true)
                .Information("Request information: {Controller}.{Action} - {StatusCode} ({Status})",
                    controller?.ToString(),
                    action?.ToString(),
                    context.Response.StatusCode,
                    (HttpStatusCode)context.Response.StatusCode);
        }
    }

    private static async Task<string?> GetRequestBodyAsync(HttpContext context, Stream stream)
    {
        if((!string.IsNullOrWhiteSpace(context.Request.ContentType) &&
            context.Request.ContentType.Contains("multipart/form-data")) ||
           context.Request.ContentLength is null or 0) return default;
        using var bodyReader = new StreamReader(context.Request.Body);
        var bodyAsText = await bodyReader.ReadToEndAsync();
        var bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
        await stream.WriteAsync(bytesToWrite);
        stream.Seek(0, SeekOrigin.Begin);
        context.Request.Body = stream;
        return bodyAsText.Replace(" ", string.Empty)
            .Replace("\r", string.Empty)
            .Replace("\n", string.Empty);
    }
}
