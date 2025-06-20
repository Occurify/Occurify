using NodaTime.Extensions;
using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineKeyCollectionExtensions
{
    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="start"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateFrom<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start) =>
        source.EnumerateFrom(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="end"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsTo<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant end) =>
        source.EnumerateBackwardsTo(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="start"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateFromIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start) =>
        source.EnumerateFromIncludingPartial(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="end"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsToIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant end) =>
        source.EnumerateBackwardsToIncludingPartial(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="end"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateTo<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant end) =>
        source.EnumerateTo(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="start"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsFrom<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start) =>
        source.EnumerateBackwardsFrom(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="end"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateToIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant end) =>
        source.EnumerateToIncludingPartial(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="start"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsFromIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start) =>
        source.EnumerateBackwardsFromIncludingPartial(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateRange<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRange(start.ToDateTimeUtc(), end.ToDateTimeUtc(), periodIncludeOptions);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateRangeBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRangeBackwards(start.ToDateTimeUtc(), end.ToDateTimeUtc(), periodIncludeOptions);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumeratePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriod(new Period(period.Start.ToDateTimeUtc(), period.End.ToDateTimeUtc()), periodIncludeOptions);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumeratePeriodBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriodBackwards(new Period(period.Start.ToDateTimeUtc(), period.End.ToDateTimeUtc()), periodIncludeOptions);
}