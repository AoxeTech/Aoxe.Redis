using System;
using System.Collections.Generic;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface ISortedSetSync
    {
        bool SortedSetAdd<T>(string key, T member, long score);

        long SortedSetAdd<T>(string key, IDictionary<T,double> values);

        double SortedSetDecrement<T>(string key, T member, long value);

        double SortedSetIncrement<T>(string key, T member, long value);

        long SortedSetLength<T>(string key);

        long SortedSetLengthByValue<T>(string key, T min, T max);

        IList<T> SortedSetRangeByScoreAscending<T>(string key, long start = 0, long stop = -1);

        IList<T> SortedSetRangeByScoreDescending<T>(string key, long start = 0, long stop = -1);

        IDictionary<T,double> SortedSetRangeByScoreWithScoresAscending<T>(string key, long start = 0, long stop = -1);

        IDictionary<T,double> SortedSetRangeByScoreWithScoresDescending<T>(string key, long start = 0, long stop = -1);

        IList<T> SortedSetRangeByValue<T>(string key, T min, T max, long skip, long take = -1);

        IList<T> SortedSetRangeByValueAscending<T>(string key, T min = default, T max = default, long skip = 0,
            long take = -1);

        IList<T> SortedSetRangeByValueDescending<T>(string key, T min = default, T max = default, long skip = 0,
            long take = -1);

        bool SortedSetRemove<T>(string key, T member);

        long SortedSetRemoveRange<T>(string key, IEnumerable<T> members);

        long SortedSetRemoveRangeByScore<T>(string key, long start, long stop);

        long SortedSetRemoveRangeByValue<T>(string key, T min, T max);

        IDictionary<T,double> SortedSetScan<T>(string key, T pattern = default, int pageSize = 10,
            long cursor = 0, int pageOffset = 0);

        double? SortedSetScore<T>(string key, T member);
    }
}