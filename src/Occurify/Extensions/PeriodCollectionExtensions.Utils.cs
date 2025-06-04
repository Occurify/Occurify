namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IEnumerable{Period}"/>.
/// </summary>
public static partial class PeriodCollectionExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool ContainsInstant(this IEnumerable<Period> periods, DateTime instant) =>
        periods.Any(p => p.ContainsInstant(instant));

    /// <summary>
    /// Determines whether <paramref name="instants"/> is on any of the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this IEnumerable<Period> periods, IEnumerable<DateTime> instants) =>
        periods.Any(p => p.ContainsAnyInstant(instants));

    /// <summary>
    /// Determines whether any instant on <paramref name="timeline"/> is on any of the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this IEnumerable<Period> periods, ITimeline timeline) =>
        periods.Any(p => p.ContainsAnyInstant(timeline));

    /// <summary>
    /// Determines whether a period starting at <paramref name="periodStart"/> and ending at <paramref name="periodEnd"/> is included in any of the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool ContainsPeriod(this IEnumerable<Period> periods, DateTime? periodStart, DateTime? periodEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        periods.Any(p => p.ContainsPeriod(periodStart, periodEnd, periodIncludeOptions));

    /// <summary>
    /// Determines whether <paramref name="period"/> is included in any of the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool ContainsPeriod(this IEnumerable<Period> periods, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        periods.Any(p => p.ContainsPeriod(period, periodIncludeOptions));

    /// <summary>
    /// Determines whether <paramref name="instant"/> is excluded by all the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool Excludes(this IEnumerable<Period> periods, DateTime instant) => periods.All(p => p.Excludes(instant));

    /// <summary>
    /// Determines whether <paramref name="period"/> is excluded by all the periods in <paramref name="period"/>.
    /// </summary>
    public static bool Excludes(this IEnumerable<Period> periods, Period period) =>
        periods.All(p => p.Excludes(period));

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