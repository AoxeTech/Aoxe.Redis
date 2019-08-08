using Zaabee.Protobuf;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Protobuf
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            o == null ? new byte[0] : o.ToProtobuf();

        public T Deserialize<T>(byte[] bytes) =>
            bytes == null || bytes.Length == 0 ? default : bytes.FromProtobuf<T>();
    }
}