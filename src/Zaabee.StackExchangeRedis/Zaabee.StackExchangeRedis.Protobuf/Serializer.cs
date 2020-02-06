using System;
using System.IO;
using ProtoBuf.Meta;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis.Protobuf
{
    public class Serializer : ISerializer
    {
        private static readonly Lazy<RuntimeTypeModel> model = new Lazy<RuntimeTypeModel>(CreateTypeModel);

        private static RuntimeTypeModel Model => model.Value;

        public byte[] Serialize<T>(T o)
        {
            if (o is null) return new byte[0];
            var stream = new MemoryStream();
            Model.Serialize(stream, o);
            return StreamToBytes(stream);
        }

        public T Deserialize<T>(byte[] bytes)
        {
            if (bytes is null || bytes.Length is 0) return default;
            using var stream = new MemoryStream(bytes);
            return (T) Model.Deserialize(stream, null, typeof(T));
        }

        private static RuntimeTypeModel CreateTypeModel()
        {
            var typeModel = TypeModel.Create();
            typeModel.UseImplicitZeroDefaults = false;
            return typeModel;
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