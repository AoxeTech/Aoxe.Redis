namespace Aoxe.StackExchangeRedis;

public static class AoxeRedisServiceProviderExtensions
{
    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        Func<AoxeStackExchangeRedisOptions> optionsFunc
    ) => services.AddSingleton<IAoxeRedisClient>(new AoxeRedisClient(optionsFunc()));

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        AoxeStackExchangeRedisOptions options
    ) => services.AddSingleton<IAoxeRedisClient>(new AoxeRedisClient(options));
}
