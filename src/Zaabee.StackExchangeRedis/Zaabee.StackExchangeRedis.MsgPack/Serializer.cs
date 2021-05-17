using Zaabee.MsgPack;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;

namespace Zaabee.StackExchangeRedis.MsgPack
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) => o.ToBytes();

        public T Deserialize<T>(byte[] bytes) => bytes.FromBytes<T>();
    }
}