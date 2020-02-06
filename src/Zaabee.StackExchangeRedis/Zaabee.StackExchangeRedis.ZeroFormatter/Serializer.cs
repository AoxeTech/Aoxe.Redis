using Zaabee.StackExchangeRedis.ISerialize;
using ZeroFormatter;

namespace Zaabee.StackExchangeRedis.ZeroFormatter
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            o is null
                ? new byte[0]
                : ZeroFormatterSerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) =>
            bytes is null || bytes.Length is 0
                ? default
                : ZeroFormatterSerializer.Deserialize<T>(bytes);
    }
}