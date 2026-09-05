using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineValueCollectionExtensions
{
    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> Enumerate<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.EnumerateFromIncludingPartial(DateTimeHelper.MinValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwards<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.EnumerateBackwardsFromIncludingPartial(DateTimeHelper.MaxValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcStart"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateFrom<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart) =>
        source.WithValues(timelines => timelines.EnumerateFrom(utcStart));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcEnd"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsTo<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcEnd) =>
        source.WithValues(timelines => timelines.EnumerateBackwardsTo(utcEnd));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcStart"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateFromIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart) =>
        source.WithValues(timelines => timelines.EnumerateFromIncludingPartial(utcStart));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcEnd"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsToIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcEnd) =>
        source.WithValues(timelines => timelines.EnumerateBackwardsToIncludingPartial(utcEnd));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcEnd"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateTo<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcEnd) =>
        source.WithValues(timelines => timelines.EnumerateTo(utcEnd));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcStart"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsFrom<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart) =>
        source.WithValues(timelines => timelines.EnumerateBackwardsFrom(utcStart));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcEnd"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateToIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcEnd) =>
        source.WithValues(timelines => timelines.EnumerateToIncludingPartial(utcEnd));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcStart"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsFromIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart) =>
        source.WithValues(timelines => timelines.EnumerateBackwardsFromIncludingPartial(utcStart));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateRange<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.WithValues(timelines => timelines.EnumerateRange(utcStart, utcEnd, periodIncludeOptions));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateRangeBackwards<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.WithValues(timelines => timelines.EnumerateRangeBackwards(utcStart, utcEnd, periodIncludeOptions));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumeratePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.WithValues(timelines => timelines.EnumeratePeriod(period, periodIncludeOptions));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumeratePeriodBackwards<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.WithValues(timelines => timelines.EnumeratePeriodBackwards(period, periodIncludeOptions));

    // See PeriodTimelineKeyCollectionExtensions.WithKeys for why this delegates to the plain collection enumerators.
    private static IEnumerable<KeyValuePair<Period, TValue[]>> WithValues<TValue>(
        this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source,
        Func<IEnumerable<IPeriodTimeline>, IEnumerable<Period>> enumerate)
    {
        var sourceArray = source.ToArray();
        return enumerate(sourceArray.Select(kvp => kvp.Key))
            .Select(p => new KeyValuePair<Period, TValue[]>(p, sourceArray.GetValuesAtExactPeriod(p)));
    }
}
