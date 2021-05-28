using System;
using StackExchange.Redis;
using Zaabee.Serializer.Abstractions;
using Zaabee.StackExchangeRedis.Abstractions;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisDatabase : IZaabeeRedisDatabase
    {
        private readonly IDatabase _db;
        private readonly ISerializer _serializer;
        private readonly TimeSpan _defaultExpiry;

        public ZaabeeRedisDatabase(IDatabase db, ISerializer serializer, TimeSpan defaultExpiry)
        {
            _db = db;
            _serializer = serializer;
            _defaultExpiry = defaultExpiry;
        }
    }
}