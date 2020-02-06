using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Xml
{
    public class Serializer : ISerializer
    {
        private static readonly ConcurrentDictionary<Type, XmlSerializer> SerializerCache =
            new ConcurrentDictionary<Type, XmlSerializer>();
        
        public byte[] Serialize<T>(T o)
        {
            if (o is null) return new byte[0];
            var type = typeof(T);
            using var stream = new MemoryStream();
            var serializer = SerializerCache.GetOrAdd(type, new XmlSerializer(type));
            serializer.Serialize(stream, o);
            return StreamToBytes(stream);
        }

        public T Deserialize<T>(byte[] bytes)
        {
            if (bytes is null || bytes.Length is 0) return default;
            var type = typeof(T);
            var serializer = SerializerCache.GetOrAdd(type, new XmlSerializer(type));
            using var ms = new MemoryStream(bytes);
            return (T) serializer.Deserialize(ms);
        }

        private static byte[] StreamToBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            if (stream.Position > 0 && stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);
            if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
    }
}