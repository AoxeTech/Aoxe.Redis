using System.Text;
using Jil;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Jil
{
    public class Serializer : ISerializer
    {
        private static Encoding _encoding;
        private static Options _options;

        public Serializer(Encoding encoding = null, Options options = null)
        {
            _encoding = encoding ?? Encoding.UTF8;
            _options = options;
        }

        public byte[] Serialize<T>(T o) =>
            o is null
                ? new byte[0]
                : _encoding.GetBytes(JSON.Serialize(o, _options));

        public T Deserialize<T>(byte[] bytes) =>
            bytes is null || bytes.Length is 0
                ? default
                : JSON.Deserialize<T>(_encoding.GetString(bytes), _options);
    }
}