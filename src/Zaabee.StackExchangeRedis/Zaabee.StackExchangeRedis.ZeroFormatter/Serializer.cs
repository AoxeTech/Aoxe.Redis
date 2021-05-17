using Zaabee.StackExchangeRedis.Serializer.Abstractions;
using Zaabee.ZeroFormatter;

namespace Zaabee.StackExchangeRedis.ZeroFormatter
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) => o.ToBytes();

        public T Deserialize<T>(byte[] bytes) => bytes.FromBytes<T>();
    }
}