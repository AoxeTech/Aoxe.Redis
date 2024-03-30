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

    private T? FromRedisValue<T>(RedisValue value)
    {
        if (value.IsNull) return default;
        if (Consts.RedisValueTypeCodes.Contains(typeof(T)))
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        return serializer.FromBytes<T>(value);
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
}
