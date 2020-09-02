namespace Zaabee.StackExchangeRedis.Serializer.Abstractions
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T o);
        T Deserialize<T>(byte[] bytes);
    }
}