using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

internal static class KeyValuePairExtensions
{
    internal static KeyValuePair<Instant, TValue> ToInstantKey<TValue>(this KeyValuePair<DateTime, TValue> source) =>
        new(source.Key.ToInstant(), source.Value);

    internal static KeyValuePair<Instant?, TValue> ToInstantKey<TValue>(this KeyValuePair<DateTime?, TValue> source) =>
        new(source.Key.ToInstant(), source.Value);

    internal static KeyValuePair<Interval?, TValue> ToIntervalKey<TValue>(this KeyValuePair<Period?, TValue> source) =>
        new(source.Key?.ToInterval(), source.Value);
}
