using NodaTime;
using Occurify.TimelineTransformations;

namespace Occurify.Extensions;

public static partial class TimelineCollectionExtensions
{
    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<ITimeline> Offset(this IEnumerable<ITimeline> source, Duration offset) =>
        source.Offset(offset.ToTimeSpan());

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// </summary>
    public static IEnumerable<ITimeline> Randomize(this IEnumerable<ITimeline> source, Duration maxDeviation) =>
        source.Randomize(maxDeviation.ToTimeSpan());

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static IEnumerable<ITimeline> Randomize(this IEnumerable<ITimeline> source, int seed, Duration maxDeviation) =>
        source.Randomize(seed, maxDeviation.ToTimeSpan());

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// </summary>
    public static IEnumerable<ITimeline> Randomize(this IEnumerable<ITimeline> source, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.Randomize(maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan());

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IEnumerable<ITimeline> Randomize(this IEnumerable<ITimeline> source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.Randomize(seed, maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan());

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IEnumerable<ITimeline> Randomize(this IEnumerable<ITimeline> source, int seed, Duration maxDeviationBefore,
        Duration maxDeviationAfter, Func<int, double> randomFunc) =>
        source.Randomize(seed, maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan(), randomFunc);
}