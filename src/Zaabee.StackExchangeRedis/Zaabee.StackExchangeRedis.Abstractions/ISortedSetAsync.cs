using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface ISortedSetAsync
    {
        Task<bool> SortedSetAddAsync<T>(string key, T member, double score);

        Task<long> SortedSetAddAsync<T>(string key, IDictionary<T, double> values);

        Task<double> SortedSetDecrementAsync<T>(string key, T member, double value);

        Task<double> SortedSetIncrementAsync<T>(string key, T member, double value);

        Task<long> SortedSetLengthAsync<T>(string key);

        Task<long> SortedSetLengthByValueAsync(string key, int min, int max);

        Task<long> SortedSetLengthByValueAsync(string key, long min, long max);

        Task<long> SortedSetLengthByValueAsync(string key, float min, float max);

        Task<long> SortedSetLengthByValueAsync(string key, double min, double max);

        Task<long> SortedSetLengthByValueAsync(string key, string min, string max);

        Task<IList<T>> SortedSetRangeByScoreAscendingAsync<T>(string key, double start = 0, double stop = -1);

        Task<IList<T>> SortedSetRangeByScoreDescendingAsync<T>(string key, double start = 0, double stop = -1);

        Task<IDictionary<T, double>> SortedSetRangeByScoreWithScoresAscendingAsync<T>(string key, long start = 0,
            long stop = -1);

        Task<IDictionary<T, double>> SortedSetRangeByScoreWithScoresDescendingAsync<T>(string key, double start = 0,
            double stop = -1);

        Task<IList<int>> SortedSetRangeByValueAsync(string key, int min, int max, long skip, long take = -1);

        Task<IList<long>> SortedSetRangeByValueAsync(string key, long min, long max, long skip, long take = -1);

        Task<IList<float>> SortedSetRangeByValueAsync(string key, float min, float max, long skip, long take = -1);

        Task<IList<double>> SortedSetRangeByValueAsync(string key, double min, double max, long skip, long take = -1);

        Task<IList<string>> SortedSetRangeByValueAsync(string key, string min, string max, long skip, long take = -1);

        Task<IList<int>> SortedSetRangeByValueAscendingAsync(string key, int min = default, int max = default,
            long skip = 0, long take = -1);

        Task<IList<long>> SortedSetRangeByValueAscendingAsync(string key, long min = default, long max = default,
            long skip = 0, long take = -1);

        Task<IList<float>> SortedSetRangeByValueAscendingAsync(string key, float min = default, float max = default,
            long skip = 0, long take = -1);

        Task<IList<double>> SortedSetRangeByValueAscendingAsync(string key, double min = default, double max = default,
            long skip = 0, long take = -1);

        Task<IList<string>> SortedSetRangeByValueAscendingAsync(string key, string min = default, string max = default,
            long skip = 0, long take = -1);

        Task<IList<int>> SortedSetRangeByValueDescendingAsync(string key, int min = default, int max = default,
            long skip = 0, long take = -1);

        Task<IList<long>> SortedSetRangeByValueDescendingAsync(string key, long min = default, long max = default,
            long skip = 0, long take = -1);

        Task<IList<float>> SortedSetRangeByValueDescendingAsync(string key, float min = default, float max = default,
            long skip = 0, long take = -1);

        Task<IList<double>> SortedSetRangeByValueDescendingAsync(string key, double min = default, double max = default,
            long skip = 0, long take = -1);

        Task<IList<string>> SortedSetRangeByValueDescendingAsync(string key, string min = default, string max = default,
            long skip = 0, long take = -1);

        Task<bool> SortedSetRemoveAsync<T>(string key, T member);

        Task<long> SortedSetRemoveRangeAsync<T>(string key, IEnumerable<T> members);

        Task<long> SortedSetRemoveRangeByScoreAsync<T>(string key, double start, double stop);

        Task<long> SortedSetRemoveRangeByValueAsync(string key, int min, int max);

        Task<long> SortedSetRemoveRangeByValueAsync(string key, long min, long max);

        Task<long> SortedSetRemoveRangeByValueAsync(string key, float min, float max);

        Task<long> SortedSetRemoveRangeByValueAsync(string key, double min, double max);

        Task<long> SortedSetRemoveRangeByValueAsync(string key, string min, string max);

        Task<IDictionary<T, double>> SortedSetScanAsync<T>(string key, T pattern = default, int pageSize = 10,
            long cursor = 0, int pageOffset = 0);

        Task<double?> SortedSetScoreAsync<T>(string key, T member);
    }
}