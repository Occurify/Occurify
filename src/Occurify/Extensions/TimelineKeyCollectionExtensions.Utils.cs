namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with collections related to <see cref="IEnumerable{KeyValuePair}"/> with <see cref="ITimeline"/> as value.
/// </summary>
public static partial class TimelineKeyCollectionExtensions
{
    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest next instant on any of <paramref name="source"/>.
    /// </summary>
    public static TimeSpan? GetTimeToNextInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        return source.GetNextUtcInstant(instant) - instant;
    }

    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest previous instant on any of <paramref name="source"/>.
    /// </summary>
    public static TimeSpan? GetTimeSincePreviousInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        return instant - source.GetPreviousUtcInstant(instant);
    }

    /// <summary>
    /// Returns the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static DateTime? GetPreviousUtcInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant) => 
        source.Select(kvp => kvp.Value).GetPreviousUtcInstant(instant);

    /// <summary>
    /// Returns the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static DateTime? GetNextUtcInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant) =>
        source.Select(kvp => kvp.Value).GetNextUtcInstant(instant);

    /// <summary>
    /// Returns the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static DateTime? GetCurrentOrPreviousUtcInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
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
    public static DateTime? GetCurrentOrNextUtcInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
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
    /// Returns the keys on of timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static TKey[] GetKeysAtUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        return source.Where(kvp => kvp.Value.IsInstant(instant)).Select(kvp => kvp.Key).ToArray();
    }

    /// <summary>
    /// Returns the keys on of timelines in <paramref name="source"/> that have an instant on the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, TKey[]> GetKeysAtPreviousUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousInstant = source.GetPreviousUtcInstant(instant);
        if (previousInstant == null)
        {
            return new KeyValuePair<DateTime?, TKey[]>(null, Array.Empty<TKey>());
        }
        return new KeyValuePair<DateTime?, TKey[]>(previousInstant, source.GetKeysAtUtcInstant(previousInstant.Value));
    }

    /// <summary>
    /// Returns the keys on of timelines in <paramref name="source"/> that have an instant on the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, TKey[]> GetKeysAtNextUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextInstant = source.GetNextUtcInstant(instant);
        if (nextInstant == null)
        {
            return new KeyValuePair<DateTime?, TKey[]>(null, Array.Empty<TKey>());
        }
        return new KeyValuePair<DateTime?, TKey[]>(nextInstant, source.GetKeysAtUtcInstant(nextInstant.Value));
    }

    /// <summary>
    /// Returns the keys on of timelines in <paramref name="source"/> that have an instant on <paramref name="instant"/> or the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, TKey[]> GetKeysAtCurrentOrPreviousUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousInstant = source.GetCurrentOrPreviousUtcInstant(instant);
        if (previousInstant == null)
        {
            return new KeyValuePair<DateTime?, TKey[]>(null, Array.Empty<TKey>());
        }
        return new KeyValuePair<DateTime?, TKey[]>(previousInstant, source.GetKeysAtUtcInstant(previousInstant.Value));
    }

    /// <summary>
    /// Returns the keys on of timelines in <paramref name="source"/> that have an instant on <paramref name="instant"/> or the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, TKey[]> GetKeysAtCurrentOrNextUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextInstant = source.GetCurrentOrNextUtcInstant(instant);
        if (nextInstant == null)
        {
            return new KeyValuePair<DateTime?, TKey[]>(null, Array.Empty<TKey>());
        }
        return new KeyValuePair<DateTime?, TKey[]>(nextInstant, source.GetKeysAtUtcInstant(nextInstant.Value));
    }

    /// <summary>
    /// Returns the timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> GetTimelinesAtUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant) =>
        source.Where(kvp => kvp.Value.IsInstant(instant));

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]> GetTimelinesAtPreviousUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousInstant = source.GetPreviousUtcInstant(instant);
        if (previousInstant == null)
        {
            return new KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]>(null, Array.Empty<KeyValuePair<TKey, ITimeline>>());
        }
        return new KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]>(previousInstant, source.GetTimelinesAtUtcInstant(previousInstant.Value).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or the timelines on <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]> GetTimelinesAtCurrentOrPreviousUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousInstant = source.GetCurrentOrPreviousUtcInstant(instant);
        if (previousInstant == null)
        {
            return new KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]>(null, Array.Empty<KeyValuePair<TKey, ITimeline>>());
        }
        return new KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]>(previousInstant, source.GetTimelinesAtUtcInstant(previousInstant.Value).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]> GetTimelinesAtNextUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextInstant = source.GetNextUtcInstant(instant);
        if (nextInstant == null)
        {
            return new KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]>(null, Array.Empty<KeyValuePair<TKey, ITimeline>>());
        }
        return new KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]>(nextInstant, source.GetTimelinesAtUtcInstant(nextInstant.Value).ToArray());
    }

    /// <summary>
    /// Returns the timelines on the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or the timelines on <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]> GetTimelinesAtCurrentOrNextUtcInstant<TKey>(
        this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextInstant = source.GetCurrentOrNextUtcInstant(instant);
        if (nextInstant == null)
        {
            return new KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]>(null, Array.Empty<KeyValuePair<TKey, ITimeline>>());
        }
        return new KeyValuePair<DateTime?, KeyValuePair<TKey, ITimeline>[]>(nextInstant, source.GetTimelinesAtUtcInstant(nextInstant.Value).ToArray());
    }

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant) =>
        source.Select(kvp => kvp.Value).ContainsInstant(instant);

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of <paramref name="source"/>.
    /// </summary>
    public static bool IsInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instant) =>
        source.Select(kvp => kvp.Value).IsInstant(instant);

    /// <summary>
    /// Determines whether none of <paramref name="source"/> contains any instants.
    /// </summary>
    public static bool AreEmpty<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source) =>
        source.Select(kvp => kvp.Value).AreEmpty();

    /// <summary>
    /// Synchronizes all <paramref name="source"/> using the same gate such that method calls cannot occur concurrently.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Synchronize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source) where TKey : notnull => 
        source.Synchronize(new());

    /// <summary>
    /// Synchronizes all <paramref name="source"/> using the same gate such that method calls cannot occur concurrently.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Synchronize<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, object gate) where TKey : notnull
    {
        return source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Synchronize(gate));
    }
}