using Zaabee.Jil;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Jil
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) => o is null ? new byte[0] : o.ToBytes();

        public T Deserialize<T>(byte[] bytes) => bytes is null || bytes.Length is 0 ? default : bytes.FromBytes<T>();
    }
}