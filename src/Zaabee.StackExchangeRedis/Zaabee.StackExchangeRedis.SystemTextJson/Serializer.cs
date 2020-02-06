using System.Text.Json;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.SystemTextJson
{
    public class Serializer : ISerializer
    {
        private static JsonSerializerOptions _jsonSerializerOptions;

        public Serializer(JsonSerializerOptions jsonSerializerOptions = null)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public byte[] Serialize<T>(T o) =>
            o is null
                ? new byte[0]
                : JsonSerializer.SerializeToUtf8Bytes(o, _jsonSerializerOptions);

        public T Deserialize<T>(byte[] bytes) =>
            bytes is null || bytes.Length is 0
                ? default
                : JsonSerializer.Deserialize<T>(bytes, _jsonSerializerOptions);
    }
}