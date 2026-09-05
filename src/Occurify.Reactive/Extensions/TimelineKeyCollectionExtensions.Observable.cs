using Occurify.Extensions;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Occurify.Reactive.Helpers;

namespace Occurify.Reactive.Extensions;

/// <summary>
/// Provides reactive extension methods for working with <see cref="IEnumerable{KeyValuePair}"/> with <see cref="ITimeline"/> as value.
/// </summary>
public static partial class TimelineKeyCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IObservable{Unit}"/> that emits a <see cref="Unit"/> every time an instant occurs on any of the timelines in <paramref name="source"/>.
    /// If <paramref name="emitPulseUponSubscribe"/> is true, a pulse will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<Unit> ToPulseObservable<TValue>(this IEnumerable<KeyValuePair<TValue, ITimeline>> source,
        IScheduler scheduler, bool emitPulseUponSubscribe = true) =>
        source.ToInstantObservable(scheduler, emitPulseUponSubscribe).Select(_ => Unit.Default);

    /// <summary>
    /// Returns a <see cref="IObservable{Unit}"/> that emits a <see cref="Unit"/> every time an instant occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitPulseUponSubscribe"/> is true, a pulse will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<Unit> ToPulseObservable<TValue>(this IEnumerable<KeyValuePair<TValue, ITimeline>> source,
        DateTime relativeTo, IScheduler scheduler, bool emitPulseUponSubscribe = true) =>
        source.ToInstantObservable(relativeTo, scheduler, emitPulseUponSubscribe).Select(_ => Unit.Default);

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that emits an instant as <see cref="DateTime"/> when it occurs on any of the timelines in <paramref name="source"/>.
    /// If <paramref name="emitInstantUponSubscribe"/> is true, the current time will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservable<TValue>(
        this IEnumerable<KeyValuePair<TValue, ITimeline>> source, IScheduler scheduler,
        bool emitInstantUponSubscribe = true) =>
        source.ToSampleObservable(scheduler, emitInstantUponSubscribe).Select(s => s.Key);

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that emits an instant as <see cref="DateTime"/> when it occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitInstantUponSubscribe"/> is true, the instant <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservable<TValue>(
        this IEnumerable<KeyValuePair<TValue, ITimeline>> source, DateTime relativeTo, IScheduler scheduler,
        bool emitInstantUponSubscribe = true) =>
        source.ToSampleObservable(relativeTo, scheduler, emitInstantUponSubscribe).Select(s => s.Key);

    /// <summary>
    /// Returns a <see cref="IObservable{TimelineSample}"/> that emits a sample every time an instant occurs on any of the timelines in <paramref name="source"/>.
    /// If <paramref name="emitSampleUponSubscribe"/> is true, a sample at the current time will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<KeyValuePair<DateTime, TValue[]>> ToSampleObservable<TValue>(
        this IEnumerable<KeyValuePair<TValue, ITimeline>> source, IScheduler scheduler,
        bool emitSampleUponSubscribe = true)
    {
        var timelines = source.ToArray();
        // The clock is read per subscription (and from the scheduler) so late or repeated subscribers don't replay samples that already passed.
        return Observable.Defer(() => timelines.ToSampleObservable(scheduler.Now.UtcDateTime, scheduler, emitSampleUponSubscribe));
    }

    /// <summary>
    /// Returns a <see cref="IObservable{TimelineSample}"/> that emits a sample every time an instant occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitSampleUponSubscribe"/> is true, the sample at <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<KeyValuePair<DateTime, TValue[]>> ToSampleObservable<TValue>(
        this IEnumerable<KeyValuePair<TValue, ITimeline>> source, DateTime relativeTo, IScheduler scheduler,
        bool emitSampleUponSubscribe = true)
    {
        DateTimeGuard.EnsureUtc(relativeTo, nameof(relativeTo));

        var timelines = source.ToArray();
        if (emitSampleUponSubscribe)
        {
            return Observable.Defer(() =>
                timelines.ToSampleObservableInternal(relativeTo, scheduler)
                    .Prepend(new KeyValuePair<DateTime, TValue[]>(relativeTo, timelines.GetKeysAtUtcInstant(relativeTo))));
        }

        return Observable.Defer(() => timelines.ToSampleObservableInternal(relativeTo, scheduler));
    }

    private static IObservable<KeyValuePair<DateTime, TValue[]>> ToSampleObservableInternal<TValue>(
        this IEnumerable<KeyValuePair<TValue, ITimeline>> source, DateTime relativeTo, IScheduler scheduler)
    {
        source = source.ToArray();
        return Observable.Generate(
            source.GetNextUtcInstant(relativeTo),
            sample => sample != null,
            sample => source.GetNextUtcInstant(sample!.Value),
            sample => new KeyValuePair<DateTime, TValue[]>(sample!.Value, source.GetKeysAtUtcInstant(sample.Value)),
            sample => sample!.Value, scheduler);
    }
}