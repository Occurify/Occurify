using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineCollectionExtensions
{
    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that start on or after <paramref name="start"/> from earliest to latest.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsFrom(this IEnumerable<IPeriodTimeline> source, Instant start) =>
        source.EnumerateFrom(start).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that start on or after <paramref name="end"/> from latest to earliest.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwardsTo(this IEnumerable<IPeriodTimeline> source, Instant end) =>
        source.EnumerateBackwardsTo(end).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or start after <paramref name="start"/> from earliest to latest.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsFromIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant start) =>
        source.EnumerateFromIncludingPartial(start).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or start after <paramref name="end"/> from latest to earliest.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwardsToIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant end) =>
        source.EnumerateBackwardsToIncludingPartial(end).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that end before <paramref name="end"/> from earliest to latest.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsTo(this IEnumerable<IPeriodTimeline> source, Instant end) =>
        source.EnumerateTo(end).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that end before <paramref name="start"/> from latest to earliest.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwardsFrom(this IEnumerable<IPeriodTimeline> source, Instant start) =>
        source.EnumerateBackwardsFrom(start).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or end before <paramref name="end"/> from earliest to latest.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsToIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant end) =>
        source.EnumerateToIncludingPartial(end).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or end before <paramref name="start"/> from latest to earliest.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwardsFromIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant start) =>
        source.EnumerateBackwardsFromIncludingPartial(start).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalRange(this IEnumerable<IPeriodTimeline> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRange(start, end, periodIncludeOptions).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalRangeBackwards(this IEnumerable<IPeriodTimeline> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRangeBackwards(start, end, periodIncludeOptions).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> within <paramref name="interval"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="interval"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervals(this IEnumerable<IPeriodTimeline> source, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateInterval(interval, periodIncludeOptions).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> within <paramref name="interval"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="interval"/>.
    /// Intervals are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwards(this IEnumerable<IPeriodTimeline> source, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriodBackwards(interval, periodIncludeOptions).Select(p => p.ToInterval());

}