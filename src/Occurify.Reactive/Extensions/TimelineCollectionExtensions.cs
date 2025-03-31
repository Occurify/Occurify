using Occurify.Extensions;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Occurify.Reactive.Extensions;

/// <summary>
/// Provides reactive extension methods for working with <see cref="IEnumerable{ITimeline}"/>.
/// </summary>
public static class TimelineCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IObservable{Unit}"/> that emits a <see cref="Unit"/> every time an instant occurs on any of the timelines in <paramref name="source"/>.
    /// If <paramref name="emitPulseUponSubscribe"/> is true, a pulse will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<Unit> ToPulseObservable(this IEnumerable<ITimeline> source, IScheduler scheduler,
        bool emitPulseUponSubscribe = true) =>
        source.ToPulseObservable(DateTime.UtcNow, scheduler, emitPulseUponSubscribe);

    /// <summary>
    /// Returns a <see cref="IObservable{Unit}"/> that emits a <see cref="Unit"/> every time an instant occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitPulseUponSubscribe"/> is true, a pulse will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<Unit> ToPulseObservable(this IEnumerable<ITimeline> source, DateTime relativeTo,
        IScheduler scheduler, bool emitPulseUponSubscribe = true) =>
        source.ToInstantObservable(relativeTo, scheduler, emitPulseUponSubscribe).Select(_ => Unit.Default);

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that emits an instant as <see cref="DateTime"/> when it occurs on any of the timelines in <paramref name="source"/>.
    /// If <paramref name="emitInstantUponSubscribe"/> is true, the current time will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservable(this IEnumerable<ITimeline> source, IScheduler scheduler,
        bool emitInstantUponSubscribe = true) =>
        source.ToInstantObservable(DateTime.UtcNow, scheduler, emitInstantUponSubscribe);

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that emits an instant as <see cref="DateTime"/> when it occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitInstantUponSubscribe"/> is true, the instant <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservable(this IEnumerable<ITimeline> source, DateTime relativeTo,
        IScheduler scheduler, bool emitInstantUponSubscribe = true) =>
        source.ToSampleObservable(relativeTo, scheduler, emitInstantUponSubscribe).Select(s => s.UtcSampleInstant);

    /// <summary>
    /// Returns a <see cref="IObservable{TimelineSample}"/> that emits a sample every time an instant occurs on any of the timelines in <paramref name="source"/>.
    /// If <paramref name="emitSampleUponSubscribe"/> is true, a sample at the current time will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<TimelineCollectionSample> ToSampleObservable(this IEnumerable<ITimeline> source,
        IScheduler scheduler, bool emitSampleUponSubscribe = true) =>
        source.ToSampleObservable(DateTime.UtcNow, scheduler, emitSampleUponSubscribe);

    /// <summary>
    /// Returns a <see cref="IObservable{TimelineSample}"/> that emits a sample every time an instant occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitSampleUponSubscribe"/> is true, the sample at <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<TimelineCollectionSample> ToSampleObservable(this IEnumerable<ITimeline> source,
        DateTime relativeTo, IScheduler scheduler, bool emitSampleUponSubscribe = true)
    {
        if (emitSampleUponSubscribe)
        {
            source = source.ToArray();
            return Observable.Defer(() =>
                source.ToSampleObservableInternal(relativeTo, scheduler).Prepend(source.SampleAt(relativeTo)));
        }

        return source.ToSampleObservableInternal(relativeTo, scheduler);
    }

    private static IObservable<TimelineCollectionSample> ToSampleObservableInternal(this IEnumerable<ITimeline> source,
        DateTime relativeTo, IScheduler scheduler)
    {
        source = source.ToArray();
        return Observable.Generate(
            source.GetNextUtcInstant(relativeTo),
            sample => sample != null,
            sample => source.GetNextUtcInstant(sample!.Value),
            sample => source.SampleAt(sample!.Value),
            sample => sample!.Value, scheduler);
    }
}