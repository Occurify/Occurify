using System.Reactive.Concurrency;

namespace Occurify.Reactive.Extensions;

public static partial class TimelineExtensions
{
    /// <summary>
    /// Subscribes an action to be executed every instant of <paramref name="timeline"/>.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe(this ITimeline timeline, Action action, IScheduler scheduler, bool includeCurrentInstant = true)
    {
        ArgumentNullException.ThrowIfNull(timeline);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return timeline.ToPulseObservable(scheduler, includeCurrentInstant).Subscribe(_ => action());
    }

    /// <summary>
    /// Subscribes an action to be executed every instant of <paramref name="timeline"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe(this ITimeline timeline, Action action, DateTime relativeTo, IScheduler scheduler, bool includeCurrentInstant = true)
    {
        ArgumentNullException.ThrowIfNull(timeline);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return timeline.ToPulseObservable(relativeTo, scheduler, includeCurrentInstant).Subscribe(_ => action());
    }

    /// <summary>
    /// Subscribes an action to be executed every instant of <paramref name="timeline"/>.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe(this ITimeline timeline, Action<DateTime> action, IScheduler scheduler, bool includeCurrentInstant = true)
    {
        ArgumentNullException.ThrowIfNull(timeline);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return timeline.ToInstantObservable(scheduler, includeCurrentInstant).Subscribe(action);
    }

    /// <summary>
    /// Subscribes an action to be executed every instant of <paramref name="timeline"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe(this ITimeline timeline, Action<DateTime> action, DateTime relativeTo, IScheduler scheduler, bool includeCurrentInstant = true)
    {
        ArgumentNullException.ThrowIfNull(timeline);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return timeline.ToInstantObservable(relativeTo, scheduler, includeCurrentInstant).Subscribe(action);
    }
}