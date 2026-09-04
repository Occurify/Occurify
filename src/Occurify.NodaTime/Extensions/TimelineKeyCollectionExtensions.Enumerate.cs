using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class TimelineKeyCollectionExtensions
{
    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from earliest to latest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<Instant, TKey[]>> EnumerateInstants<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source) =>
        source.Enumerate().Select(kvp => kvp.ToInstantKey());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from latest to earliest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<Instant, TKey[]>> EnumerateInstantsBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source) =>
        source.EnumerateBackwards().Select(kvp => kvp.ToInstantKey());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="start"/> from earliest to latest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<Instant, TKey[]>> EnumerateInstantsFrom<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant start) =>
        source.EnumerateFrom(start.ToDateTimeUtc()).Select(kvp => kvp.ToInstantKey());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="end"/> from latest to earliest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<Instant, TKey[]>> EnumerateInstantsBackwardsTo<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant end) =>
        source.EnumerateBackwardsTo(end.ToDateTimeUtc()).Select(kvp => kvp.ToInstantKey());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="end"/> from earliest to latest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<Instant, TKey[]>> EnumerateInstantsTo<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant end) =>
        source.EnumerateTo(end.ToDateTimeUtc()).Select(kvp => kvp.ToInstantKey());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="start"/> from latest to earliest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<Instant, TKey[]>> EnumerateInstantsBackwardsFrom<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant start) =>
        source.EnumerateBackwardsFrom(start.ToDateTimeUtc()).Select(kvp => kvp.ToInstantKey());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="start"/> and <paramref name="end"/> from earliest to latest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<Instant, TKey[]>> EnumerateInstantRange<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant start, Instant end) =>
        source.EnumerateRange(start.ToDateTimeUtc(), end.ToDateTimeUtc()).Select(kvp => kvp.ToInstantKey());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="start"/> and <paramref name="end"/> from latest to earliest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<Instant, TKey[]>> EnumerateInstantRangeBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant start, Instant end) =>
        source.EnumerateRangeBackwards(start.ToDateTimeUtc(), end.ToDateTimeUtc()).Select(kvp => kvp.ToInstantKey());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="interval"/> from earliest to latest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<Instant, TKey[]>> EnumerateInstants<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Interval interval) =>
        source.EnumeratePeriod(interval.ToPeriod()).Select(kvp => kvp.ToInstantKey());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="interval"/> from latest to earliest and returns the instant along with the keys of the timelines that include this instant.
    /// </summary>
    public static IEnumerable<KeyValuePair<Instant, TKey[]>> EnumerateInstantsBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Interval interval) =>
        source.EnumeratePeriodBackwards(interval.ToPeriod()).Select(kvp => kvp.ToInstantKey());
}
