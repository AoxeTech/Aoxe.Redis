namespace Zaabee.StackExchangeRedis;

public static class Consts
{
    internal static List<Type> RedisValueTypes =
        new() { typeof(int), typeof(long), typeof(double), typeof(bool) };
}
