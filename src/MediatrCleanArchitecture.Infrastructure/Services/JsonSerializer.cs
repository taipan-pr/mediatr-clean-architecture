using System.Text.Json;
using MediatrCleanArchitecture.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace MediatrCleanArchitecture.Infrastructure.Services;

internal class JsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions _options;
    private readonly ILogger _logger;

    public JsonSerializer(IOptions<JsonSerializerOptions> options, ILogger logger)
    {
        _logger = logger;
        _options = options.Value;
    }

    public string Serialize<T>(T value, JsonSerializerOptions? options = null)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(value, options ?? _options);
        return json;
    }

    public T Deserialize<T>(string json, JsonSerializerOptions? options = null)
    {
        var obj = System.Text.Json.JsonSerializer.Deserialize<T>(json, options ?? _options);
        return obj;
    }

    public object Deserialize(string json, Type type, JsonSerializerOptions? options = null)
    {
        var obj = System.Text.Json.JsonSerializer.Deserialize(json, type, options ?? _options);
        return obj;
    }

    public bool TryDeserialize<T>(string json, out T? value, JsonSerializerOptions? options = null)
    {
        if(!IsValidJsonString(json))
        {
            value = default;
            return false;
        }

        value = System.Text.Json.JsonSerializer.Deserialize<T>(json, options ?? _options);
        return value is not null;
    }

    private bool IsValidJsonString(string json)
    {
        if(string.IsNullOrWhiteSpace(json))
            return false;

        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch (JsonException)
        {
            _logger
                .ForContext("JSON", json)
                .Warning("Invalid JSON Format");
            return false;
        }
    }
}
