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
    /// </summary>
    public static IObservable<Unit> ToPulseObservable(this IEnumerable<ITimeline> source, IScheduler scheduler) =>
        source.ToPulseObservable(DateTime.UtcNow, scheduler);

    /// <summary>
    /// Returns a <see cref="IObservable{Unit}"/> that emits a <see cref="Unit"/> every time an instant occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// </summary>
    public static IObservable<Unit> ToPulseObservable(this IEnumerable<ITimeline> source, DateTime relativeTo, IScheduler scheduler) =>
        source.ToInstantObservable(relativeTo, scheduler).Select(_ => Unit.Default);

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that emits an instant as <see cref="DateTime"/> when it occurs on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservable(this IEnumerable<ITimeline> source, IScheduler scheduler) =>
        source.ToInstantObservable(DateTime.UtcNow, scheduler);

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that emits an instant as <see cref="DateTime"/> when it occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservable(this IEnumerable<ITimeline> source, DateTime relativeTo, IScheduler scheduler) =>
        source.ToSampleObservable(relativeTo, scheduler).Select(s => s.UtcSampleInstant);

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that immediately emits <see cref="DateTime.UtcNow"/> upon subscribing and then emits an instant as <see cref="DateTime"/> when it occurs on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservableIncludingCurrentInstant(this IEnumerable<ITimeline> source, IScheduler scheduler)
    {
        return Observable.Defer(() =>
        {
            var utcNow = DateTime.UtcNow;
            return source.ToInstantObservable(utcNow, scheduler).Prepend(utcNow);
        });
    }

    /// <summary>
    /// Returns a <see cref="IObservable{DateTime}"/> that immediately emits <paramref name="relativeTo"/> upon subscribing and then emits an instant as <see cref="DateTime"/> when it occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// </summary>
    public static IObservable<DateTime> ToInstantObservableIncludingCurrentInstant(this IEnumerable<ITimeline> source, DateTime relativeTo, IScheduler scheduler)
    {
        return Observable.Defer(() => source.ToInstantObservable(relativeTo, scheduler).Prepend(relativeTo));
    }

    /// <summary>
    /// Returns a <see cref="IObservable{TimelineSample}"/> that emits a sample every time an instant occurs on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static IObservable<TimelineCollectionSample> ToSampleObservable(this IEnumerable<ITimeline> source, IScheduler scheduler) =>
        source.ToSampleObservable(DateTime.UtcNow, scheduler);

    /// <summary>
    /// Returns a <see cref="IObservable{TimelineSample}"/> that emits a sample every time an instant occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// </summary>
    public static IObservable<TimelineCollectionSample> ToSampleObservable(this IEnumerable<ITimeline> source, DateTime relativeTo, IScheduler scheduler)
    {
        source = source.ToArray();
        return Observable.Generate(
            source.GetNextUtcInstant(relativeTo),
            sample => sample != null,
            sample => source.GetNextUtcInstant(sample!.Value),
            sample => source.SampleAt(sample!.Value),
            sample => sample!.Value, scheduler);
    }

    /// <summary>
    /// Returns a <see cref="IObservable{TimelineSample}"/> that immediately emits a sample upon subscribing and then emits a sample every time an instant occurs on any of the timelines in <paramref name="source"/>.
    /// </summary>
    public static IObservable<TimelineCollectionSample> ToSampleObservableIncludingCurrentInstant(this IEnumerable<ITimeline> source, IScheduler scheduler)
    {
        source = source.ToArray();
        return Observable.Defer(() =>
        {
            var utcNow = DateTime.UtcNow;
            return source.ToSampleObservable(utcNow, scheduler).Prepend(source.SampleAt(utcNow));
        });
    }

    /// <summary>
    /// Returns a <see cref="IObservable{TimelineSample}"/> that immediately emits a sample upon subscribing and then emits a sample every time an instant occurs on any of the timelines in <paramref name="source"/> using <paramref name="relativeTo"/> as a starting time.
    /// </summary>
    public static IObservable<TimelineCollectionSample> ToSampleObservableIncludingCurrentInstant(this IEnumerable<ITimeline> source, DateTime relativeTo, IScheduler scheduler)
    {
        source = source.ToArray();
        return Observable.Defer(() => source.ToSampleObservable(relativeTo, scheduler).Prepend(source.SampleAt(relativeTo)));
    }
}