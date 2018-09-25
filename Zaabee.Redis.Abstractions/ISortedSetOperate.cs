using System;
using System.Collections.Generic;

namespace Zaabee.Redis.Abstractions
{
    public interface ISortedSetOperate
    {
        bool SortedSetAdd<T>(string key, T member, long score);

        long SortedSetAdd<T>(string key, List<Tuple<T, long>> values);

        double SortedSetDecrement<T>(string key, T member, long value);

        double SortedSetIncrement<T>(string key, T member, long value);

        long SortedSetLength<T>(string key);

        long SortedSetLengthByValue<T>(string key, T min, T max);

        List<T> SortedSetRangeByScoreAscending<T>(string key, long start = 0, long stop = -1);

        List<T> SortedSetRangeByScoreDescending<T>(string key, long start = 0, long stop = -1);

        List<Tuple<T, double>> SortedSetRangeByScoreWithScoresAscending<T>(string key, long start = 0, long stop = -1);

        List<Tuple<T, double>> SortedSetRangeByScoreWithScoresDescending<T>(string key, long start = 0, long stop = -1);

        List<T> SortedSetRangeByValue<T>(string key, T min, T max, long skip, long take = -1);

        List<T> SortedSetRangeByValueAscending<T>(string key, T min = default(T), T max = default(T), long skip = 0,
            long take = -1);

        List<T> SortedSetRangeByValueDescending<T>(string key, T min = default(T), T max = default(T), long skip = 0,
            long take = -1);

        bool SortedSetRemove<T>(string key, T member);

        long SortedSetRemove<T>(string key, List<T> members);

        long SortedSetRemoveRangeByScore<T>(string key, long start, long stop);

        long SortedSetRemoveRangeByValue<T>(string key, T min, T max);

        List<Tuple<T, double>> SortedSetScan<T>(string key, T pattern = default(T), int pageSize = 10,
            long cursor = 0, int pageOffset = 0);

        double? SortedSetScore<T>(string key, T member);
    }
}