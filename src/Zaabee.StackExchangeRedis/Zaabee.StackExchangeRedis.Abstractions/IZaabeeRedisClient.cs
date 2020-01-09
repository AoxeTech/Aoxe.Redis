using System;
using System.Net;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface IZaabeeRedisClient : IZaabeeRedisDatabase, IDisposable
    {
        EndPoint[] GetEndPoints(bool configuredOnly = false);
        string GetStatus();
        int GetHashSlot(string key);
        string GetStormLog();
        void ResetStormLog();
        IZaabeeRedisDatabase GetDatabase(int db = -1, object asyncState = null);
        IZaabeeRedisServer GetServer(string host, int port, object asyncState = null);
        IZaabeeRedisServer GetServer(string hostAndPort, object asyncState = null);
        IZaabeeRedisServer GetServer(IPAddress host, int port);
        IZaabeeRedisServer GetServer(EndPoint endpoint, object asyncState = null);
    }
}