namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with collections related to <see cref="IEnumerable{KeyValuePair}"/> with <see cref="ITimeline"/> as key.
/// </summary>
public static partial class TimelineValueCollectionExtensions
{
    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest next instant on any of <paramref name="source"/>.
    /// </summary>
    public static TimeSpan? GetTimeToNextInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        return source.GetNextUtcInstant(instant) - instant;
    }

    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest previous instant on any of <paramref name="source"/>.
    /// </summary>
    public static TimeSpan? GetTimeSincePreviousInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        return instant - source.GetPreviousUtcInstant(instant);
    }

    /// <summary>
    /// Returns the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static DateTime? GetPreviousUtcInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant) => 
        source.Select(kvp => kvp.Key).GetPreviousUtcInstant(instant);

    /// <summary>
    /// Returns the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static DateTime? GetNextUtcInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant) =>
        source.Select(kvp => kvp.Key).GetNextUtcInstant(instant);

    /// <summary>
    /// Returns the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static DateTime? GetCurrentOrPreviousUtcInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        if (instant == DateTime.MaxValue)
        {
            source = source.ToArray();
            return source.IsInstant(instant) ? instant : source.GetPreviousUtcInstant(instant);
        }
        // By using instant + TimeSpan.FromTicks(1), we improve performance, as we don't have to call timeline.IsInstant(instant).
        return source.GetPreviousUtcInstant(instant + TimeSpan.FromTicks(1));
    }

    /// <summary>
    /// Returns the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static DateTime? GetCurrentOrNextUtcInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        if (instant == DateTime.MinValue)
        {
            source = source.ToArray();
            return source.IsInstant(instant) ? instant : source.GetNextUtcInstant(instant);
        }
        // By using instant - TimeSpan.FromTicks(1), we improve performance, as we don't have to call timeline.IsInstant(instant).
        return source.GetNextUtcInstant(instant - TimeSpan.FromTicks(1));
    }

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static TValue[] GetValuesAtUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        return source.Where(kvp => kvp.Key.IsInstant(instant)).Select(kvp => kvp.Value).ToArray();
    }

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have an instant on the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, TValue[]> GetValuesAtPreviousUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousInstant = source.GetPreviousUtcInstant(instant);
        if (previousInstant == null)
        {
            return new KeyValuePair<DateTime?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<DateTime?, TValue[]>(previousInstant, source.GetValuesAtUtcInstant(previousInstant.Value));
    }

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have an instant on the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, TValue[]> GetValuesAtNextUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextInstant = source.GetNextUtcInstant(instant);
        if (nextInstant == null)
        {
            return new KeyValuePair<DateTime?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<DateTime?, TValue[]>(nextInstant, source.GetValuesAtUtcInstant(nextInstant.Value));
    }

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have an instant on <paramref name="instant"/> or the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, TValue[]> GetValuesAtCurrentOrPreviousUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousInstant = source.GetCurrentOrPreviousUtcInstant(instant);
        if (previousInstant == null)
        {
            return new KeyValuePair<DateTime?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<DateTime?, TValue[]>(previousInstant, source.GetValuesAtUtcInstant(previousInstant.Value));
    }

    /// <summary>
    /// Returns the values of timelines in <paramref name="source"/> that have an instant on <paramref name="instant"/> or the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, TValue[]> GetValuesAtCurrentOrNextUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextInstant = source.GetCurrentOrNextUtcInstant(instant);
        if (nextInstant == null)
        {
            return new KeyValuePair<DateTime?, TValue[]>(null, Array.Empty<TValue>());
        }
        return new KeyValuePair<DateTime?, TValue[]>(nextInstant, source.GetValuesAtUtcInstant(nextInstant.Value));
    }

    /// <summary>
    /// Returns the timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<ITimeline, TValue>[] GetTimelinesAtUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        return source.Where(kvp => kvp.Key.IsInstant(instant)).ToArray();
    }

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]> GetTimelinesAtPreviousUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousInstant = source.GetPreviousUtcInstant(instant);
        if (previousInstant == null)
        {
            return new KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]>(null, Array.Empty<KeyValuePair<ITimeline, TValue>>());
        }
        return new KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]>(previousInstant, source.GetTimelinesAtUtcInstant(previousInstant.Value));
    }

    /// <summary>
    /// Returns the timelines on the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]> GetTimelinesAtNextUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextInstant = source.GetNextUtcInstant(instant);
        if (nextInstant == null)
        {
            return new KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]>(null, Array.Empty<KeyValuePair<ITimeline, TValue>>());
        }
        return new KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]>(nextInstant, source.GetTimelinesAtUtcInstant(nextInstant.Value));
    }

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or the timelines on <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]> GetTimelinesAtCurrentOrPreviousUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousInstant = source.GetCurrentOrPreviousUtcInstant(instant);
        if (previousInstant == null)
        {
            return new KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]>(null, Array.Empty<KeyValuePair<ITimeline, TValue>>());
        }
        return new KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]>(previousInstant, source.GetTimelinesAtUtcInstant(previousInstant.Value));
    }

    /// <summary>
    /// Returns the timelines on the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or the timelines on <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]> GetTimelinesAtCurrentOrNextUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextInstant = source.GetCurrentOrNextUtcInstant(instant);
        if (nextInstant == null)
        {
            return new KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]>(null, Array.Empty<KeyValuePair<ITimeline, TValue>>());
        }
        return new KeyValuePair<DateTime?, KeyValuePair<ITimeline, TValue>[]>(nextInstant, source.GetTimelinesAtUtcInstant(nextInstant.Value));
    }

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant) =>
        source.Select(kvp => kvp.Key).ContainsInstant(instant);

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of <paramref name="source"/>.
    /// </summary>
    public static bool IsInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant) =>
        source.Select(kvp => kvp.Key).IsInstant(instant);

    /// <summary>
    /// Determines whether none of <paramref name="source"/> contains any instants.
    /// </summary>
    public static bool AreEmpty<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source) =>
        source.Select(kvp => kvp.Key).AreEmpty();

    /// <summary>
    /// Synchronizes all <paramref name="source"/> using the same gate such that method calls cannot occur concurrently.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Synchronize<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source) => 
        source.Synchronize(new());

    /// <summary>
    /// Synchronizes all <paramref name="source"/> using the same gate such that method calls cannot occur concurrently.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Synchronize<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, object gate)
    {
        return source.ToDictionary(kvp => kvp.Key.Synchronize(gate), kvp => kvp.Value);
    }
}