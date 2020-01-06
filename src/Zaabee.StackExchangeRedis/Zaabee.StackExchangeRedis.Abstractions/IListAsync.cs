using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface IListAsync
    {
        Task<T> ListGetByIndexAsync<T>(string key, long index);

        Task<long> ListInsertAfterAsync<T>(string key, T pivot, T value);

        Task<long> ListInsertBeforeAsync<T>(string key, T pivot, T value);

        Task<T> ListLeftPopAsync<T>(string key);

        Task<long> ListLeftPushAsync<T>(string key, T value);

        Task<long> ListLeftPushRangeAsync<T>(string key, IEnumerable<T> values);

        Task<long> ListLengthAsync(string key);

        Task<IList<T>> ListRangeAsync<T>(string key, long start = 0, long stop = -1);

        Task<long> ListRemoveAsync<T>(string key, T value, long count = 0);

        Task<T> ListRightPopAsync<T>(string key);

        Task<T> ListRightPopLeftPushAsync<T>(string source, string destination);

        Task<long> ListRightPushAsync<T>(string key, T value);

        Task<long> ListRightPushRangeAsync<T>(string key, IEnumerable<T> values);

        Task ListSetByIndexAsync<T>(string key, long index, T value);

        Task ListTrimAsync(string key, long start, long stop);
    }
}