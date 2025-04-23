using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineCollectionExtensions
{
    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> Enumerate(this IEnumerable<IPeriodTimeline> source) =>
        source.EnumerateFromIncludingPartial(DateTimeHelper.MinValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwards(this IEnumerable<IPeriodTimeline> source) =>
        source.EnumerateBackwardsFromIncludingPartial(DateTimeHelper.MaxValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcStart"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateFrom(this IEnumerable<IPeriodTimeline> source, DateTime utcStart)
    {
        return source.Select(t => t.EnumerateFrom(utcStart)).CombineOrderedEnumerables();
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcEnd"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsTo(this IEnumerable<IPeriodTimeline> source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(p => p.Start >= utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcStart"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateFromIncludingPartial(this IEnumerable<IPeriodTimeline> source, DateTime utcStart)
    {
        return source.Select(t => t.EnumerateFromIncludingPartial(utcStart)).CombineOrderedEnumerables();
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcEnd"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsToIncludingPartial(this IEnumerable<IPeriodTimeline> source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(p => p.End == null || p.End > utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcEnd"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateTo(this IEnumerable<IPeriodTimeline> source, DateTime utcEnd) =>
        source.Enumerate().TakeWhile(p => p.End <= utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcStart"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsFrom(this IEnumerable<IPeriodTimeline> source, DateTime utcStart)
    {
        return source.Select(t => t.EnumerateBackwardsFrom(utcStart)).CombineOrderedEnumerables(descending: true);
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcEnd"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateToIncludingPartial(this IEnumerable<IPeriodTimeline> source, DateTime utcEnd) =>
        source.Enumerate().TakeWhile(p => p.Start == null || p.Start < utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcStart"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsFromIncludingPartial(this IEnumerable<IPeriodTimeline> source, DateTime utcStart)
    {
        return source.Select(t => t.EnumerateBackwardsFromIncludingPartial(utcStart)).CombineOrderedEnumerables(descending: true);
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateRange(this IEnumerable<IPeriodTimeline> source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        return source.Select(t => t.EnumerateRange(utcStart, utcEnd, periodIncludeOptions)).CombineOrderedEnumerables();
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumerateRangeBackwards(this IEnumerable<IPeriodTimeline> source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        return source.Select(t => t.EnumerateRangeBackwards(utcStart, utcEnd, periodIncludeOptions)).CombineOrderedEnumerables(descending: true);
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumeratePeriod(this IEnumerable<IPeriodTimeline> source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        return source.Select(t => t.EnumeratePeriod(period, periodIncludeOptions)).CombineOrderedEnumerables();
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<Period> EnumeratePeriodBackwards(this IEnumerable<IPeriodTimeline> source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        return source.Select(t => t.EnumeratePeriodBackwards(period, periodIncludeOptions)).CombineOrderedEnumerables(descending: true);
    }
}