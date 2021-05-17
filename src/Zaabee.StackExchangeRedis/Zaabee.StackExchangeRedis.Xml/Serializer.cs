using Zaabee.StackExchangeRedis.Serializer.Abstractions;
using Zaabee.Xml;

namespace Zaabee.StackExchangeRedis.Xml
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) => o.ToBytes();

        public T Deserialize<T>(byte[] bytes) => bytes.FromBytes<T>();
    }
}