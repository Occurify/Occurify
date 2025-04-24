using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Occurify.Reactive.Extensions;

/// <summary>
/// Provides reactive extension methods for working with <see cref="IEnumerable{IPeriodTimeline}"/>.
/// </summary>
public static partial class PeriodTimelineCollectionExtensions
{
    /// <summary>
    /// Returns a <c>IObservable</c> that emits <c>true</c> when any timeline in <paramref name="source"/> has a period. Otherwise <c>false</c> is emitted.
    /// If <paramref name="emitStateUponSubscribe"/> is true, the state at the current time will be emitted immediately upon subscribing.
    /// Output is distinct, meaning the observable will only emit <c>true</c> when the first timeline starts a period, and <c>false</c> when the last timeline ends a period.
    /// </summary>
    public static IObservable<bool> ToAnyBooleanObservable(this IEnumerable<IPeriodTimeline> source, IScheduler scheduler,
        bool emitStateUponSubscribe = true)
    {
        // Note: We create an observable using emitStateUponSubscribe true because we need the initial values for CombineLatest. If we do not desire a state upon subscribing, we filter out that sample.
        var observable = source.Select(tl => tl.ToBooleanObservable(scheduler, emitStateUponSubscribe: true))
            .CombineLatest(values => values.Any(b => b)).DistinctUntilChanged();
        if (!emitStateUponSubscribe)
        {
            // Note: technically it could happen that we now skip a valid value if it occurs the exact tick we subscribe. This is very unlikely, and doesn't matter in practice.
            return observable.Skip(1);
        }

        return observable;
    }

    /// <summary>
    /// Returns a <c>IObservable</c> that emits <c>true</c> when any timeline in <paramref name="source"/> has a period. Otherwise <c>false</c> is emitted.
    /// If <paramref name="emitStateUponSubscribe"/> is true, the state at <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// Output is distinct, meaning the observable will only emit <c>true</c> when the first timeline starts a period, and <c>false</c> when the last timeline ends a period.
    /// </summary>
    public static IObservable<bool> ToAnyBooleanObservable(this IEnumerable<IPeriodTimeline> source, DateTime relativeTo,
        IScheduler scheduler, bool emitStateUponSubscribe = true)
    {
        // Note: We create an observable using emitStateUponSubscribe true because we need the initial values for CombineLatest. If we do not desire a state upon subscribing, we filter out that sample.
        var observable = source.Select(tl => tl.ToBooleanObservable(relativeTo, scheduler, emitStateUponSubscribe: true))
            .CombineLatest(values => values.Any(b => b)).DistinctUntilChanged();
        if (!emitStateUponSubscribe)
        {
            // Note: technically it could happen that we now skip a valid value if it occurs the exact tick we subscribe. This is very unlikely, and doesn't matter in practice.
            return observable.Skip(1);
        }

        return observable;
    }

    /// <summary>
    /// Returns a <c>IObservable</c> that emits <c>true</c> when all timelines in <paramref name="source"/> have a period. Otherwise <c>false</c> is emitted.
    /// If <paramref name="emitStateUponSubscribe"/> is true, the state at the current time will be emitted immediately upon subscribing.
    /// Output is distinct, meaning the observable will only emit <c>true</c> when the last timeline starts a period, and <c>false</c> when the first timeline ends a period.
    /// </summary>
    public static IObservable<bool> ToAllBooleanObservable(this IEnumerable<IPeriodTimeline> source, IScheduler scheduler,
        bool emitStateUponSubscribe = true)
    {
        // Note: We create an observable using emitStateUponSubscribe true because we need the initial values for CombineLatest. If we do not desire a state upon subscribing, we filter out that sample.
        var observable = source.Select(tl => tl.ToBooleanObservable(scheduler, emitStateUponSubscribe: true))
            .CombineLatest(values => values.All(b => b)).DistinctUntilChanged();
        if (!emitStateUponSubscribe)
        {
            // Note: technically it could happen that we now skip a valid value if it occurs the exact tick we subscribe. This is very unlikely, and doesn't matter in practice.
            return observable.Skip(1);
        }

        return observable;
    }

    /// <summary>
    /// Returns a <c>IObservable</c> that emits <c>true</c> when all timelines in <paramref name="source"/> have a period. Otherwise <c>false</c> is emitted.
    /// If <paramref name="emitStateUponSubscribe"/> is true, the state at <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// Output is distinct, meaning the observable will only emit <c>true</c> when the last timeline starts a period, and <c>false</c> when the first timeline ends a period.
    /// </summary>
    public static IObservable<bool> ToAllBooleanObservable(this IEnumerable<IPeriodTimeline> source, DateTime relativeTo,
        IScheduler scheduler, bool emitStateUponSubscribe = true)
    {
        // Note: We create an observable using emitStateUponSubscribe true because we need the initial values for CombineLatest. If we do not desire a state upon subscribing, we filter out that sample.
        var observable = source.Select(tl => tl.ToBooleanObservable(relativeTo, scheduler, emitStateUponSubscribe: true))
            .CombineLatest(values => values.All(b => b)).DistinctUntilChanged();
        if (!emitStateUponSubscribe)
        {
            // Note: technically it could happen that we now skip a valid value if it occurs the exact tick we subscribe. This is very unlikely, and doesn't matter in practice.
            return observable.Skip(1);
        }

        return observable;
    }
}