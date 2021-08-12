using System;
using StackExchange.Redis;
using Zaabee.Serializer.Abstractions;

namespace Zaabee.StackExchangeRedis
{
    public class ZaabeeStackExchangeRedisOptions
    {
        private string _connectionString;
        private ConfigurationOptions _options;
        public IBytesSerializer Serializer { get; set; }

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

        public ZaabeeStackExchangeRedisOptions()
        {
        }

        public ZaabeeStackExchangeRedisOptions(string connectionString, IBytesSerializer serializer,
            TimeSpan? defaultExpiry = null)
        {
            ConnectionString = connectionString;
            Serializer = serializer;
            DefaultExpiry = defaultExpiry ?? DefaultExpiry;
        }

        public ZaabeeStackExchangeRedisOptions(ConfigurationOptions options, IBytesSerializer serializer,
            TimeSpan? defaultExpiry = null)
        {
            Options = options;
            Serializer = serializer;
            DefaultExpiry = defaultExpiry ?? DefaultExpiry;
        }
    }
}