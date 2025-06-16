using System.Diagnostics.CodeAnalysis;
using NodaTime;
using Occurify.Helpers;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IPeriodTimeline"/>.
/// </summary>
public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the periods in <paramref name="periodTimeline"/>.
    /// </summary>
    public static bool ContainsInstant(this IPeriodTimeline periodTimeline, Instant instant)
    {
        return periodTimeline.TryGetPeriod(instant, out var period) && period.ContainsInstant(instant);
    }

    /// <summary>
    /// Determines whether <paramref name="period"/> is included in any of the periods in <paramref name="periodTimeline"/>.
    /// </summary>
    public static bool ContainsPeriod(this IPeriodTimeline periodTimeline, Period period)
    {
        if (period.Start == null && period.End == null)
        {
            return false;
        }

        Period? mask;
        if (period.Start != null)
        {
            return periodTimeline.TryGetPeriod(period.Start!.Value, out mask) && mask.ContainsPeriod(period);
        }

        if (period.End!.Value == Instant.MinValue)
        {
            var firstStart = periodTimeline.StartTimeline.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc);
            var firstEnd = periodTimeline.EndTimeline.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc);
            if (firstEnd == null)
            {
                return false;
            }

            return firstStart == null || firstEnd <= firstStart;
        }

        return periodTimeline.TryGetPeriod(period.End!.Value.AddTicks(-1), out mask) && mask.ContainsPeriod(period);
    }

    /// <summary>
    /// Determines whether any of the periods in <paramref name="periodTimeline"/> is exactly the same as <paramref name="period"/>.
    /// </summary>
    public static bool ContainsExactPeriod(this IPeriodTimeline periodTimeline, Period period)
    {
        if (period.Start == null && period.End == null)
        {
            return false;
        }

        Period? foundPeriod;
        if (period.Start != null)
        {
            return periodTimeline.TryGetPeriod(period.Start!.Value, out foundPeriod) && foundPeriod == period;
        }

        if (period.End!.Value == Instant.MinValue)
        {
            return periodTimeline.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc);
        }

        return periodTimeline.TryGetPeriod(period.End!.Value.AddTicks(-1), out foundPeriod) && foundPeriod == period;
    }

    /// <summary>
    /// Returns the first complete period on <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousCompletePeriod(this IPeriodTimeline source, Instant instant)
    {
        var endOfPeriod = source.EndTimeline.GetCurrentOrPreviousUtcInstant(instant);
        if (endOfPeriod == null)
        {
            return null;
        }
        var startOfPeriod = source.StartTimeline.GetPreviousUtcInstant(endOfPeriod.Value);
        return Period.Create(startOfPeriod, endOfPeriod);
    }

    /// <summary>
    /// Returns the first complete period on <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousPeriodIncludingPartial(this IPeriodTimeline source, Instant instant) =>
        source.EnumerateBackwardsFromIncludingPartial(instant).FirstOrDefault();

    /// <summary>
    /// Returns the first complete period on <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextCompletePeriod(this IPeriodTimeline source, Instant instant)
    {
        var startOfPeriod = source.StartTimeline.GetCurrentOrNextUtcInstant(instant);
        if (startOfPeriod == null)
        {
            return null;
        }
        var endOfPeriod = source.EndTimeline.GetNextUtcInstant(startOfPeriod.Value);
        return Period.Create(startOfPeriod, endOfPeriod);
    }

    /// <summary>
    /// Returns the first complete period on <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextPeriodIncludingPartial(this IPeriodTimeline source, Instant instant) =>
        source.EnumerateFromIncludingPartial(instant).FirstOrDefault();

    /// <summary>
    /// Returns whether <paramref name="source"/> is empty.
    /// </summary>
    public static bool IsEmpty(this IPeriodTimeline source) =>
        source.StartTimeline.IsEmpty() &&
        source.EndTimeline.IsEmpty();

    /// <summary>
    /// Returns whether <c>Instant.UtcNow</c> is inside a period on <paramref name="source"/>.
    /// </summary>
    public static bool IsNow(this IPeriodTimeline source) => source.ContainsInstant(Instant.UtcNow);

    /// <summary>
    /// Takes a sample of <paramref name="source"/> at <paramref name="instant"/>.
    /// </summary>
    public static PeriodTimelineSample SampleAt(this IPeriodTimeline source, Instant instant)
    {
        Instant? nextStart, nextEnd;
        var previousStart = source.StartTimeline.GetCurrentOrPreviousUtcInstant(instant);
        var previousEnd = source.EndTimeline.GetCurrentOrPreviousUtcInstant(instant);
            
        if (previousStart == null && previousEnd == null)
        {
            nextStart = source.StartTimeline.GetNextUtcInstant(instant);
            nextEnd = source.EndTimeline.GetNextUtcInstant(instant);
            if (nextStart == null && nextEnd == null)
            {
                return new PeriodTimelineSample(instant, null, new Period(null, null));
            }

            if (nextEnd == null || (nextStart != null && nextStart < nextEnd))
            {
                return new PeriodTimelineSample(instant, null, new Period(null, nextStart));
            }

            return new PeriodTimelineSample(instant, new Period(null, nextEnd), null);
        }

        if (previousEnd == null || (previousStart != null && previousStart >= previousEnd))
        {
            nextEnd = source.EndTimeline.GetNextUtcInstant(instant);
            return new PeriodTimelineSample(instant, new Period(previousStart, nextEnd), null);
        }
        nextStart = source.StartTimeline.GetNextUtcInstant(instant);
        return new PeriodTimelineSample(instant, null, new Period(previousEnd, nextStart));
    }

    /// <summary>
    /// Gets the period at <paramref name="instant"/>. If no period is at <paramref name="instant"/>, false is returned and <paramref name="period"/> is <c>null</c>.
    /// </summary>
    public static bool TryGetPeriod(this IPeriodTimeline source, Instant instant, [NotNullWhen(true)] out Period? period)
    {
        var sample = source.SampleAt(instant);
        if (sample.IsGap)
        {
            period = null;
            return false;
        }

        period = sample.Period;
        return true;
    }

    /// <summary>
    /// Synchronizes <paramref name="source"/> such that method calls cannot occur concurrently.
    /// </summary>
    public static IPeriodTimeline Synchronize(this IPeriodTimeline source) => source.Synchronize(new());

    /// <summary>
    /// Synchronizes <paramref name="source"/> such that method calls cannot occur concurrently.
    /// </summary>
    public static IPeriodTimeline Synchronize(this IPeriodTimeline source, object gate)
    {
        return source.StartTimeline.Synchronize(gate).To(source.EndTimeline.Synchronize(gate));
    }

    /// <summary>
    /// Calculates the total duration of <paramref name="source"/>.
    /// </summary>
    public static Duration? TotalDuration(this IPeriodTimeline source) =>
        source.Aggregate((Duration?)Duration.Zero, (sum, p) =>
        {
            if (sum == null || p.Duration == null)
            {
                return null;
            }
            // Note: As a period timeline doesn't contain any overlapping periods, the total duration will never exceed the range of a Instant if no infinite periods are present.
            // As this is far smaller than the range of a Duration, we don't need to check for overflow here.
            return sum.Value + p.Duration.Value;
        });

    internal static bool ContainsEnd(this IPeriodTimeline periodTimeline, Instant endOfPeriod)
    {
        Period? period;
        if (endOfPeriod == Instant.MinValue)
        {
            return periodTimeline.EndTimeline.IsInstant(Instant.MinValue) ||
                   (periodTimeline.TryGetPeriod(endOfPeriod, out period) && period.ContainsEnd(endOfPeriod));
        }
        return periodTimeline.TryGetPeriod(endOfPeriod.AddTicks(-1), out period) && period.ContainsEnd(endOfPeriod);
    }
}