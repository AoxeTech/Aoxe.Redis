using Zaabee.Protobuf;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;

namespace Zaabee.StackExchangeRedis.Protobuf
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            ProtobufSerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) =>
            ProtobufSerializer.Deserialize<T>(bytes);
    }
}