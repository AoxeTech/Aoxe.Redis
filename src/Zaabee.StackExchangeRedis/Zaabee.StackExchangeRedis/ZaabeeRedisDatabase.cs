namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase(
    IDatabase db,
    IBytesSerializer serializer,
    TimeSpan defaultExpiry
) : IZaabeeRedisDatabase;
