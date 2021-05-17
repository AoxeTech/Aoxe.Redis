using System.Text.Json;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;
using Zaabee.SystemTextJson;

namespace Zaabee.StackExchangeRedis.SystemTextJson
{
    public class Serializer : ISerializer
    {
        private static JsonSerializerOptions _jsonSerializerOptions;

        public Serializer(JsonSerializerOptions jsonSerializerOptions = null)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public byte[] Serialize<T>(T o) => o.ToBytes(_jsonSerializerOptions);

        public T Deserialize<T>(byte[] bytes) => bytes.FromBytes<T>(_jsonSerializerOptions);
    }
}