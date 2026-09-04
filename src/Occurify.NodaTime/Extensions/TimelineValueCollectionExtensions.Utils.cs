using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{ITimeline, TValue}"/>.
/// </summary>
public static partial class TimelineValueCollectionExtensions
{
    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest next instant on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static Duration? GetTimeToNextInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetTimeToNextInstant(instant.ToDateTimeUtc()).ToDuration();

    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest previous instant on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static Duration? GetTimeSincePreviousInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetTimeSincePreviousInstant(instant.ToDateTimeUtc()).ToDuration();

    /// <summary>
    /// Returns the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static Instant? GetPreviousInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static Instant? GetNextInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetNextUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static Instant? GetCurrentOrPreviousInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetCurrentOrPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static Instant? GetCurrentOrNextInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetCurrentOrNextUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the values of the timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static TValue[] GetValuesAtInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetValuesAtUtcInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Returns the values of the timelines on the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Instant?, TValue[]> GetValuesAtPreviousInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetValuesAtPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the values of the timelines on the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Instant?, TValue[]> GetValuesAtNextInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetValuesAtNextUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the values of the timelines on the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or on <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static KeyValuePair<Instant?, TValue[]> GetValuesAtCurrentOrPreviousInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetValuesAtCurrentOrPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the values of the timelines on the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or on <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static KeyValuePair<Instant?, TValue[]> GetValuesAtCurrentOrNextInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetValuesAtCurrentOrNextUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> GetTimelinesAtInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetTimelinesAtUtcInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Instant?, KeyValuePair<ITimeline, TValue>[]> GetTimelinesAtPreviousInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetTimelinesAtPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or on <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static KeyValuePair<Instant?, KeyValuePair<ITimeline, TValue>[]> GetTimelinesAtCurrentOrPreviousInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetTimelinesAtCurrentOrPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the timelines on the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Instant?, KeyValuePair<ITimeline, TValue>[]> GetTimelinesAtNextInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetTimelinesAtNextUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Returns the timelines on the closest next instant on any of the timelines in <paramref name="source"/> relative to <paramref name="instant"/>, or on <paramref name="instant"/> itself if it is on any of the timelines.
    /// </summary>
    public static KeyValuePair<Instant?, KeyValuePair<ITimeline, TValue>[]> GetTimelinesAtCurrentOrNextInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.GetTimelinesAtCurrentOrNextUtcInstant(instant.ToDateTimeUtc()).ToInstantKey();

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.ContainsInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static bool IsInstant<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Instant instant) =>
        source.IsInstant(instant.ToDateTimeUtc());
}
