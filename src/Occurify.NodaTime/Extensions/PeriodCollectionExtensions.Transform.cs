using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class PeriodCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, Instant instant) => source.AsPeriodTimeline().Cut(instant);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, IEnumerable<Instant> instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, params Instant[] instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Period> source, IEnumerable<Interval> intervalsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(intervalsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="intervalsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Period> source, params Interval[] intervalsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(intervalsToIntersect);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with <paramref name="intervalToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Period> source, Interval intervalToMerge) => source.AsPeriodTimeline().Merge(intervalToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Period> source, IEnumerable<Interval> intervalsToMerge) => source.AsPeriodTimeline().Merge(intervalsToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all intervals in <paramref name="intervalsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Period> source, params Interval[] intervalsToMerge) => source.AsPeriodTimeline().Merge(intervalsToMerge);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all intervals in <paramref name="subtrahends"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Period> source, IEnumerable<Interval> subtrahends) => source.AsPeriodTimeline().Subtract(subtrahends);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all intervals in <paramref name="subtrahends"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Period> source, params Interval[] subtrahends) => source.AsPeriodTimeline().Subtract(subtrahends);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<Period> Offset(this IEnumerable<Period> source, Duration offset) => source.Offset(offset.ToTimeSpan());
}
