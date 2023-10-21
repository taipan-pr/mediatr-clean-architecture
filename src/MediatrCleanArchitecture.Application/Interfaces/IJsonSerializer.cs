using System.Text.Json;

namespace MediatrCleanArchitecture.Application.Interfaces;

public interface IJsonSerializer
{
    string Serialize<T>(T value, JsonSerializerOptions? options = null);
    T Deserialize<T>(string json, JsonSerializerOptions? options = null);
    object Deserialize(string json, Type type, JsonSerializerOptions? options = null);
    bool TryDeserialize<T>(string json, out T? value, JsonSerializerOptions? options = null);
}
