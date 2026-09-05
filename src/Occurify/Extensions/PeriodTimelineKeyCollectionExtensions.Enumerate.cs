using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineKeyCollectionExtensions
{
    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> Enumerate<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) =>
        source.EnumerateFromIncludingPartial(DateTimeHelper.MinValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) =>
        source.EnumerateBackwardsFromIncludingPartial(DateTimeHelper.MaxValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcStart"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateFrom<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart) =>
        source.WithKeys(timelines => timelines.EnumerateFrom(utcStart));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcEnd"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsTo<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcEnd) =>
        source.WithKeys(timelines => timelines.EnumerateBackwardsTo(utcEnd));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcStart"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateFromIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart) =>
        source.WithKeys(timelines => timelines.EnumerateFromIncludingPartial(utcStart));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcEnd"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsToIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcEnd) =>
        source.WithKeys(timelines => timelines.EnumerateBackwardsToIncludingPartial(utcEnd));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcEnd"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateTo<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcEnd) =>
        source.WithKeys(timelines => timelines.EnumerateTo(utcEnd));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcStart"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsFrom<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart) =>
        source.WithKeys(timelines => timelines.EnumerateBackwardsFrom(utcStart));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcEnd"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateToIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcEnd) =>
        source.WithKeys(timelines => timelines.EnumerateToIncludingPartial(utcEnd));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcStart"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsFromIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart) =>
        source.WithKeys(timelines => timelines.EnumerateBackwardsFromIncludingPartial(utcStart));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateRange<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.WithKeys(timelines => timelines.EnumerateRange(utcStart, utcEnd, periodIncludeOptions));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateRangeBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.WithKeys(timelines => timelines.EnumerateRangeBackwards(utcStart, utcEnd, periodIncludeOptions));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumeratePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.WithKeys(timelines => timelines.EnumeratePeriod(period, periodIncludeOptions));

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumeratePeriodBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.WithKeys(timelines => timelines.EnumeratePeriodBackwards(period, periodIncludeOptions));

    // The plain collection enumerators merge the per-timeline enumerations, which is the only way to keep periods from
    // different timelines that overlap. Enumerating by "next complete period" would skip any period that starts before
    // the previous one ended.
    private static IEnumerable<KeyValuePair<Period, TKey[]>> WithKeys<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source,
        Func<IEnumerable<IPeriodTimeline>, IEnumerable<Period>> enumerate)
    {
        var sourceArray = source.ToArray();
        return enumerate(sourceArray.Select(kvp => kvp.Value))
            .Select(p => new KeyValuePair<Period, TKey[]>(p, sourceArray.GetKeysAtExactPeriod(p)));
    }
}
