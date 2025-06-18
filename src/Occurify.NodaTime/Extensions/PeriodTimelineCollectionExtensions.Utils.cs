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
    public static bool ContainsPeriod(this IEnumerable<IPeriodTimeline> source, Interval interval)
    {
        return source.Any(pp => pp.ContainsInterval(interval));
    }

    /// <summary>
    /// Determines whether any of the intervals in the timelines in <paramref name="source"/> is exactly <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsExactPeriod(this IEnumerable<IPeriodTimeline> source, Interval interval)
    {
        return source.Any(pp => pp.ContainsExactInterval(interval));
    }

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Period? GetPreviousCompletePeriod(this IEnumerable<IPeriodTimeline> source, Instant instant)
    {
        return source.Max(tl => tl.GetPreviousCompletePeriod(instant));
    }

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Period? GetPreviousPeriodIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.EnumerateBackwardsFromIncludingPartial(instant).FirstOrDefault();

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Period? GetNextCompletePeriod(this IEnumerable<IPeriodTimeline> source, Instant instant)
    {
        return source.Min(tl => tl.GetNextCompletePeriod(instant));
    }

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Period? GetNextPeriodIncludingPartial(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
        source.EnumerateFromIncludingPartial(instant).FirstOrDefault();

    /// <summary>
    /// Returns the timelines in <paramref name="source"/> that contain a interval that is exactly <paramref name="interval"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> GetTimelinesAtExactPeriod(this IEnumerable<IPeriodTimeline> source, Interval interval) =>
        source.Where(kvp => kvp.ContainsExactInterval(interval));

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
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
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
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
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
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
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
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
    /// Takes a sample of the timelines in <paramref name="source"/> at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<PeriodTimelineSample> SampleAt(this IEnumerable<IPeriodTimeline> source, Instant instant) =>
            source.Select(tl => tl.SampleAt(instant));
}