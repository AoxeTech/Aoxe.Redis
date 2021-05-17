using Zaabee.StackExchangeRedis.Serializer.Abstractions;
using Zaabee.ZeroFormatter;

namespace Zaabee.StackExchangeRedis.ZeroFormatter
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            ZeroSerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) =>
            ZeroSerializer.Deserialize<T>(bytes);
    }
}