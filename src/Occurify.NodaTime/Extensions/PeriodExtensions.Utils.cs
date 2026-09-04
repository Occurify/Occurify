using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="Period"/>.
/// </summary>
public static partial class PeriodExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on <paramref name="period"/>.
    /// </summary>
    public static bool ContainsInstant(this Period period, Instant instant) => period.ContainsInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Determines whether any of <paramref name="instants"/> is on <paramref name="period"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this Period period, IEnumerable<Instant> instants) =>
        period.ContainsAnyInstant(instants.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Determines whether a period starting at <paramref name="periodStart"/> and ending at <paramref name="periodEnd"/> is included in <paramref name="period"/>.
    /// </summary>
    public static bool ContainsPeriod(this Period period, Instant? periodStart, Instant? periodEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        period.ContainsPeriod(periodStart?.ToDateTimeUtc(), periodEnd?.ToDateTimeUtc(), periodIncludeOptions);

    /// <summary>
    /// Determines whether <paramref name="interval"/> is included in <paramref name="period"/>.
    /// </summary>
    public static bool ContainsPeriod(this Period period, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        period.ContainsPeriod(interval.ToPeriod(), periodIncludeOptions);

    /// <summary>
    /// Determines whether <paramref name="instant"/> is not on <paramref name="period"/>.
    /// </summary>
    public static bool Excludes(this Period period, Instant instant) => period.Excludes(instant.ToDateTimeUtc());

    /// <summary>
    /// Determines whether <paramref name="interval"/> is excluded by <paramref name="period"/>.
    /// </summary>
    public static bool Excludes(this Period period, Interval interval) => period.Excludes(interval.ToPeriod());
}
