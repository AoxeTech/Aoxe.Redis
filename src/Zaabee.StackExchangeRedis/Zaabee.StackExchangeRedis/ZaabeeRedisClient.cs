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
        )
    {
    }

    private T? FromRedisValue<T>(RedisValue redisValue)
    {
        if (redisValue.IsNull) return default;
        if (RedisValueTypeCodes.Contains(typeof(T)))
            return (T)(object)redisValue;
        return serializer.FromBytes<T>(redisValue);
    }

    private RedisValue ToRedisValue<T>(T value) =>
        value switch
        {
            null => RedisValue.Null,
            bool b => b,
            byte[] b => b,
            ReadOnlyMemory<byte> b => b,
            Memory<byte> b => b,
            short i => i,
            int i => i,
            uint i => i,
            long l => l,
            ulong l => l,
            float f => f,
            double d => d,
            string s => s,
            _ => serializer.ToBytes(value)
        };

    private static readonly List<Type> RedisValueTypeCodes =
    [
        typeof(bool),
        typeof(byte[]),
        typeof(ReadOnlyMemory<byte>),
        typeof(Memory<byte>),
        typeof(short),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(float),
        typeof(double),
        typeof(string)
    ];
}
