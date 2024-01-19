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
}
