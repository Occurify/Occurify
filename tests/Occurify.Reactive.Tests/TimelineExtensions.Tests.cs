using Microsoft.Reactive.Testing;
using Occurify.Reactive.Extensions;

namespace Occurify.Reactive.Tests;

[TestClass]
public class TimelineExtensionsTests
{
    [TestMethod]
    public void ToInstantObservable_ExcludingCurrentInstant()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<DateTime>();

        var time1 = now + TimeSpan.FromTicks(timeGap1);
        var time2 = now + TimeSpan.FromTicks(timeGap1 + timeGap2);
        var timeline = Timeline.FromInstants(time1, time2);
            
        var observable = timeline.ToInstantObservable(now, scheduler, emitInstantUponSubscribe: false);
            
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
    public void ToInstantObservable_IncludingCurrentInstant()
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
}