using System.Text;

namespace Zaabee.StackExchangeRedis.Jil
{
    public static class EncodingExtension
    {
        public static byte[] SerializeUtf8(this string str)
        {
            return str != null ? Encoding.UTF8.GetBytes(str) : null;
        }

        public static string DeserializeUtf8(this byte[] stream)
        {
            return stream != null ? Encoding.UTF8.GetString(stream) : null;
        }
    }
}