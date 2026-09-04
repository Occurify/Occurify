using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class TimelineCollectionExtensions
{
    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from earliest to latest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstants(this IEnumerable<ITimeline> source) =>
        source.Enumerate().Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from latest to earliest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsBackwards(this IEnumerable<ITimeline> source) =>
        source.EnumerateBackwards().Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="start"/> from earliest to latest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsFrom(this IEnumerable<ITimeline> source, Instant start) =>
        source.EnumerateFrom(start.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="end"/> from latest to earliest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsBackwardsTo(this IEnumerable<ITimeline> source, Instant end) =>
        source.EnumerateBackwardsTo(end.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="end"/> from earliest to latest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsTo(this IEnumerable<ITimeline> source, Instant end) =>
        source.EnumerateTo(end.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="start"/> from latest to earliest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsBackwardsFrom(this IEnumerable<ITimeline> source, Instant start) =>
        source.EnumerateBackwardsFrom(start.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="start"/> and <paramref name="end"/> from earliest to latest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantRange(this IEnumerable<ITimeline> source, Instant start, Instant end) =>
        source.EnumerateRange(start.ToDateTimeUtc(), end.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="start"/> and <paramref name="end"/> from latest to earliest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantRangeBackwards(this IEnumerable<ITimeline> source, Instant start, Instant end) =>
        source.EnumerateRangeBackwards(start.ToDateTimeUtc(), end.ToDateTimeUtc()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="interval"/> from earliest to latest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstants(this IEnumerable<ITimeline> source, Interval interval) =>
        source.EnumeratePeriod(interval.ToPeriod()).Select(i => i.ToInstant());

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="interval"/> from latest to earliest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<Instant> EnumerateInstantsBackwards(this IEnumerable<ITimeline> source, Interval interval) =>
        source.EnumeratePeriodBackwards(interval.ToPeriod()).Select(i => i.ToInstant());
}
