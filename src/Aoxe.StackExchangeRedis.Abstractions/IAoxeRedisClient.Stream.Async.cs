// using System.Collections.Generic;
// using System.Threading.Tasks;
//
// namespace Aoxe.StackExchangeRedis.Abstractions
// {
//     public partial interface IAoxeRedisClient
//     {
//         ValueTask<long> StreamAcknowledgeAsync(string key, string groupName, string messageId);
//
//         ValueTask<long> StreamAcknowledgeAsync(string key, string groupName, string[] messageIds);
//
//         ValueTask<string> StreamAddAsync<T>(string key, string streamField, T? streamValue,
//             string messageId = null, int? maxLength = null, bool useApproximateMaxLength = false);
//
//         ValueTask<string> StreamAddAsync<T>(string key, (string Field,T? value)[] streamPairs, string messageId = null,
//             int? maxLength = null, bool useApproximateMaxLength = false);
//
//         ValueTask<StreamEntry[]> StreamClaimAsync(string key, string consumerGroup, RedisValue claimingConsumer,
//             long minIdleTimeInMs, string[] messageIds);
//
//         ValueTask<RedisValue[]> StreamClaimIdsOnlyAsync(string key, string consumerGroup, RedisValue claimingConsumer,
//             long minIdleTimeInMs, string[] messageIds);
//
//         ValueTask<bool> StreamConsumerGroupSetPositionAsync(string key, string groupName, string messageId);
//
//         ValueTask<StreamConsumerInfo[]> StreamConsumerInfoAsync(string key, string groupName);
//
//         ValueTask<bool> StreamCreateConsumerGroupAsync(string key, string groupName, string messageId = null, bool createStream = true);
//
//         ValueTask<long> StreamDeleteAsync(string key, string[] messageIds);
//
//         ValueTask<long> StreamDeleteConsumerAsync(string key, string groupName, string consumerName);
//
//         ValueTask<bool> StreamDeleteConsumerGroupAsync(string key, string groupName);
//
//         ValueTask<StreamGroupInfo[]> StreamGroupInfoAsync(string key);
//
//         ValueTask<StreamInfo> StreamInfoAsync(string key);
//
//         ValueTask<long> StreamLengthAsync(string key);
//
//         ValueTask<StreamPendingInfo> StreamPendingAsync(string key, string groupName);
//
//         ValueTask<StreamPendingMessageInfo[]> StreamPendingMessagesAsync(string key, string groupName, int count,
//             string consumerName, string minId = null, string maxId = null);
//
//         ValueTask<Dictionary<string, (string Field,T? value)>> StreamRangeAsync<T>(string key, string minId = null,
//             string maxId = null, int? count = null);
//
//         ValueTask<Dictionary<string, (string Field,T? value)>> StreamRevRangeAsync<T>(string key, string minId = null,
//             string maxId = null, int? count = null);
//
//         ValueTask<Dictionary<string, (string Field,T? value)>> StreamReadAsync<T>(string key, string messageId, int? count = null);
//
//         ValueTask<RedisStream[]> StreamReadAsync((string Key, string MessageId)[] streamPositions,
//             int? countPerStream = null);
//
//         ValueTask<(Dictionary<string, (string Field,T? value)>> StreamReadGroupAsync<T>(string key, string groupName, string consumerName,
//             string position = null, int? count = null, bool noAck = false);
//
//         ValueTask<RedisStream[]> StreamReadGroupAsync((string Key, string MessageId)[] streamPositions, string groupName,
//             string consumerName, int? countPerStream = null, bool noAck = false);
//
//         ValueTask<long> StreamTrimAsync(string key, int maxLength, bool useApproximateMaxLength = false);
//     }
// }
