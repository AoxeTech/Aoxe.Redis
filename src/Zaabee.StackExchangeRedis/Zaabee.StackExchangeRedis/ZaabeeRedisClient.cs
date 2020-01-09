using System;
using System.Net;
using StackExchange.Redis;
using Zaabee.StackExchangeRedis.Abstractions;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient : IZaabeeRedisClient, IDisposable
    {
        private IConnectionMultiplexer _conn;
        private IDatabase _db;
        private ISerializer _serializer;
        private TimeSpan _defaultExpiry;

        public ZaabeeRedisClient(RedisConfig config, ISerializer serializer) =>
            Init(config.Options, config.DefaultExpiry, serializer);

        public ZaabeeRedisClient(string connectionString, TimeSpan defaultExpiry, ISerializer serializer) =>
            Init(ConfigurationOptions.Parse(connectionString), defaultExpiry, serializer);

        public ZaabeeRedisClient(ConfigurationOptions options, TimeSpan defaultExpiry, ISerializer serializer) =>
            Init(options, defaultExpiry, serializer);

        private void Init(ConfigurationOptions options, TimeSpan defaultExpiry, ISerializer serializer)
        {
            _defaultExpiry = defaultExpiry;
            _conn = ConnectionMultiplexer.Connect(options);
            _serializer = serializer;
            _db = _conn.GetDatabase();
        }

        public EndPoint[] GetEndPoints(bool configuredOnly = false) => _conn.GetEndPoints(configuredOnly);

        public string GetStatus() => _conn.GetStatus();

        public int GetHashSlot(string key) => _conn.GetHashSlot(key);

        public string GetStormLog() => _conn.GetStormLog();

        public void ResetStormLog() => _conn.ResetStormLog();

        public IZaabeeRedisServer GetServer(string host, int port, object asyncState = null) =>
            new ZaabeeRedisServer(_conn.GetServer(host, port, asyncState));

        public IZaabeeRedisServer GetServer(string hostAndPort, object asyncState = null) =>
            new ZaabeeRedisServer(_conn.GetServer(hostAndPort, asyncState));

        public IZaabeeRedisServer GetServer(IPAddress host, int port) =>
            new ZaabeeRedisServer(_conn.GetServer(host, port));

        public IZaabeeRedisServer GetServer(EndPoint endpoint, object asyncState = null) =>
            new ZaabeeRedisServer(_conn.GetServer(endpoint, asyncState));

        public void Dispose() => _conn.Dispose();
    }
}