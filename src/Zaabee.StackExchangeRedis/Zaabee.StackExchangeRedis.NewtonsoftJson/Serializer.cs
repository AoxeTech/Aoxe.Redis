using System.Text;
using Newtonsoft.Json;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.NewtonsoftJson
{
    public class Serializer : ISerializer
    {
        private static Encoding _encoding;
        private static JsonSerializerSettings _settings;

        public Serializer(Encoding encoding = null, JsonSerializerSettings settings = null)
        {
            _encoding = encoding ?? Encoding.UTF8;
            _settings = settings;
        }

        public byte[] Serialize<T>(T o) =>
            o is null
                ? new byte[0]
                : _encoding.GetBytes(JsonConvert.SerializeObject(o, _settings));

        public T Deserialize<T>(byte[] bytes) =>
            bytes is null || bytes.Length is 0
                ? default
                : (T) JsonConvert.DeserializeObject(_encoding.GetString(bytes), typeof(T), _settings);
    }
}