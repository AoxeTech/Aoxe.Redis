using Zaabee.StackExchangeRedis.ISerialize;
using Zaabee.Utf8Json;

namespace Zaabee.StackExchangeRedis.Utf8Json
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            o == null ? new byte[0] : o.Utf8JsonToBytes();

        public T Deserialize<T>(byte[] bytes) =>
            bytes == null || bytes.Length == 0 ? default(T) : bytes.FromUtf8Json<T>();
    }
}