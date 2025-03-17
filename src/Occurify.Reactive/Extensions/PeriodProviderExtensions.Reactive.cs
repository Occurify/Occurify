using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Occurify.Extensions;

namespace Occurify.Reactive.Extensions;

public static class PeriodTimelineExtensions
{
    /// <summary>
    /// Returns a <c>IObservable</c> that emits <c>true</c> when a period starts and <c>false</c> when it ends.
    /// </summary>
    public static IObservable<bool> ToBooleanObservable(this IPeriodTimeline periodTimeline, IScheduler scheduler) =>
        periodTimeline.ToBooleanObservable(DateTime.UtcNow, scheduler);

    /// <summary>
    /// Returns a <c>IObservable</c> that emits <c>true</c> when a period starts and <c>false</c> when it ends using <paramref name="relativeTo"/> as a starting time.
    /// </summary>
    public static IObservable<bool> ToBooleanObservable(this IPeriodTimeline periodTimeline, DateTime relativeTo, IScheduler scheduler) => periodTimeline.ToPeriodObservable(relativeTo, scheduler).Select(s => s.IsPeriod);

    /// <summary>
    /// Returns a <c>IObservable</c> that immediately emits a boolean based on the period state on <see cref="DateTime.UtcNow"/> upon subscribing and then emits <c>true</c> when a period starts and <c>false</c> when it ends.
    /// </summary>
    public static IObservable<bool> ToBooleanObservableIncludingCurrent(this IPeriodTimeline periodTimeline, IScheduler scheduler) => periodTimeline.ToPeriodObservableIncludingCurrentSample(scheduler).Select(s => s.IsPeriod);

    /// <summary>
    /// Returns a <c>IObservable</c> that immediately emits a boolean based on the period state on <paramref name="relativeTo"/> upon subscribing and then emits <c>true</c> when a period starts and <c>false</c> when it ends using <paramref name="relativeTo"/> as a starting time.
    /// </summary>
    public static IObservable<bool> ToBooleanObservableIncludingCurrent(this IPeriodTimeline periodTimeline, DateTime relativeTo, IScheduler scheduler) => periodTimeline.ToPeriodObservableIncludingCurrentSample(relativeTo, scheduler).Select(s => s.IsPeriod);

    /// <summary>
    /// Returns a <c>IObservable</c> that emits a <see cref="PeriodTimelineSample"/> every time a period starts or ends.
    /// </summary>
    public static IObservable<PeriodTimelineSample> ToPeriodObservable(this IPeriodTimeline periodTimeline,
        IScheduler scheduler) => periodTimeline.ToPeriodObservable(DateTime.UtcNow, scheduler);

    /// <summary>
    /// Returns a <c>IObservable</c> that emits a <see cref="PeriodTimelineSample"/> every time a period starts or ends using <paramref name="relativeTo"/> as a starting time.
    /// </summary>
    public static IObservable<PeriodTimelineSample> ToPeriodObservable(this IPeriodTimeline periodTimeline,
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
            sample => sample!.Value, scheduler).Prepend(periodTimeline.SampleAt(relativeTo));

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

    /// <summary>
    /// Returns a <c>IObservable</c> that immediately emits a <see cref="PeriodTimelineSample"/> on <see cref="DateTime.UtcNow"/> upon subscribing and then emits a <see cref="PeriodTimelineSample"/> every time a period starts or ends.
    /// </summary>
    public static IObservable<PeriodTimelineSample> ToPeriodObservableIncludingCurrentSample(this IPeriodTimeline periodTimeline, IScheduler scheduler)
    {
        return Observable.Defer(() =>
        {
            var utcNow = DateTime.UtcNow;
            return periodTimeline.ToPeriodObservable(utcNow, scheduler)
                .Prepend(periodTimeline.SampleAt(utcNow));
        });
    }

    /// <summary>
    /// Returns a <c>IObservable</c> that immediately emits a <see cref="PeriodTimelineSample"/> on <paramref name="relativeTo"/> upon subscribing and then emits a <see cref="PeriodTimelineSample"/> every time a period starts or ends using <paramref name="relativeTo"/> as a starting time.
    /// </summary>
    public static IObservable<PeriodTimelineSample> ToPeriodObservableIncludingCurrentSample(this IPeriodTimeline periodTimeline,
        DateTime relativeTo, IScheduler scheduler)
    {
        return Observable.Defer(() => periodTimeline.ToPeriodObservable(relativeTo, scheduler).Prepend(periodTimeline.SampleAt(relativeTo)));
    }
}