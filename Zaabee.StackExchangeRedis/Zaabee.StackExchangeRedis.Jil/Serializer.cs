using System.Text;
using Zaabee.Jil;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Jil
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            o == null ? new byte[0] : Encoding.UTF8.GetBytes(o.ToJil());

        public T Deserialize<T>(byte[] bytes) =>
            bytes == null || bytes.Length == 0 ? default(T) : Encoding.UTF8.GetString(bytes).FromJil<T>();
    }
}