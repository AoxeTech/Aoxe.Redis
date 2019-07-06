using Zaabee.Binary;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Binary
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o)
        {
            return o == null ? new byte[0] : o.ToBytes();
        }

        public T Deserialize<T>(byte[] bytes)
        {
            return bytes == null || bytes.Length == 0 ? default(T) : bytes.FromBytes<T>();
        }
    }
}