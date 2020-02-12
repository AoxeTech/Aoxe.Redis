using Utf8Json;
using Zaabee.StackExchangeRedis.ISerialize;
using Zaabee.Utf8Json;

namespace Zaabee.StackExchangeRedis.Utf8Json
{
    public class Serializer : ISerializer
    {
        private static IJsonFormatterResolver _jsonFormatterResolver;

        public Serializer(IJsonFormatterResolver jsonFormatterResolver = null)
        {
            _jsonFormatterResolver = jsonFormatterResolver;
        }

        public byte[] Serialize<T>(T o) => Utf8JsonSerializer.Serialize(o, _jsonFormatterResolver);

        public T Deserialize<T>(byte[] bytes) => Utf8JsonSerializer.Deserialize<T>(bytes, _jsonFormatterResolver);
    }
}