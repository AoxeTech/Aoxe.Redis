using System;

namespace Zaabee.Redis
{
    public class RedisConfig
    {
        public string ConnectionString { get; set; }
        public TimeSpan DefaultExpiry { get; set; } = TimeSpan.FromMinutes(10);

        public RedisConfig()
        {
        }

        public RedisConfig(string connectionString, TimeSpan? defaultExpiry = null)
        {
            ConnectionString = connectionString;
            DefaultExpiry = defaultExpiry ?? TimeSpan.FromMinutes(10);
        }
    }
}