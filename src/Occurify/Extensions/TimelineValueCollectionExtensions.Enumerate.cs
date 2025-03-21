using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class TimelineValueCollectionExtensions
{
    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from earliest to latest and returns the instant along with the timelines that include this instant and their corresponding value.
    /// </summary>
    public static IEnumerable<TimelineValueCollectionEntry<TValue>> Enumerate<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source) =>
        source.EnumerateFrom(DateTimeHelper.MinValueUtc);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from latest to earliest and returns the instant along with the timelines that include this instant and their corresponding value.
    /// </summary>
    public static IEnumerable<TimelineValueCollectionEntry<TValue>> EnumerateBackwards<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source)
    {
        source = source.ToArray();
        var current = source.GetTimelinesAtCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        while (current.instant != null)
        {
            yield return new TimelineValueCollectionEntry<TValue>(current.instant.Value, current.timelines);
            current = source.GetTimelinesAtPreviousUtcInstant(current.instant.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="utcStart"/> from earliest to latest and returns the instant along with the timelines that include this instant and their corresponding value.
    /// </summary>
    public static IEnumerable<TimelineValueCollectionEntry<TValue>> EnumerateFrom<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetTimelinesAtCurrentOrNextUtcInstant(utcStart);
        while (current.instant != null)
        {
            yield return new TimelineValueCollectionEntry<TValue>(current.instant.Value, current.timelines);
            current = source.GetTimelinesAtNextUtcInstant(current.instant.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="utcEnd"/> from latest to earliest and returns the instant along with the timelines that include this instant and their corresponding value.
    /// </summary>
    public static IEnumerable<TimelineValueCollectionEntry<TValue>> EnumerateBackwardsTo<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(x => x.Instant >= utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="utcEnd"/> from earliest to latest and returns the instant along with the timelines that include this instant and their corresponding value.
    /// </summary>
    public static IEnumerable<TimelineValueCollectionEntry<TValue>> EnumerateTo<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcEnd) =>
        source.Enumerate().TakeWhile(x => x.Instant < utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="utcStart"/> from latest to earliest and returns the instant along with the timelines that include this instant and their corresponding value.
    /// </summary>
    public static IEnumerable<TimelineValueCollectionEntry<TValue>> EnumerateBackwardsFrom<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetTimelinesAtPreviousUtcInstant(utcStart);

        while (current.instant != null)
        {
            yield return new TimelineValueCollectionEntry<TValue>(current.instant.Value, current.timelines);
            current = source.GetTimelinesAtPreviousUtcInstant(current.instant.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest and returns the instant along with the timelines that include this instant and their corresponding value.
    /// </summary>
    public static IEnumerable<TimelineValueCollectionEntry<TValue>> EnumerateRange<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcStart, DateTime utcEnd)
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
        var current = source.GetTimelinesAtCurrentOrNextUtcInstant(utcStart);
        while (current.instant != null && current.instant.Value < utcEnd)
        {
            yield return new TimelineValueCollectionEntry<TValue>(current.instant.Value, current.timelines);
            current = source.GetTimelinesAtNextUtcInstant(current.instant.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest and returns the instant along with the timelines that include this instant and their corresponding value.
    /// </summary>
    public static IEnumerable<TimelineValueCollectionEntry<TValue>> EnumerateRangeBackwards<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime utcStart, DateTime utcEnd)
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
        var current = source.GetTimelinesAtPreviousUtcInstant(utcEnd);
        while (current.instant != null && current.instant.Value >= utcStart)
        {
            yield return new TimelineValueCollectionEntry<TValue>(current.instant.Value, current.timelines);
            current = source.GetTimelinesAtPreviousUtcInstant(current.instant.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="period"/> from earliest to latest and returns the instant along with the timelines that include this instant and their corresponding value.
    /// </summary>
    public static IEnumerable<TimelineValueCollectionEntry<TValue>> EnumeratePeriod<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period period)
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
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="period"/> from latest to earliest and returns the instant along with the timelines that include this instant and their corresponding value.
    /// </summary>
    public static IEnumerable<TimelineValueCollectionEntry<TValue>> EnumeratePeriodBackwards<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period period)
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