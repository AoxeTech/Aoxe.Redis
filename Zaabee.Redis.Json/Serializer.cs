using Zaabee.JsonUtility;
using Zaabee.Redis.ISerialize;

namespace Zaabee.Redis.Json
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o)
        {
            return o == null ? new byte[0] : o.ToJson().SerializeUtf8();
        }

        public T Deserialize<T>(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return default(T);
            return bytes.DeserializeUtf8().FromJson<T>();
        }
    }
}