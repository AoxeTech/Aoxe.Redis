using System.Threading.Tasks;
using Zaabee.Protobuf;
using Zaabee.Redis.ISerialize;

namespace Zaabee.Redis.Protobuf
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o)
        {
            return o == null ? new byte[0] : o.ToProtobuf();
        }

        public Task<byte[]> SerializeAsync<T>(T o)
        {
            return o == null ? Task.FromResult(new byte[0]) : Task.Factory.StartNew(() => o.ToProtobuf());
        }

        public T Deserialize<T>(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return default(T);
            return bytes.FromProtobuf<T>();
        }

        public Task<T> DeserializeAsync<T>(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return Task.FromResult(default(T));
            return Task.Factory.StartNew(bytes.FromProtobuf<T>);
        }
    }
}