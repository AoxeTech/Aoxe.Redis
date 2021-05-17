using System.Text;
using Jil;
using Zaabee.Jil;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;

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

        public byte[] Serialize<T>(T o) => o.ToBytes(_options, _encoding);

        public T Deserialize<T>(byte[] bytes) => JilSerializer.Deserialize<T>(bytes, _options, _encoding);
    }
}