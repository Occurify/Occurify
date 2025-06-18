using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IPeriodTimeline"/>.
/// </summary>
public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the intervals in <paramref name="intervalTimeline"/>.
    /// </summary>
    public static bool ContainsInstant(this IPeriodTimeline intervalTimeline, Instant instant) =>
        intervalTimeline.ContainsInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Determines whether <paramref name="interval"/> is included in any of the intervals in <paramref name="intervalTimeline"/>.
    /// </summary>
    public static bool ContainsInterval(this IPeriodTimeline intervalTimeline, Interval interval) =>
        intervalTimeline.ContainsPeriod(interval.ToPeriod());

    /// <summary>
    /// Determines whether any of the intervals in <paramref name="intervalTimeline"/> is exactly the same as <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsExactInterval(this IPeriodTimeline intervalTimeline, Interval interval) =>
        intervalTimeline.ContainsExactPeriod(interval.ToPeriod());

    /// <summary>
    /// Returns the first complete period on <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousCompletePeriod(this IPeriodTimeline source, Instant instant) =>
        source.GetPreviousCompletePeriod(instant.ToDateTimeUtc());

    /// <summary>
    /// Returns the first complete period on <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousPeriodIncludingPartial(this IPeriodTimeline source, Instant instant) =>
        source.GetPreviousPeriodIncludingPartial(instant.ToDateTimeUtc());

    /// <summary>
    /// Returns the first complete period on <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextCompletePeriod(this IPeriodTimeline source, Instant instant) =>
        source.GetNextCompletePeriod(instant.ToDateTimeUtc());

    /// <summary>
    /// Returns the first complete period on <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextPeriodIncludingPartial(this IPeriodTimeline source, Instant instant) =>
        source.GetNextCompletePeriod(instant.ToDateTimeUtc());

    /// <summary>
    /// Takes a sample of <paramref name="source"/> at <paramref name="instant"/>.
    /// </summary>
    public static PeriodTimelineSample SampleAt(this IPeriodTimeline source, Instant instant) =>
        source.SampleAt(instant.ToDateTimeUtc());

    /// <summary>
    /// Gets the period at <paramref name="instant"/>. If no period is at <paramref name="instant"/>, false is returned and <paramref name="period"/> is <c>null</c>.
    /// </summary>
    public static bool TryGetPeriod(this IPeriodTimeline source, Instant instant, [NotNullWhen(true)] out Period? period) =>
        source.TryGetPeriod(instant.ToDateTimeUtc(), out period);
}