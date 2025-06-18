using NodaTime;

namespace Occurify.Extensions;

public static partial class PeriodTimelineValueCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which intervals in the timelines in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Cut<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.ToDictionary(kvp => kvp.Key.Cut(instant), kvp => kvp.Value);

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which intervals in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Cut<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Instant> instants) =>
        source.ToDictionary(kvp => kvp.Key.Cut(instants), kvp => kvp.Value);

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which intervals in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Cut<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Instant[] instants) =>
        source.ToDictionary(kvp => kvp.Key.Cut(instants), kvp => kvp.Value);

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="intervalToIntersect"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> IntersectInterval<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval intervalToIntersect) =>
        source.ToDictionary(kvp => kvp.Key.IntersectInterval(intervalToIntersect), kvp => kvp.Value);

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> IntersectIntervals<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Interval> intervalsToIntersect) =>
        source.ToDictionary(kvp => kvp.Key.IntersectIntervals(intervalsToIntersect), kvp => kvp.Value);

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> IntersectIntervals<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Interval[] intervalsToIntersect) =>
        source.ToDictionary(kvp => kvp.Key.IntersectIntervals(intervalsToIntersect), kvp => kvp.Value);

    /// <summary>
    /// Merges all intervals in the timelines in <paramref name="source"/> with <paramref name="intervalToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Merge<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval intervalToMerge) =>
        source.ToDictionary(kvp => kvp.Key.Merge(intervalToMerge), kvp => kvp.Value);

    /// <summary>
    /// Merges all intervals in the timelines in <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Merge<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Interval> intervalsToMerge) =>
        source.ToDictionary(kvp => kvp.Key.Merge(intervalsToMerge), kvp => kvp.Value);

    /// <summary>
    /// Merges all intervals in the timelines in <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Merge<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Interval[] intervalsToMerge) =>
        source.ToDictionary(kvp => kvp.Key.Merge(intervalsToMerge), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Offset<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Duration offset) =>
        source.ToDictionary(kvp => kvp.Key.Offset(offset), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Duration maxDeviation) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(maxDeviation), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, int seed, Duration maxDeviation) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(seed, maxDeviation), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(maxDeviationBefore, maxDeviationAfter), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(seed, maxDeviationBefore, maxDeviationAfter), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter, Func<int, double> randomFunc) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(seed, maxDeviationBefore, maxDeviationAfter, randomFunc), kvp => kvp.Value);

    /// <summary>
    /// Subtracts <paramref name="subtrahend"/> from all intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Subtract<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval subtrahend) =>
        source.ToDictionary(kvp => kvp.Key.Subtract(subtrahend), kvp => kvp.Value);

    /// <summary>
    /// Subtracts all intervals in <paramref name="subtrahends"/> from all intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Subtract<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Interval> subtrahends) =>
        source.ToDictionary(kvp => kvp.Key.Subtract(subtrahends), kvp => kvp.Value);

    /// <summary>
    /// Subtracts all intervals in <paramref name="subtrahends"/> from all intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Subtract<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Interval[] subtrahends) =>
        source.ToDictionary(kvp => kvp.Key.Subtract(subtrahends), kvp => kvp.Value);
}