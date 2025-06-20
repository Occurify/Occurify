using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineValueCollectionExtensions
{
    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="start"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateFrom<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start) =>
        source.EnumerateFrom(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="end"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsTo<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant end) =>
        source.EnumerateBackwardsTo(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="start"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateFromIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start) =>
        source.EnumerateFromIncludingPartial(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="end"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsToIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant end) =>
        source.EnumerateBackwardsToIncludingPartial(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="end"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateTo<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant end) =>
        source.EnumerateTo(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="start"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsFrom<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start) =>
        source.EnumerateBackwardsFrom(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="end"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateToIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant end) =>
        source.EnumerateToIncludingPartial(end.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="start"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsFromIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start) =>
        source.EnumerateBackwardsFromIncludingPartial(start.ToDateTimeUtc());

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateRange<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRange(start.ToDateTimeUtc(), end.ToDateTimeUtc(), periodIncludeOptions);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="start"/> and <paramref name="end"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="start"/> or <paramref name="end"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateRangeBackwards<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant start, Instant end, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumerateRangeBackwards(start.ToDateTimeUtc(), end.ToDateTimeUtc(), periodIncludeOptions);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from earliest to latest and returns the period along with the values of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumeratePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriod(new Period(period.Start.ToDateTimeUtc(), period.End.ToDateTimeUtc()), periodIncludeOptions);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from latest to earliest and returns the period along with the values of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumeratePeriodBackwards<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        source.EnumeratePeriodBackwards(new Period(period.Start.ToDateTimeUtc(), period.End.ToDateTimeUtc()), periodIncludeOptions);
}