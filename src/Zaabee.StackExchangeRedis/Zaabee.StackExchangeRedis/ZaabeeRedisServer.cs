using System;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Redis;
using Zaabee.StackExchangeRedis.Abstractions;

namespace Zaabee.StackExchangeRedis
{
    public class ZaabeeRedisServer : IZaabeeRedisServer
    {
        private readonly IServer _server;
        public EndPoint EndPoint => _server.EndPoint;
        public bool IsConnected => _server.IsConnected;
        public bool IsSlave => _server.IsSlave;
        public bool AllowSlaveWrites => _server.AllowSlaveWrites;
        public Version Version => _server.Version;
        public int DatabaseCount => _server.DatabaseCount;

        public ZaabeeRedisServer(IServer server)
        {
            _server = server;
        }

        public void FlushAllDatabases() => _server.FlushAllDatabases();

        public async Task FlushAllDatabasesAsync() => await _server.FlushAllDatabasesAsync();

        public void FlushDatabase(int database = 0) => _server.FlushDatabase(database);

        public async Task FlushDatabaseAsync(int database = 0) => await _server.FlushDatabaseAsync(database);
    }
}