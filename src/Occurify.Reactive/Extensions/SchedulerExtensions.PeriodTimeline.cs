using System.Reactive.Concurrency;
using Occurify.Extensions;

namespace Occurify.Reactive.Extensions;

public static partial class SchedulerExtensions
{
    /// <summary>
    /// Subscribes actions to be executed when a period on <paramref name="periodTimeline"/> starts or ends.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStartEnd(this IScheduler scheduler, IPeriodTimeline periodTimeline, Action startAction,
        Action endAction, bool includeCurrentSample = true) =>
        periodTimeline.SubscribeStartEnd(startAction, endAction, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when a period on <paramref name="periodTimeline"/> starts or ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStartEnd(this IScheduler scheduler, IPeriodTimeline periodTimeline, Action startAction, Action endAction, DateTime relativeTo, bool includeCurrentSample = true) =>
        periodTimeline.SubscribeStartEnd(startAction, endAction, relativeTo, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> starts.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStart(this IScheduler scheduler, IPeriodTimeline periodTimeline, Action startAction, bool includeCurrentSample = true) =>
        periodTimeline.SubscribeStart(startAction, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> starts using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStart(this IScheduler scheduler, IPeriodTimeline periodTimeline, Action startAction, DateTime relativeTo, bool includeCurrentSample = true) =>
        periodTimeline.SubscribeStart(startAction, relativeTo, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> ends.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeEnd(this IScheduler scheduler, IPeriodTimeline periodTimeline, Action endAction, bool includeCurrentSample = true) =>
        periodTimeline.SubscribeEnd(endAction, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeEnd(this IScheduler scheduler, IPeriodTimeline periodTimeline, Action endAction, DateTime relativeTo, bool includeCurrentSample = true) =>
        periodTimeline.SubscribeEnd(endAction, relativeTo, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes an action to be executed when a period on <paramref name="periodTimeline"/> starts or ends.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe(this IScheduler scheduler, IPeriodTimeline periodTimeline, Action<PeriodTimelineSample> action, bool includeCurrentSample = true) =>
        periodTimeline.Subscribe(action, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes an action to be executed when a period on <paramref name="periodTimeline"/> starts or ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable Subscribe(this IScheduler scheduler, IPeriodTimeline periodTimeline, Action<PeriodTimelineSample> action, DateTime relativeTo, bool includeCurrentSample = true) =>
        periodTimeline.Subscribe(action, relativeTo, scheduler, includeCurrentSample);
}