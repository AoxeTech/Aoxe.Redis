using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface ISortedSetAsync
    {
        Task<bool> SortedSetAddAsync<T>(string key, T member, double score);

        Task<long> SortedSetAddAsync<T>(string key, IDictionary<T,double> values);

        Task<double> SortedSetDecrementAsync<T>(string key, T member, double value);

        Task<double> SortedSetIncrementAsync<T>(string key, T member, double value);

        Task<long> SortedSetLengthAsync<T>(string key);

        Task<long> SortedSetLengthByValueAsync<T>(string key, T min, T max);

        Task<IList<T>> SortedSetRangeByScoreAscendingAsync<T>(string key, double start = 0, double stop = -1);

        Task<IList<T>> SortedSetRangeByScoreDescendingAsync<T>(string key, double start = 0, double stop = -1);

        Task<IDictionary<T,double>> SortedSetRangeByScoreWithScoresAscendingAsync<T>(string key, long start = 0,
            long stop = -1);

        Task<IDictionary<T,double>> SortedSetRangeByScoreWithScoresDescendingAsync<T>(string key, double start = 0,
            double stop = -1);

        Task<IList<T>> SortedSetRangeByValueAsync<T>(string key, T min, T max, long skip, long take = -1);

        Task<IList<T>> SortedSetRangeByValueAscendingAsync<T>(string key, T min = default, T max = default,
            long skip = 0,
            long take = -1);

        Task<IList<T>> SortedSetRangeByValueDescendingAsync<T>(string key, T min = default, T max = default,
            long skip = 0,
            long take = -1);

        Task<bool> SortedSetRemoveAsync<T>(string key, T member);

        Task<long> SortedSetRemoveRangeAsync<T>(string key, IEnumerable<T> members);

        Task<long> SortedSetRemoveRangeByScoreAsync<T>(string key, double start, double stop);

        Task<long> SortedSetRemoveRangeByValueAsync<T>(string key, T min, T max);

        Task<IDictionary<T,double>> SortedSetScanAsync<T>(string key, T pattern = default, int pageSize = 10,
            long cursor = 0, int pageOffset = 0);

        Task<double?> SortedSetScoreAsync<T>(string key, T member);
    }
}