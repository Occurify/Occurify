namespace Occurify.Extensions;

public static partial class PeriodTimelineKeyCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Cut<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Cut(instant));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Cut<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<DateTime> instants) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Cut<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params DateTime[] instants) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Cut<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, ITimeline instants) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Cut<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<ITimeline> instants) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which periods in the timelines in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Cut<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params ITimeline[] instants) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Cut(instants));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> in which all periods in the timelines in <paramref name="source"/> with equal end and start instants are combined into a single period.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Stitch<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Stitch());

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodToIntersect"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> IntersectPeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period periodToIntersect) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.IntersectPeriod(periodToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> IntersectPeriods<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Period> periodsToIntersect) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.IntersectPeriods(periodsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> IntersectPeriods<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Period[] periodsToIntersect) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.IntersectPeriods(periodsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> IntersectPeriods<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IPeriodTimeline periodsToIntersect) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.IntersectPeriods(periodsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> IntersectPeriods<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<IPeriodTimeline> periodsToIntersect) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.IntersectPeriods(periodsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of the timelines in <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> IntersectPeriods<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params IPeriodTimeline[] periodsToIntersect) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.IntersectPeriods(periodsToIntersect));

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> with the intersections of all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) =>
        source.Select(kvp => kvp.Value).IntersectPeriods();

    /// <summary>
    /// Returns a <see cref="IEnumerable{IPeriodTimeline}"/> that are the inverted timelines of <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Invert<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Invert());

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Merge<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period periodToMerge) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Merge(periodToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Merge<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Period> periodsToMerge) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Merge(periodsToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Merge<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Period[] periodsToMerge) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Merge(periodsToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Merge<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IPeriodTimeline periodsToMerge) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Merge(periodsToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Merge<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<IPeriodTimeline> periodsToMerge) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Merge(periodsToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Merge<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params IPeriodTimeline[] periodsToMerge) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Merge(periodsToMerge));

    /// <summary>
    /// Merges all periods in the timelines in <paramref name="source"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) =>
        source.Select(kvp => kvp.Value).Merge();

    /// <summary>
    /// Applies <see cref="Normalize"/> on every timeline in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Normalize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Normalize());

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Offset<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, TimeSpan offset) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Offset(offset));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="ticks"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> OffsetTicks<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, long ticks) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetTicks(ticks));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="microseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> OffsetMicroseconds<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, double microseconds) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetMicroseconds(microseconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="milliseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> OffsetMilliseconds<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, double milliseconds) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetMilliseconds(milliseconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="seconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> OffsetSeconds<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, double seconds) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetSeconds(seconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="minutes"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> OffsetMinutes<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, double minutes) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetMinutes(minutes));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="hours"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> OffsetHours<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, double hours) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetHours(hours));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="days"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> OffsetDays<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, double days) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetDays(days));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, TimeSpan maxDeviation) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, int seed, TimeSpan maxDeviation) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(seed, maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, int seed, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(seed, maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, int seed, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter, Func<int, double> randomFunc) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(seed, maxDeviationBefore, maxDeviationAfter, randomFunc));

    /// <summary>
    /// Subtracts <paramref name="subtrahend"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Subtract<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period subtrahend) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Subtract(subtrahend));

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Subtract<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Period> subtrahends) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Subtract(subtrahends));

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Subtract<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Period[] subtrahends) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Subtract(subtrahends));

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahend"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Subtract<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IPeriodTimeline subtrahend) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Subtract(subtrahend));

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Subtract<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<IPeriodTimeline> subtrahends) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Subtract(subtrahends));

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Subtract<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params IPeriodTimeline[] subtrahends) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Subtract(subtrahends));

    // Note: Subtract(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> periodTimelines) is not implemented on purpose as the signature doesn't feel logical.
}