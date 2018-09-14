using System.Threading.Tasks;

namespace Zaabee.Redis.ISerialize
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T o);
        Task<byte[]> SerializeAsync<T>(T o);
        T Deserialize<T>(byte[] bytes);
        Task<T> DeserializeAsync<T>(byte[] bytes);
    }
}