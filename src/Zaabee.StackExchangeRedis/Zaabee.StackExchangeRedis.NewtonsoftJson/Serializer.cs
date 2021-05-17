using System.Text;
using Newtonsoft.Json;
using Zaabee.NewtonsoftJson;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;

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
            NewtonsoftJsonSerializer.Serialize(o, _settings, _encoding);

        public T Deserialize<T>(byte[] bytes) =>
            NewtonsoftJsonSerializer.Deserialize<T>(bytes, _settings, _encoding);
    }
}