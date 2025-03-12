using Occurify.Extensions;

namespace Occurify;

public partial record Period
{
    /// <summary>
    /// Indicates whether the duration of <paramref name="period"/> is smaller than the duration of <paramref name="other"/>
    /// </summary>
    public static bool operator <(Period period, Period other)
    {
        if (period.Duration != null && other.Duration != null)
        {
            return period.Duration < other.Duration;
        }

        return period.Duration != null;
    }

    /// <summary>
    /// Indicates whether the duration of <paramref name="period"/> is larger than the duration of <paramref name="other"/>
    /// </summary>
    public static bool operator >(Period period, Period other)
    {
        if (period.Duration != null && other.Duration != null)
        {
            return period.Duration > other.Duration;
        }

        return other.Duration != null;
    }

    /// <summary>
    /// Merges <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlap is combined.
    /// </summary>
    public static IPeriodTimeline operator +(Period source, Period periodToMerge) => source.Merge(periodToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator +(Period source, IEnumerable<Period> periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator +(Period source, IPeriodTimeline periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator +(Period source, IEnumerable<IPeriodTimeline> periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="subtrahend"/> is subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline operator -(Period source, Period subtrahend) => source.Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline operator -(Period source, IEnumerable<Period> subtrahend) => source.Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline operator -(Period source, IPeriodTimeline subtrahend) => source.Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline operator -(Period source, IEnumerable<IPeriodTimeline> subtrahend) => source.Subtract(subtrahend);

    /// <summary>
    /// Merges <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlap is combined.
    /// </summary>
    public static IPeriodTimeline operator |(Period source, Period periodToMerge) => source.Merge(periodToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator |(Period source, IEnumerable<Period> periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator |(Period source, IPeriodTimeline periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline operator |(Period source, IEnumerable<IPeriodTimeline> periodsToMerge) => source.Merge(periodsToMerge);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline operator &(Period source, Period periodToIntersect) => source.IntersectPeriod(periodToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline operator &(Period source, IEnumerable<Period> periodsToIntersect) => source.IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline operator &(Period source, IPeriodTimeline periodsToIntersect) => source.IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline operator &(Period source, IEnumerable<IPeriodTimeline> periodsToIntersect) => source.IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Offsets <paramref name="period"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period operator +(Period period, TimeSpan offset) => period.Offset(offset);

    /// <summary>
    /// Offsets <paramref name="period"/> with -<paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period operator -(Period period, TimeSpan offset) => period.Offset(-offset);
}