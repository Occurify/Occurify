using NodaTime;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IEnumerable{IPeriodTimeline}"/>.
/// </summary>
public static partial class PeriodTimelineCollectionExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant(this IEnumerable<IPeriodTimeline> source, Instant instant)
    {
        return source.Any(pp => pp.ContainsInstant(instant));
    }

    /// <summary>
    /// Determines whether <paramref name="interval"/> is included in any of the intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInterval(this IEnumerable<IPeriodTimeline> source, Interval interval)
    {
        return source.Any(pp => pp.ContainsInterval(interval));
    }

    /// <summary>
    /// Determines whether any of the intervals in the timelines in <paramref name="source"/> is exactly <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsExactInterval(this IEnumerable<IPeriodTimeline> source, Interval interval)
    {
        return source.Any(pp => pp.ContainsExactInterval(interval));
    }

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousCompletePeriod(this IEnumerable<IPeriodTimeline> source, Instant instant)
    {
        return source.Max(tl => tl.GetPreviousCompletePeriod(instant));
    }

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetPreviousCompleteInterval(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.GetPreviousCompletePeriod(instant)?.ToInterval();

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousPeriodIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.EnumerateBackwardsFromIncludingPartial(instant).FirstOrDefault();

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetPreviousIntervalIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.GetPreviousPeriodIncludingPartial(instant)?.ToInterval();

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextCompletePeriod(this IEnumerable<IPeriodTimeline> source, Instant instant)
    {
        return source.Min(tl => tl.GetNextCompletePeriod(instant));
    }

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetNextCompleteInterval(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.GetNextCompletePeriod(instant)?.ToInterval();

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextPeriodIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.EnumerateFromIncludingPartial(instant).FirstOrDefault();

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetNextIntervalIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.GetNextPeriodIncludingPartial(instant)?.ToInterval();

    /// <summary>
    /// Returns the timelines in <paramref name="source"/> that contain an interval that is exactly <paramref name="interval"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> GetTimelinesAtExactInterval(this IEnumerable<IPeriodTimeline> source, Interval interval) =>
        source.Where(ptl => ptl.ContainsExactInterval(interval));

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, IPeriodTimeline[]> GetTimelinesAtPreviousCompletePeriod(this IEnumerable<IPeriodTimeline> source, Instant instant)
    {
        source = source.ToArray();
        var previousCompletePeriod = source.GetPreviousCompletePeriod(instant);
        if (previousCompletePeriod == null)
        {
            return new KeyValuePair<Period?, IPeriodTimeline[]>(null, Array.Empty<IPeriodTimeline>());
        }
        return new KeyValuePair<Period?, IPeriodTimeline[]>(previousCompletePeriod, source.GetTimelinesAtExactPeriod(previousCompletePeriod).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, IPeriodTimeline[]> GetTimelinesAtPreviousCompleteInterval(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.GetTimelinesAtPreviousCompletePeriod(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, IPeriodTimeline[]> GetTimelinesAtPreviousPeriodIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant instant)
    {
        source = source.ToArray();
        var previousPeriodIncludingPartial = source.GetPreviousPeriodIncludingPartial(instant);
        if (previousPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, IPeriodTimeline[]>(null, Array.Empty<IPeriodTimeline>());
        }
        return new KeyValuePair<Period?, IPeriodTimeline[]>(previousPeriodIncludingPartial, source.GetTimelinesAtExactPeriod(previousPeriodIncludingPartial).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, IPeriodTimeline[]> GetTimelinesAtPreviousIntervalIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.GetTimelinesAtPreviousPeriodIncludingPartial(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, IPeriodTimeline[]> GetTimelinesAtNextCompletePeriod(this IEnumerable<IPeriodTimeline> source, Instant instant)
    {
        source = source.ToArray();
        var nextCompletePeriod = source.GetNextCompletePeriod(instant);
        if (nextCompletePeriod == null)
        {
            return new KeyValuePair<Period?, IPeriodTimeline[]>(null, Array.Empty<IPeriodTimeline>());
        }
        return new KeyValuePair<Period?, IPeriodTimeline[]>(nextCompletePeriod, source.GetTimelinesAtExactPeriod(nextCompletePeriod).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, IPeriodTimeline[]> GetTimelinesAtNextCompleteInterval(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.GetTimelinesAtNextCompletePeriod(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, IPeriodTimeline[]> GetTimelinesAtNextPeriodIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant instant)
    {
        source = source.ToArray();
        var nextPeriodIncludingPartial = source.GetNextPeriodIncludingPartial(instant);
        if (nextPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, IPeriodTimeline[]>(null, Array.Empty<IPeriodTimeline>());
        }
        return new KeyValuePair<Period?, IPeriodTimeline[]>(nextPeriodIncludingPartial, source.GetTimelinesAtExactPeriod(nextPeriodIncludingPartial).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, IPeriodTimeline[]> GetTimelinesAtNextIntervalIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.GetTimelinesAtNextPeriodIncludingPartial(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Takes a sample of the timelines in <paramref name="source"/> at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<PeriodTimelineSample> SampleAt(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
            source.Select(tl => tl.SampleAt(instant));

    internal static KeyValuePair<Interval?, TKey> ConvertPeriodKvpToIntervalKvp<TKey>(this KeyValuePair<Period?, TKey> periodKvp)
    {
        if (periodKvp.Key == null)
        {
            return new KeyValuePair<Interval?, TKey>(null, periodKvp.Value);
        }
        return new KeyValuePair<Interval?, TKey>(periodKvp.Key.ToInterval(), periodKvp.Value);
    }
}