using System.Collections.Generic;
using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on <paramref name="period"/>.
    /// </summary>
    public static bool ContainsInstant(this Period period, DateTime instant) =>
        (period.Start == null || instant >= period.Start) &&
        (period.End == null || instant < period.End);

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool ContainsInstant(this IEnumerable<Period> periods, DateTime instant) =>
        periods.Any(p => p.ContainsInstant(instant));

    /// <summary>
    /// Determines whether any of <paramref name="instants"/> is on <paramref name="period"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this Period period, IEnumerable<DateTime> instants) =>
        instants.Any(period.ContainsInstant);

    /// <summary>
    /// Determines whether any instant on <paramref name="timeline"/> is on <paramref name="period"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this Period period, ITimeline timeline)
    {
        if (period.IsInfiniteInBothDirections)
        {
            return !timeline.IsEmpty();
        }

        if (period.End == null)
        {
            var currentOrPrevious = timeline.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
            return currentOrPrevious != null && currentOrPrevious >= period.Start;
        }

        var currentOrNext = timeline.GetCurrentOrNextUtcInstant(period.Start ?? DateTimeHelper.MinValueUtc);
        return currentOrNext != null && currentOrNext < period.End;
    }

    /// <summary>
    /// Determines whether a period starting at <paramref name="periodStart"/> and ending at <paramref name="periodEnd"/> is included in <paramref name="period"/>.
    /// </summary>
    public static bool ContainsPeriod(this Period period, DateTime? periodStart, DateTime? periodEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        period.ContainsPeriod(periodStart.To(periodEnd), periodIncludeOptions);

    /// <summary>
    /// Determines whether <paramref name="otherPeriod"/> is included in <paramref name="period"/>.
    /// </summary>
    public static bool ContainsPeriod(this Period period, Period otherPeriod, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
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
    /// Determines whether <paramref name="period"/> is included in any of the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool ContainsPeriod(this IEnumerable<Period> periods, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        periods.Any(p => p.ContainsPeriod(period, periodIncludeOptions));

    /// <summary>
    /// Determines whether <paramref name="instant"/> is not on <paramref name="period"/>.
    /// </summary>
    public static bool Excludes(this Period period, DateTime instant) => !period.ContainsInstant(instant);

    /// <summary>
    /// Determines whether <paramref name="instant"/> is excluded by all the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool Excludes(this IEnumerable<Period> periods, DateTime instant) => periods.All(p => p.Excludes(instant));

    /// <summary>
    /// Determines whether <paramref name="otherPeriod"/> is excluded by <paramref name="period"/>.
    /// </summary>
    public static bool Excludes(this Period period, Period otherPeriod) => !period.ContainsPeriod(otherPeriod, PeriodIncludeOptions.PartialAllowed);

    /// <summary>
    /// Determines whether <paramref name="period"/> is excluded by all the periods in <paramref name="period"/>.
    /// </summary>
    public static bool Excludes(this IEnumerable<Period> periods, Period period) =>
        periods.All(p => p.Excludes(period));

    internal static bool ContainsEnd(this Period period, DateTime endOfPeriod) =>
        (period.Start == null || endOfPeriod > period.Start) &&
        (period.End == null || endOfPeriod <= period.End);

    /// <summary>
    /// Calculates the total duration of <paramref name="periods"/>.
    /// </summary>
    /// <param name="periods"></param>
    /// <param name="mergeOverlapping">When set to <c>true</c>, overlapping parts of periods will be counted only once.</param>
    public static TimeSpan? TotalDuration(this IEnumerable<Period> periods, bool mergeOverlapping = false)
    {
        if (mergeOverlapping)
        {
            return periods.AsPeriodTimeline().Aggregate((TimeSpan?)TimeSpan.Zero, (sum, p) =>
            {
                if (sum == null || p.Duration == null)
                {
                    return null;
                }

                return sum.Value + p.Duration.Value;
            });
        }
        return periods.Aggregate((TimeSpan?)TimeSpan.Zero, (sum, p) =>
        {
            if (sum == null || p.Duration == null)
            {
                return null;
            }

            return sum.Value + p.Duration.Value;
        });
    }
}