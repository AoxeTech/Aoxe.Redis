namespace Zaabee.Redis.ISerialize
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T o);
        T Deserialize<T>(byte[] bytes);
    }
}