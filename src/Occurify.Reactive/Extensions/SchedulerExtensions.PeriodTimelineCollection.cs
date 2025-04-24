using System.Reactive.Concurrency;

namespace Occurify.Reactive.Extensions;

public static partial class SchedulerExtensions
{
    /// <summary>
    /// Subscribes actions to be executed when the first period on any timeline in <paramref name="source"/> starts or the last period ends.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyStartEnd(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source,
        Action startAction,
        Action endAction, bool includeCurrentSample = true) =>
        source.SubscribeAnyStartEnd(startAction, endAction, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when the first period on any timeline in <paramref name="source"/> starts or the last period ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyStartEnd(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action startAction, Action endAction, DateTime relativeTo, bool includeCurrentSample = true) =>
        source.SubscribeAnyStartEnd(startAction, endAction, relativeTo, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when the first period on any timeline in <paramref name="source"/> starts.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyStart(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action startAction, bool includeCurrentSample = true) =>
        source.SubscribeAnyStart(startAction, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when the first period on any timeline in <paramref name="source"/> starts using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyStart(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action startAction, DateTime relativeTo, bool includeCurrentSample = true) =>
        source.SubscribeAnyStart(startAction, relativeTo, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when the last period on any timeline in <paramref name="source"/> ends.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyEnd(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action endAction, bool includeCurrentSample = true) =>
        source.SubscribeAnyEnd(endAction, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when the last period on any timeline in <paramref name="source"/> ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyEnd(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action endAction, DateTime relativeTo, bool includeCurrentSample = true) =>
        source.SubscribeAnyEnd(endAction, relativeTo, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when the all periods in <paramref name="source"/> have a period or when this is no longer the case.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllStartEnd(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action startAction,
        Action endAction, bool includeCurrentSample = true) =>
        source.SubscribeAllStartEnd(startAction, endAction, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when the all periods in <paramref name="source"/> have a period or when this is no longer the case. Using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllStartEnd(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action startAction, Action endAction, DateTime relativeTo, bool includeCurrentSample = true) =>
        source.SubscribeAllStartEnd(startAction, endAction, relativeTo, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when the all periods in <paramref name="source"/> have a period.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllStart(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action startAction, bool includeCurrentSample = true) =>
        source.SubscribeAllStart(startAction, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when the all periods in <paramref name="source"/> have a period using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllStart(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action startAction, DateTime relativeTo, bool includeCurrentSample = true) =>
        source.SubscribeAllStart(startAction, relativeTo, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when not all periods in <paramref name="source"/> have a period.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllEnd(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action endAction, bool includeCurrentSample = true) =>
        source.SubscribeAllEnd(endAction, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when not all periods in <paramref name="source"/> have a period using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllEnd(this IScheduler scheduler, IEnumerable<IPeriodTimeline> source, Action endAction, DateTime relativeTo, bool includeCurrentSample = true) =>
        source.SubscribeAllEnd(endAction, relativeTo, scheduler, includeCurrentSample);
}