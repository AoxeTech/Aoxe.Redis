using System;
using StackExchange.Redis;
using Zaabee.StackExchangeRedis.Abstractions;

namespace Zaabee.StackExchangeRedis.TestProject
{
    public class ZaabeeRedisClientFactory
    {
        private static readonly IZaabeeRedisClient Client =
            new ZaabeeRedisClient(
                ConfigurationOptions.Parse(
                    "192.168.78.140:6379,192.168.78.141:6379,192.168.78.142:6379,192.168.78.140:7379,192.168.78.141:7379,192.168.78.142:7379,abortConnect=false,syncTimeout=3000"),
                TimeSpan.FromMinutes(10),
                new Protobuf.Serializer());

        public static IZaabeeRedisClient GetClient() => Client;
    }
}