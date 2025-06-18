
using NodaTime;

namespace Occurify.Extensions;

public static partial class IntervalExtensions
{
    /// <summary>
    /// Converts a NodaTime <see cref="Interval"/> to an Occurify <see cref="Interval"/>.
    /// </summary>
    public static Period ToPeriod(this Interval interval)
    {
        DateTime? start = interval.HasStart ? interval.Start.ToDateTimeUtc() : null;
        DateTime? end = interval.HasEnd ? interval.End.ToDateTimeUtc() : null;
        return new Period(start, end);
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instant"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Interval source, Instant instant) => source.AsPeriodTimeline().Cut(instant);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Interval source, IEnumerable<Instant> instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Interval source, params Instant[] instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Interval source, ITimeline instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Interval source, IEnumerable<ITimeline> instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Interval source, params ITimeline[] instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriod(this Interval source, Interval periodToIntersect) => source.AsPeriodTimeline().IntersectPeriod(periodToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Interval source, IEnumerable<Interval> periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Interval source, params Interval[] periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Interval source, IPeriodTimeline periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Interval source, IEnumerable<IPeriodTimeline> periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Interval source, params IPeriodTimeline[] periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> that is inverted of <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Invert(this Interval source) => source.AsPeriodTimeline().Invert();

    /// <summary>
    /// Merges <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlap is combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Interval source, Interval periodToMerge) => source.AsPeriodTimeline().Merge(periodToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Interval source, IEnumerable<Interval> periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Interval source, params Interval[] periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Interval source, IPeriodTimeline periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all period timelines in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Interval source, IEnumerable<IPeriodTimeline> periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all period timelines in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Interval source, params IPeriodTimeline[] periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="subtrahend"/> is subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Interval source, Interval subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Interval source, IEnumerable<Interval> subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Interval source, params Interval[] subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="subtrahend"/> is subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Interval source, IPeriodTimeline subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all period timelines in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Interval source, IEnumerable<IPeriodTimeline> subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all period timelines in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Interval source, params IPeriodTimeline[] subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    // Note: Subtract(this IEnumerable<Interval> periods) is not implemented on purpose as the signature doesn't feel logical.
    
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with a single period <paramref name="interval"/>.
    /// </summary>
    public static IPeriodTimeline AsPeriodTimeline(this Interval interval) => PeriodTimeline.FromPeriod(interval.ToPeriod());

}