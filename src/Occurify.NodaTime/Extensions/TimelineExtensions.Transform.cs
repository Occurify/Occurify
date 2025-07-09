using NodaTime;
using Occurify.NodaTime.Extensions;
using Occurify.TimelineTransformations;

namespace Occurify.Extensions;

public static partial class TimelineExtensions
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instant"/>.
    /// </summary>
    public static ITimeline Combine(this ITimeline source, Instant instant) =>
        source.Combine(instant.ToDateTimeUtc());

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instants"/>.
    /// </summary>
    public static ITimeline Combine(this ITimeline source, IEnumerable<Instant> instants) =>
        source.Combine(instants.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instants"/>.
    /// </summary>
    public static ITimeline Combine(this ITimeline source, params Instant[] instants) =>
        source.Combine(instants.Select(i => i.ToDateTimeUtc()).ToArray());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants.
    /// </summary>
    public static ITimeline Randomize(this ITimeline source, Duration maxDeviation) =>
        source.Randomize(maxDeviation.ToTimeSpan());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants.
    /// Identical inputs with the same seed will result in the same output.
    /// </summary>
    public static ITimeline Randomize(this ITimeline source, int seed, Duration maxDeviation) =>
        source.Randomize(seed, maxDeviation.ToTimeSpan());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants.
    /// </summary>
    public static ITimeline Randomize(this ITimeline source, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.Randomize(maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants.
    /// Identical inputs with the same <paramref name="seed"/> will result in the same output.
    /// </summary>
    public static ITimeline Randomize(this ITimeline source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter) =>
        source.Randomize(seed, maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of instant count or in overlapping instants.
    /// Identical inputs with the same <paramref name="seed"/> will result in the same output.
    /// </summary>
    public static ITimeline Randomize(this ITimeline source, int seed, Duration maxDeviationBefore, Duration maxDeviationAfter, Func<int, double> randomFunc) =>
        source.Randomize(seed, maxDeviationBefore.ToTimeSpan(), maxDeviationAfter.ToTimeSpan(), randomFunc);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="periodStartTimeline"/> and ending with <paramref name="periodEndInstants"/>.
    /// The result is Normalized.
    /// </summary>
    public static IPeriodTimeline To(this ITimeline periodStartTimeline, IEnumerable<Instant> periodEndInstants) =>
        periodStartTimeline.To(periodEndInstants.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="periodStartTimeline"/> and ending with <paramref name="periodEndInstants"/>.
    /// The result is Normalized.
    /// </summary>
    public static IPeriodTimeline To(this ITimeline periodStartTimeline, params Instant[] periodEndInstants) =>
        periodStartTimeline.To(periodEndInstants.Select(i => i.ToDateTimeUtc()).ToArray());
}