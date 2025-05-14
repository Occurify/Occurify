
namespace Occurify.Extensions;

public static partial class TimelineValueCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from all keys in <paramref name="source"/>.
    /// Values are lost.
    /// </summary>
    public static ITimeline AsCombinedTimeline<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source) =>
        source.Select(kvp => kvp.Key).AsCombinedTimeline();
    
    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<ITimeline, TValue> Offset<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, TimeSpan offset) =>
            source.ToDictionary(kvp => kvp.Key.Offset(offset), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="ticks"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<ITimeline, TValue> OffsetTicks<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, long ticks) =>
        source.ToDictionary(kvp => kvp.Key.OffsetTicks(ticks), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="microseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<ITimeline, TValue> OffsetMicroseconds<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, double microseconds) =>
        source.ToDictionary(kvp => kvp.Key.OffsetMicroseconds(microseconds), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="milliseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<ITimeline, TValue> OffsetMilliseconds<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, double milliseconds) =>
        source.ToDictionary(kvp => kvp.Key.OffsetMilliseconds(milliseconds), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="seconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<ITimeline, TValue> OffsetSeconds<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, double seconds) =>
        source.ToDictionary(kvp => kvp.Key.OffsetSeconds(seconds), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="minutes"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<ITimeline, TValue> OffsetMinutes<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, double minutes) =>
        source.ToDictionary(kvp => kvp.Key.OffsetMinutes(minutes), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="hours"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<ITimeline, TValue> OffsetHours<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, double hours) =>
        source.ToDictionary(kvp => kvp.Key.OffsetHours(hours), kvp => kvp.Value);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="days"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<ITimeline, TValue> OffsetDays<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, double days) =>
        source.ToDictionary(kvp => kvp.Key.OffsetDays(days), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// </summary>
    public static Dictionary<ITimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, TimeSpan maxDeviation) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(maxDeviation), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static Dictionary<ITimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, int seed, TimeSpan maxDeviation) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(seed, maxDeviation), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// </summary>
    public static Dictionary<ITimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(maxDeviationBefore, maxDeviationAfter), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<ITimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, int seed, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(seed, maxDeviationBefore, maxDeviationAfter), kvp => kvp.Value);

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<ITimeline, TValue> Randomize<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, int seed, TimeSpan maxDeviationBefore,
        TimeSpan maxDeviationAfter, Func<int, double> randomFunc) =>
        source.ToDictionary(kvp => kvp.Key.Randomize(seed, maxDeviationBefore, maxDeviationAfter, randomFunc), kvp => kvp.Value);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending with instants in the timelines in <paramref name="source"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> AsConsecutivePeriodTimelines<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source) =>
        source.ToDictionary(kvp => kvp.Key.AsConsecutivePeriodTimeline(), kvp => kvp.Value);
}