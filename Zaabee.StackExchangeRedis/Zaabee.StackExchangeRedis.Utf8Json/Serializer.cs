using Zaabee.StackExchangeRedis.ISerialize;
using Zaabee.Utf8Json;

namespace Zaabee.StackExchangeRedis.Utf8Json
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o)
        {
            return o.Utf8JsonToBytes();
        }

        public T Deserialize<T>(byte[] bytes)
        {
            return bytes.FromUtf8Json<T>();
        }
    }
}