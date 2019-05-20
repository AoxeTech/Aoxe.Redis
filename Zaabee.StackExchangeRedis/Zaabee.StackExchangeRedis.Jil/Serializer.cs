using Zaabee.Jil;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Jil
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o)
        {
            return o == null ? new byte[0] : o.ToJil().SerializeUtf8();
        }

        public T Deserialize<T>(byte[] bytes)
        {
            return bytes == null || bytes.Length == 0 ? default(T) : bytes.DeserializeUtf8().FromJil<T>();
        }
    }
}