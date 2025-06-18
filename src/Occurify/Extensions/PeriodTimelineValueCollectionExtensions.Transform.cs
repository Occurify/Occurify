namespace Occurify.Extensions;

public static partial class PeriodTimelineValueCollectionExtensions
{
    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Cut<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant) =>
        source.ToDictionary(kvp => kvp.Key.Cut(instant), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Cut<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<DateTime> instants) =>
        source.ToDictionary(kvp => kvp.Key.Cut(instants), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Cut<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params DateTime[] instants) =>
        source.ToDictionary(kvp => kvp.Key.Cut(instants), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Cut<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, ITimeline instants) =>
        source.ToDictionary(kvp => kvp.Key.Cut(instants), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Cut<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<ITimeline> instants) =>
        source.ToDictionary(kvp => kvp.Key.Cut(instants), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Cut<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params ITimeline[] instants) =>
        source.ToDictionary(kvp => kvp.Key.Cut(instants), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; in which all periods in the timelines in <paramref name="source"/> with equal end and start instants are combined into a single period.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Stitch<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.ToDictionary(kvp => kvp.Key.Stitch(), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodToIntersect"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> IntersectPeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period periodToIntersect) =>
        source.ToDictionary(kvp => kvp.Key.IntersectPeriod(periodToIntersect), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> IntersectPeriods<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Period> periodsToIntersect) =>
        source.ToDictionary(kvp => kvp.Key.IntersectPeriods(periodsToIntersect), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> IntersectPeriods<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Period[] periodsToIntersect) =>
        source.ToDictionary(kvp => kvp.Key.IntersectPeriods(periodsToIntersect), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> IntersectPeriods<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IPeriodTimeline periodsToIntersect) =>
        source.ToDictionary(kvp => kvp.Key.IntersectPeriods(periodsToIntersect), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> IntersectPeriods<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<IPeriodTimeline> periodsToIntersect) =>
        source.ToDictionary(kvp => kvp.Key.IntersectPeriods(periodsToIntersect), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> IntersectPeriods<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params IPeriodTimeline[] periodsToIntersect) =>
        source.ToDictionary(kvp => kvp.Key.IntersectPeriods(periodsToIntersect), kvp => kvp.Value);

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; with the intersections of all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.Select(kvp => kvp.Key).IntersectPeriods();

    /// <summary>
    /// Returns a Dictionary&lt;IPeriodTimeline, TValue&gt; that are the inverted timelines of <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Invert<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.ToDictionary(kvp => kvp.Key.Invert(), kvp => kvp.Value);

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Merge<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period periodToMerge) =>
        source.ToDictionary(kvp => kvp.Key.Merge(periodToMerge), kvp => kvp.Value);

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Merge<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Period> periodsToMerge) =>
        source.ToDictionary(kvp => kvp.Key.Merge(periodsToMerge), kvp => kvp.Value);

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Merge<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Period[] periodsToMerge) =>
        source.ToDictionary(kvp => kvp.Key.Merge(periodsToMerge), kvp => kvp.Value);

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Merge<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IPeriodTimeline periodsToMerge) =>
        source.ToDictionary(kvp => kvp.Key.Merge(periodsToMerge), kvp => kvp.Value);

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Merge<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<IPeriodTimeline> periodsToMerge) =>
        source.ToDictionary(kvp => kvp.Key.Merge(periodsToMerge), kvp => kvp.Value);

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Merge<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params IPeriodTimeline[] periodsToMerge) =>
        source.ToDictionary(kvp => kvp.Key.Merge(periodsToMerge), kvp => kvp.Value);

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.Select(kvp => kvp.Key).Merge();

    /// <summary>
    /// Applies <see cref="Normalize"/> on every timeline in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Normalize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.ToDictionary(kvp => kvp.Key.Normalize(), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Offset<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, TimeSpan offset) =>
        source.ToDictionary(kvp => kvp.Key.Offset(offset), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="ticks"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> OffsetTicks<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, long ticks) =>
        source.ToDictionary(kvp => kvp.Key.OffsetTicks(ticks), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="microseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> OffsetMicroseconds<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, double microseconds) =>
        source.ToDictionary(kvp => kvp.Key.OffsetMicroseconds(microseconds), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="milliseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> OffsetMilliseconds<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, double milliseconds) =>
        source.ToDictionary(kvp => kvp.Key.OffsetMilliseconds(milliseconds), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="seconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> OffsetSeconds<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, double seconds) =>
        source.ToDictionary(kvp => kvp.Key.OffsetSeconds(seconds), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="minutes"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> OffsetMinutes<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, double minutes) =>
        source.ToDictionary(kvp => kvp.Key.OffsetMinutes(minutes), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="hours"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> OffsetHours<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, double hours) =>
        source.ToDictionary(kvp => kvp.Key.OffsetHours(hours), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="days"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> OffsetDays<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, double days) =>
        source.ToDictionary(kvp => kvp.Key.OffsetDays(days), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, TimeSpan maxDeviation) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(maxDeviation), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, int seed, TimeSpan maxDeviation) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(seed, maxDeviation), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(maxDeviationBefore, maxDeviationAfter), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, int seed, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(seed, maxDeviationBefore, maxDeviationAfter), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, int seed, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter, Func<int, double> randomFunc) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(seed, maxDeviationBefore, maxDeviationAfter, randomFunc), kvp => kvp.Value);

    /// <summary>
    /// Subtracts <paramref name="subtrahend"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Subtract<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period subtrahend) =>
        source.ToDictionary(kvp => kvp.Key.Subtract(subtrahend), kvp => kvp.Value);

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Subtract<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Period> subtrahends) =>
        source.ToDictionary(kvp => kvp.Key.Subtract(subtrahends), kvp => kvp.Value);

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Subtract<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Period[] subtrahends) =>
        source.ToDictionary(kvp => kvp.Key.Subtract(subtrahends), kvp => kvp.Value);

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahend"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Subtract<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IPeriodTimeline subtrahend) =>
        source.ToDictionary(kvp => kvp.Key.Subtract(subtrahend), kvp => kvp.Value);

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Subtract<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<IPeriodTimeline> subtrahends) =>
        source.ToDictionary(kvp => kvp.Key.Subtract(subtrahends), kvp => kvp.Value);

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<IPeriodTimeline, TValue> Subtract<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params IPeriodTimeline[] subtrahends) =>
        source.ToDictionary(kvp => kvp.Key.Subtract(subtrahends), kvp => kvp.Value);

    // Note: Subtract(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> periodTimelines) is not implemented on purpose as the signature doesn't feel logical.
}