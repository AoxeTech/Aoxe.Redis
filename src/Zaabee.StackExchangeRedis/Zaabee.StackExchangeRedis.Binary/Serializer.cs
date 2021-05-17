using Zaabee.Binary;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;

namespace Zaabee.StackExchangeRedis.Binary
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) => o.ToBytes();
        public T Deserialize<T>(byte[] bytes) => bytes.FromBytes<T>();
    }
}