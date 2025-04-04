using System.Reactive.Concurrency;

namespace Occurify.Reactive.Extensions;

public static partial class TimelineValueCollectionExtensions
{
    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="source"/>.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Action action, IScheduler scheduler, bool includeCurrentInstant = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToPulseObservable(scheduler, includeCurrentInstant).Subscribe(_ => action());
    }

    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Action action, DateTime relativeTo, IScheduler scheduler, bool includeCurrentInstant = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToPulseObservable(relativeTo, scheduler, includeCurrentInstant).Subscribe(_ => action());
    }

    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="source"/>.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Action<DateTime> action, IScheduler scheduler, bool includeCurrentInstant = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToInstantObservable(scheduler, includeCurrentInstant).Subscribe(action);
    }

    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Action<DateTime> action, DateTime relativeTo, IScheduler scheduler, bool includeCurrentInstant = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToInstantObservable(relativeTo, scheduler, includeCurrentInstant).Subscribe(action);
    }

    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="source"/>.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Action<KeyValuePair<DateTime, TValue[]>> action, IScheduler scheduler, bool includeCurrentInstant = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToSampleObservable(scheduler, includeCurrentInstant).Subscribe(action);
    }

    /// <summary>
    /// Subscribes an action to be executed every instant on <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentInstant"/> is true, the action will be executed immediately with the sample at <paramref name="relativeTo"/>.
    /// </summary>
    public static IDisposable Subscribe<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Action<KeyValuePair<DateTime, TValue[]>> action, DateTime relativeTo, IScheduler scheduler, bool includeCurrentInstant = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToSampleObservable(relativeTo, scheduler, includeCurrentInstant).Subscribe(action);
    }
}