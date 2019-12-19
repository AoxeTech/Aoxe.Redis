using System;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public class RedisConfig
    {
        private string _connectionString;
        private ConfigurationOptions _options;

        public string ConnectionString
        {
            get => _connectionString;
            set => _options = ConfigurationOptions.Parse(_connectionString = value);
        }

        public ConfigurationOptions Options
        {
            get => _options;
            set => _connectionString = (_options = value).ToString();
        }

        public TimeSpan DefaultExpiry { get; set; } = TimeSpan.FromMinutes(10);

        public RedisConfig()
        {
        }

        public RedisConfig(string connectionString, TimeSpan? defaultExpiry = null)
        {
            ConnectionString = connectionString;
            DefaultExpiry = defaultExpiry ?? DefaultExpiry;
        }

        public RedisConfig(ConfigurationOptions options, TimeSpan? defaultExpiry = null)
        {
            Options = options;
            DefaultExpiry = defaultExpiry ?? DefaultExpiry;
        }
    }
}