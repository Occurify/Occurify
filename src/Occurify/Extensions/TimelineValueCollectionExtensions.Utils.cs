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
    /// Returns the timelines on the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static (DateTime? instant, IEnumerable<KeyValuePair<ITimeline, TValue>> timelines) GetTimelinesAtPreviousUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousInstant = source.GetPreviousUtcInstant(instant);
        return (previousInstant,
            source.Where(s => s.Key.GetPreviousUtcInstant(instant) == previousInstant)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
    }

    /// <summary>
    /// Returns the timelines on the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static (DateTime? instant, IEnumerable<KeyValuePair<ITimeline, TValue>> timelines) GetTimelinesAtNextUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextInstant = source.GetNextUtcInstant(instant);
        return (nextInstant,
            source.Where(s => s.Key.GetNextUtcInstant(instant) == nextInstant)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
    }

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or the timelines on <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static (DateTime? instant, IEnumerable<KeyValuePair<ITimeline, TValue>> timelines) GetTimelinesAtCurrentOrPreviousUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var previousInstant = source.GetCurrentOrPreviousUtcInstant(instant);
        return (previousInstant,
            source.Where(s => s.Key.GetCurrentOrPreviousUtcInstant(instant) == previousInstant)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
    }

    /// <summary>
    /// Returns the timelines on the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or the timelines on <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static (DateTime? instant, IEnumerable<KeyValuePair<ITimeline, TValue>> timelines) GetTimelinesAtCurrentOrNextUtcInstant<TValue>(
        this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        source = source.ToArray();
        var nextInstant = source.GetCurrentOrNextUtcInstant(instant);
        return (nextInstant,
            source.Where(s => s.Key.GetCurrentOrNextUtcInstant(instant) == nextInstant)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
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
    /// Takes a sample of <paramref name="source"/> at <paramref name="instant"/>.
    /// </summary>
    public static TimelineValueCollectionSample<TValue> SampleAt<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant)
    {
        var sourceArray = source.ToArray();
        return new TimelineValueCollectionSample<TValue>(sourceArray, sourceArray.Select(kvp => kvp.Key).SampleAt(instant));
    }

    /// <summary>
    /// Returns all timelines from <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> GetTimelinesAt<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instant) =>
        source.SampleAt(instant).TimelinesWithInstantOnSampleLocation;

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