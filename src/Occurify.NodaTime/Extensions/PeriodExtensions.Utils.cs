using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="Period"/>.
/// </summary>
public static partial class PeriodExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on <paramref name="period"/>.
    /// </summary>
    public static bool ContainsInstant(this Period period, Instant instant) =>
        (period.Start == null || instant >= period.Start) &&
        (period.End == null || instant < period.End);

    /// <summary>
    /// Determines whether any of <paramref name="instants"/> is on <paramref name="period"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this Period period, IEnumerable<Instant> instants) =>
        instants.Any(period.ContainsInstant);

    /// <summary>
    /// Determines whether a period starting at <paramref name="periodStart"/> and ending at <paramref name="periodEnd"/> is included in <paramref name="period"/>.
    /// </summary>
    public static bool ContainsInterval(this Period period, Instant? periodStart, Instant? periodEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        period.ContainsPeriod(periodStart.To(periodEnd), periodIncludeOptions);

    /// <summary>
    /// Determines whether <paramref name="otherPeriod"/> is included in <paramref name="period"/>.
    /// </summary>
    public static bool ContainsInterval(this Period period, Interval interval, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        var startIsInPeriod =
            (period.Start == null || (otherPeriod.Start != null && otherPeriod.Start >= period.Start)) &&
            (period.End == null || otherPeriod.Start == null || otherPeriod.Start < period.End);
        var endIsInPeriod =
            (period.End == null || (otherPeriod.End != null && otherPeriod.End <= period.End)) &&
            (period.Start == null || otherPeriod.End == null || otherPeriod.End > period.Start);

        switch (periodIncludeOptions)
        {
            case PeriodIncludeOptions.CompleteOnly:
                return startIsInPeriod && endIsInPeriod;
            case PeriodIncludeOptions.StartPartialAllowed:
                return endIsInPeriod;
            case PeriodIncludeOptions.EndPartialAllowed:
                return startIsInPeriod;
            case PeriodIncludeOptions.PartialAllowed:
                return startIsInPeriod || endIsInPeriod || otherPeriod.ContainsPeriod(period);
            default:
                throw new ArgumentOutOfRangeException(nameof(periodIncludeOptions), periodIncludeOptions, null);
        }
    }

    /// <summary>
    /// Determines whether <paramref name="instant"/> is not on <paramref name="period"/>.
    /// </summary>
    public static bool Excludes(this Period period, Instant instant) => !period.ContainsInstant(instant);
}