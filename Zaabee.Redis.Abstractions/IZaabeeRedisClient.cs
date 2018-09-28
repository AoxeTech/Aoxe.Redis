namespace Zaabee.Redis.Abstractions
{
    public interface IZaabeeRedisClient : IKeySync, IStringSync, IListSync, IHashSync, ISetSync,
        ISortedSetSync, IKeyAsync, IStringAsync, IListAsync, IHashAsync, ISetAsync, ISortedSetAsync
    {

    }
}