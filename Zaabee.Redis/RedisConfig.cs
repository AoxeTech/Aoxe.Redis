using System;

namespace Zaabee.Redis
{
    public class RedisConfig
    {
        public string ConnectionString { get; set; }
        public TimeSpan DefaultExpiry { get; set; }

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