using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Xml
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) => Zaabee.Xml.XmlSerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) => Zaabee.Xml.XmlSerializer.Deserialize<T>(bytes);
    }
}