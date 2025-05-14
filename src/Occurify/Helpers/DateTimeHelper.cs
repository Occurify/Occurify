
using Occurify.Extensions;

namespace Occurify.Helpers;

internal static class DateTimeHelper
{
    internal static DateTime MinValueUtc = new (0L, DateTimeKind.Utc);
    internal static DateTime MaxValueUtc = DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);

    internal static DateTime GetEarliest(DateTime date1, DateTime date2)
    {
        return date1 < date2 ? date1 : date2;
    }

    internal static DateTime GetLatest(DateTime date1, DateTime date2)
    {
        return date1 > date2 ? date1 : date2;
    }

    internal static DateTime? MinAssumingNullIsPlusInfinity(DateTime? dateTime1, DateTime? dateTime2)
    {
        if (dateTime1 == null && dateTime2 == null)
        {
            return null;
        }

        if (dateTime1 != null && dateTime2 == null)
        {
            return dateTime1;
        }

        if (dateTime1 == null && dateTime2 != null)
        {
            return dateTime2;
        }

        return dateTime1 < dateTime2 ? dateTime1 : dateTime2;
    }

    internal static DateTime? MaxAssumingNullIsMinInfinity(DateTime? dateTime1, DateTime? dateTime2)
    {
        if (dateTime1 == null && dateTime2 == null)
        {
            return null;
        }

        if (dateTime1 != null && dateTime2 == null)
        {
            return dateTime1;
        }

        if (dateTime1 == null && dateTime2 != null)
        {
            return dateTime2;
        }

        return dateTime1 < dateTime2 ? dateTime2 : dateTime1;
    }

    /// <summary>
    /// Returns a date in between the provided start and end date based on the fraction.
    /// A fraction of 0 will return startDate and a fraction of 1 will return endDate.
    /// If null is provided as startDate, it is interpreted as DateTime.MinValue and if null is provided as endDate, DateTime.MaxValue will be returned if fraction is 1.
    /// </summary>
    internal static DateTime GetDateInBetween(DateTime? startDate, DateTime? endDate, double fraction)
    {
        var boundStartDate = startDate ?? MinValueUtc;
        var boundEndDate = endDate ?? MaxValueUtc;

        var timeSpan = boundEndDate - boundStartDate;
        var ticks = (long)Math.Round(timeSpan.Ticks * fraction, MidpointRounding.AwayFromZero);

        // Note: with very large numbers, we start getting problems with rounding. Therefor we have to check the cap ourselves.
        if (ticks > timeSpan.Ticks)
        {
            ticks = timeSpan.Ticks;
        }
        var newTimeSpan = TimeSpan.FromTicks(ticks);
        return boundStartDate.Add(newTimeSpan);
    }

    /// <summary>
    /// Returns a random instant between instant-_maxDeviationBefore and instant+_maxDeviationAfter.
    /// If however, the middle point between the provided boundary instants are closer to the instant, they will be used as boundaries for the randomization.
    /// Note that both boundaries are inclusive.
    /// </summary>
    internal static DateTime GetRandomDateTimeBetweenBoundaries(
        DateTime origin,
        TimeSpan maxDeviationBefore,
        TimeSpan maxDeviationAfter,
        DateTime? beforeBoundary, 
        DateTime? afterBoundary, 
        int seed,
        Func<int, double> randomFunc)
    {
        // Note: Currently this method does not support a random that "disappears" before DateTime.MinValue or past DateTime.MaxValue. This could be a possible improvement.
        // If doing that, it causes edge cases for period timelines with only a single start or a single end.

        var lowerBoundary = origin.AddOrNullOnOverflow(-maxDeviationBefore);
        if (beforeBoundary != null && maxDeviationBefore.Ticks != 0)
        {
            var beforeFraction = maxDeviationBefore.Ticks / (maxDeviationBefore.Ticks + (double)maxDeviationAfter.Ticks);
            var bound = GetDateInBetween(beforeBoundary.Value, origin, 1 - beforeFraction);
            lowerBoundary = lowerBoundary == null ? bound : GetLatest(bound, lowerBoundary.Value);
        }

        var upperBoundary = origin.AddOrNullOnOverflow(maxDeviationAfter);
        if (afterBoundary != null && maxDeviationAfter.Ticks != 0)
        {
            var afterFraction = maxDeviationAfter.Ticks / (maxDeviationBefore.Ticks + (double)maxDeviationAfter.Ticks);
            var bound = GetDateInBetween(origin, afterBoundary.Value, afterFraction);
            upperBoundary = upperBoundary == null ? bound : GetEarliest(bound, upperBoundary.Value);
        }

        var randomDouble = randomFunc(origin.GetHashCode() ^ seed);
        return GetDateInBetween(lowerBoundary, upperBoundary, randomDouble);
    }
}