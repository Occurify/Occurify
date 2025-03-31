using Occurify.Extensions;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Occurify.Reactive.Extensions;

/// <summary>
/// Provides reactive extension methods for working with <see cref="ITimeline"/>.
/// </summary>
public static class TimelineExtensions
{
    /// <summary>
    /// Returns a <see cref="IObservable{Unit}"/> that emits a <see cref="Unit"/> every time an instant occurs.
    /// If <paramref name="emitPulseUponSubscribe"/> is true, a pulse will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<Unit> ToPulseObservable(this ITimeline timeline, IScheduler scheduler,
        bool emitPulseUponSubscribe = true) =>
        timeline.ToPulseObservable(DateTime.UtcNow, scheduler, emitPulseUponSubscribe);

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
        timeline.ToInstantObservable(DateTime.UtcNow, scheduler, emitInstantUponSubscribe);

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that emits an instant as <see cref="DateTime"/> when it occurs using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitInstantUponSubscribe"/> is true, the instant <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservable(this ITimeline timeline, DateTime relativeTo,
        IScheduler scheduler, bool emitInstantUponSubscribe = true) =>
        timeline.ToSampleObservable(relativeTo, scheduler, emitInstantUponSubscribe).Select(s => s.UtcSampleInstant);

    /// <summary>
    /// Returns a <see cref="IObservable{TimelineSample}"/> that emits a sample every time an instant occurs.
    /// If <paramref name="emitSampleUponSubscribe"/> is true, a sample at the current time will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<TimelineSample> ToSampleObservable(this ITimeline timeline, IScheduler scheduler,
        bool emitSampleUponSubscribe = true) =>
        timeline.ToSampleObservable(DateTime.UtcNow, scheduler, emitSampleUponSubscribe);

    /// <summary>
    /// Returns a <see cref="IObservable{TimelineSample}"/> that emits a sample every time an instant occurs using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitSampleUponSubscribe"/> is true, the sample at <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<TimelineSample> ToSampleObservable(this ITimeline timeline, DateTime relativeTo,
        IScheduler scheduler, bool emitSampleUponSubscribe = true)
    {
        if (emitSampleUponSubscribe)
        {
            return Observable.Defer(() =>
                timeline.ToSampleObservableInternal(relativeTo, scheduler)
                    .Prepend(timeline.SampleAt(relativeTo)));
        }

        return timeline.ToSampleObservableInternal(relativeTo, scheduler);
    }

    private static IObservable<TimelineSample> ToSampleObservableInternal(this ITimeline timeline, DateTime relativeTo,
        IScheduler scheduler)
    {
        return Observable.Generate(
            timeline.GetNextUtcInstant(relativeTo),
            sample => sample != null,
            sample => timeline.GetNextUtcInstant(sample!.Value),
            sample => timeline.SampleAt(sample!.Value),
            sample => sample!.Value, scheduler);
    }
}