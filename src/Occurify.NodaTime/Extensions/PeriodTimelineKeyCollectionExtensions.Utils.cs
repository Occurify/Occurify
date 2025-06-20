
using NodaTime;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with collections related to <see cref="IEnumerable{KeyValuePair}"/> with <see cref="IPeriodTimeline"/> as value.
/// </summary>
public static partial class PeriodTimelineKeyCollectionExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.Select(kvp => kvp.Value).ContainsInstant(instant);

    /// <summary>
    /// Determines whether <paramref name="interval"/> is included in any of the intervals in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInterval<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval interval) =>
        source.Select(kvp => kvp.Value).ContainsInterval(interval);

    /// <summary>
    /// Determines whether any of the intervals in the timelines in <paramref name="source"/> is exactly <paramref name="interval"/>.
    /// </summary>
    public static bool ContainsExactInterval<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval interval) =>
        source.Select(kvp => kvp.Value).ContainsExactInterval(interval);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.Select(kvp => kvp.Value).GetPreviousCompletePeriod(instant);

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetPreviousCompleteInterval<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.Select(kvp => kvp.Value).GetPreviousCompleteInterval(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.Select(kvp => kvp.Value).GetPreviousPeriodIncludingPartial(instant);

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetPreviousIntervalIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.Select(kvp => kvp.Value).GetPreviousIntervalIncludingPartial(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.Select(kvp => kvp.Value).GetNextCompletePeriod(instant);

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetNextCompleteInterval<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.Select(kvp => kvp.Value).GetNextCompleteInterval(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.Select(kvp => kvp.Value).GetNextPeriodIncludingPartial(instant);

    /// <summary>
    /// Returns the first complete interval on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no interval is found.
    /// </summary>
    public static Interval? GetNextIntervalIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.Select(kvp => kvp.Value).GetNextIntervalIncludingPartial(instant);

    /// <summary>
    /// Returns the keys of timelines in <paramref name="source"/> that have a interval at <paramref name="instant"/>.
    /// </summary>
    public static TKey[] GetKeysAtUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant)
    {
        return source.Where(kvp => kvp.Value.ContainsInstant(instant)).Select(kvp => kvp.Key).ToArray();
    }

    /// <summary>
    /// Returns the keys of timelines in <paramref name="source"/> that have a interval that contains <paramref name="interval"/>.
    /// </summary>
    public static TKey[] GetKeysAtInterval<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval interval)
    {
        return source.Where(kvp => kvp.Value.ContainsInterval(interval)).Select(kvp => kvp.Key).ToArray();
    }

    /// <summary>
    /// Returns the keys of timelines in <paramref name="source"/> that have a interval that exactly matches <paramref name="interval"/>.
    /// </summary>
    public static TKey[] GetKeysAtExactInterval<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval interval)
    {
        return source.Where(kvp => kvp.Value.ContainsExactInterval(interval)).Select(kvp => kvp.Key).ToArray();
    }

    /// <summary>
    /// Returns the keys of the timelines on the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TKey[]> GetKeysAtPreviousCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant)
    {
        source = source.ToArray();
        var previousCompletePeriod = source.GetPreviousCompletePeriod(instant);
        if (previousCompletePeriod == null)
        {
            return new KeyValuePair<Period?, TKey[]>(null, Array.Empty<TKey>());
        }
        return new KeyValuePair<Period?, TKey[]>(previousCompletePeriod, source.GetKeysAtExactPeriod(previousCompletePeriod));
    }

    /// <summary>
    /// Returns the keys of the timelines on the first complete interval on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, TKey[]> GetKeysAtPreviousCompleteInterval<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.GetKeysAtPreviousCompletePeriod(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the keys of the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TKey[]> GetKeysAtPreviousPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant)
    {
        source = source.ToArray();
        var previousPeriodIncludingPartial = source.GetPreviousPeriodIncludingPartial(instant);
        if (previousPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, TKey[]>(null, Array.Empty<TKey>());
        }
        return new KeyValuePair<Period?, TKey[]>(previousPeriodIncludingPartial, source.GetKeysAtExactPeriod(previousPeriodIncludingPartial));
    }

    /// <summary>
    /// Returns the keys of the timelines on the first complete interval on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, TKey[]> GetKeysAtPreviousIntervalIncludingPartial<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.GetKeysAtPreviousPeriodIncludingPartial(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the keys of the timelines on the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TKey[]> GetKeysAtNextCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant)
    {
        source = source.ToArray();
        var nextCompletePeriod = source.GetNextCompletePeriod(instant);
        if (nextCompletePeriod == null)
        {
            return new KeyValuePair<Period?, TKey[]>(null, Array.Empty<TKey>());
        }
        return new KeyValuePair<Period?, TKey[]>(nextCompletePeriod, source.GetKeysAtExactPeriod(nextCompletePeriod));
    }

    /// <summary>
    /// Returns the keys of the timelines on the first complete interval on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, TKey[]> GetKeysAtNextCompleteInterval<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.GetKeysAtNextCompletePeriod(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the keys of the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TKey[]> GetKeysAtNextPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant)
    {
        source = source.ToArray();
        var nextPeriodIncludingPartial = source.GetNextPeriodIncludingPartial(instant);
        if (nextPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, TKey[]>(null, Array.Empty<TKey>());
        }
        return new KeyValuePair<Period?, TKey[]>(nextPeriodIncludingPartial, source.GetKeysAtExactPeriod(nextPeriodIncludingPartial));
    }

    /// <summary>
    /// Returns the keys of the timelines on the first complete interval on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, TKey[]> GetKeysAtNextIntervalIncludingPartial<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.GetKeysAtNextPeriodIncludingPartial(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines in <paramref name="source"/> that contain a interval that is exactly <paramref name="interval"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> GetTimelinesAtExactInterval<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Interval interval) =>
        source.Where(kvp => kvp.Value.ContainsExactInterval(interval));

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtPreviousCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant)
    {
        source = source.ToArray();
        var previousCompletePeriod = source.GetPreviousCompletePeriod(instant);
        if (previousCompletePeriod == null)
        {
            return new KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]>(null, Array.Empty<KeyValuePair<TKey, IPeriodTimeline>>());
        }
        return new KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]>(previousCompletePeriod, source.GetTimelinesAtExactPeriod(previousCompletePeriod).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtPreviousCompleteInterval<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.GetTimelinesAtPreviousCompletePeriod(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtPreviousPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant)
    {
        source = source.ToArray();
        var previousPeriodIncludingPartial = source.GetPreviousPeriodIncludingPartial(instant);
        if (previousPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]>(null, Array.Empty<KeyValuePair<TKey, IPeriodTimeline>>());
        }
        return new KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]>(previousPeriodIncludingPartial, source.GetTimelinesAtExactPeriod(previousPeriodIncludingPartial).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtPreviousIntervalIncludingPartial<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.GetTimelinesAtPreviousPeriodIncludingPartial(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtNextCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant)
    {
        source = source.ToArray();
        var nextCompletePeriod = source.GetNextCompletePeriod(instant);
        if (nextCompletePeriod == null)
        {
            return new KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]>(null, Array.Empty<KeyValuePair<TKey, IPeriodTimeline>>());
        }
        return new KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]>(nextCompletePeriod, source.GetTimelinesAtExactPeriod(nextCompletePeriod).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtNextCompleteInterval<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.GetTimelinesAtNextCompletePeriod(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtNextPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant)
    {
        source = source.ToArray();
        var nextPeriodIncludingPartial = source.GetNextPeriodIncludingPartial(instant);
        if (nextPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]>(null, Array.Empty<KeyValuePair<TKey, IPeriodTimeline>>());
        }
        return new KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]>(nextPeriodIncludingPartial, source.GetTimelinesAtExactPeriod(nextPeriodIncludingPartial).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the first complete interval on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Interval?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtNextIntervalIncludingPartial<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.GetTimelinesAtNextPeriodIncludingPartial(instant).ConvertPeriodKvpToIntervalKvp();

    /// <summary>
    /// Takes a sample of the timelines in <paramref name="source"/> at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, PeriodTimelineSample>> SampleAt<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Instant instant) =>
        source.Select(kvp => new KeyValuePair<TKey, PeriodTimelineSample>(kvp.Key, kvp.Value.SampleAt(instant)));
}