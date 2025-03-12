using Occurify.TimelineTransformations;

namespace Occurify.Extensions;

public static partial class TimelineExtensions
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instant"/>.
    /// </summary>
    public static ITimeline Combine(this ITimeline source, DateTime instant) => source.Combine(instant.AsTimeline());

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instants"/>.
    /// </summary>
    public static ITimeline Combine(this ITimeline source, IEnumerable<DateTime> instants) =>
        source.Combine(instants.AsTimeline());

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instants"/>.
    /// </summary>
    public static ITimeline Combine(this ITimeline source, params DateTime[] instants) =>
        source.Combine(instants.AsTimeline());

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="timeline"/>.
    /// </summary>
    public static ITimeline Combine(this ITimeline source, ITimeline timeline) =>
        new CompositeTimeline(timeline);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="timelines"/>.
    /// </summary>
    public static ITimeline Combine(this ITimeline source, IEnumerable<ITimeline> timelines) =>
        new CompositeTimeline(timelines.Prepend(source));

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="timelines"/>.
    /// </summary>
    public static ITimeline Combine(this ITimeline source, params ITimeline[] timelines) =>
        new CompositeTimeline(timelines.Prepend(source));

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from all <paramref name="timelines"/>.
    /// </summary>
    public static ITimeline Combine(this IEnumerable<ITimeline> timelines) => new CompositeTimeline(timelines);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline Offset(this ITimeline source, TimeSpan offset) => new OffsetTimeline(source, offset);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="ticks"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline OffsetTicks(this ITimeline source, long ticks) => source.Offset(TimeSpan.FromTicks(ticks));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="microseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline OffsetMicroseconds(this ITimeline source, double microseconds) =>
        source.Offset(TimeSpan.FromMicroseconds(microseconds));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="milliseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline OffsetMilliseconds(this ITimeline source, double milliseconds) =>
        source.Offset(TimeSpan.FromMilliseconds(milliseconds));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="seconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline OffsetSeconds(this ITimeline source, double seconds) =>
        source.Offset(TimeSpan.FromSeconds(seconds));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="minutes"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline OffsetMinutes(this ITimeline source, double minutes) =>
        source.Offset(TimeSpan.FromMinutes(minutes));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="hours"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline OffsetHours(this ITimeline source, double hours) =>
        source.Offset(TimeSpan.FromHours(hours));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="days"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline OffsetDays(this ITimeline source, double days) => source.Offset(TimeSpan.FromDays(days));

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants.
    /// </summary>
    public static ITimeline Randomize(this ITimeline source, TimeSpan maxDeviation) =>
        source.Randomize(new Random().Next(), maxDeviation, maxDeviation, s => new Random(s).NextDouble());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static ITimeline Randomize(this ITimeline source, int seed, TimeSpan maxDeviation) =>
        source.Randomize(seed, maxDeviation, maxDeviation, s => new Random(s).NextDouble());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants.
    /// </summary>
    public static ITimeline Randomize(this ITimeline source, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter) =>
        source.Randomize(new Random().Next(), maxDeviationBefore, maxDeviationAfter, s => new Random(s).NextDouble());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of instant count or in overlapping instants.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static ITimeline Randomize(this ITimeline source, int seed, TimeSpan maxDeviationBefore,
        TimeSpan maxDeviationAfter) =>
        source.Randomize(seed, maxDeviationBefore, maxDeviationAfter, s => new Random(s).NextDouble());

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of instant count or in overlapping instants.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static ITimeline Randomize(this ITimeline source, int seed, TimeSpan maxDeviationBefore,
        TimeSpan maxDeviationAfter, Func<int, double> randomFunc) =>
        new RandomizedTimeline(source, seed, maxDeviationBefore, maxDeviationAfter, randomFunc);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="periodStartTimeline"/> and ending with <paramref name="periodEndTimeline"/>.
    /// The result is Normalized.
    /// </summary>
    public static IPeriodTimeline To(this ITimeline periodStartTimeline, ITimeline periodEndTimeline) =>
        PeriodTimeline.Between(periodStartTimeline, periodEndTimeline);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="periodStartTimeline"/> and ending with <paramref name="periodEndInstants"/>.
    /// The result is Normalized.
    /// </summary>
    public static IPeriodTimeline To(this ITimeline periodStartTimeline, IEnumerable<DateTime> periodEndInstants) =>
        PeriodTimeline.Between(periodStartTimeline, periodEndInstants.AsTimeline());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="periodStartTimeline"/> and ending with <paramref name="periodEndInstants"/>.
    /// The result is Normalized.
    /// </summary>
    public static IPeriodTimeline To(this ITimeline periodStartTimeline, params DateTime[] periodEndInstants) =>
        PeriodTimeline.Between(periodStartTimeline, periodEndInstants.AsTimeline());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending with instants in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline AsConsecutivePeriodTimeline(this ITimeline source) =>
        PeriodTimeline.FromInstantsAsConsecutive(source);
}