using NodaTime;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IEnumerable{Interval}"/>.
/// </summary>
public static partial class IntervalCollectionExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the intervals in <paramref name="intervals"/>.
    /// </summary>
    public static bool ContainsInstant(this IEnumerable<Interval> intervals, Instant instant) =>
        intervals.Any(p => p.Contains(instant));

    /// <summary>
    /// Determines whether <paramref name="instants"/> is on any of the intervals in <paramref name="intervals"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this IEnumerable<Interval> intervals, IEnumerable<Instant> instants) =>
        intervals.Any(p => instants.Any(p.Contains));

    /// <summary>
    /// Determines whether any instant on <paramref name="timeline"/> is on any of the intervals in <paramref name="intervals"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this IEnumerable<Interval> intervals, ITimeline timeline) =>
        intervals.Any(p => p.ContainsAnyInstant(timeline));

    /// <summary>
    /// Determines whether a interval starting at <paramref name="periodStart"/> and ending at <paramref name="periodEnd"/> is included in any of the intervals in <paramref name="intervals"/>.
    /// </summary>
    public static bool ContainsPeriod(this IEnumerable<Interval> intervals, Instant? periodStart, Instant? periodEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        intervals.Any(p => p.ContainsPeriod(periodStart, periodEnd, periodIncludeOptions));

    /// <summary>
    /// Determines whether <paramref name="interval"/> is included in any of the intervals in <paramref name="intervals"/>.
    /// </summary>
    public static bool ContainsPeriod(this IEnumerable<Interval> intervals, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        intervals.Any(p => p.ContainsPeriod(interval, periodIncludeOptions));

    /// <summary>
    /// Determines whether <paramref name="instant"/> is excluded by all the intervals in <paramref name="intervals"/>.
    /// </summary>
    public static bool Excludes(this IEnumerable<Interval> intervals, Instant instant) => intervals.All(p => p.Excludes(instant));

    /// <summary>
    /// Determines whether <paramref name="interval"/> is excluded by all the intervals in <paramref name="interval"/>.
    /// </summary>
    public static bool Excludes(this IEnumerable<Interval> intervals, Interval interval) =>
        intervals.All(p => p.Excludes(interval));
}