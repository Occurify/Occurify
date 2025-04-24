using Microsoft.Reactive.Testing;
using Occurify.Extensions;
using Occurify.Reactive.Extensions;

namespace Occurify.Reactive.Tests;

[TestClass]
public class PeriodTimelineCollectionExtensionsTests
{
    [TestMethod]
    public void ToAnyBooleanObservable_ExcludingCurrentSample()
    {
        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<bool>();

        var periodTimeline1 = Period.Create(now, TimeSpan.FromDays(3)).AsPeriodTimeline();
        var periodTimeline2 = Period.Create(now + TimeSpan.FromDays(1), TimeSpan.FromDays(1)).AsPeriodTimeline();
        var periodTimeline3 = Period.Create(now + TimeSpan.FromDays(2), TimeSpan.FromDays(2)).AsPeriodTimeline();

        var periodTimelines = new[] { periodTimeline1, periodTimeline2, periodTimeline3 };

        var observable = periodTimelines.ToAnyBooleanObservable(now.AddTicks(-1), scheduler, emitStateUponSubscribe: false);

        observable.Subscribe(results.Add);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks - 1);

        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { true }, results);

        scheduler.AdvanceBy(TimeSpan.FromDays(4).Ticks - 1);
        CollectionAssert.AreEqual(new[] { true }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { true, false }, results);
    }

    [TestMethod]
    public void ToAnyBooleanObservable_IncludingCurrentSample()
    {
        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<bool>();

        var periodTimeline1 = Period.Create(now, TimeSpan.FromDays(3)).AsPeriodTimeline();
        var periodTimeline2 = Period.Create(now + TimeSpan.FromDays(1), TimeSpan.FromDays(1)).AsPeriodTimeline();
        var periodTimeline3 = Period.Create(now + TimeSpan.FromDays(2), TimeSpan.FromDays(2)).AsPeriodTimeline();

        var periodTimelines = new[] { periodTimeline1, periodTimeline2, periodTimeline3 };

        var observable = periodTimelines.ToAnyBooleanObservable(now.AddTicks(-1), scheduler, emitStateUponSubscribe: true);

        observable.Subscribe(results.Add);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks - 1);

        CollectionAssert.AreEqual(new[] { false }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { false, true }, results);

        scheduler.AdvanceBy(TimeSpan.FromDays(4).Ticks - 1);
        CollectionAssert.AreEqual(new[] { false, true }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { false, true, false }, results);
    }

    [TestMethod]
    public void ToAllBooleanObservable_ExcludingCurrentSample()
    {
        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<bool>();

        var periodTimeline1 = Period.Create(now, TimeSpan.FromDays(3)).AsPeriodTimeline();
        var periodTimeline2 = Period.Create(now + TimeSpan.FromDays(1), TimeSpan.FromDays(2)).AsPeriodTimeline();
        var periodTimeline3 = Period.Create(now + TimeSpan.FromDays(2), TimeSpan.FromDays(2)).AsPeriodTimeline();

        var periodTimelines = new[] { periodTimeline1, periodTimeline2, periodTimeline3 };

        var observable = periodTimelines.ToAllBooleanObservable(now.AddTicks(-1), scheduler, emitStateUponSubscribe: false);

        observable.Subscribe(results.Add);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks - 1);

        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(1);
        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(TimeSpan.FromDays(2).Ticks - 1);
        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { true }, results);

        scheduler.AdvanceBy(TimeSpan.FromDays(1).Ticks - 1);
        CollectionAssert.AreEqual(new[] { true }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { true, false }, results);
    }

    [TestMethod]
    public void ToAllBooleanObservable_IncludingCurrentSample()
    {
        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<bool>();

        var periodTimeline1 = Period.Create(now, TimeSpan.FromDays(3)).AsPeriodTimeline();
        var periodTimeline2 = Period.Create(now + TimeSpan.FromDays(1), TimeSpan.FromDays(2)).AsPeriodTimeline();
        var periodTimeline3 = Period.Create(now + TimeSpan.FromDays(2), TimeSpan.FromDays(2)).AsPeriodTimeline();

        var periodTimelines = new[] { periodTimeline1, periodTimeline2, periodTimeline3 };

        var observable = periodTimelines.ToAllBooleanObservable(now.AddTicks(-1), scheduler, emitStateUponSubscribe: true);

        observable.Subscribe(results.Add);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks - 1);

        CollectionAssert.AreEqual(new[] { false }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { false }, results);

        scheduler.AdvanceBy(TimeSpan.FromDays(2).Ticks - 1);
        CollectionAssert.AreEqual(new[] { false }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { false, true }, results);

        scheduler.AdvanceBy(TimeSpan.FromDays(1).Ticks - 1);
        CollectionAssert.AreEqual(new[] { false, true }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { false, true, false }, results);
    }
}