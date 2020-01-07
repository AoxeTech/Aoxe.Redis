using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface ISetAsync
    {
        Task<bool> SetAddAsync<T>(string key, T value);

        Task<long> SetAddRangeAsync<T>(string key, IEnumerable<T> values);

        Task<IList<T>> SetCombineUnionAsync<T>(string firstKey, string secondKey);

        Task<IList<T>> SetCombineUnionAsync<T>(IEnumerable<string> keys);

        Task<IList<T>> SetCombineIntersectAsync<T>(string firstKey, string secondKey);

        Task<IList<T>> SetCombineIntersectAsync<T>(IEnumerable<string> keys);

        Task<IList<T>> SetCombineDifferenceAsync<T>(string firstKey, string secondKey);

        Task<IList<T>> SetCombineDifferenceAsync<T>(IEnumerable<string> keys);

        Task<long> SetCombineAndStoreUnionAsync<T>(string destination, string firstKey, string secondKey);

        Task<long> SetCombineAndStoreUnionAsync<T>(string destination, IEnumerable<string> keys);

        Task<long> SetCombineAndStoreIntersectAsync<T>(string destination, string firstKey, string secondKey);

        Task<long> SetCombineAndStoreIntersectAsync<T>(string destination, IEnumerable<string> keys);

        Task<long> SetCombineAndStoreDifferenceAsync<T>(string destination, string firstKey, string secondKey);

        Task<long> SetCombineAndStoreDifferenceAsync<T>(string destination, IEnumerable<string> keys);

        Task<bool> SetContainsAsync<T>(string key, T value);

        Task<long> SetLengthAsync<T>(string key);

        Task<IList<T>> SetMembersAsync<T>(string key);

        Task<bool> SetMoveAsync<T>(string source, string destination, T value);

        Task<T> SetPopAsync<T>(string key);

        Task<IList<T>> SetPopAsync<T>(string key, long count);

        Task<T> SetRandomMemberAsync<T>(string key);

        Task<IList<T>> SetRandomMembersAsync<T>(string key, long count);

        Task<bool> SetRemoveAsync<T>(string key, T value);

        Task<long> SetRemoveRangeAsync<T>(string key, IEnumerable<T> values);

        Task<IList<T>> SetScanAsync<T>(string key, T pattern = default, int pageSize = 10,
            long cursor = 0, int pageOffset = 0);
    }
}