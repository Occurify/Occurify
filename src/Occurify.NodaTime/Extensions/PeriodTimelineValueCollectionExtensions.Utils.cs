
using NodaTime;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with collections related to <see cref="IEnumerable{KeyValuePair}"/> with <see cref="IPeriodTimeline"/> as key.
/// </summary>
public static partial class PeriodTimelineValueCollectionExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.Select(kvp => kvp.Key).ContainsInstant(instant);

    /// <summary>
    /// Determines whether <paramref name="interval"/> is included in any of the intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInterval<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval interval) =>
        source.Select(kvp => kvp.Key).ContainsInterval(interval);

    /// <summary>
    /// Determines whether any of the intervals in the timelines in <paramref name="source"/> is exactly <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsExactInterval<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval interval) =>
        source.Select(kvp => kvp.Key).ContainsExactInterval(interval);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.Select(kvp => kvp.Key).GetPreviousCompletePeriod(instant);

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetPreviousCompleteInterval<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.Select(kvp => kvp.Key).GetPreviousCompleteInterval(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.Select(kvp => kvp.Key).GetPreviousPeriodIncludingPartial(instant);

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetPreviousIntervalIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.Select(kvp => kvp.Key).GetPreviousIntervalIncludingPartial(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.Select(kvp => kvp.Key).GetNextCompletePeriod(instant);

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetNextCompleteInterval<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.Select(kvp => kvp.Key).GetNextCompleteInterval(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.Select(kvp => kvp.Key).GetNextPeriodIncludingPartial(instant);

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetNextIntervalIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.Select(kvp => kvp.Key).GetNextIntervalIncludingPartial(instant);

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have a interval at <paramref name="instant"/>.
    /// </summary>
    public static TValue[] GetValuesAtInstant<TValue>(
        this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant)
    {
        return source.Where(kvp => kvp.Key.ContainsInstant(instant)).Select(kvp => kvp.Value).ToArray();
    }

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have a interval that contains <paramref name="interval"/>.
    /// </summary>
    public static TValue[] GetValuesAtInterval<TValue>(
        this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval interval)
    {
        return source.Where(kvp => kvp.Key.ContainsInterval(interval)).Select(kvp => kvp.Value).ToArray();
    }

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have a interval that exactly matches <paramref name="interval"/>.
    /// </summary>
    public static TValue[] GetValuesAtExactInterval<TValue>(
        this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval interval)
    {
        return source.Where(kvp => kvp.Key.ContainsExactInterval(interval)).Select(kvp => kvp.Value).ToArray();
    }

    /// <summary>
    /// Returns the values of the timelines on the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TValue[]> GetValuesAtPreviousCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant)
    {
        source = source.ToArray();
        var previousCompletePeriod = source.GetPreviousCompletePeriod(instant);
        if (previousCompletePeriod == null)
        {
            return new KeyValuePair<Period?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<Period?, TValue[]>(previousCompletePeriod, source.GetValuesAtExactPeriod(previousCompletePeriod));
    }

    /// <summary>
    /// Returns the values of the timelines on the first complete interval on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, TValue[]> GetValuesAtPreviousCompleteInterval<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.GetValuesAtPreviousCompletePeriod(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the values of the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TValue[]> GetValuesAtPreviousPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant)
    {
        source = source.ToArray();
        var previousPeriodIncludingPartial = source.GetPreviousPeriodIncludingPartial(instant);
        if (previousPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<Period?, TValue[]>(previousPeriodIncludingPartial, source.GetValuesAtExactPeriod(previousPeriodIncludingPartial));
    }

    /// <summary>
    /// Returns the values of the timelines on the first complete interval on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, TValue[]> GetValuesAtPreviousIntervalIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.GetValuesAtPreviousPeriodIncludingPartial(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the values of the timelines on the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TValue[]> GetValuesAtNextCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant)
    {
        source = source.ToArray();
        var nextCompletePeriod = source.GetNextCompletePeriod(instant);
        if (nextCompletePeriod == null)
        {
            return new KeyValuePair<Period?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<Period?, TValue[]>(nextCompletePeriod, source.GetValuesAtExactPeriod(nextCompletePeriod));
    }

    /// <summary>
    /// Returns the values of the timelines on the first complete interval on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, TValue[]> GetValuesAtNextCompleteInterval<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.GetValuesAtNextCompletePeriod(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the values of the timelines on the first complete interval on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TValue[]> GetValuesAtNextPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant)
    {
        source = source.ToArray();
        var nextPeriodIncludingPartial = source.GetNextPeriodIncludingPartial(instant);
        if (nextPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<Period?, TValue[]>(nextPeriodIncludingPartial, source.GetValuesAtExactPeriod(nextPeriodIncludingPartial));
    }

    /// <summary>
    /// Returns the values of the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, TValue[]> GetValuesAtNextIntervalIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.GetValuesAtNextPeriodIncludingPartial(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines in <paramref name="source"/> that contain an interval that is exactly <paramref name="interval"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> GetTimelinesAtExactInterval<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Interval interval) =>
        source.Where(kvp => kvp.Key.ContainsExactInterval(interval));

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtPreviousCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant)
    {
        source = source.ToArray();
        var previousCompletePeriod = source.GetPreviousCompletePeriod(instant);
        if (previousCompletePeriod == null)
        {
            return new KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]>(null, Array.Empty<KeyValuePair<IPeriodTimeline, TValue>>());
        }
        return new KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]>(previousCompletePeriod, source.GetTimelinesAtExactPeriod(previousCompletePeriod).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtPreviousCompleteInterval<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.GetTimelinesAtPreviousCompletePeriod(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtPreviousPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant)
    {
        source = source.ToArray();
        var previousPeriodIncludingPartial = source.GetPreviousPeriodIncludingPartial(instant);
        if (previousPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]>(null, Array.Empty<KeyValuePair<IPeriodTimeline, TValue>>());
        }
        return new KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]>(previousPeriodIncludingPartial, source.GetTimelinesAtExactPeriod(previousPeriodIncludingPartial).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first interval (including partial) on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtPreviousIntervalIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.GetTimelinesAtPreviousPeriodIncludingPartial(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtNextCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant)
    {
        source = source.ToArray();
        var nextCompletePeriod = source.GetNextCompletePeriod(instant);
        if (nextCompletePeriod == null)
        {
            return new KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]>(null, Array.Empty<KeyValuePair<IPeriodTimeline, TValue>>());
        }
        return new KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]>(nextCompletePeriod, source.GetTimelinesAtExactPeriod(nextCompletePeriod).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtNextCompleteInterval<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.GetTimelinesAtNextCompletePeriod(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtNextPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant)
    {
        source = source.ToArray();
        var nextPeriodIncludingPartial = source.GetNextPeriodIncludingPartial(instant);
        if (nextPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]>(null, Array.Empty<KeyValuePair<IPeriodTimeline, TValue>>());
        }
        return new KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]>(nextPeriodIncludingPartial, source.GetTimelinesAtExactPeriod(nextPeriodIncludingPartial).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first interval (including partial) on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtNextIntervalIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.GetTimelinesAtNextPeriodIncludingPartial(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Takes a sample of the timelines in <paramref name="source"/> at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<PeriodTimelineSample, TValue>> SampleAt<TValue>(
        this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Instant instant) =>
        source.Select(kvp => new KeyValuePair<PeriodTimelineSample, TValue>(kvp.Key.SampleAt(instant), kvp.Value));
}