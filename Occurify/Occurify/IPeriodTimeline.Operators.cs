using Occurify.Extensions;

namespace Occurify;

public partial interface IPeriodTimeline
{
    /// <summary>
    /// Merges all periods in <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator +(IPeriodTimeline source, Period periodToMerge) => source.Merge(periodToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator +(IPeriodTimeline source, IEnumerable<Period> periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator +(IPeriodTimeline source, IPeriodTimeline periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator +(IPeriodTimeline source, IEnumerable<IPeriodTimeline> periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Subtracts <paramref name="subtrahend"/> from all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline operator -(IPeriodTimeline source, Period subtrahend) => source.Subtract(subtrahend);

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahend"/> from all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline operator -(IPeriodTimeline source, IEnumerable<Period> subtrahend) => source.Subtract(subtrahend);

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahend"/> from all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline operator -(IPeriodTimeline source, IPeriodTimeline subtrahend) => source.Subtract(subtrahend);

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahend"/> from all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline operator -(IPeriodTimeline source, IEnumerable<IPeriodTimeline> subtrahend) => source.Subtract(subtrahend);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator |(IPeriodTimeline source, Period periodToMerge) => source.Merge(periodToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator |(IPeriodTimeline source, IEnumerable<Period> periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator |(IPeriodTimeline source, IPeriodTimeline periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator |(IPeriodTimeline source, IEnumerable<IPeriodTimeline> periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline operator &(IPeriodTimeline source, Period periodToIntersect) => source.IntersectPeriod(periodToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline operator &(IPeriodTimeline source, IEnumerable<Period> periodsToIntersect) => source.IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline operator &(IPeriodTimeline source, IPeriodTimeline periodsToIntersect) => source.IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline operator &(IPeriodTimeline source, IEnumerable<IPeriodTimeline> periodsToIntersect) => source.IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static IPeriodTimeline operator -(IPeriodTimeline source, DateTime instant) => source.Cut(instant);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline operator -(IPeriodTimeline source, IEnumerable<DateTime> instants) => source.Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline operator -(IPeriodTimeline source, ITimeline instants) => source.Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline operator -(IPeriodTimeline source, IEnumerable<ITimeline> instants) => source.Cut(instants);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline operator +(IPeriodTimeline source, TimeSpan offset) => source.Offset(offset);

    /// <summary>
    /// Offsets <paramref name="source"/> with -<paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline operator -(IPeriodTimeline source, TimeSpan offset) => source.Offset(-offset);

    /// <summary>
    /// Offsets <paramref name="source"/> with -<paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline operator <<(IPeriodTimeline source, TimeSpan offset) => source.Offset(-offset);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline operator >>(IPeriodTimeline source, TimeSpan offset) => source.Offset(offset);
}