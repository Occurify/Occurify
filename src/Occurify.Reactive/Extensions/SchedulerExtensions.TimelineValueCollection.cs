using System.Reactive.Concurrency;

namespace Occurify.Reactive.Extensions;

public static partial class SchedulerExtensions
{
    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="timelines"/>.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IScheduler scheduler, IEnumerable<KeyValuePair<ITimeline, TValue>> timelines, Action action, bool includeCurrentInstant = true) =>
        timelines.Subscribe(action, scheduler, includeCurrentInstant);

    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="timelines"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IScheduler scheduler, IEnumerable<KeyValuePair<ITimeline, TValue>> timelines, Action action, DateTime relativeTo, bool includeCurrentInstant = true) =>
        timelines.Subscribe(action, relativeTo, scheduler, includeCurrentInstant);

    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="timelines"/>.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IScheduler scheduler, IEnumerable<KeyValuePair<ITimeline, TValue>> timelines, Action<DateTime> action, bool includeCurrentInstant = true) =>
        timelines.Subscribe(action, scheduler, includeCurrentInstant);

    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="timelines"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IScheduler scheduler, IEnumerable<KeyValuePair<ITimeline, TValue>> timelines, Action<DateTime> action, DateTime relativeTo, bool includeCurrentInstant = true) =>
        timelines.Subscribe(action, relativeTo, scheduler, includeCurrentInstant);

    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="timelines"/>.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IScheduler scheduler, IEnumerable<KeyValuePair<ITimeline, TValue>> timelines, Action<KeyValuePair<DateTime, TValue[]>> action, bool includeCurrentInstant = true) =>
        timelines.Subscribe(action, scheduler, includeCurrentInstant);

    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="timelines"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately with the sample at <paramref name="relativeTo"/>.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IScheduler scheduler, IEnumerable<KeyValuePair<ITimeline, TValue>> timelines, Action<KeyValuePair<DateTime, TValue[]>> action, DateTime relativeTo, bool includeCurrentInstant = true) =>
        timelines.Subscribe(action, relativeTo, scheduler, includeCurrentInstant);
}