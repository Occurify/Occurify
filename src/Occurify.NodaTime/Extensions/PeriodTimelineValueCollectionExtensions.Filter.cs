using NodaTime;

namespace Occurify.Extensions;

public static partial class PeriodTimelineValueCollectionExtensions
{
    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are inside <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Within<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Within<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Interval> mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Within<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Interval[] mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals not in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Outside<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Outside<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Interval> mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Outside<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Interval[] mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain <paramref name="intervalToContain"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval intervalToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(intervalToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the intervals in <paramref name="intervalsToContain"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Interval> intervalsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(intervalsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the intervals in <paramref name="intervalsToContain"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Interval[] intervalsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(intervalsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain <paramref name="instantToContain"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instantToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Instant> instantsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Instant[] instantsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals do not contain <paramref name="intervalNotToContain"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Without<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval intervalNotToContain) =>
        source.ToDictionary(kvp => kvp.Key.Without(intervalNotToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals do not contain any of the intervals in <paramref name="intervalsNotToContain"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Without<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Interval> intervalsNotToContain) =>
        source.ToDictionary(kvp => kvp.Key.Without(intervalsNotToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals do not contain any of the intervals in <paramref name="intervalsNotToContain"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Without<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Interval[] intervalsNotToContain) =>
        source.ToDictionary(kvp => kvp.Key.Without(intervalsNotToContain), kvp => kvp.Value);
}