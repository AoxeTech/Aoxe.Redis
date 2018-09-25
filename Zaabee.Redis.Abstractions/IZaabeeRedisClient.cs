namespace Zaabee.Redis.Abstractions
{
    public interface IZaabeeRedisClient : IKeyOperate, IStringOperate, IListOperate, IHashOperate, ISetOperate,
        ISortedSetOperate
    {

    }
}