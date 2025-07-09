
using NodaTime;

namespace Occurify.Extensions;

public static partial class IntervalCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all intervals in <paramref name="source"/> with equal end and start instants are combined into a single interval.
    /// </summary>
    public static IPeriodTimeline Stitch(this IEnumerable<Interval> source) => source.AsPeriodTimeline().Stitch();

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which intervals in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Interval> source, Instant instant) => source.AsPeriodTimeline().Cut(instant);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which intervals in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Interval> source, IEnumerable<Instant> instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which intervals in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Interval> source, params Instant[] instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which intervals in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Interval> source, ITimeline instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which intervals in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Interval> source, IEnumerable<ITimeline> instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which intervals in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Interval> source, params ITimeline[] instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of all intervals in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Interval> source) => source.Select(p => p.AsPeriodTimeline()).IntersectPeriods();

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Interval> source, IEnumerable<Interval> periodsToIntersect) => source.AsPeriodTimeline().IntersectIntervals(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Interval> source, params Interval[] periodsToIntersect) => source.AsPeriodTimeline().IntersectIntervals(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Interval> source, IPeriodTimeline periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Interval> source, IEnumerable<IPeriodTimeline> periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Interval> source, params IPeriodTimeline[] periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> that is inverted of all intervals on <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Invert(this IEnumerable<Interval> source) => source.AsPeriodTimeline().Invert();

    /// <summary>
    /// Merges all intervals in <paramref name="source"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Interval> source) => source.AsPeriodTimeline();

    /// <summary>
    /// Merges all intervals in <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Interval> source, Interval periodToMerge) => source.AsPeriodTimeline().Merge(periodToMerge);

    /// <summary>
    /// Merges all intervals in <paramref name="source"/> with all intervals in <paramref name="periodsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Interval> source, IEnumerable<Interval> periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges all intervals in <paramref name="source"/> with all intervals in <paramref name="periodsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Interval> source, params Interval[] periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges all intervals in <paramref name="source"/> with <paramref name="periodsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Interval> source, IPeriodTimeline periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges all intervals in <paramref name="source"/> with all interval timelines in <paramref name="periodsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Interval> source, IEnumerable<IPeriodTimeline> periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges all intervals in <paramref name="source"/> with all interval timelines in <paramref name="periodsToMerge"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Interval> source, params IPeriodTimeline[] periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all intervals in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Interval> source, IEnumerable<Interval> subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all intervals in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Interval> source, params Interval[] subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="subtrahend"/> is subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Interval> source, IPeriodTimeline subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all interval timelines in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Interval> source, IEnumerable<IPeriodTimeline> subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all interval timelines in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Interval> source, params IPeriodTimeline[] subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with intervals <paramref name="source"/>. Overlapping intervals are combined.
    /// </summary>
    public static IPeriodTimeline AsPeriodTimeline(this IEnumerable<Interval> source) => PeriodTimeline.FromPeriods(source.Select(i => i.ToPeriod()));
}