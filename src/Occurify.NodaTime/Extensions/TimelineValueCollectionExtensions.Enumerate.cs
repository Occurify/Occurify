using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class TimelineValueCollectionExtensions
{
    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from earliest to latest and returns the instant along with the values of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TValue[]>> Enumerate<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source) =>
        source.EnumerateFrom(DateTimeHelper.MinValueUtc);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from latest to earliest and returns the instant along with the values of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TValue[]>> EnumerateBackwards<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source)
    {
        source = source.ToArray();
        var current = source.GetValuesAtCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        while (current.Key != null)
        {
            yield return new KeyValuePair<DateTime, TValue[]>(current.Key.Value, current.Value);
            current = source.GetValuesAtPreviousUtcInstant(current.Key.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="utcStart"/> from earliest to latest and returns the instant along with the values of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TValue[]>> EnumerateFrom<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetValuesAtCurrentOrNextUtcInstant(utcStart);
        while (current.Key != null)
        {
            yield return new KeyValuePair<DateTime, TValue[]>(current.Key.Value, current.Value);
            current = source.GetValuesAtNextUtcInstant(current.Key.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="utcEnd"/> from latest to earliest and returns the instant along with the values of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TValue[]>> EnumerateBackwardsTo<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(x => x.Key >= utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="utcEnd"/> from earliest to latest and returns the instant along with the values of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TValue[]>> EnumerateTo<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcEnd) =>
        source.Enumerate().TakeWhile(x => x.Key < utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="utcStart"/> from latest to earliest and returns the instant along with the values of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TValue[]>> EnumerateBackwardsFrom<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetValuesAtPreviousUtcInstant(utcStart);

        while (current.Key != null)
        {
            yield return new KeyValuePair<DateTime, TValue[]>(current.Key.Value, current.Value);
            current = source.GetValuesAtPreviousUtcInstant(current.Key.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest and returns the instant along with the values of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TValue[]>> EnumerateRange<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcStart, DateTime utcEnd)
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
        var current = source.GetValuesAtCurrentOrNextUtcInstant(utcStart);
        while (current.Key != null && current.Key.Value < utcEnd)
        {
            yield return new KeyValuePair<DateTime, TValue[]>(current.Key.Value, current.Value);
            current = source.GetValuesAtNextUtcInstant(current.Key.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest and returns the instant along with the values of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TValue[]>> EnumerateRangeBackwards<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcStart, DateTime utcEnd)
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
        var current = source.GetValuesAtPreviousUtcInstant(utcEnd);
        while (current.Key != null && current.Key.Value >= utcStart)
        {
            yield return new KeyValuePair<DateTime, TValue[]>(current.Key.Value, current.Value);
            current = source.GetValuesAtPreviousUtcInstant(current.Key.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="period"/> from earliest to latest and returns the instant along with the values of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TValue[]>> EnumeratePeriod<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period period)
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
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="period"/> from latest to earliest and returns the instant along with the values of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<DateTime, TValue[]>> EnumeratePeriodBackwards<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period period)
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