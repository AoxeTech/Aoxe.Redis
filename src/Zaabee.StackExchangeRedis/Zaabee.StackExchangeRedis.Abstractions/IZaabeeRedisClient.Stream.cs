// using System.Collections.Generic;
//
// namespace Zaabee.StackExchangeRedis.Abstractions
// {
//     public partial interface IZaabeeRedisClient
//     {
//         long StreamAcknowledge(string key, string groupName, string messageId);
//
//         long StreamAcknowledge(string key, string groupName, string[] messageIds);
//
//         string StreamAdd<T>(string key, string streamField, T? streamValue, string messageId = null,
//             int? maxLength = null, bool useApproximateMaxLength = false);
//
//         string StreamAdd<T>(string key, (string Field,T? value)[] streamPairs, string messageId = null,
//             int? maxLength = null, bool useApproximateMaxLength = false);
//
//         StreamEntry[] StreamClaim(string key, string consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs,
//             string[] messageIds);
//
//         RedisValue[] StreamClaimIdsOnly(string key, string consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, string[] messageIds);
//
//         bool StreamConsumerGroupSetPosition(string key, string groupName, string messageId);
//
//         StreamConsumerInfo[] StreamConsumerInfo(string key, string groupName);
//
//         bool StreamCreateConsumerGroup(string key, string groupName, string messageId = null, bool createStream = true);
//
//         long StreamDelete(string key, string[] messageIds);
//
//         long StreamDeleteConsumer(string key, string groupName, string consumerName);
//
//         bool StreamDeleteConsumerGroup(string key, string groupName);
//
//         StreamGroupInfo[] StreamGroupInfo(string key);
//
//         StreamInfo StreamInfo(string key);
//
//         long StreamLength(string key);
//
//         StreamPendingInfo StreamPending(string key, string groupName);
//
//         StreamPendingMessageInfo[] StreamPendingMessages(string key, string groupName, int count, string consumerName,
//             string minId = null, string maxId = null);
//
//         Dictionary<string, (string Field,T? value)> StreamRange<T>(string key, string minId = null, string maxId = null,
//             int? count = null);
//
//         Dictionary<string, (string Field,T? value)> StreamRevRange<T>(string key, string minId = null,
//             string maxId = null, int? count = null);
//
//         Dictionary<string, (string Field,T? value)> StreamRead<T>(string key, string messageId, int? count = null);
//
//         RedisStream[] StreamRead((string Key, string MessageId)[] streamPositions, int? countPerStream = null);
//
//         Dictionary<string, (string Field,T? value)> StreamReadGroup<T>(string key, string groupName, string consumerName, string position = null,
//             int? count = null, bool noAck = false);
//
//         RedisStream[] StreamReadGroup((string Key, string MessageId)[] streamPositions, string groupName,
//             string consumerName, int? countPerStream = null, bool noAck = false);
//
//         long StreamTrim(string key, int maxLength, bool useApproximateMaxLength = false);
//     }
// }
