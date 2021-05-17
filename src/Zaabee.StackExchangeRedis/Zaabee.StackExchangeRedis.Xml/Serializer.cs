using Zaabee.StackExchangeRedis.Serializer.Abstractions;
using Zaabee.Xml;

namespace Zaabee.StackExchangeRedis.Xml
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            XmlSerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) =>
            XmlSerializer.Deserialize<T>(bytes);
    }
}