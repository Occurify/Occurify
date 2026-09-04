using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey, ITimeline}"/>.
/// </summary>
public static partial class TimelineKeyCollectionExtensions
{
    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest next instant on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static Duration? GetTimeToNextInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetTimeToNextInstant(instant.ToDateTimeUtc()).ToDuration();

    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest previous instant on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static Duration? GetTimeSincePreviousInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetTimeSincePreviousInstant(instant.ToDateTimeUtc()).ToDuration();

    /// <summary>
    /// Returns the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static Instant? GetPreviousInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static Instant? GetNextInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetNextUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static Instant? GetCurrentOrPreviousInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetCurrentOrPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static Instant? GetCurrentOrNextInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetCurrentOrNextUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the keys of the timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static TKey[] GetKeysAtInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetKeysAtUtcInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Returns the keys of the timelines on the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Instant?, TKey[]> GetKeysAtPreviousInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetKeysAtPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the keys of the timelines on the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Instant?, TKey[]> GetKeysAtNextInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetKeysAtNextUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the keys of the timelines on the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or on <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static KeyValuePair<Instant?, TKey[]> GetKeysAtCurrentOrPreviousInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetKeysAtCurrentOrPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the keys of the timelines on the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or on <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static KeyValuePair<Instant?, TKey[]> GetKeysAtCurrentOrNextInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetKeysAtCurrentOrNextUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> GetTimelinesAtInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetTimelinesAtUtcInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Instant?, KeyValuePair<TKey, ITimeline>[]> GetTimelinesAtPreviousInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetTimelinesAtPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or on <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static KeyValuePair<Instant?, KeyValuePair<TKey, ITimeline>[]> GetTimelinesAtCurrentOrPreviousInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetTimelinesAtCurrentOrPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the timelines on the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Instant?, KeyValuePair<TKey, ITimeline>[]> GetTimelinesAtNextInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetTimelinesAtNextUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the timelines on the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or on <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static KeyValuePair<Instant?, KeyValuePair<TKey, ITimeline>[]> GetTimelinesAtCurrentOrNextInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.GetTimelinesAtCurrentOrNextUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.ContainsInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool IsInstant<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instant) =>
        source.IsInstant(instant.ToDateTimeUtc());
}
