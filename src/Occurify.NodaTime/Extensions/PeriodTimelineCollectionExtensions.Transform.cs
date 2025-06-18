using NodaTime;
using Occurify.PeriodTimelineCollectionTransformations;

namespace Occurify.Extensions;

public static partial class PeriodTimelineCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which intervals in the timelines in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Cut(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.Select(t => t.Cut(instant));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which intervals in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Cut(this IEnumerable<IPeriodTimeline> source, IEnumerable<Instant> instants) =>
        source.Select(t => t.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which intervals in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Cut(this IEnumerable<IPeriodTimeline> source, params Instant[] instants) =>
        source.Select(t => t.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="intervalToIntersect"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> IntersectInterval(this IEnumerable<IPeriodTimeline> source, Interval intervalToIntersect) =>
        source.Select(t => t.IntersectInterval(intervalToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> IntersectIntervals(this IEnumerable<IPeriodTimeline> source, IEnumerable<Interval> intervalsToIntersect) =>
        source.Select(t => t.IntersectIntervals(intervalsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> IntersectIntervals(this IEnumerable<IPeriodTimeline> source, params Interval[] intervalsToIntersect) =>
        source.Select(t => t.IntersectIntervals(intervalsToIntersect));

    /// <summary>
    /// Merges all intervals in the timelines in <paramref name="source"/> with <paramref name="intervalToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Merge(this IEnumerable<IPeriodTimeline> source, Interval intervalToMerge) =>
        source.Select(t => t.Merge(intervalToMerge));

    /// <summary>
    /// Merges all intervals in the timelines in <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Merge(this IEnumerable<IPeriodTimeline> source, IEnumerable<Interval> intervalsToMerge) =>
        source.Select(t => t.Merge(intervalsToMerge));

    /// <summary>
    /// Merges all intervals in the timelines in <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Merge(this IEnumerable<IPeriodTimeline> source, params Interval[] intervalsToMerge) =>
        source.Select(t => t.Merge(intervalsToMerge));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Offset(this IEnumerable<IPeriodTimeline> source, Duration offset) =>
        source.Select(t => t.Offset(offset));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Randomize(this IEnumerable<IPeriodTimeline> source, Duration maxDeviation) =>
        source.Select(t => t.Randomize(maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Randomize(this IEnumerable<IPeriodTimeline> source, int seed, Duration maxDeviation) =>
        source.Select(t => t.Randomize(seed, maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Randomize(this IEnumerable<IPeriodTimeline> source, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.Select(t => t.Randomize(maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Randomize(this IEnumerable<IPeriodTimeline> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.Select(t => t.Randomize(seed, maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Randomize(this IEnumerable<IPeriodTimeline> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter, Func<int, double> randomFunc) =>
        source.Select(t => t.Randomize(seed, maxDeviationBefore, maxDeviationAfter, randomFunc));

    /// <summary>
    /// Subtracts <paramref name="subtrahend"/> from all intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Subtract(this IEnumerable<IPeriodTimeline> source, Interval subtrahend) =>
        source.Select(t => t.Subtract(subtrahend));

    /// <summary>
    /// Subtracts all intervals in <paramref name="subtrahends"/> from all intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Subtract(this IEnumerable<IPeriodTimeline> source, IEnumerable<Interval> subtrahends) =>
        source.Select(t => t.Subtract(subtrahends));

    /// <summary>
    /// Subtracts all intervals in <paramref name="subtrahends"/> from all intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Subtract(this IEnumerable<IPeriodTimeline> source, params Interval[] subtrahends) =>
        source.Select(t => t.Subtract(subtrahends));
}