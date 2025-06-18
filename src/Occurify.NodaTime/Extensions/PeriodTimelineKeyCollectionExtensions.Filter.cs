using NodaTime;

namespace Occurify.Extensions;

public static partial class PeriodTimelineKeyCollectionExtensions
{
    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are inside <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Within<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Within<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Interval> mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Within<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Interval[] mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals not in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Interval> mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Interval[] mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain <paramref name="intervalToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval intervalToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(intervalToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the intervals in <paramref name="intervalsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Interval> intervalsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(intervalsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the intervals in <paramref name="intervalsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Interval[] intervalsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(intervalsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain <paramref name="instantToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instantToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Instant> instantsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Instant[] instantsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals do not contain <paramref name="intervalNotToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Without<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval intervalNotToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(intervalNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals do not contain any of the intervals in <paramref name="intervalsNotToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Without<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Interval> intervalsNotToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(intervalsNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals do not contain any of the intervals in <paramref name="intervalsNotToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Without<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Interval[] intervalsNotToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(intervalsNotToContain));
}