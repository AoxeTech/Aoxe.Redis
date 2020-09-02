using Zaabee.Binary;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;

namespace Zaabee.StackExchangeRedis.Binary
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) => BinarySerializer.Serialize(o);
        public T Deserialize<T>(byte[] bytes) => BinarySerializer.Deserialize<T>(bytes);
    }
}