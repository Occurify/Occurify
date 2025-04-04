using System.Reactive.Concurrency;

namespace Occurify.Reactive.Extensions;

/// <summary>
/// Extension methods for <see cref="IScheduler"/>.
/// </summary>
public static partial class SchedulerExtensions
{
    /// <summary>
    /// Subscribes an action to be executed every instant of <paramref name="timeline"/>.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe(this IScheduler scheduler, ITimeline timeline, Action action, bool includeCurrentInstant = true) =>
        timeline.Subscribe(action, scheduler, includeCurrentInstant);

    /// <summary>
    /// Subscribes an action to be executed every instant of <paramref name="timeline"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe(this IScheduler scheduler, ITimeline timeline, Action action, DateTime relativeTo, bool includeCurrentInstant = true) =>
        timeline.Subscribe(action, relativeTo, scheduler, includeCurrentInstant);

    /// <summary>
    /// Subscribes an action to be executed every instant of <paramref name="timeline"/>.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe(this IScheduler scheduler, ITimeline timeline, Action<DateTime> action, bool includeCurrentInstant = true) =>
        timeline.Subscribe(action, scheduler, includeCurrentInstant);

    /// <summary>
    /// Subscribes an action to be executed every instant of <paramref name="timeline"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe(this IScheduler scheduler, ITimeline timeline, Action<DateTime> action, DateTime relativeTo, bool includeCurrentInstant = true) =>
        timeline.Subscribe(action, relativeTo, scheduler, includeCurrentInstant);
}