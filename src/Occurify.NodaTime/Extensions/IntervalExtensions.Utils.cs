using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="Interval"/>.
/// </summary>
public static partial class IntervalExtensions
{
    /// <summary>
    /// Converts a NodaTime <see cref="Interval"/> to an Occurify <see cref="Interval"/>.
    /// </summary>
    public static Interval ToPeriod(this Interval interval)
    {
        DateTime? start = interval.HasStart ? interval.Start.ToDateTimeUtc() : null;
        DateTime? end = interval.HasEnd ? interval.End.ToDateTimeUtc() : null;
        return new Interval(start, end);
    }

    /// <summary>
    /// Determines whether any instant on <paramref name="timeline"/> is on <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this Interval interval, ITimeline timeline)
    {
        if (interval.IsInfiniteInBothDirections)
        {
            return !timeline.IsEmpty();
        }

        if (interval.End == null)
        {
            var currentOrPrevious = timeline.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
            return currentOrPrevious != null && currentOrPrevious >= interval.Start;
        }

        var currentOrNext = timeline.GetCurrentOrNextUtcInstant(interval.Start ?? DateTimeHelper.MinValueUtc);
        return currentOrNext != null && currentOrNext < interval.End;
    }

    /// <summary>
    /// Determines whether a interval starting at <paramref name="periodStart"/> and ending at <paramref name="periodEnd"/> is included in <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsInterval(this Interval interval, Instant? periodStart, Instant? periodEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        interval.ContainsInterval(periodStart.To(periodEnd), periodIncludeOptions);

    /// <summary>
    /// Determines whether <paramref name="otherPeriod"/> is included in <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsInterval(this Interval interval, Interval otherPeriod, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        var startIsInPeriod =
            (interval.Start == null || (otherPeriod.Start != null && otherPeriod.Start >= interval.Start)) &&
            (interval.End == null || otherPeriod.Start == null || otherPeriod.Start < interval.End);
        var endIsInPeriod =
            (interval.End == null || (otherPeriod.End != null && otherPeriod.End <= interval.End)) &&
            (interval.Start == null || otherPeriod.End == null || otherPeriod.End > interval.Start);

        switch (periodIncludeOptions)
        {
            case PeriodIncludeOptions.CompleteOnly:
                return startIsInPeriod && endIsInPeriod;
            case PeriodIncludeOptions.StartPartialAllowed:
                return endIsInPeriod;
            case PeriodIncludeOptions.EndPartialAllowed:
                return startIsInPeriod;
            case PeriodIncludeOptions.PartialAllowed:
                return startIsInPeriod || endIsInPeriod || otherPeriod.ContainsPeriod(interval);
            default:
                throw new ArgumentOutOfRangeException(nameof(periodIncludeOptions), periodIncludeOptions, null);
        }
    }

    /// <summary>
    /// Determines whether <paramref name="instant"/> is not on <paramref name="interval"/>.
    /// </summary>
    public static bool Excludes(this Interval interval, Instant instant) => !interval.ContainsInstant(instant);

    /// <summary>
    /// Determines whether <paramref name="otherPeriod"/> is excluded by <paramref name="interval"/>.
    /// </summary>
    public static bool Excludes(this Interval interval, Interval otherPeriod) => !interval.ContainsPeriod(otherPeriod, PeriodIncludeOptions.PartialAllowed);
}