namespace Aoxe.StackExchangeRedis.TestProject;

public static class AoxeRedisClientFactory
{
    private static readonly IAoxeRedisClient Client = new AoxeRedisClient(
        new AoxeStackExchangeRedisOptions(
            //            "192.168.78.140:6379,192.168.78.141:6379,192.168.78.142:6379,192.168.78.140:7379,192.168.78.141:7379,192.168.78.142:7379,abortConnect=false,syncTimeout=3000",
            "192.168.78.130:6379,abortConnect=false,syncTimeout=3000",
            new Protobuf.Serializer(),
            TimeSpan.FromMinutes(10)
        )
    );

    public static IAoxeRedisClient GetClient() => Client;
}
