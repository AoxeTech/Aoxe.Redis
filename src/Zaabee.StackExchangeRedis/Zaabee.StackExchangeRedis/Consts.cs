namespace Zaabee.StackExchangeRedis;

internal static class Consts
{
    internal static readonly List<Type> RedisValueTypeCodes =
    [
        typeof(bool),
        typeof(byte[]),
        typeof(ReadOnlyMemory<byte>),
        typeof(Memory<byte>),
        typeof(short),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(float),
        typeof(double),
        typeof(string)
    ];
}
