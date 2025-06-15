
using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class InstantExtensions
{
    /// <summary>
    /// Combines <paramref name="source"/> with <paramref name="instant"/> into a <see cref="ITimeline"/>.
    /// </summary>
    public static ITimeline Combine(this Instant source, Instant instant) => source.AsTimeline().Combine(instant);

    /// <summary>
    /// Combines <paramref name="source"/> with <paramref name="instants"/> into a <see cref="ITimeline"/>.
    /// </summary>
    public static ITimeline Combine(this Instant source, IEnumerable<Instant> instants) => source.AsTimeline().Combine(instants);

    /// <summary>
    /// Combines <paramref name="source"/> with <paramref name="instants"/> into a <see cref="ITimeline"/>.
    /// </summary>
    public static ITimeline Combine(this Instant source, params Instant[] instants) => source.AsTimeline().Combine(instants);

    /// <summary>
    /// Combines <paramref name="source"/> with <paramref name="timelines"/> into a <see cref="ITimeline"/>.
    /// </summary>
    public static ITimeline Combine(this Instant source, IEnumerable<ITimeline> timelines) => source.AsTimeline().Combine(timelines);

    /// <summary>
    /// Combines <paramref name="source"/> with <paramref name="timelines"/> into a <see cref="ITimeline"/>.
    /// </summary>
    public static ITimeline Combine(this Instant source, params ITimeline[] timelines) => source.AsTimeline().Combine(timelines);

    /// <summary>
    /// Returns a <c>Period</c> starting at <paramref name="start"/> and ending with <paramref name="end"/>.
    /// <c>null</c> as <paramref name="end"/> means the period never ends.
    /// </summary>
    public static Period To(this Instant start, Instant? end) => Period.Create(start, end);

    /// <summary>
    /// Returns a <c>Period</c> starting at <paramref name="start"/> and ending with <paramref name="end"/>.
    /// <c>null</c> as <paramref name="start"/> means the period has always started.
    /// <c>null</c> as <paramref name="end"/> means the period never ends.
    /// </summary>
    public static Period To(this Instant? start, Instant? end) => Period.Create(start, end);

    /// <summary>
    /// Returns a <c>Period</c> starting at <paramref name="start"/> with duration <paramref name="duration"/>.
    /// If <paramref name="start"/> + <paramref name="duration"/> overflows <c>Instant.MaxValue</c>, period end will be set to <c>null</c>, meaning the period never ends.
    /// </summary>
    public static Period ToPeriodWithDuration(this Instant start, Duration duration) => Period.Create(start, duration);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with a single instant <paramref name="instant"/>.
    /// </summary>
    public static ITimeline AsTimeline(this Instant instant) => Timeline.FromInstant(instant);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with a single instant <paramref name="instant"/>.
    /// If <paramref name="instant"/> is <c>null</c>, an empty timeline is returned.
    /// </summary>
    public static ITimeline AsTimeline(this Instant? instant) => Timeline.FromInstant(instant);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with two periods: One from <c>null</c> to <paramref name="source"/> and one from <paramref name="source"/> to <c>null</c>.
    /// </summary>
    public static IPeriodTimeline AsConsecutivePeriodTimeline(this Instant source) => PeriodTimeline.FromInstantAsConsecutive(source);
}