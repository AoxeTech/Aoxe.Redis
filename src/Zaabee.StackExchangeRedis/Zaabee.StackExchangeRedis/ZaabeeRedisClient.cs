namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient(
    IDatabase db,
    IBytesSerializer serializer,
    TimeSpan defaultExpiry
) : IZaabeeRedisClient
{
    public ZaabeeRedisClient(ZaabeeStackExchangeRedisOptions zaabeeStackExchangeRedisOptions)
        : this(
            ConnectionMultiplexer.Connect(zaabeeStackExchangeRedisOptions.Options).GetDatabase(),
            zaabeeStackExchangeRedisOptions.Serializer,
            zaabeeStackExchangeRedisOptions.DefaultExpiry
        ) { }

    private T? FromRedisValue<T>(RedisValue redisValue)
    {
        if (redisValue.IsNullOrEmpty || !redisValue.HasValue)
            return default;
        return typeof(T) switch
        {
            { } t when t == typeof(bool) => (T)(object)bool.Parse(redisValue!),
            { } t when t == typeof(short) => (T)(object)short.Parse(redisValue!),
            { } t when t == typeof(int) => (T)(object)int.Parse(redisValue!),
            { } t when t == typeof(uint) => (T)(object)uint.Parse(redisValue!),
            { } t when t == typeof(long) => (T)(object)long.Parse(redisValue!),
            { } t when t == typeof(ulong) => (T)(object)ulong.Parse(redisValue!),
            { } t when t == typeof(float) => (T)(object)float.Parse(redisValue!),
            { } t when t == typeof(double) => (T)(object)double.Parse(redisValue!),
            { } t when t == typeof(string) => (T)(object)redisValue.ToString(),
            { } t when t == typeof(byte[]) => (T)(object)(byte[])redisValue!,
            { } t when t == typeof(float) => (T)(object)(ReadOnlyMemory<byte>)redisValue!,
            { } t when t == typeof(double) => (T)(object)new Memory<byte>(redisValue!),
            _ => FromRedisValue<T>(redisValue)
        };
    }

    private RedisValue ToRedisValue<T>(T value) =>
        value switch
        {
            null => RedisValue.Null,
            bool b => b,
            short i => i,
            int i => i,
            uint i => i,
            long l => l,
            ulong l => l,
            float f => f,
            double d => d,
            string s => s,
            byte[] b => b,
            ReadOnlyMemory<byte> b => b,
            Memory<byte> b => b,
            _ => serializer.ToBytes(value)
        };
}
