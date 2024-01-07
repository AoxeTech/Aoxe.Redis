namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient(
    IDatabase db,
    IBytesSerializer serializer,
    TimeSpan defaultExpiry
) : IZaabeeRedisClient;
