using System.IO;
using MsgPack.Serialization;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.MsgPack
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o)
        {
            if (o is null) return new byte[0];
            var serializer = MessagePackSerializer.Get<T>();
            using var stream = new MemoryStream();
            serializer.Pack(stream, o);
            var bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        public T Deserialize<T>(byte[] bytes)
        {
            if (bytes is null || bytes.Length is 0) return default;
            var serializer = MessagePackSerializer.Get<T>();
            using var stream = new MemoryStream(bytes);
            return serializer.Unpack(stream);
        }
    }
}