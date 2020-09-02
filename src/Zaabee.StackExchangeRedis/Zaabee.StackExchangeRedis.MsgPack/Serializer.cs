using Zaabee.MsgPack;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;

namespace Zaabee.StackExchangeRedis.MsgPack
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) => MsgPackSerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) => MsgPackSerializer.Deserialize<T>(bytes);
    }
}