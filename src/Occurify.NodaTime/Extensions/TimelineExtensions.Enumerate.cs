using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class TimelineExtensions
{
    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstants(this ITimeline source) =>
        source.Enumerate().Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsBackwards(this ITimeline source) =>
        source.EnumerateBackwards().Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="start"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsFrom(this ITimeline source, Instant start) =>
        source.EnumerateFrom(start.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="end"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsBackwardsTo(this ITimeline source, Instant end) =>
        source.EnumerateBackwardsTo(end.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="end"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsTo(this ITimeline source, Instant end) =>
        source.EnumerateTo(end.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="start"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsBackwardsFrom(this ITimeline source, Instant start) =>
        source.EnumerateBackwardsFrom(start.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="start"/> and <paramref name="end"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantRange(this ITimeline source, Instant start, Instant end) =>
        source.EnumerateRange(start.ToDateTimeUtc(), end.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="start"/> and <paramref name="end"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantRangeBackwards(this ITimeline source, Instant start, Instant end) =>
        source.EnumerateRangeBackwards(start.ToDateTimeUtc(), end.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="interval"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstants(this ITimeline source, Interval interval) =>
        source.EnumeratePeriod(interval.ToPeriod()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="interval"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsBackwards(this ITimeline source, Interval interval) =>
        source.EnumeratePeriodBackwards(interval.ToPeriod()).Select(i => i.ToInstant());
}
