using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="start"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Period> EnumerateFrom(this IPeriodTimeline source, Instant start)
        => source.EnumerateFrom(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="end"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsTo(this IPeriodTimeline source, Instant end) =>
    source.EnumerateBackwardsTo(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="start"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Period> EnumerateFromIncludingPartial(this IPeriodTimeline source, Instant start) =>
        source.EnumerateFromIncludingPartial(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="end"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsToIncludingPartial(this IPeriodTimeline source, Instant end) =>
        source.EnumerateBackwardsToIncludingPartial(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="end"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Period> EnumerateTo(this IPeriodTimeline source, Instant end) =>
        source.EnumerateTo(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="start"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsFrom(this IPeriodTimeline source, Instant start) =>
        source.EnumerateBackwardsFrom(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="end"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Period> EnumerateToIncludingPartial(this IPeriodTimeline source, Instant end) =>
        source.EnumerateToIncludingPartial(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="start"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsFromIncludingPartial(this IPeriodTimeline source, Instant start) =>
        source.EnumerateBackwardsFromIncludingPartial(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// </summary>
    public static IEnumerable<Period> EnumerateRange(this IPeriodTimeline source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRange(start.ToDateTimeUtc(), end.ToDateTimeUtc(), periodIncludeOptions);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// </summary>
    public static IEnumerable<Period> EnumerateRangeBackwards(this IPeriodTimeline source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRangeBackwards(start.ToDateTimeUtc(), end.ToDateTimeUtc(), periodIncludeOptions);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="interval"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="interval"/>.
    /// </summary>
    public static IEnumerable<Period> EnumeratePeriod(this IPeriodTimeline source, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
        => source.EnumeratePeriod(interval.ToPeriod(), periodIncludeOptions);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="interval"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="interval"/>.
    /// </summary>
    public static IEnumerable<Period> EnumeratePeriodBackwards(this IPeriodTimeline source, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
        => source.EnumeratePeriodBackwards(interval.ToPeriod(), periodIncludeOptions);
}