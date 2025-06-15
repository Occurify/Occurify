using NodaTime;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IEnumerable{Period}"/>.
/// </summary>
public static partial class PeriodCollectionExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool ContainsInstant(this IEnumerable<Period> periods, Instant instant) =>
        periods.Any(p => p.ContainsInstant(instant));

    /// <summary>
    /// Determines whether <paramref name="instants"/> is on any of the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool ContainsAnyInstant(this IEnumerable<Period> periods, IEnumerable<Instant> instants) =>
        periods.Any(p => p.ContainsAnyInstant(instants));

    /// <summary>
    /// Determines whether a period starting at <paramref name="periodStart"/> and ending at <paramref name="periodEnd"/> is included in any of the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool ContainsPeriod(this IEnumerable<Period> periods, Instant? periodStart, Instant? periodEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly) =>
        periods.Any(p => p.ContainsPeriod(periodStart, periodEnd, periodIncludeOptions));

    /// <summary>
    /// Determines whether <paramref name="instant"/> is excluded by all the periods in <paramref name="periods"/>.
    /// </summary>
    public static bool Excludes(this IEnumerable<Period> periods, Instant instant) => periods.All(p => p.Excludes(instant));
}