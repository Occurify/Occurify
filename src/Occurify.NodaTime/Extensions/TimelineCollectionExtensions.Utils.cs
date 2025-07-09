
using NodaTime;
using Occurify.NodaTime.Extensions;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IEnumerable{ITimeline}"/>.
/// </summary>
public static partial class TimelineCollectionExtensions
{
    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest next instant on any of <paramref name="source"/>.
    /// </summary>
    public static Duration? GetTimeToNextInstant(this IEnumerable<ITimeline> source, Instant instant) =>
        source.GetTimeToNextInstant(instant.ToDateTimeUtc()).ToDuration();

    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest previous instant on any of <paramref name="source"/>.
    /// </summary>
    public static Duration? GetTimeSincePreviousInstant(this IEnumerable<ITimeline> source, Instant instant) =>
        source.GetTimeSincePreviousInstant(instant.ToDateTimeUtc()).ToDuration();

    /// <summary>
    /// Returns the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static Instant? GetPreviousUtcInstant(this IEnumerable<ITimeline> source, Instant instant) =>
        source.GetPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static Instant? GetNextUtcInstant(this IEnumerable<ITimeline> source, Instant instant) =>
        source.GetNextUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static Instant? GetCurrentOrPreviousUtcInstant(this IEnumerable<ITimeline> source, Instant instant) =>
        source.GetCurrentOrPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static Instant? GetCurrentOrNextUtcInstant(this IEnumerable<ITimeline> source, Instant instant) =>
        source.GetCurrentOrNextUtcInstant(instant.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the timelines in <paramref name="source"/> that have an instant at <paramref name="instant"/>.
    /// </summary>
    public static IEnumerable<ITimeline> GetTimelinesAtUtcInstant(this IEnumerable<ITimeline> source, Instant instant) =>
        source.GetTimelinesAtUtcInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Instant?, ITimeline[]> GetTimelinesAtPreviousUtcInstant(this IEnumerable<ITimeline> source, Instant instant)
    {
        var pair = source.GetTimelinesAtPreviousUtcInstant(instant.ToDateTimeUtc());
        return new KeyValuePair<Instant?, ITimeline[]>(pair.Key.ToInstant(), pair.Value);
    }

    /// <summary>
    /// Returns the timelines on the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or the timelines on <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static KeyValuePair<Instant?, ITimeline[]> GetTimelinesAtCurrentOrPreviousUtcInstant(this IEnumerable<ITimeline> source, Instant instant)
    {
        var pair = source.GetTimelinesAtCurrentOrPreviousUtcInstant(instant.ToDateTimeUtc());
        return new KeyValuePair<Instant?, ITimeline[]>(pair.Key.ToInstant(), pair.Value);
    }

    /// <summary>
    /// Returns the timelines on the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static KeyValuePair<Instant?, ITimeline[]> GetTimelinesAtNextUtcInstant(this IEnumerable<ITimeline> source, Instant instant)
    {
        var pair = source.GetTimelinesAtNextUtcInstant(instant.ToDateTimeUtc());
        return new KeyValuePair<Instant?, ITimeline[]>(pair.Key.ToInstant(), pair.Value);
    }

    /// <summary>
    /// Returns the timelines on the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or the timelines on <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static KeyValuePair<Instant?, ITimeline[]> GetTimelinesAtCurrentOrNextUtcInstant(this IEnumerable<ITimeline> source, Instant instant)
    {
        var pair = source.GetTimelinesAtCurrentOrNextUtcInstant(instant.ToDateTimeUtc());
        return new KeyValuePair<Instant?, ITimeline[]>(pair.Key.ToInstant(), pair.Value);
    }

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant(this IEnumerable<ITimeline> source, Instant instant) =>
        source.ContainsInstant(instant.ToDateTimeUtc());

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of <paramref name="source"/>.
    /// </summary>
    public static bool IsInstant(this IEnumerable<ITimeline> source, Instant instant) =>
        source.IsInstant(instant.ToDateTimeUtc());
}