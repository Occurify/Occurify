using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineCollectionExtensions
{
    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="start"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateFrom(this IEnumerable<IPeriodTimeline> source, Instant start) => source.EnumerateFrom(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="end"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsTo(this IEnumerable<IPeriodTimeline> source, Instant end) =>
        source.EnumerateBackwardsTo(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="start"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateFromIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant start)
        => source.EnumerateFromIncludingPartial(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="end"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsToIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant end) =>
        source.EnumerateBackwardsToIncludingPartial(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="end"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateTo(this IEnumerable<IPeriodTimeline> source, Instant end) =>
        source.EnumerateTo(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="start"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsFrom(this IEnumerable<IPeriodTimeline> source, Instant start)
        => source.EnumerateBackwardsFrom(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="end"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateToIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant end) =>
        source.EnumerateToIncludingPartial(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="start"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsFromIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant start)
        => source.EnumerateBackwardsFromIncludingPartial(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateRange(this IEnumerable<IPeriodTimeline> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
        => source.EnumerateRange(start.ToDateTimeUtc(), end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateRangeBackwards(this IEnumerable<IPeriodTimeline> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
        => source.EnumerateRangeBackwards(start.ToDateTimeUtc(), end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="interval"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="interval"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateInterval(this IEnumerable<IPeriodTimeline> source, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
        => source.EnumeratePeriod(interval.ToPeriod(), periodIncludeOptions);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="interval"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="interval"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumeratePeriodBackwards(this IEnumerable<IPeriodTimeline> source, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
        => source.EnumeratePeriodBackwards(interval.ToPeriod(), periodIncludeOptions);
}