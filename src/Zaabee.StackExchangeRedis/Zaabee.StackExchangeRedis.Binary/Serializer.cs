using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Binary
{
    public class Serializer : ISerializer
    {
        [ThreadStatic] private static BinaryFormatter _binaryFormatter;

        public byte[] Serialize<T>(T o)
        {
            if (o is null) return new byte[0];
            _binaryFormatter ??= new BinaryFormatter();
            using var stream = new MemoryStream();
            _binaryFormatter.Serialize(stream, o);
            var bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        public T Deserialize<T>(byte[] bytes)
        {
            if (bytes is null || bytes.Length is 0) return default;
            using var stream = new MemoryStream(bytes);
            _binaryFormatter ??= new BinaryFormatter();
            return (T) _binaryFormatter.Deserialize(stream);
        }
    }
}