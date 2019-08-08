using Zaabee.NewtonsoftJson;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.NewtonsoftJson
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            o == null ? new byte[0] : o.ToJson().SerializeUtf8();

        public T Deserialize<T>(byte[] bytes) =>
            bytes == null || bytes.Length == 0 ? default(T) : bytes.DeserializeUtf8().FromJson<T>();
    }
}