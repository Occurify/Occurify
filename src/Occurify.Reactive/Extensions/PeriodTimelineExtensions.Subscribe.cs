
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Occurify.Reactive.Extensions;

public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Subscribes actions to be executed when a period on <paramref name="periodTimeline"/> starts or ends.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStartEnd(this IPeriodTimeline periodTimeline, Action startAction,
        Action endAction, IScheduler scheduler, bool includeCurrentSample = true) =>
        periodTimeline.SubscribeStartEnd(startAction, endAction, DateTime.UtcNow, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes actions to be executed when a period on <paramref name="periodTimeline"/> starts or ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStartEnd(this IPeriodTimeline periodTimeline, Action startAction, Action endAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(scheduler);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(endAction);

        return periodTimeline.ToBooleanObservable(relativeTo, scheduler, includeCurrentSample).Subscribe(b =>
        {
            if (b)
            {
                startAction();
                return;
            }

            endAction();
        });
    }

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> starts.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStart(this IPeriodTimeline periodTimeline, Action startAction,
        IScheduler scheduler, bool includeCurrentSample = true) =>
        periodTimeline.SubscribeStart(startAction, DateTime.UtcNow, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> starts using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStart(this IPeriodTimeline periodTimeline, Action startAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(scheduler);
        ArgumentNullException.ThrowIfNull(startAction);

        return periodTimeline
            .ToBooleanObservable(relativeTo, scheduler, includeCurrentSample)
            .Where(x => x)
            .Subscribe(_ => startAction());
    }

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> ends.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeEnd(this IPeriodTimeline periodTimeline, Action startAction,
        IScheduler scheduler, bool includeCurrentSample = true) =>
        periodTimeline.SubscribeEnd(startAction, DateTime.UtcNow, scheduler, includeCurrentSample);

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeEnd(this IPeriodTimeline periodTimeline, Action endAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(scheduler);
        ArgumentNullException.ThrowIfNull(endAction);

        return periodTimeline
            .ToBooleanObservable(relativeTo, scheduler, includeCurrentSample)
            .Where(x => !x)
            .Subscribe(_ => endAction());
    }
}