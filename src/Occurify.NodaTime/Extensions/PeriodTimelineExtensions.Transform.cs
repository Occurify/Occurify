using NodaTime;
using Occurify.NodaTime.Extensions;
using Occurify.PeriodTimelineTransformations;
using System.Reflection.Metadata;

namespace Occurify.Extensions;

public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which intervals in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IPeriodTimeline source, Instant instant) =>
        source.Cut(instant.ToDateTimeUtc());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which intervals in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IPeriodTimeline source, IEnumerable<Instant> instants) =>
        source.Cut(instants.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which intervals in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IPeriodTimeline source, params Instant[] instants) =>
        source.Cut(instants.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="intervalToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectInterval(this IPeriodTimeline source, Interval intervalToIntersect) =>
        source.IntersectPeriod(intervalToIntersect.ToPeriod());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectIntervals(this IPeriodTimeline source, IEnumerable<Interval> intervalsToIntersect) =>
        source.IntersectPeriods(intervalsToIntersect.Select(i => i.ToPeriod()));

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectIntervals(this IPeriodTimeline source, params Interval[] intervalsToIntersect) =>
        source.IntersectPeriods(intervalsToIntersect.Select(i => i.ToPeriod()));

    /// <summary>
    /// Merges all intervals in <paramref name="source"/> with <paramref name="intervalToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IPeriodTimeline source, Interval intervalToMerge) =>
        source.Merge(intervalToMerge.ToPeriod());

    /// <summary>
    /// Merges all intervals in <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IPeriodTimeline source, IEnumerable<Interval> intervalsToMerge) =>
        source.Merge(intervalsToMerge.Select(i => i.ToPeriod()));

    /// <summary>
    /// Merges all intervals in <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IPeriodTimeline source, params Interval[] intervalsToMerge) =>
        source.Merge(intervalsToMerge.Select(i => i.ToPeriod()));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline Offset(this IPeriodTimeline source, Duration offset) =>
        source.Offset(offset.ToTimeSpan());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// </summary>
    public static IPeriodTimeline Randomize(this IPeriodTimeline source, Duration maxDeviation) =>
        source.Randomize(maxDeviation.ToTimeSpan());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static IPeriodTimeline Randomize(this IPeriodTimeline source, int seed, Duration maxDeviation) =>
        source.Randomize(seed, maxDeviation.ToTimeSpan());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// </summary>
    public static IPeriodTimeline Randomize(this IPeriodTimeline source, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.Randomize(maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IPeriodTimeline Randomize(this IPeriodTimeline source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.Randomize(seed, maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of interval count or in overlapping intervals.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IPeriodTimeline Randomize(this IPeriodTimeline source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter, Func<int, double> randomFunc) =>
        source.Randomize(seed, maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan(), randomFunc);

    /// <summary>
    /// Subtracts <paramref name="subtrahend"/> from all intervals in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IPeriodTimeline source, Interval subtrahend) =>
        source.Subtract(subtrahend.ToPeriod());

    /// <summary>
    /// Subtracts all intervals in <paramref name="subtrahends"/> from all intervals in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IPeriodTimeline source, IEnumerable<Interval> subtrahends) =>
        source.Subtract(subtrahends.Select(i => i.ToPeriod()));

    /// <summary>
    /// Subtracts all intervals in <paramref name="subtrahends"/> from all intervals in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IPeriodTimeline source, params Interval[] subtrahends) =>
        source.Subtract(subtrahends.Select(i => i.ToPeriod()));
}