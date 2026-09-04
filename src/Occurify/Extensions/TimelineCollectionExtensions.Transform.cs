using Occurify.TimelineTransformations;

namespace Occurify.Extensions;

public static partial class TimelineCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from all <paramref name="timelines"/>.
    /// </summary>
    public static ITimeline Combine(this IEnumerable<ITimeline> timelines) => AsCombinedTimeline(timelines);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from all <paramref name="timelines"/>.
    /// </summary>
    public static ITimeline AsCombinedTimeline(this IEnumerable<ITimeline> timelines) => new CompositeTimeline(timelines);

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<ITimeline> Offset(this IEnumerable<ITimeline> source, TimeSpan offset) =>
            source.Select(tl => tl.Offset(offset));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="ticks"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<ITimeline> OffsetTicks(this IEnumerable<ITimeline> source, long ticks) =>
        source.Select(tl => tl.OffsetTicks(ticks));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="microseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<ITimeline> OffsetMicroseconds(this IEnumerable<ITimeline> source, double microseconds) =>
        source.Select(tl => tl.OffsetMicroseconds(microseconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="milliseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<ITimeline> OffsetMilliseconds(this IEnumerable<ITimeline> source, double milliseconds) =>
        source.Select(tl => tl.OffsetMilliseconds(milliseconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="seconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<ITimeline> OffsetSeconds(this IEnumerable<ITimeline> source, double seconds) =>
        source.Select(tl => tl.OffsetSeconds(seconds));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="minutes"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<ITimeline> OffsetMinutes(this IEnumerable<ITimeline> source, double minutes) =>
        source.Select(tl => tl.OffsetMinutes(minutes));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="hours"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<ITimeline> OffsetHours(this IEnumerable<ITimeline> source, double hours) =>
        source.Select(tl => tl.OffsetHours(hours));

    /// <summary>
    /// Offsets the timelines in <paramref name="source"/> with <paramref name="days"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<ITimeline> OffsetDays(this IEnumerable<ITimeline> source, double days) =>
        source.Select(tl => tl.OffsetDays(days));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// </summary>
    public static IEnumerable<ITimeline> Randomize(this IEnumerable<ITimeline> source, TimeSpan maxDeviation) =>
        source.Select(tl => tl.Randomize(maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static IEnumerable<ITimeline> Randomize(this IEnumerable<ITimeline> source, int seed, TimeSpan maxDeviation) =>
        source.Select(tl => tl.Randomize(seed, maxDeviation));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// </summary>
    public static IEnumerable<ITimeline> Randomize(this IEnumerable<ITimeline> source, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) =>
        source.Select(tl => tl.Randomize(maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IEnumerable<ITimeline> Randomize(this IEnumerable<ITimeline> source, int seed, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) =>
        source.Select(tl => tl.Randomize(seed, maxDeviationBefore, maxDeviationAfter));

    /// <summary>
    /// Randomizes the timelines in <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of instant count or in overlapping instants in each timeline.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IEnumerable<ITimeline> Randomize(this IEnumerable<ITimeline> source, int seed, TimeSpan maxDeviationBefore,
        TimeSpan maxDeviationAfter, Func<int, double> randomFunc) =>
        source.Select(tl => tl.Randomize(seed, maxDeviationBefore, maxDeviationAfter, randomFunc));

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending with instants in the timelines in <paramref name="source"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> AsConsecutivePeriodTimelines(this IEnumerable<ITimeline> source) =>
        source.Select(tl => tl.AsConsecutivePeriodTimeline());
}