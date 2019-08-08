using System.Text;
using Zaabee.NewtonsoftJson;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.NewtonsoftJson
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            o == null ? new byte[0] : Encoding.UTF8.GetBytes(o.ToJson());

        public T Deserialize<T>(byte[] bytes) =>
            bytes == null || bytes.Length == 0 ? default : Encoding.UTF8.GetString(bytes).FromJson<T>();
    }
}