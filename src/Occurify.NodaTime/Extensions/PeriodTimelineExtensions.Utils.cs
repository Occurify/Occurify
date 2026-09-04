using System.Diagnostics.CodeAnalysis;
using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IPeriodTimeline"/>.
/// </summary>
public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the periods on <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant(this IPeriodTimeline source, Instant instant) =>
        source.ContainsInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Determines whether <paramref name="interval"/> is included in any of the periods on <paramref name="source"/>.
    /// </summary>
    public static bool ContainsPeriod(this IPeriodTimeline source, Interval interval) =>
        source.ContainsPeriod(interval.ToPeriod());

    /// <summary>
    /// Determines whether any of the periods on <paramref name="source"/> is exactly the same as <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsExactPeriod(this IPeriodTimeline source, Interval interval) =>
        source.ContainsExactPeriod(interval.ToPeriod());

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
        source.GetNextPeriodIncludingPartial(instant.ToDateTimeUtc());

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

    /// <summary>
    /// Returns the first complete interval on <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetPreviousCompleteInterval(this IPeriodTimeline source, Instant instant) =>
        source.GetPreviousCompletePeriod(instant)?.ToInterval();

    /// <summary>
    /// Returns the first interval on <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetPreviousIntervalIncludingPartial(this IPeriodTimeline source, Instant instant) =>
        source.GetPreviousPeriodIncludingPartial(instant)?.ToInterval();

    /// <summary>
    /// Returns the first complete interval on <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetNextCompleteInterval(this IPeriodTimeline source, Instant instant) =>
        source.GetNextCompletePeriod(instant)?.ToInterval();

    /// <summary>
    /// Returns the first interval on <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetNextIntervalIncludingPartial(this IPeriodTimeline source, Instant instant) =>
        source.GetNextPeriodIncludingPartial(instant)?.ToInterval();
}
