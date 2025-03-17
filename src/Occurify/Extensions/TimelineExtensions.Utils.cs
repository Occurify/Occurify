using Occurify.Helpers;
using Occurify.TimelineUtils;

namespace Occurify.Extensions;

public static partial class TimelineExtensions
{
    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the next instant on <paramref name="timeline"/>
    /// </summary>
    public static TimeSpan? GetTimeToNextInstant(this ITimeline timeline, DateTime instant)
    {
        return timeline.GetNextUtcInstant(instant) - instant;
    }

    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the previous instant on <paramref name="timeline"/>
    /// </summary>
    public static TimeSpan? GetTimeSincePreviousInstant(this ITimeline timeline, DateTime instant)
    {
        return instant - timeline.GetPreviousUtcInstant(instant);
    }

    /// <summary>
    /// Returns the previous instant on <paramref name="timeline"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on <paramref name="timeline"/>.
    /// </summary>
    public static DateTime? GetCurrentOrPreviousUtcInstant(this ITimeline timeline, DateTime instant)
    {
        if (instant == DateTime.MaxValue)
        {
            return timeline.IsInstant(instant) ? instant : timeline.GetPreviousUtcInstant(instant);
        }
        // By using instant + TimeSpan.FromTicks(1), we improve performance, as we don't have to call timeline.IsInstant(instant).
        return timeline.GetPreviousUtcInstant(instant + TimeSpan.FromTicks(1));
    }

    /// <summary>
    /// Returns the next instant on <paramref name="timeline"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on <paramref name="timeline"/>.
    /// </summary>
    public static DateTime? GetCurrentOrNextUtcInstant(this ITimeline timeline, DateTime instant)
    {
        if (instant == DateTime.MinValue)
        {
            return timeline.IsInstant(instant) ? instant : timeline.GetNextUtcInstant(instant);
        }
        // By using instant - TimeSpan.FromTicks(1), we improve performance, as we don't have to call timeline.IsInstant(instant).
        return timeline.GetNextUtcInstant(instant - TimeSpan.FromTicks(1));
    }

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on <paramref name="timeline"/>.
    /// </summary>
    public static bool ContainsInstant(this ITimeline timeline, DateTime instant) => timeline.IsInstant(instant);

    /// <summary>
    /// Determines whether <paramref name="timeline"/> contains no instants.
    /// </summary>
    public static bool IsEmpty(this ITimeline timeline) => timeline.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc) == null;

    /// <summary>
    /// Synchronizes <paramref name="source"/> such that method calls cannot occur concurrently.
    /// </summary>
    public static ITimeline Synchronize(this ITimeline source)
    {
        return new SynchronizedTimeline(source);
    }

    /// <summary>
    /// Synchronizes <paramref name="source"/> such that method calls cannot occur concurrently.
    /// </summary>
    public static ITimeline Synchronize(this ITimeline source, object gate)
    {
        return new SynchronizedTimeline(source, gate);
    }
}