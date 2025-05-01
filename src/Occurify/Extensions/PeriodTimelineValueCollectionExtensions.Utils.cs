
namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with collections related to <see cref="IEnumerable{KeyValuePair}"/> with <see cref="IPeriodTimeline"/> as key.
/// </summary>
public static partial class PeriodTimelineValueCollectionExtensions
{
    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source,
        DateTime instant) =>
        source.Select(kvp => kvp.Key).ContainsInstant(instant);

    /// <summary>
    /// Determines whether <paramref name="period"/> is included in any of the periods in the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsPeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period period) =>
        source.Select(kvp => kvp.Key).ContainsPeriod(period);

    /// <summary>
    /// Determines whether any of the periods in the timelines in <paramref name="source"/> is exactly <paramref name="period"/>.
    /// </summary>
    public static bool ContainsExactPeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period period) =>
        source.Select(kvp => kvp.Key).ContainsExactPeriod(period);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant) =>
        source.Select(kvp => kvp.Key).GetPreviousCompletePeriod(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetPreviousPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant) =>
        source.Select(kvp => kvp.Key).GetPreviousPeriodIncludingPartial(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant) =>
        source.Select(kvp => kvp.Key).GetNextCompletePeriod(instant);

    /// <summary>
    /// Returns the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// <c>null</c> if no period is found.
    /// </summary>
    public static Period? GetNextPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant) =>
        source.Select(kvp => kvp.Key).GetNextPeriodIncludingPartial(instant);

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static TValue[] GetValuesAtUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant)
    {
        return source.Where(kvp => kvp.Key.ContainsInstant(instant)).Select(kvp => kvp.Value).ToArray();
    }

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static TValue[] GetValuesAtPeriod<TValue>(
        this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period period)
    {
        return source.Where(kvp => kvp.Key.ContainsPeriod(period)).Select(kvp => kvp.Value).ToArray();
    }

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static TValue[] GetValuesAtExactPeriod<TValue>(
        this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period period)
    {
        return source.Where(kvp => kvp.Key.ContainsExactPeriod(period)).Select(kvp => kvp.Value).ToArray();
    }

    public static KeyValuePair<Period?, TValue[]> GetValuesAtPreviousCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousCompletePeriod = source.GetPreviousCompletePeriod(instant);
        if (previousCompletePeriod == null)
        {
            return new KeyValuePair<Period?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<Period?, TValue[]>(previousCompletePeriod, source.GetValuesAtExactPeriod(previousCompletePeriod));
    }

    public static KeyValuePair<Period?, TValue[]> GetValuesAtPreviousPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousPeriodIncludingPartial = source.GetPreviousPeriodIncludingPartial(instant);
        if (previousPeriodIncludingPartial == null)
        {
            return new KeyValuePair<Period?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<Period?, TValue[]>(previousPeriodIncludingPartial, source.GetValuesAtExactPeriod(previousPeriodIncludingPartial));
    }

    public static KeyValuePair<Period?, TValue[]> GetValuesAtNextCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextCompletePeriod = source.GetNextCompletePeriod(instant);
        if (nextCompletePeriod == null)
        {
            return new KeyValuePair<Period?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<Period?, TValue[]>(nextCompletePeriod, source.GetValuesAtExactPeriod(nextCompletePeriod));
    }

    public static KeyValuePair<Period?, TValue[]> GetValuesAtNextPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant)
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
    /// Returns the timelines in <paramref name="source"/> that contain a period that is exactly <paramref name="period"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> GetTimelinesAtExactPeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period period) =>
        source.Where(kvp => kvp.Key.ContainsExactPeriod(period));

    /// <summary>
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> ending on or earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtPreviousCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant)
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
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or ends earlier than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtPreviousPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant)
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
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> starting on or later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtNextCompletePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant)
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
    /// Returns the timelines on the first complete period on the timelines in <paramref name="source"/> that includes or starts later than <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Period?, KeyValuePair<IPeriodTimeline, TValue>[]> GetTimelinesAtNextPeriodIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant)
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
    /// Returns whether all the timelines in <paramref name="source"/> is empty.
    /// </summary>
    public static bool AreEmpty<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.Select(kvp => kvp.Key).AreEmpty();

    /// <summary>
    /// Returns whether <c>DateTime.UtcNow</c> is inside any period on the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool IsNow<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.Select(kvp => kvp.Key).IsNow();

    /// <summary>
    /// Takes a sample of the timelines in <paramref name="source"/> at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<PeriodTimelineSample, TValue>> SampleAt<TValue>(
        this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instant) =>
        source.Select(kvp => new KeyValuePair<PeriodTimelineSample, TValue>(kvp.Key.SampleAt(instant), kvp.Value));

    /// <summary>
    /// Synchronizes all <paramref name="source"/> such that method calls cannot occur concurrently.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Synchronize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.Synchronize(new());

    /// <summary>
    /// Synchronizes all <paramref name="source"/> such that method calls cannot occur concurrently.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Synchronize<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, object gate)
    {
        return source.ToDictionary(kvp => kvp.Key.Synchronize(gate), kvp => kvp.Value);
    }

    /// <summary>
    /// Calculates the total duration of the timelines in <paramref name="source"/>.
    /// If <paramref name="addIndividualTimelineDurations"/> is <c>true</c>, the total duration is calculated by summing the durations of all individual timelines. If <c>false</c>, the total duration of the merged timelines is calculated.
    /// </summary>
    public static TimeSpan? TotalDuration<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, bool addIndividualTimelineDurations = false) =>
        source.Select(kvp => kvp.Key).TotalDuration(addIndividualTimelineDurations);
}