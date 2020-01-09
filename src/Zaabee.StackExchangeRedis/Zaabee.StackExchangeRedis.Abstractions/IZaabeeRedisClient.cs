using System.Net;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface IZaabeeRedisClient : IKeySync, IStringSync, IListSync, IHashSync, ISetSync,
        ISortedSetSync, IKeyAsync, IStringAsync, IListAsync, IHashAsync, ISetAsync, ISortedSetAsync
    {
        EndPoint[] GetEndPoints(bool configuredOnly = false);
        string GetStatus();
        int GetHashSlot(string key);
        string GetStormLog();
        void ResetStormLog();
        IZaabeeRedisServer GetServer(string host, int port, object asyncState = null);
        IZaabeeRedisServer GetServer(string hostAndPort, object asyncState = null);
        IZaabeeRedisServer GetServer(IPAddress host, int port);
        IZaabeeRedisServer GetServer(EndPoint endpoint, object asyncState = null);
    }
}