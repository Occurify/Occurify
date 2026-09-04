using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class TimelineKeyCollectionExtensions
{
    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Offset<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Duration offset) where TKey : notnull =>
        source.Offset(offset.ToTimeSpan());

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Duration maxDeviation) where TKey : notnull =>
        source.Randomize(maxDeviation.ToTimeSpan());

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, int seed, Duration maxDeviation) where TKey : notnull =>
        source.Randomize(seed, maxDeviation.ToTimeSpan());

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Duration maxDeviationBefore, Duration maxDeviationAfter) where TKey : notnull =>
        source.Randomize(maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan());

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter) where TKey : notnull =>
        source.Randomize(seed, maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan());

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Randomize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter, Func<int, double> randomFunc) where TKey : notnull =>
        source.Randomize(seed, maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan(), randomFunc);
}
