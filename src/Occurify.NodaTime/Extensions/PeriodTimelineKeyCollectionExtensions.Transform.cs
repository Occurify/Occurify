using NodaTime;

namespace Occurify.Extensions;

public static partial class PeriodTimelineKeyCollectionExtensions
{
    /// <summary>
    /// Returns a Dictionary&lt;TKey, IPeriodTimeline&gt; in which intervals in the timelines in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Cut<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Cut(instant));

    /// <summary>
    /// Returns a Dictionary&lt;TKey, IPeriodTimeline&gt; in which intervals in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Cut<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Instant> instants) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Cut(instants));

    /// <summary>
    /// Returns a Dictionary&lt;TKey, IPeriodTimeline&gt; in which intervals in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Cut<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Instant[] instants) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Cut(instants));

    /// <summary>
    /// Returns a Dictionary&lt;TKey, IPeriodTimeline&gt; with the intersections of the timelines in <paramref name="source"/> with <paramref name="intervalToIntersect"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> IntersectInterval<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval intervalToIntersect) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.IntersectInterval(intervalToIntersect));

    /// <summary>
    /// Returns a Dictionary&lt;TKey, IPeriodTimeline&gt; with the intersections of the timelines in <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> IntersectIntervals<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Interval> intervalsToIntersect) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.IntersectIntervals(intervalsToIntersect));

    /// <summary>
    /// Returns a Dictionary&lt;TKey, IPeriodTimeline&gt; with the intersections of the timelines in <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> IntersectIntervals<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Interval[] intervalsToIntersect) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.IntersectIntervals(intervalsToIntersect));

    /// <summary>
    /// Merges all intervals in the timelines in <paramref name="source"/> with <paramref name="intervalToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Merge<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval intervalToMerge) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Merge(intervalToMerge));

    /// <summary>
    /// Merges all intervals in the timelines in <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Merge<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Interval> intervalsToMerge) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Merge(intervalsToMerge));

    /// <summary>
    /// Merges all intervals in the timelines in <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Merge<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Interval[] intervalsToMerge) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Merge(intervalsToMerge));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Offset<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Duration offset) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Offset(offset));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Duration maxDeviation) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, int seed, Duration maxDeviation) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(seed, maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Duration maxDeviationBefore, Duration maxDeviationAfter) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(seed, maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter, Func<int, double> randomFunc) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(seed, maxDeviationBefore, maxDeviationAfter, randomFunc));

    /// <summary>
    /// Subtracts <paramref name="subtrahend"/> from all intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Subtract<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval subtrahend) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Subtract(subtrahend));

    /// <summary>
    /// Subtracts all intervals in <paramref name="subtrahends"/> from all intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Subtract<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Interval> subtrahends) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Subtract(subtrahends));

    /// <summary>
    /// Subtracts all intervals in <paramref name="subtrahends"/> from all intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Subtract<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Interval[] subtrahends) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Subtract(subtrahends));
}