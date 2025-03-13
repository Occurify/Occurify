
namespace Occurify.Extensions;

public static partial class PeriodExtensions
{
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="source"/> with equal end and start instants are combined into a single period.
    /// </summary>
    public static IPeriodTimeline Stitch(this IEnumerable<Period> source) => source.AsPeriodTimeline().Stitch();

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, DateTime instant) => source.AsPeriodTimeline().Cut(instant);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, IEnumerable<DateTime> instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, params DateTime[] instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, ITimeline instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, IEnumerable<ITimeline> instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, params ITimeline[] instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriod(this Period source, Period periodToIntersect) => source.AsPeriodTimeline().IntersectPeriod(periodToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Period source, IEnumerable<Period> periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Period source, params Period[] periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Period source, IPeriodTimeline periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Period source, IEnumerable<IPeriodTimeline> periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this Period source, params IPeriodTimeline[] periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Period> source) => source.Select(AsPeriodTimeline).IntersectPeriods();

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Period> source, IEnumerable<Period> periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Period> source, params Period[] periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Period> source, IPeriodTimeline periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Period> source, IEnumerable<IPeriodTimeline> periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<Period> source, params IPeriodTimeline[] periodsToIntersect) => source.AsPeriodTimeline().IntersectPeriods(periodsToIntersect);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> that is inverted of <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Invert(this Period source) => source.AsPeriodTimeline().Invert();

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> that is inverted of all periods on <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Invert(this IEnumerable<Period> source) => source.AsPeriodTimeline().Invert();

    /// <summary>
    /// Merges <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlap is combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Period source, Period periodToMerge) => source.AsPeriodTimeline().Merge(periodToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Period source, IEnumerable<Period> periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Period source, params Period[] periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Period source, IPeriodTimeline periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all period timelines in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Period source, IEnumerable<IPeriodTimeline> periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges <paramref name="source"/> with all period timelines in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this Period source, params IPeriodTimeline[] periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Period> source) => source.AsPeriodTimeline();

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Period> source, Period periodToMerge) => source.AsPeriodTimeline().Merge(periodToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Period> source, IEnumerable<Period> periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Period> source, params Period[] periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Period> source, IPeriodTimeline periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all period timelines in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Period> source, IEnumerable<IPeriodTimeline> periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all period timelines in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<Period> source, params IPeriodTimeline[] periodsToMerge) => source.AsPeriodTimeline().Merge(periodsToMerge);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="subtrahend"/> is subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Period source, Period subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Period source, IEnumerable<Period> subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Period source, params Period[] subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="subtrahend"/> is subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Period source, IPeriodTimeline subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all period timelines in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Period source, IEnumerable<IPeriodTimeline> subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all period timelines in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this Period source, params IPeriodTimeline[] subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    // Note: Subtract(this IEnumerable<Period> periods) is not implemented on purpose as the signature doesn't feel logical.

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Period> source, IEnumerable<Period> subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Period> source, params Period[] subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="subtrahend"/> is subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Period> source, IPeriodTimeline subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all period timelines in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Period> source, IEnumerable<IPeriodTimeline> subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all period timelines in <paramref name="subtrahend"/> are subtracted from <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IEnumerable<Period> source, params IPeriodTimeline[] subtrahend) => source.AsPeriodTimeline().Subtract(subtrahend);

    /// <summary>
    /// Offsets <paramref name="period"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period Offset(this Period period, TimeSpan offset)
    {
        var start = period.Start;
        var end = period.End;
        if (start == null && end == null)
        {
            return period;
        }
        if (start != null)
        {
            start = start.Value.AddOrNullOnOverflow(offset);
            if (offset > TimeSpan.Zero && start == null)
            {
                throw new OverflowException("Start is not allowed to overflow DateTime.MaxValue.");
            }
        }
        if (end != null)
        {
            end = end.Value.AddOrNullOnOverflow(offset);
            if (offset < TimeSpan.Zero && end == null)
            {
                throw new OverflowException("End is not allowed to overflow DateTime.MinValue.");
            }
        }
        return new Period(start, end);
    }

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="ticks"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period OffsetTicks(this Period source, long ticks) => source + TimeSpan.FromTicks(ticks);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="microseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period OffsetMicroseconds(this Period source, double microseconds) =>
#if NET7_0 || NET8_0 || NET9_0
        source + TimeSpan.FromMicroseconds(microseconds);
#else
        source + TimeSpan.FromTicks((long)(microseconds * 10));
#endif

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="milliseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period OffsetMilliseconds(this Period source, double milliseconds) => source + TimeSpan.FromMilliseconds(milliseconds);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="seconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period OffsetSeconds(this Period source, double seconds) => source + TimeSpan.FromSeconds(seconds);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="minutes"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period OffsetMinutes(this Period source, double minutes) => source + TimeSpan.FromMinutes(minutes);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="hours"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period OffsetHours(this Period source, double hours) => source + TimeSpan.FromHours(hours);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="days"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period OffsetDays(this Period source, double days) => source + TimeSpan.FromDays(days);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<Period> Offset(this IEnumerable<Period> source, TimeSpan offset) => source.Select(p => p.Offset(offset));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="ticks"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<Period> OffsetTicks(this IEnumerable<Period> source, long ticks) => source.Select(p => p.OffsetTicks(ticks));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="microseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<Period> OffsetMicroseconds(this IEnumerable<Period> source, double microseconds) => source.Select(p => p.OffsetMicroseconds(microseconds));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="milliseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<Period> OffsetMilliseconds(this IEnumerable<Period> source, double milliseconds) => source.Select(p => p.OffsetMilliseconds(milliseconds));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="seconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<Period> OffsetSeconds(this IEnumerable<Period> source, double seconds) => source.Select(p => p.OffsetSeconds(seconds));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="minutes"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<Period> OffsetMinutes(this IEnumerable<Period> source, double minutes) => source.Select(p => p.OffsetMinutes(minutes));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="hours"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<Period> OffsetHours(this IEnumerable<Period> source, double hours) => source.Select(p => p.OffsetHours(hours));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="days"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<Period> OffsetDays(this IEnumerable<Period> source, double days) => source.Select(p => p.OffsetDays(days));

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with a single period <paramref name="period"/>.
    /// </summary>
    public static IPeriodTimeline AsPeriodTimeline(this Period period) => PeriodTimeline.FromPeriod(period);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods <paramref name="source"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline AsPeriodTimeline(this IEnumerable<Period> source) => PeriodTimeline.FromPeriods(source);
}