
namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with collections related to <see cref="IEnumerable{ITimeline}"/>.
/// </summary>
public static partial class TimelineCollectionExtensions
{
    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest next instant on any of <paramref name="source"/>.
    /// </summary>
    public static TimeSpan? GetTimeToNextInstant(this IEnumerable<ITimeline> source, DateTime instant)
    {
        return source.GetNextUtcInstant(instant) - instant;
    }

    /// <summary>
    /// Returns the time between <paramref name="instant"/> and the closest previous instant on any of <paramref name="source"/>.
    /// </summary>
    public static TimeSpan? GetTimeSincePreviousInstant(this IEnumerable<ITimeline> source, DateTime instant)
    {
        return instant - source.GetPreviousUtcInstant(instant);
    }

    /// <summary>
    /// Returns the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static DateTime? GetPreviousUtcInstant(this IEnumerable<ITimeline> source, DateTime instant)
    {
        return source.Max(tl => tl.GetPreviousUtcInstant(instant));
    }

    /// <summary>
    /// Returns the closest next instant on any of <paramref name="source"/> relative to <paramref name="instant"/>.
    /// </summary>
    public static DateTime? GetNextUtcInstant(this IEnumerable<ITimeline> source, DateTime instant)
    {
        return source.Min(tl => tl.GetNextUtcInstant(instant));
    }

    /// <summary>
    /// Returns the closest previous instant on any of <paramref name="source"/> relative to <paramref name="instant"/>, or <paramref name="instant"/> itself if it is on any of <paramref name="source"/>.
    /// </summary>
    public static DateTime? GetCurrentOrPreviousUtcInstant(this IEnumerable<ITimeline> source, DateTime instant)
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
    public static DateTime? GetCurrentOrNextUtcInstant(this IEnumerable<ITimeline> source, DateTime instant)
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
    /// Determines whether <paramref name="instant"/> is on any of <paramref name="source"/>.
    /// </summary>
    public static bool ContainsInstant(this IEnumerable<ITimeline> source, DateTime instant) => 
        source.Any(s => s.IsInstant(instant));

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of <paramref name="source"/>.
    /// </summary>
    public static bool IsInstant(this IEnumerable<ITimeline> source, DateTime instant) =>
        source.Any(s => s.IsInstant(instant));

    /// <summary>
    /// Determines whether none of <paramref name="source"/> contains any instants.
    /// </summary>
    public static bool AreEmpty(this IEnumerable<ITimeline> source) => source.All(s => s.IsEmpty());

    /// <summary>
    /// Synchronizes all <paramref name="source"/> using the same gate such that method calls cannot occur concurrently.
    /// </summary>
    public static IEnumerable<ITimeline> Synchronize(this IEnumerable<ITimeline> source) => source.Synchronize(new());

    /// <summary>
    /// Synchronizes all <paramref name="source"/> using the same gate such that method calls cannot occur concurrently.
    /// </summary>
    public static IEnumerable<ITimeline> Synchronize(this IEnumerable<ITimeline> source, object gate)
    {
        return source.Select(s => s.Synchronize(gate));
    }
}