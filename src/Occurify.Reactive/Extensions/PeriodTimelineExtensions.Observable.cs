using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Occurify.Extensions;

namespace Occurify.Reactive.Extensions;

/// <summary>
/// Provides reactive extension methods for working with <see cref="IPeriodTimeline"/>.
/// </summary>
public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Returns a <c>IObservable</c> that emits <c>true</c> when a period starts and <c>false</c> when it ends.
    /// If <paramref name="emitStateUponSubscribe"/> is true, the state at the current time will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<bool> ToBooleanObservable(this IPeriodTimeline periodTimeline, IScheduler scheduler,
        bool emitStateUponSubscribe = true) =>
        periodTimeline.ToSampleObservable(scheduler, emitStateUponSubscribe).Select(s => s.IsPeriod);

    /// <summary>
    /// Returns a <c>IObservable</c> that emits <c>true</c> when a period starts and <c>false</c> when it ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitStateUponSubscribe"/> is true, the state at <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<bool> ToBooleanObservable(this IPeriodTimeline periodTimeline, DateTime relativeTo,
        IScheduler scheduler, bool emitStateUponSubscribe = true) =>
        periodTimeline.ToSampleObservable(relativeTo, scheduler, emitStateUponSubscribe).Select(s => s.IsPeriod);

    /// <summary>
    /// Returns a <c>IObservable</c> that emits a <see cref="PeriodTimelineSample"/> every time a period starts or ends.
    /// If <paramref name="emitSampleUponSubscribe"/> is true, the sample at the current time will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<PeriodTimelineSample> ToSampleObservable(this IPeriodTimeline periodTimeline,
        IScheduler scheduler, bool emitSampleUponSubscribe = true) =>
        periodTimeline.ToSampleObservable(DateTime.UtcNow, scheduler, emitSampleUponSubscribe);

    /// <summary>
    /// Returns a <c>IObservable</c> that emits a <see cref="PeriodTimelineSample"/> every time a period starts or ends using <paramref name="relativeTo"/> as a starting time.
    /// If <paramref name="emitSampleUponSubscribe"/> is true, the sample at <paramref name="relativeTo"/> will be emitted immediately upon subscribing.
    /// </summary>
    public static IObservable<PeriodTimelineSample> ToSampleObservable(this IPeriodTimeline periodTimeline,
        DateTime relativeTo, IScheduler scheduler, bool emitSampleUponSubscribe = true)
    {
        if (emitSampleUponSubscribe)
        {
            return Observable.Defer(() =>
                periodTimeline.ToPeriodObservableInternal(relativeTo, scheduler)
                    .Prepend(periodTimeline.SampleAt(relativeTo)));
        }

        return periodTimeline.ToPeriodObservableInternal(relativeTo, scheduler);
    }

    private static IObservable<PeriodTimelineSample> ToPeriodObservableInternal(this IPeriodTimeline periodTimeline,
        DateTime relativeTo, IScheduler scheduler)
    {
        return Observable.Generate(
            MinAssumingNullIsPlusInfinity(
                periodTimeline.StartTimeline.GetNextUtcInstant(relativeTo),
                periodTimeline.EndTimeline.GetNextUtcInstant(relativeTo)),
            sample => sample != null,
            sample => MinAssumingNullIsPlusInfinity(
                periodTimeline.StartTimeline.GetNextUtcInstant(sample!.Value),
                periodTimeline.EndTimeline.GetNextUtcInstant(sample.Value)),
            sample => periodTimeline.SampleAt(sample!.Value),
            sample => sample!.Value, scheduler);

        static DateTime? MinAssumingNullIsPlusInfinity(DateTime? dateTime1, DateTime? dateTime2)
        {
            if (dateTime1 == null && dateTime2 == null)
            {
                return null;
            }

            if (dateTime1 != null && dateTime2 == null)
            {
                return dateTime1;
            }

            if (dateTime1 == null && dateTime2 != null)
            {
                return dateTime2;
            }

            return dateTime1 < dateTime2 ? dateTime1 : dateTime2;
        }
    }
}