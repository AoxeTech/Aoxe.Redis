using Zaabee.StackExchangeRedis.ISerialize;
using Zaabee.ZeroFormatter;

namespace Zaabee.StackExchangeRedis.ZeroFormatter
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            o == null ? new byte[0] : o.ToZeroFormatter();

        public T Deserialize<T>(byte[] bytes) =>
            bytes == null || bytes.Length == 0 ? default(T) : bytes.FromZeroFormatter<T>();
    }
}