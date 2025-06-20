using NodaTime.Extensions;
using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineKeyCollectionExtensions
{
    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that start on or after <paramref name="start"/> from earliest to latest and returns the interval along with the keys of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalsFrom<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start) =>
        source.EnumerateFrom(start).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that start on or after <paramref name="end"/> from latest to earliest and returns the interval along with the keys of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalsBackwardsTo<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant end) =>
        source.EnumerateBackwardsTo(end).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or start after <paramref name="start"/> from earliest to latest and returns the interval along with the keys of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalsFromIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start) =>
        source.EnumerateFromIncludingPartial(start).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or start after <paramref name="end"/> from latest to earliest and returns the interval along with the keys of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalsBackwardsToIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant end) =>
        source.EnumerateBackwardsToIncludingPartial(end).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that end before <paramref name="end"/> from earliest to latest and returns the interval along with the keys of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalsTo<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant end) =>
        source.EnumerateTo(end).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that end before <paramref name="start"/> from latest to earliest and returns the interval along with the keys of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalsBackwardsFrom<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start) =>
        source.EnumerateBackwardsFrom(start).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or end before <paramref name="end"/> from earliest to latest and returns the interval along with the keys of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalsToIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant end) =>
        source.EnumerateToIncludingPartial(end).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or end before <paramref name="start"/> from latest to earliest and returns the interval along with the keys of the timelines that include this exact interval.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalsBackwardsFromIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start) =>
        source.EnumerateBackwardsFromIncludingPartial(start).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from earliest to latest and returns the interval along with the keys of the timelines that include this exact interval.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalRange<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRange(start, end, periodIncludeOptions).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from latest to earliest and returns the interval along with the keys of the timelines that include this exact interval.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalRangeBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRangeBackwards(start, end, periodIncludeOptions).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> within <paramref name="period"/> from earliest to latest and returns the interval along with the keys of the timelines that include this exact interval.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervals<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriod(period, periodIncludeOptions).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> within <paramref name="period"/> from latest to earliest and returns the interval along with the keys of the timelines that include this exact interval.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Interval, TKey[]>> EnumerateIntervalsBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriodBackwards(period, periodIncludeOptions).Select(kvp => new KeyValuePair<Interval, TKey[]>(kvp.Key.ToInterval(), kvp.Value));

}