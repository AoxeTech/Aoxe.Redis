// using System.Collections.Generic;
// using System.Threading.Tasks;
//
// namespace Zaabee.StackExchangeRedis.Abstractions
// {
//     public partial interface IZaabeeRedisDatabase
//     {
//         Task<long> StreamAcknowledgeAsync(string key, string groupName, string messageId);
//
//         Task<long> StreamAcknowledgeAsync(string key, string groupName, string[] messageIds);
//
//         Task<string> StreamAddAsync<T>(string key, string streamField, T? streamValue,
//             string messageId = null, int? maxLength = null, bool useApproximateMaxLength = false);
//
//         Task<string> StreamAddAsync<T>(string key, (string Field,T? value)[] streamPairs, string messageId = null,
//             int? maxLength = null, bool useApproximateMaxLength = false);
//
//         Task<StreamEntry[]> StreamClaimAsync(string key, string consumerGroup, RedisValue claimingConsumer,
//             long minIdleTimeInMs, string[] messageIds);
//
//         Task<RedisValue[]> StreamClaimIdsOnlyAsync(string key, string consumerGroup, RedisValue claimingConsumer,
//             long minIdleTimeInMs, string[] messageIds);
//
//         Task<bool> StreamConsumerGroupSetPositionAsync(string key, string groupName, string messageId);
//
//         Task<StreamConsumerInfo[]> StreamConsumerInfoAsync(string key, string groupName);
//
//         Task<bool> StreamCreateConsumerGroupAsync(string key, string groupName, string messageId = null, bool createStream = true);
//
//         Task<long> StreamDeleteAsync(string key, string[] messageIds);
//
//         Task<long> StreamDeleteConsumerAsync(string key, string groupName, string consumerName);
//
//         Task<bool> StreamDeleteConsumerGroupAsync(string key, string groupName);
//
//         Task<StreamGroupInfo[]> StreamGroupInfoAsync(string key);
//
//         Task<StreamInfo> StreamInfoAsync(string key);
//
//         Task<long> StreamLengthAsync(string key);
//
//         Task<StreamPendingInfo> StreamPendingAsync(string key, string groupName);
//
//         Task<StreamPendingMessageInfo[]> StreamPendingMessagesAsync(string key, string groupName, int count,
//             string consumerName, string minId = null, string maxId = null);
//
//         Task<Dictionary<string, (string Field,T? value)>> StreamRangeAsync<T>(string key, string minId = null,
//             string maxId = null, int? count = null);
//
//         Task<Dictionary<string, (string Field,T? value)>> StreamRevRangeAsync<T>(string key, string minId = null,
//             string maxId = null, int? count = null);
//
//         Task<Dictionary<string, (string Field,T? value)>> StreamReadAsync<T>(string key, string messageId, int? count = null);
//
//         Task<RedisStream[]> StreamReadAsync((string Key, string MessageId)[] streamPositions,
//             int? countPerStream = null);
//
//         Task<(Dictionary<string, (string Field,T? value)>> StreamReadGroupAsync<T>(string key, string groupName, string consumerName,
//             string position = null, int? count = null, bool noAck = false);
//
//         Task<RedisStream[]> StreamReadGroupAsync((string Key, string MessageId)[] streamPositions, string groupName,
//             string consumerName, int? countPerStream = null, bool noAck = false);
//
//         Task<long> StreamTrimAsync(string key, int maxLength, bool useApproximateMaxLength = false);
//     }
// }