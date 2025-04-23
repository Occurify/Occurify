namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IEnumerable{IPeriodTimeline}"/>.
/// </summary>
public static partial class PeriodTimelineCollectionExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant(this IEnumerable<IPeriodTimeline> source, DateTime instant)
    {
        return source.Any(pp => pp.ContainsInstant(instant));
    }

    /// <summary>
    /// Determines whether <paramref name="period"/> is included in any of the periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsPeriod(this IEnumerable<IPeriodTimeline> source, Period period)
    {
        return source.Any(pp => pp.ContainsPeriod(period));
    }

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousCompletePeriod(this IEnumerable<IPeriodTimeline> source, DateTime instant)
    {
        return source.Max(tl => tl.GetPreviousCompletePeriod(instant));
    }

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousPeriodIncludingPartial(this IEnumerable<IPeriodTimeline> source, DateTime instant) =>
        source.EnumerateBackwardsFromIncludingPartial(instant).FirstOrDefault();

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextCompletePeriod(this IEnumerable<IPeriodTimeline> source, DateTime instant)
    {
        return source.Min(tl => tl.GetNextCompletePeriod(instant));
    }

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextPeriodIncludingPartial(this IEnumerable<IPeriodTimeline> source, DateTime instant) =>
        source.EnumerateFromIncludingPartial(instant).FirstOrDefault();

    /// <summary>
    /// Returns whether all the timelines in <paramref name="source"/> is empty.
    /// </summary>
    public static bool AreEmpty(this IEnumerable<IPeriodTimeline> source) => source.All(s => s.IsEmpty());

    /// <summary>
    /// Returns whether <c>DateTime.UtcNow</c> is inside any period on the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool IsNow(this IEnumerable<IPeriodTimeline> source) => source.ContainsInstant(DateTime.UtcNow);

    /// <summary>
    /// Takes a sample of the timelines in <paramref name="source"/> at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<PeriodTimelineSample> SampleAt(this IEnumerable<IPeriodTimeline> source, DateTime instant) =>
            source.Select(tl => tl.SampleAt(instant));

    /// <summary>
    /// Synchronizes all <paramref name="source"/> such that method calls cannot occur concurrently.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Synchronize(this IEnumerable<IPeriodTimeline> source) => source.Synchronize(new());

    /// <summary>
    /// Synchronizes all <paramref name="source"/> such that method calls cannot occur concurrently.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Synchronize(this IEnumerable<IPeriodTimeline> source, object gate) =>
        source.Select(s => s.Synchronize(gate));

    /// <summary>
    /// Calculates the total duration of the timelines in <paramref name="source"/>.
    /// If <paramref name="addIndividualTimelineDurations"/> is <c>true</c>, the total duration is calculated by summing the durations of all individual timelines. If <c>false</c>, the total duration of the merged timelines is calculated.
    /// </summary>
    public static TimeSpan? TotalDuration(this IEnumerable<IPeriodTimeline> source, bool addIndividualTimelineDurations = false)
    {
        if (addIndividualTimelineDurations)
        {
            return source.Aggregate((TimeSpan?)TimeSpan.Zero, (sum, p) =>
            {
                if (sum == null)
                {
                    return null;
                }

                var duration = p.TotalDuration();
                if (duration == null)
                {
                    return null;
                }

                // Note: Even though unlikely due to the range of TimeSpan (which is the range of a long), given enough period timelines it is possible to overflow the range of TimeSpan.
                // Therefor we need to check for overflow here.
                return sum.Value.AddOrNullOnOverflow(duration.Value);
            });
        }
        return source.Merge().TotalDuration();
    }
}