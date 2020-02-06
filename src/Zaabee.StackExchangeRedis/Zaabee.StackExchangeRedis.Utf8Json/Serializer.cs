using Utf8Json;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Utf8Json
{
    public class Serializer : ISerializer
    {
        private static IJsonFormatterResolver _jsonFormatterResolver;

        public Serializer(IJsonFormatterResolver jsonFormatterResolver = null)
        {
            _jsonFormatterResolver = jsonFormatterResolver;
        }

        public byte[] Serialize<T>(T o) =>
            o is null
                ? new byte[0]
                : JsonSerializer.Serialize(o, _jsonFormatterResolver);

        public T Deserialize<T>(byte[] bytes) =>
            bytes is null || bytes.Length is 0
                ? default
                : JsonSerializer.Deserialize<T>(bytes, _jsonFormatterResolver);
    }
}