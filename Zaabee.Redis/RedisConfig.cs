namespace Zaabee.Redis
{
    public class RedisConfig
    {
        public string ConnectionString { get; set; }
        public double DefaultExpireMinutes { get; set; }

        public RedisConfig(string connectionString, double defaultExpireMinutes = 10)
        {
            ConnectionString = connectionString;
            DefaultExpireMinutes = defaultExpireMinutes;
        }
    }
}