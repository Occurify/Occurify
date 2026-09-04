using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class PeriodExtensions
{
    /// <summary>
    /// Converts an Occurify <see cref="Period"/> to a NodaTime <see cref="Interval"/>.
    /// A <c>null</c> start or end results in an interval without a start or end.
    /// </summary>
    public static Interval ToInterval(this Period period) => new(period.Start.ToInstant(), period.End.ToInstant());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instant"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Period source, Instant instant) => source.AsPeriodTimeline().Cut(instant);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Period source, IEnumerable<Instant> instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Period source, params Instant[] instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="intervalToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriod(this Period source, Interval intervalToIntersect) => source.AsPeriodTimeline().IntersectPeriod(intervalToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Period source, IEnumerable<Interval> intervalsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(intervalsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Period source, params Interval[] intervalsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(intervalsToIntersect);

    /// <summary>
    /// Merges <paramref name="source"/> with <paramref name="intervalToMerge"/>. Overlap is combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Period source, Interval intervalToMerge) => source.AsPeriodTimeline().Merge(intervalToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Period source, IEnumerable<Interval> intervalsToMerge) => source.AsPeriodTimeline().Merge(intervalsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Period source, params Interval[] intervalsToMerge) => source.AsPeriodTimeline().Merge(intervalsToMerge);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="subtrahend"/> is subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Period source, Interval subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all intervals in <paramref name="subtrahends"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Period source, IEnumerable<Interval> subtrahends) => source.AsPeriodTimeline().Subtract(subtrahends);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all intervals in <paramref name="subtrahends"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Period source, params Interval[] subtrahends) => source.AsPeriodTimeline().Subtract(subtrahends);

    /// <summary>
    /// Offsets <paramref name="period"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period Offset(this Period period, Duration offset) => period.Offset(offset.ToTimeSpan());
}
