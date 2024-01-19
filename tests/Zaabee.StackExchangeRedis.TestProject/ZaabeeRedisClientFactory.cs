namespace Zaabee.StackExchangeRedis.TestProject;

public static class ZaabeeRedisClientFactory
{
    private static readonly IZaabeeRedisClient Client = new ZaabeeRedisClient(
        new ZaabeeStackExchangeRedisOptions(
            "192.168.78.140:6379,192.168.78.141:6379,192.168.78.142:6379,192.168.78.140:7379,192.168.78.141:7379,192.168.78.142:7379,abortConnect=false,syncTimeout=3000",
            new Protobuf.Serializer(),
            TimeSpan.FromMinutes(10)
        )
    );

    public static IZaabeeRedisClient GetClient() => Client;
}
