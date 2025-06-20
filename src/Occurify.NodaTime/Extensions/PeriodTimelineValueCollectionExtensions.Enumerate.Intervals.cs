using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineValueCollectionExtensions
{
    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that start on or after <paramref name="start"/> from earliest to latest and returns the interval along with the values of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalsFrom<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start) =>
        source.EnumerateFrom(start).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that start on or after <paramref name="end"/> from latest to earliest and returns the interval along with the values of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalsBackwardsTo<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant end) =>
        source.EnumerateBackwardsTo(end).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or start after <paramref name="start"/> from earliest to latest and returns the interval along with the values of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalsFromIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start) =>
        source.EnumerateFromIncludingPartial(start).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or start after <paramref name="end"/> from latest to earliest and returns the interval along with the values of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalsBackwardsToIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant end) =>
        source.EnumerateBackwardsToIncludingPartial(end).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that end before <paramref name="end"/> from earliest to latest and returns the interval along with the values of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalsTo<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant end) =>
        source.EnumerateTo(end).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that end before <paramref name="start"/> from latest to earliest and returns the interval along with the values of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalsBackwardsFrom<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start) =>
        source.EnumerateBackwardsFrom(start).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or end before <paramref name="end"/> from earliest to latest and returns the interval along with the values of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalsToIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant end) =>
        source.EnumerateToIncludingPartial(end).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or end before <paramref name="start"/> from latest to earliest and returns the interval along with the values of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalsBackwardsFromIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start) =>
        source.EnumerateBackwardsFromIncludingPartial(start).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from earliest to latest and returns the interval along with the values of the timelines that include this exact interval.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalRange<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRange(start, end, periodIncludeOptions).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from latest to earliest and returns the interval along with the values of the timelines that include this exact interval.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalRangeBackwards<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRangeBackwards(start, end, periodIncludeOptions).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> within <paramref name="period"/> from earliest to latest and returns the interval along with the values of the timelines that include this exact interval.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervals<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriod(period, periodIncludeOptions).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> within <paramref name="period"/> from latest to earliest and returns the interval along with the values of the timelines that include this exact interval.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TValue[]>> EnumerateIntervalsBackwards<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriodBackwards(period, periodIncludeOptions).Select(kvp => new KeyValuePair<Interval, TValue[]>(kvp.Key.ToInterval(), kvp.Value));
}