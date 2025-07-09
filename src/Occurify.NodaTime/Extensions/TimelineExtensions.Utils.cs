using NodaTime;
using Occurify.Helpers;
using Occurify.NodaTime.Extensions;
using Occurify.TimelineUtils;

namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="ITimeline"/>.
/// </summary>
public static partial class TimelineExtensions
{
    /// <summary>
    /// Returns the previous instant relative to <paramref name="instantRelativeTo"/>.
    /// </summary>
    public static Instant? GetPreviousInstant(this ITimeline timeline, Instant instantRelativeTo) =>
        timeline.GetPreviousUtcInstant(instantRelativeTo.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the next instant relative to <paramref name="instantRelativeTo"/>.
    /// </summary>
    public static Instant? GetNextInstant(this ITimeline timeline, Instant instantRelativeTo) =>
        timeline.GetNextUtcInstant(instantRelativeTo.ToDateTimeUtc()).ToInstant();

    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the next instant on <paramref name="timeline"/>.
    /// </summary>
    /// <param name="timeline">The timeline to search.</param>
    /// <param name="instant">The reference instant.</param>
    /// <returns>The duration until the next instant, or null if none exists.</returns>
    public static Duration? GetTimeToNextInstant(this ITimeline timeline, Instant instant)
    {
        return timeline.GetTimeToNextInstant(instant.ToDateTimeUtc()).ToDuration();
    }

    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the previous instant on <paramref name="timeline"/>.
    /// </summary>
    /// <param name="timeline">The timeline to search.</param>
    /// <param name="instant">The reference instant.</param>
    /// <returns>The duration since the previous instant, or null if none exists.</returns>
    public static Duration? GetTimeSincePreviousInstant(this ITimeline timeline, Instant instant)
    {
        return timeline.GetTimeSincePreviousInstant(instant.ToDateTimeUtc()).ToDuration();
    }

    /// <summary>
    /// Returns the previous instant on <paramref name="timeline"/> relative to <paramref name="instant"/>,
    /// or <paramref name="instant"/> itself if it is on <paramref name="timeline"/>.
    /// </summary>
    /// <param name="timeline">The timeline to search.</param>
    /// <param name="instant">The reference instant.</param>
    /// <returns>The previous or current instant, or null if none exists.</returns>
    public static Instant? GetCurrentOrPreviousInstant(this ITimeline timeline, Instant instant)
    {
        return timeline.GetCurrentOrPreviousUtcInstant(instant.ToDateTimeUtc()).ToInstant();
    }

    /// <summary>
    /// Returns the next instant on <paramref name="timeline"/> relative to <paramref name="instant"/>,
    /// or <paramref name="instant"/> itself if it is on <paramref name="timeline"/>.
    /// </summary>
    /// <param name="timeline">The timeline to search.</param>
    /// <param name="instant">The reference instant.</param>
    /// <returns>The next or current instant, or null if none exists.</returns>
    public static Instant? GetCurrentOrNextInstant(this ITimeline timeline, Instant instant)
    {
        return timeline.GetCurrentOrNextUtcInstant(instant.ToDateTimeUtc()).ToInstant();
    }

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on <paramref name="timeline"/>.
    /// </summary>
    /// <param name="timeline">The timeline to check.</param>
    /// <param name="instant">The instant to test for membership.</param>
    /// <returns>True if the instant is on the timeline; otherwise, false.</returns>
    public static bool ContainsInstant(this ITimeline timeline, Instant instant)
    {
        return timeline.ContainsInstant(instant.ToDateTimeUtc());
    }
}