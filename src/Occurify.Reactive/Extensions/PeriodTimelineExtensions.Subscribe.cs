
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Occurify.Extensions;

namespace Occurify.Reactive.Extensions;

public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Subscribes actions to be executed when a period on <paramref name="periodTimeline"/> starts or ends.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStartEnd(this IPeriodTimeline periodTimeline, Action startAction,
        Action endAction, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(periodTimeline);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return periodTimeline.ToBooleanObservable(scheduler, includeCurrentSample).Subscribe(b =>
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
    /// Subscribes actions to be executed when a period on <paramref name="periodTimeline"/> starts or ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStartEnd(this IPeriodTimeline periodTimeline, Action startAction, Action endAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(periodTimeline);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

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
        IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(periodTimeline);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return periodTimeline
            .ToBooleanObservable(scheduler, includeCurrentSample)
            .Where(x => x)
            .Subscribe(_ => startAction());
    }

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> starts using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeStart(this IPeriodTimeline periodTimeline, Action startAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(periodTimeline);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return periodTimeline
            .ToBooleanObservable(relativeTo, scheduler, includeCurrentSample)
            .Where(x => x)
            .Subscribe(_ => startAction());
    }

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> ends.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeEnd(this IPeriodTimeline periodTimeline, Action endAction,
        IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(periodTimeline);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return periodTimeline
            .ToBooleanObservable(scheduler, includeCurrentSample)
            .Where(x => !x)
            .Subscribe(_ => endAction());
    }

    /// <summary>
    /// Subscribes action to be executed when a period on <paramref name="periodTimeline"/> ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeEnd(this IPeriodTimeline periodTimeline, Action endAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(periodTimeline);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return periodTimeline
            .ToBooleanObservable(relativeTo, scheduler, includeCurrentSample)
            .Where(x => !x)
            .Subscribe(_ => endAction());
    }

    /// <summary>
    /// Subscribes actions to be executed when a period on <paramref name="periodTimeline"/> starts or ends.
    /// If <paramref name="includeCurrentSample"/> is true, the action will be executed immediately.
    /// </summary>
    public static IDisposable SubscribeToSamples(this IPeriodTimeline periodTimeline, Action<PeriodTimelineSample> action, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(periodTimeline);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return periodTimeline.ToSampleObservable(scheduler, includeCurrentSample).Subscribe(action);
    }

    /// <summary>
    /// Subscribes actions to be executed when a period on <paramref name="periodTimeline"/> starts or ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately.
    /// </summary>
    public static IDisposable SubscribeToSamples(this IPeriodTimeline periodTimeline, Action<PeriodTimelineSample> action, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(periodTimeline);
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(scheduler);

        return periodTimeline.ToSampleObservable(relativeTo, scheduler, includeCurrentSample).Subscribe(action);
    }
}