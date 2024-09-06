namespace Aoxe.StackExchangeRedis.Client;

public class AoxeStackExchangeRedisOptions
{
    public IBytesSerializer Serializer { get; set; }
    public TimeSpan DefaultExpiry { get; set; } = TimeSpan.FromMinutes(10);
    public string ConnectionString { get; set; }
    public ConfigurationOptions Options { get; set; }

    public AoxeStackExchangeRedisOptions(
        string connectionString,
        IBytesSerializer serializer,
        TimeSpan? defaultExpiry = null
    )
    {
        ConnectionString = connectionString;
        Options = ConfigurationOptions.Parse(ConnectionString);
        Serializer = serializer;
        DefaultExpiry = defaultExpiry ?? DefaultExpiry;
    }

    public AoxeStackExchangeRedisOptions(
        ConfigurationOptions options,
        IBytesSerializer serializer,
        TimeSpan? defaultExpiry = null
    )
    {
        Options = options;
        ConnectionString = Options.ToString();
        Serializer = serializer;
        DefaultExpiry = defaultExpiry ?? DefaultExpiry;
    }
}
