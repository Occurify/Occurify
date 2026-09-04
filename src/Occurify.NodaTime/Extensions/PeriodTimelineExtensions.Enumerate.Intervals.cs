using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervals(this IPeriodTimeline source) =>
        source.Enumerate().Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwards(this IPeriodTimeline source) =>
        source.EnumerateBackwards().Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that start on or after <paramref name="start"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsFrom(this IPeriodTimeline source, Instant start) =>
        source.EnumerateFrom(start).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that start on or after <paramref name="end"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwardsTo(this IPeriodTimeline source, Instant end) =>
        source.EnumerateBackwardsTo(end).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or start after <paramref name="start"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsFromIncludingPartial(this IPeriodTimeline source, Instant start) =>
        source.EnumerateFromIncludingPartial(start).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or start after <paramref name="end"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwardsToIncludingPartial(this IPeriodTimeline source, Instant end) =>
        source.EnumerateBackwardsToIncludingPartial(end).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that end before <paramref name="end"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsTo(this IPeriodTimeline source, Instant end) =>
        source.EnumerateTo(end).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that end before <paramref name="start"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwardsFrom(this IPeriodTimeline source, Instant start) =>
        source.EnumerateBackwardsFrom(start).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or end before <paramref name="end"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsToIncludingPartial(this IPeriodTimeline source, Instant end) =>
        source.EnumerateToIncludingPartial(end).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> that include or end before <paramref name="start"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwardsFromIncludingPartial(this IPeriodTimeline source, Instant start) =>
        source.EnumerateBackwardsFromIncludingPartial(start).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of intervals around <paramref name="start"/> or <paramref name="end"/>.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsRange(this IPeriodTimeline source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRange(start, end, periodIncludeOptions).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of intervals around <paramref name="start"/> or <paramref name="end"/>.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsRangeBackwards(this IPeriodTimeline source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRangeBackwards(start, end, periodIncludeOptions).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> within <paramref name="interval"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of intervals around the start and end of <paramref name="interval"/>.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervals(this IPeriodTimeline source, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriod(interval, periodIncludeOptions).Select(p => p.ToInterval());

    /// <summary>
    /// Enumerates all intervals on <paramref name="source"/> within <paramref name="interval"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of intervals around the start and end of <paramref name="interval"/>.
    /// </summary>
    public static IEnumerable<Interval> EnumerateIntervalsBackwards(this IPeriodTimeline source, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriodBackwards(interval, periodIncludeOptions).Select(p => p.ToInterval());
}
