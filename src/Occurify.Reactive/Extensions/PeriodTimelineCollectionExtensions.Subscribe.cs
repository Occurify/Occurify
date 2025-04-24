
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Occurify.Reactive.Extensions;

public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Subscribes actions to be executed when the first period on any timeline in <paramref name="source"/> starts or the last period ends.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyStartEnd(this IEnumerable<IPeriodTimeline> source, Action startAction,
        Action endAction, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAnyBooleanObservable(scheduler, includeCurrentSample).Subscribe(b =>
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
    /// Subscribes actions to be executed when the first period on any timeline in <paramref name="source"/> starts or the last period ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyStartEnd(this IEnumerable<IPeriodTimeline> source, Action startAction, Action endAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAnyBooleanObservable(relativeTo, scheduler, includeCurrentSample).Subscribe(b =>
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
    /// Subscribes actions to be executed when the first period on any timeline in <paramref name="source"/> starts.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyStart(this IEnumerable<IPeriodTimeline> source, Action startAction, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAnyBooleanObservable(scheduler, includeCurrentSample).Where(x => x).Subscribe(_ => startAction());
    }

    /// <summary>
    /// Subscribes actions to be executed when the first period on any timeline in <paramref name="source"/> starts using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyStart(this IEnumerable<IPeriodTimeline> source, Action startAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAnyBooleanObservable(relativeTo, scheduler, includeCurrentSample).Where(x => x).Subscribe(_ => startAction());
    }

    /// <summary>
    /// Subscribes actions to be executed when the last period on any timeline in <paramref name="source"/> ends.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyEnd(this IEnumerable<IPeriodTimeline> source, Action endAction, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAnyBooleanObservable(scheduler, includeCurrentSample).Where(x => !x).Subscribe(_ => endAction());
    }

    /// <summary>
    /// Subscribes actions to be executed when the last period on any timeline in <paramref name="source"/> ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAnyEnd(this IEnumerable<IPeriodTimeline> source, Action endAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAnyBooleanObservable(relativeTo, scheduler, includeCurrentSample).Where(x => !x).Subscribe(_ => endAction());
    }

    /// <summary>
    /// Subscribes actions to be executed when the all periods in <paramref name="source"/> have a period or when this is no longer the case.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllStartEnd(this IEnumerable<IPeriodTimeline> source, Action startAction,
        Action endAction, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAllBooleanObservable(scheduler, includeCurrentSample).Subscribe(b =>
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
    /// Subscribes actions to be executed when the all periods in <paramref name="source"/> have a period or when this is no longer the case. Using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllStartEnd(this IEnumerable<IPeriodTimeline> source, Action startAction, Action endAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAllBooleanObservable(relativeTo, scheduler, includeCurrentSample).Subscribe(b =>
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
    /// Subscribes actions to be executed when the all periods in <paramref name="source"/> have a period.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllStart(this IEnumerable<IPeriodTimeline> source, Action startAction, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAllBooleanObservable(scheduler, includeCurrentSample).Where(x => x).Subscribe(_ => startAction());
    }

    /// <summary>
    /// Subscribes actions to be executed when the all periods in <paramref name="source"/> have a period using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllStart(this IEnumerable<IPeriodTimeline> source, Action startAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(startAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAllBooleanObservable(relativeTo, scheduler, includeCurrentSample).Where(x => x).Subscribe(_ => startAction());
    }

    /// <summary>
    /// Subscribes actions to be executed when not all periods in <paramref name="source"/> have a period.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllEnd(this IEnumerable<IPeriodTimeline> source, Action endAction, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAllBooleanObservable(scheduler, includeCurrentSample).Where(x => !x).Subscribe(_ => endAction());
    }

    /// <summary>
    /// Subscribes actions to be executed when not all periods in <paramref name="source"/> have a period using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="includeCurrentSample"/> is true, the actions will be executed immediately based on the current period state.
    /// </summary>
    public static IDisposable SubscribeAllEnd(this IEnumerable<IPeriodTimeline> source, Action endAction, DateTime relativeTo, IScheduler scheduler, bool includeCurrentSample = true)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(endAction);
        ArgumentNullException.ThrowIfNull(scheduler);

        return source.ToAllBooleanObservable(relativeTo, scheduler, includeCurrentSample).Where(x => !x).Subscribe(_ => endAction());
    }
}