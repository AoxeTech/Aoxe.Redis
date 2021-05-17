using Zaabee.Protobuf;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;

namespace Zaabee.StackExchangeRedis.Protobuf
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) => o.ToBytes();

        public T Deserialize<T>(byte[] bytes) => bytes.FromBytes<T>();
    }
}