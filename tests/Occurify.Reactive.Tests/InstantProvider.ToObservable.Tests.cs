using Microsoft.Reactive.Testing;
using Occurify.Extensions;
using Occurify.Reactive.Extensions;

namespace Occurify.Reactive.Tests;

[TestClass]
public class TimelineToObservableTests
{
    [TestMethod]
    public void ToInstantObservable()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<DateTime>();

        var time1 = now + TimeSpan.FromTicks(timeGap1);
        var time2 = now + TimeSpan.FromTicks(timeGap1 + timeGap2);
        var timeline = Timeline.FromInstants(time1, time2);
            
        var observable = timeline.ToInstantObservable(now, scheduler);
            
        observable.Subscribe(results.Add);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks);
            
        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(timeGap1 - 1);
        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { time1 }, results);

        scheduler.AdvanceBy(timeGap2 - 1);
        CollectionAssert.AreEqual(new[] { time1 }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { time1, time2 }, results);
    }

    [TestMethod]
    public void ToInstantObservableIncludingCurrentInstant()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<DateTime>();

        var time1 = now + TimeSpan.FromTicks(timeGap1);
        var time2 = now + TimeSpan.FromTicks(timeGap1 + timeGap2);
        var timeline = Timeline.FromInstants(time1, time2);

        var observable = timeline.ToInstantObservableIncludingCurrentInstant(now, scheduler);

        // The first result should only be emitted after Subscribe is called.
        Assert.IsFalse(results.Any());

        observable.Subscribe(results.Add);

        // The observable should have provided the current time.
        CollectionAssert.AreEqual(new[] { now }, results);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks);

        CollectionAssert.AreEqual(new[] { now }, results);

        scheduler.AdvanceBy(timeGap1 - 1);
        CollectionAssert.AreEqual(new[] { now }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { now, time1 }, results);

        scheduler.AdvanceBy(timeGap2 - 1);
        CollectionAssert.AreEqual(new[] { now, time1 }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { now, time1, time2 }, results);
    }

    [TestMethod]
    public void ToSampleObservable()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<TimelineSample>();

        var time1 = now + TimeSpan.FromTicks(timeGap1);
        var time2 = now + TimeSpan.FromTicks(timeGap1 + timeGap2);
        var timeline = Timeline.FromInstants(time1, time2);

        var observable = timeline.ToSampleObservable(now, scheduler);

        observable.Subscribe(results.Add);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks);

        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(timeGap1 - 1);
        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { new TimelineSample(time1, true, null, time2) }, results);

        scheduler.AdvanceBy(timeGap2 - 1);
        CollectionAssert.AreEqual(new[] { new TimelineSample(time1, true, null, time2) }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(
            new[] {
                new TimelineSample(time1, true, null, time2),
                new TimelineSample(time2, true, time1, null)
            },
            results);
    }

    [TestMethod]
    public void ToSampleObservableIncludingCurrentInstant()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<TimelineSample>();

        var time1 = now + TimeSpan.FromTicks(timeGap1);
        var time2 = now + TimeSpan.FromTicks(timeGap1 + timeGap2);
        var timeline = Timeline.FromInstants(time1, time2);

        var observable = timeline.ToSampleObservableIncludingCurrentInstant(now, scheduler);

        // The first result should only be emitted after Subscribe is called.
        Assert.IsFalse(results.Any());

        observable.Subscribe(results.Add);

        // The observable should have provided the current time.
        CollectionAssert.AreEqual(new[] { new TimelineSample(now, false, null, time1) }, results);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks);

        CollectionAssert.AreEqual(new[] { new TimelineSample(now, false, null, time1) }, results);

        scheduler.AdvanceBy(timeGap1 - 1);
        CollectionAssert.AreEqual(new[] { new TimelineSample(now, false, null, time1) }, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(
            new[] { 
                new TimelineSample(now, false, null, time1), 
                new TimelineSample(time1, true, null, time2)
            },
            results);

        scheduler.AdvanceBy(timeGap2 - 1);
        CollectionAssert.AreEqual(
            new[] {
                new TimelineSample(now, false, null, time1),
                new TimelineSample(time1, true, null, time2)
            },
            results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(
            new[] {
                new TimelineSample(now, false, null, time1),
                new TimelineSample(time1, true, null, time2),
                new TimelineSample(time2, true, time1, null)
            },
            results);
    }
}