namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface IZaabeeRedisDatabase : IKeySync, IStringSync, IListSync, IHashSync, ISetSync,
        ISortedSetSync, IKeyAsync, IStringAsync, IListAsync, IHashAsync, ISetAsync, ISortedSetAsync
    {

    }
}