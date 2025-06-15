
namespace Occurify.Extensions;

public static partial class TimelineCollectionExtensions
{
    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from earliest to latest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<DateTime> Enumerate(this IEnumerable<ITimeline> source) =>
        source.Combine().Enumerate();

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from latest to earliest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateBackwards(this IEnumerable<ITimeline> source) =>
        source.Combine().EnumerateBackwards();

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="utcStart"/> from earliest to latest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateFrom(this IEnumerable<ITimeline> source, DateTime utcStart) =>
        source.Combine().EnumerateFrom(utcStart);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="utcEnd"/> from latest to earliest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateBackwardsTo(this IEnumerable<ITimeline> source, DateTime utcEnd)=>
        source.Combine().EnumerateBackwardsTo(utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="utcEnd"/> from earliest to latest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateTo(this IEnumerable<ITimeline> source, DateTime utcEnd) =>
        source.Combine().EnumerateTo(utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="utcStart"/> from latest to earliest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateBackwardsFrom(this IEnumerable<ITimeline> source, DateTime utcStart) =>
        source.Combine().EnumerateTo(utcStart);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateRange(this IEnumerable<ITimeline> source, DateTime utcStart, DateTime utcEnd) =>
        source.Combine().EnumerateRange(utcStart, utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateRangeBackwards(this IEnumerable<ITimeline> source, DateTime utcStart, DateTime utcEnd) =>
        source.Combine().EnumerateRangeBackwards(utcStart, utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="period"/> from earliest to latest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<DateTime> EnumeratePeriod(this IEnumerable<ITimeline> source, Period period) =>
        source.Combine().EnumeratePeriod(period);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="period"/> from latest to earliest.
    /// Duplicates are removed.
    /// </summary>
    public static IEnumerable<DateTime> EnumeratePeriodBackwards(this IEnumerable<ITimeline> source, Period period) =>
        source.Combine().EnumeratePeriodBackwards(period);
}