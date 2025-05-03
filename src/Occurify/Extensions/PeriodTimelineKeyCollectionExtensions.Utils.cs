
namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with collections related to <see cref="IEnumerable{KeyValuePair}"/> with <see cref="IPeriodTimeline"/> as value.
/// </summary>
public static partial class PeriodTimelineKeyCollectionExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source,
        DateTime instant) =>
        source.Select(kvp => kvp.Value).ContainsInstant(instant);

    /// <summary>
    /// Determines whether <paramref name="period"/> is included in any of the periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsPeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period period) =>
        source.Select(kvp => kvp.Value).ContainsPeriod(period);

    /// <summary>
    /// Determines whether any of the periods in the timelines in <paramref name="source"/> is exactly <paramref name="period"/>.
    /// </summary>
    public static bool ContainsExactPeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period period) =>
        source.Select(kvp => kvp.Value).ContainsExactPeriod(period);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant) =>
        source.Select(kvp => kvp.Value).GetPreviousCompletePeriod(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant) =>
        source.Select(kvp => kvp.Value).GetPreviousPeriodIncludingPartial(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant) =>
        source.Select(kvp => kvp.Value).GetNextCompletePeriod(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant) =>
        source.Select(kvp => kvp.Value).GetNextPeriodIncludingPartial(instant);

    /// <summary>
    /// Returns the keys of timelines in <paramref name="source"/> that have a period at <paramref name="instant"/>.
    /// </summary>
    public static TKey[] GetKeysAtUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant)
    {
        return source.Where(kvp => kvp.Value.ContainsInstant(instant)).Select(kvp => kvp.Key).ToArray();
    }

    /// <summary>
    /// Returns the keys of timelines in <paramref name="source"/> that have a period that contains <paramref name="period"/>.
    /// </summary>
    public static TKey[] GetKeysAtPeriod<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period period)
    {
        return source.Where(kvp => kvp.Value.ContainsPeriod(period)).Select(kvp => kvp.Key).ToArray();
    }

    /// <summary>
    /// Returns the keys of timelines in <paramref name="source"/> that have a period that exactly matches <paramref name="period"/>.
    /// </summary>
    public static TKey[] GetKeysAtExactPeriod<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period period)
    {
        return source.Where(kvp => kvp.Value.ContainsExactPeriod(period)).Select(kvp => kvp.Key).ToArray();
    }

    /// <summary>
    /// Returns the keys of the timelines on the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TKey[]> GetKeysAtPreviousCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant)
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
    /// Returns the keys of the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TKey[]> GetKeysAtPreviousPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant)
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
    /// Returns the keys of the timelines on the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TKey[]> GetKeysAtNextCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant)
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
    /// Returns the keys of the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, TKey[]> GetKeysAtNextPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant)
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
    /// Returns the timelines in <paramref name="source"/> that contain a period that is exactly <paramref name="period"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> GetTimelinesAtExactPeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period period) =>
        source.Where(kvp => kvp.Value.ContainsExactPeriod(period));

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtPreviousCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant)
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
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtPreviousPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant)
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
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtNextCompletePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant)
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
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<TKey, IPeriodTimeline>[]> GetTimelinesAtNextPeriodIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant)
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
    /// Returns whether all the timelines in <paramref name="source"/> is empty.
    /// </summary>
    public static bool AreEmpty<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) =>
        source.Select(kvp => kvp.Value).AreEmpty();

    /// <summary>
    /// Returns whether <c>DateTime.UtcNow</c> is inside any period on the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool IsNow<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) =>
        source.Select(kvp => kvp.Value).IsNow();

    /// <summary>
    /// Takes a sample of the timelines in <paramref name="source"/> at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, PeriodTimelineSample>> SampleAt<TKey>(
        this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instant) =>
        source.Select(kvp => new KeyValuePair<TKey, PeriodTimelineSample>(kvp.Key, kvp.Value.SampleAt(instant)));

    /// <summary>
    /// Synchronizes all <paramref name="source"/> such that method calls cannot occur concurrently.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Synchronize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) where TKey : notnull =>
        source.Synchronize(new());

    /// <summary>
    /// Synchronizes all <paramref name="source"/> such that method calls cannot occur concurrently.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Synchronize<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, object gate) where TKey : notnull
    {
        return source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Synchronize(gate));
    }

    /// <summary>
    /// Calculates the total duration of the timelines in <paramref name="source"/>.
    /// If <paramref name="addIndividualTimelineDurations"/> is <c>true</c>, the total duration is calculated by summing the durations of all individual timelines. If <c>false</c>, the total duration of the merged timelines is calculated.
    /// </summary>
    public static TimeSpan? TotalDuration<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, bool addIndividualTimelineDurations = false) =>
        source.Select(kvp => kvp.Value).TotalDuration(addIndividualTimelineDurations);
}