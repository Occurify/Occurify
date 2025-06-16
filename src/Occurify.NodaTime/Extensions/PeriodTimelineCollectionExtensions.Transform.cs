using NodaTime;
using Occurify.PeriodTimelineCollectionTransformations;

namespace Occurify.Extensions;

public static partial class PeriodTimelineCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Cut(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.Select(t => t.Cut(instant));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Cut(this IEnumerable<IPeriodTimeline> source, IEnumerable<Instant> instants) =>
        source.Select(t => t.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Cut(this IEnumerable<IPeriodTimeline> source, params Instant[] instants) =>
        source.Select(t => t.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Cut(this IEnumerable<IPeriodTimeline> source, ITimeline instants) =>
        source.Select(t => t.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Cut(this IEnumerable<IPeriodTimeline> source, IEnumerable<ITimeline> instants) =>
        source.Select(t => t.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Cut(this IEnumerable<IPeriodTimeline> source, params ITimeline[] instants) =>
        source.Select(t => t.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which all periods in the timelines in <paramref name="source"/> with equal end and start instants are combined into a single period.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Stitch(this IEnumerable<IPeriodTimeline> source) =>
        source.Select(t => t.Stitch());

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodToIntersect"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> IntersectPeriod(this IEnumerable<IPeriodTimeline> source, Period periodToIntersect) =>
        source.Select(t => t.IntersectPeriod(periodToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> IntersectPeriods(this IEnumerable<IPeriodTimeline> source, IEnumerable<Period> periodsToIntersect) =>
        source.Select(t => t.IntersectPeriods(periodsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> IntersectPeriods(this IEnumerable<IPeriodTimeline> source, params Period[] periodsToIntersect) =>
        source.Select(t => t.IntersectPeriods(periodsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> IntersectPeriods(this IEnumerable<IPeriodTimeline> source, IPeriodTimeline periodsToIntersect) =>
        source.Select(t => t.IntersectPeriods(periodsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> IntersectPeriods(this IEnumerable<IPeriodTimeline> source, IEnumerable<IPeriodTimeline> periodsToIntersect) =>
        source.Select(t => t.IntersectPeriods(periodsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> IntersectPeriods(this IEnumerable<IPeriodTimeline> source, params IPeriodTimeline[] periodsToIntersect) =>
        source.Select(t => t.IntersectPeriods(periodsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IEnumerable<IPeriodTimeline> source)
    {
        return source.Aggregate((current, pp) => current.IntersectPeriods(pp));
    }

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> that are the inverted timelines of <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Invert(this IEnumerable<IPeriodTimeline> source) =>
        source.Select(t => t.Invert());

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Merge(this IEnumerable<IPeriodTimeline> source, Period periodToMerge) =>
        source.Select(t => t.Merge(periodToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Merge(this IEnumerable<IPeriodTimeline> source, IEnumerable<Period> periodsToMerge) =>
        source.Select(t => t.Merge(periodsToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Merge(this IEnumerable<IPeriodTimeline> source, params Period[] periodsToMerge) =>
        source.Select(t => t.Merge(periodsToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Merge(this IEnumerable<IPeriodTimeline> source, IPeriodTimeline periodsToMerge) =>
        source.Select(t => t.Merge(periodsToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Merge(this IEnumerable<IPeriodTimeline> source, IEnumerable<IPeriodTimeline> periodsToMerge) =>
        source.Select(t => t.Merge(periodsToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Merge(this IEnumerable<IPeriodTimeline> source, params IPeriodTimeline[] periodsToMerge) =>
        source.Select(t => t.Merge(periodsToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IEnumerable<IPeriodTimeline> source)
    {
        return source.Aggregate((current, pp) => current.Merge(pp));
    }

    /// <summary>
    /// Applies <see cref="Normalize"/> on every timeline in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Normalize(this IEnumerable<IPeriodTimeline> source) =>
        source.Select(t => t.Normalize());

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Offset(this IEnumerable<IPeriodTimeline> source, Duration offset) =>
        source.Select(t => t.Offset(offset));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="ticks"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> OffsetTicks(this IEnumerable<IPeriodTimeline> source, long ticks) =>
        source.Select(t => t.OffsetTicks(ticks));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="microseconds"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> OffsetMicroseconds(this IEnumerable<IPeriodTimeline> source, double microseconds) =>
        source.Select(t => t.OffsetMicroseconds(microseconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="milliseconds"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> OffsetMilliseconds(this IEnumerable<IPeriodTimeline> source, double milliseconds) =>
        source.Select(t => t.OffsetMilliseconds(milliseconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="seconds"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> OffsetSeconds(this IEnumerable<IPeriodTimeline> source, double seconds) =>
        source.Select(t => t.OffsetSeconds(seconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="minutes"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> OffsetMinutes(this IEnumerable<IPeriodTimeline> source, double minutes) =>
        source.Select(t => t.OffsetMinutes(minutes));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="hours"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> OffsetHours(this IEnumerable<IPeriodTimeline> source, double hours) =>
        source.Select(t => t.OffsetHours(hours));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="days"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> OffsetDays(this IEnumerable<IPeriodTimeline> source, double days) =>
        source.Select(t => t.OffsetDays(days));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Randomize(this IEnumerable<IPeriodTimeline> source, Duration maxDeviation) =>
        source.Select(t => t.Randomize(maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Randomize(this IEnumerable<IPeriodTimeline> source, int seed, Duration maxDeviation) =>
        source.Select(t => t.Randomize(seed, maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Randomize(this IEnumerable<IPeriodTimeline> source, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.Select(t => t.Randomize(maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Randomize(this IEnumerable<IPeriodTimeline> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.Select(t => t.Randomize(seed, maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Randomize(this IEnumerable<IPeriodTimeline> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter, Func<int, double> randomFunc) =>
        source.Select(t => t.Randomize(seed, maxDeviationBefore, maxDeviationAfter, randomFunc));

    /// <summary>
    /// Subtracts <paramref name="subtrahend"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Subtract(this IEnumerable<IPeriodTimeline> source, Period subtrahend) =>
        source.Select(t => t.Subtract(subtrahend));

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahend"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Subtract(this IEnumerable<IPeriodTimeline> source, IEnumerable<Period> subtrahend) =>
        source.Select(t => t.Subtract(subtrahend));

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Subtract(this IEnumerable<IPeriodTimeline> source, params Period[] subtrahends) =>
        source.Select(t => t.Subtract(subtrahends));

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahend"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Subtract(this IEnumerable<IPeriodTimeline> source, IPeriodTimeline subtrahend) =>
        source.Select(t => t.Subtract(subtrahend));

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Subtract(this IEnumerable<IPeriodTimeline> source, IEnumerable<IPeriodTimeline> subtrahends) =>
        source.Select(t => t.Subtract(subtrahends));

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Subtract(this IEnumerable<IPeriodTimeline> source, params IPeriodTimeline[] subtrahends) =>
        source.Select(t => t.Subtract(subtrahends));

    // Note: Subtract(this IEnumerable<IPeriodTimeline> periodTimelines) is not implemented on purpose as the signature doesn't feel logical.

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods that start and end as the amount of overlapping periods in <paramref name="source"/> is at least <paramref name="minOverlapping"/> .
    /// </summary>
    public static IPeriodTimeline WhereOverlapCountAtLeast(this IEnumerable<IPeriodTimeline> source, int minOverlapping) =>
        source.WhereOverlapCount(c => c >= minOverlapping);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods that start and end as the amount of overlapping periods in <paramref name="source"/> is at most <paramref name="maxOverlapping"/> .
    /// </summary>
    public static IPeriodTimeline WhereOverlapCountAtMost(this IEnumerable<IPeriodTimeline> source, int maxOverlapping) =>
        source.WhereOverlapCount(c => c >= maxOverlapping);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods that start and end as the amount of overlapping periods in <paramref name="source"/> trigger <paramref name="predicate"/> to become true or false.
    /// </summary>
    public static IPeriodTimeline WhereOverlapCount(this IEnumerable<IPeriodTimeline> source, Func<int, bool> predicate)
    {
        var sourceArray = source.ToArray();
        return new PeriodTimeline(
            new WhereOverlapCountStartTimeline(sourceArray, predicate),
            new WhereOverlapCountEndTimeline(sourceArray, predicate));
    }

    /// <summary>
    /// Returns a Dictionary with the amount of overlapping periods in <paramref name="source"/>.
    /// </summary>
    public static IDictionary<int, IPeriodTimeline> ToOverlapTimelines(this IEnumerable<IPeriodTimeline> source)
    {
        source = source.ToArray();
        return source.Select((_, index) => index + 1)
            .ToDictionary(overlapCount => overlapCount, overlapCount => source.WhereOverlapCount(c => c == overlapCount));
    }
}