using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class TimelineKeyCollectionExtensions
{
    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from earliest to latest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TKey[]>> Enumerate<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source) =>
        source.EnumerateFrom(DateTimeHelper.MinValueUtc);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from latest to earliest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TKey[]>> EnumerateBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source)
    {
        source = source.ToArray();
        var current = source.GetKeysAtCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        while (current.Key != null)
        {
            yield return new KeyValuePair<DateTime, TKey[]>(current.Key.Value, current.Value);
            current = source.GetKeysAtPreviousUtcInstant(current.Key.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="utcStart"/> from earliest to latest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TKey[]>> EnumerateFrom<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetKeysAtCurrentOrNextUtcInstant(utcStart);
        while (current.Key != null)
        {
            yield return new KeyValuePair<DateTime, TKey[]>(current.Key.Value, current.Value);
            current = source.GetKeysAtNextUtcInstant(current.Key.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="utcEnd"/> from latest to earliest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TKey[]>> EnumerateBackwardsTo<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(x => x.Key >= utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="utcEnd"/> from earliest to latest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TKey[]>> EnumerateTo<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime utcEnd) =>
        source.Enumerate().TakeWhile(x => x.Key < utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="utcStart"/> from latest to earliest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TKey[]>> EnumerateBackwardsFrom<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetKeysAtPreviousUtcInstant(utcStart);

        while (current.Key != null)
        {
            yield return new KeyValuePair<DateTime, TKey[]>(current.Key.Value, current.Value);
            current = source.GetKeysAtPreviousUtcInstant(current.Key.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TKey[]>> EnumerateRange<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime utcStart, DateTime utcEnd)
    {
        if (utcStart == utcEnd)
        {
            yield break;
        }

        if (utcStart > utcEnd)
        {
            (utcEnd, utcStart) = (utcStart, utcEnd);
        }

        source = source.ToArray();
        var current = source.GetKeysAtCurrentOrNextUtcInstant(utcStart);
        while (current.Key != null && current.Key.Value < utcEnd)
        {
            yield return new KeyValuePair<DateTime, TKey[]>(current.Key.Value, current.Value);
            current = source.GetKeysAtNextUtcInstant(current.Key.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TKey[]>> EnumerateRangeBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime utcStart, DateTime utcEnd)
    {
        if (utcStart == utcEnd)
        {
            yield break;
        }

        if (utcStart > utcEnd)
        {
            (utcEnd, utcStart) = (utcStart, utcEnd);
        }

        source = source.ToArray();
        var current = source.GetKeysAtPreviousUtcInstant(utcEnd);
        while (current.Key != null && current.Key.Value >= utcStart)
        {
            yield return new KeyValuePair<DateTime, TKey[]>(current.Key.Value, current.Value);
            current = source.GetKeysAtPreviousUtcInstant(current.Key.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="period"/> from earliest to latest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TKey[]>> EnumeratePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Period period)
    {
        if (period.Start != null && period.End != null)
        {
            return source.EnumerateRange(period.Start.Value, period.End.Value);
        }

        if (period.End != null)
        {
            return source.EnumerateTo(period.End.Value);
        }

        if (period.Start != null)
        {
            return source.EnumerateFrom(period.Start.Value);
        }

        return source.Enumerate();
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="period"/> from latest to earliest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TKey[]>> EnumeratePeriodBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Period period)
    {
        if (period.Start != null && period.End != null)
        {
            return source.EnumerateRangeBackwards(period.Start.Value, period.End.Value);
        }

        if (period.End != null)
        {
            return source.EnumerateBackwardsFrom(period.End.Value);
        }

        if (period.Start != null)
        {
            return source.EnumerateBackwardsTo(period.Start.Value);
        }

        return source.EnumerateBackwards();
    }
}