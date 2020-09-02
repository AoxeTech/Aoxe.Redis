using System;
using System.Net;
using System.Threading.Tasks;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface IZaabeeRedisServer
    {
        /// <summary>Gets the address of the connected server</summary>
        EndPoint EndPoint { get; }

        /// <summary>
        /// Gets whether the connection to the server is active and usable
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gets whether the connected server is a replica / slave
        /// </summary>
        bool IsReplica { get; }

        /// <summary>Explicitly opt in for slave writes on writable slaves</summary>
        bool AllowReplicaWrites { get; }

        /// <summary>Gets the version of the connected server</summary>
        Version Version { get; }

        /// <summary>The number of databases supported on this server</summary>
        int DatabaseCount { get; }

        /// <summary>Delete all the keys of all databases on the server.</summary>
        /// <remarks>https://redis.io/commands/flushall</remarks>
        void FlushAllDatabases();

        /// <summary>Delete all the keys of all databases on the server.</summary>
        /// <remarks>https://redis.io/commands/flushall</remarks>
        Task FlushAllDatabasesAsync();

        /// <summary>Delete all the keys of the database.</summary>
        /// <param name="database">The database ID.</param>
        /// <remarks>https://redis.io/commands/flushdb</remarks>
        void FlushDatabase(int database = 0);

        /// <summary>Delete all the keys of the database.</summary>
        /// <param name="database">The database ID.</param>
        /// <remarks>https://redis.io/commands/flushdb</remarks>
        Task FlushDatabaseAsync(int database = 0);
    }
}