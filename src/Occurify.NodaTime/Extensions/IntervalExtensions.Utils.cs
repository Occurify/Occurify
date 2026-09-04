using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="Interval"/>.
/// </summary>
public static partial class IntervalExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsInstant(this Interval interval, Instant instant) => interval.Contains(instant);

    /// <summary>
    /// Determines whether any of <paramref name="instants"/> is on <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this Interval interval, IEnumerable<Instant> instants) => instants.Any(interval.Contains);

    /// <summary>
    /// Determines whether any instant on <paramref name="timeline"/> is on <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this Interval interval, ITimeline timeline) => interval.ToPeriod().ContainsAnyInstant(timeline);

    /// <summary>
    /// Determines whether an interval starting at <paramref name="intervalStart"/> and ending at <paramref name="intervalEnd"/> is included in <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsPeriod(this Interval interval, Instant? intervalStart, Instant? intervalEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        interval.ToPeriod().ContainsPeriod(intervalStart?.ToDateTimeUtc(), intervalEnd?.ToDateTimeUtc(), periodIncludeOptions);

    /// <summary>
    /// Determines whether <paramref name="otherInterval"/> is included in <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsPeriod(this Interval interval, Interval otherInterval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        interval.ToPeriod().ContainsPeriod(otherInterval.ToPeriod(), periodIncludeOptions);

    /// <summary>
    /// Determines whether <paramref name="instant"/> is not on <paramref name="interval"/>.
    /// </summary>
    public static bool Excludes(this Interval interval, Instant instant) => !interval.Contains(instant);

    /// <summary>
    /// Determines whether <paramref name="otherInterval"/> is excluded by <paramref name="interval"/>.
    /// </summary>
    public static bool Excludes(this Interval interval, Interval otherInterval) => interval.ToPeriod().Excludes(otherInterval.ToPeriod());
}
