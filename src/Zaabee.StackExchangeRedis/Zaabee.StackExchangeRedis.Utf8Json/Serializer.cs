using Utf8Json;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;
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

        public byte[] Serialize<T>(T o) => o.ToBytes(_jsonFormatterResolver);

        public T Deserialize<T>(byte[] bytes) => bytes.FromBytes<T>(_jsonFormatterResolver);
    }
}