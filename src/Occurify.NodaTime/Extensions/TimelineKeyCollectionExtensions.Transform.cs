
namespace Occurify.Extensions;

public static partial class TimelineKeyCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from all values in <paramref name="source"/>.
    /// Keys are lost.
    /// </summary>
    public static ITimeline AsCombinedTimeline<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source) =>
        source.Select(kvp => kvp.Value).AsCombinedTimeline();
    
    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Offset<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, TimeSpan offset) where TKey : notnull =>
            source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Offset(offset));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="ticks"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> OffsetTicks<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, long ticks) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetTicks(ticks));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="microseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> OffsetMicroseconds<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, double microseconds) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetMicroseconds(microseconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="milliseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> OffsetMilliseconds<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, double milliseconds) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetMilliseconds(milliseconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="seconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> OffsetSeconds<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, double seconds) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetSeconds(seconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="minutes"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> OffsetMinutes<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, double minutes) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetMinutes(minutes));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="hours"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> OffsetHours<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, double hours) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetHours(hours));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="days"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> OffsetDays<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, double days) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.OffsetDays(days));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, TimeSpan maxDeviation) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, int seed, TimeSpan maxDeviation) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(seed, maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, int seed, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(seed, maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, int seed, TimeSpan maxDeviationBefore,
        TimeSpan maxDeviationAfter, Func<int, double> randomFunc) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Randomize(seed, maxDeviationBefore, maxDeviationAfter, randomFunc));

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending with instants in the timelines in <paramref name="source"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> AsConsecutivePeriodTimelines<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.AsConsecutivePeriodTimeline());
}