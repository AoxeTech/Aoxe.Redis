namespace Aoxe.StackExchangeRedis;

public static class AoxeRedisServiceProviderExtensions
{
    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        Func<AoxeStackExchangeRedisOptions> optionsFactory
    ) => services.AddSingleton<IAoxeRedisClient>(new AoxeRedisClient(optionsFactory));

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        AoxeStackExchangeRedisOptions options
    ) => services.AddSingleton<IAoxeRedisClient>(new AoxeRedisClient(options));
}
