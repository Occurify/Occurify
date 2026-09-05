using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Occurify.Reactive.Helpers;

namespace Occurify.Reactive.Extensions;

/// <summary>
/// Provides reactive extension methods for working with <see cref="ITimeline"/>.
/// </summary>
public static partial class TimelineExtensions
{
    /// <summary>
    /// Returns a <see cref="IObservable{Unit}"/> that emits a <see cref="Unit"/> every time an instant occurs.
    /// If <paramref name="emitPulseUponSubscribe"/> is true, a pulse will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<Unit> ToPulseObservable(this ITimeline timeline, IScheduler scheduler,
        bool emitPulseUponSubscribe = true) =>
        timeline.ToInstantObservable(scheduler, emitPulseUponSubscribe).Select(_ => Unit.Default);

    /// <summary>
    /// Returns a <see cref="IObservable{Unit}"/> that emits a <see cref="Unit"/> every time an instant occurs using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitPulseUponSubscribe"/> is true, a pulse will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<Unit> ToPulseObservable(this ITimeline timeline, DateTime relativeTo,
        IScheduler scheduler, bool emitPulseUponSubscribe = true) =>
        timeline.ToInstantObservable(relativeTo, scheduler, emitPulseUponSubscribe).Select(_ => Unit.Default);

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that emits an instant as <see cref="DateTime"/> when it occurs.
    /// If <paramref name="emitInstantUponSubscribe"/> is true, the current time will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservable(this ITimeline timeline, IScheduler scheduler,
        bool emitInstantUponSubscribe = true) =>
        // The clock is read per subscription (and from the scheduler) so late or repeated subscribers don't replay instants that already passed.
        Observable.Defer(() => timeline.ToInstantObservable(scheduler.Now.UtcDateTime, scheduler, emitInstantUponSubscribe));

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that emits an instant as <see cref="DateTime"/> when it occurs using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitInstantUponSubscribe"/> is true, the instant <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservable(this ITimeline timeline, DateTime relativeTo,
        IScheduler scheduler, bool emitInstantUponSubscribe = true)
    {
        DateTimeGuard.EnsureUtc(relativeTo, nameof(relativeTo));

        if (emitInstantUponSubscribe)
        {
            return Observable.Defer(() =>
                timeline.ToInstantObservableInternal(relativeTo, scheduler)
                    .Prepend(relativeTo));
        }

        return Observable.Defer(() => timeline.ToInstantObservableInternal(relativeTo, scheduler));
    }

    private static IObservable<DateTime> ToInstantObservableInternal(this ITimeline timeline, DateTime relativeTo,
        IScheduler scheduler)
    {
        return Observable.Generate(
            timeline.GetNextUtcInstant(relativeTo),
            sample => sample != null,
            sample => timeline.GetNextUtcInstant(sample!.Value),
            sample => sample!.Value,
            sample => sample!.Value, scheduler);
    }
}