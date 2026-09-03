using Microsoft.Reactive.Testing;
using Occurify.Reactive.Extensions;

namespace Occurify.Reactive.Tests;

[TestClass]
public class TimelineCollectionExtensionsTests
{
    [TestMethod]
    public void ToInstantObservable_ExcludingCurrentSample()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;
        const int timeGap3 = timeGap1 + timeGap2;
        const int timeGap4 = timeGap2 - timeGap1;

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<DateTime>();

        var time1 = now + TimeSpan.FromTicks(timeGap1);
        var time2 = now + TimeSpan.FromTicks(timeGap1 + timeGap2);
        var time3 = now + TimeSpan.FromTicks(timeGap1 + timeGap2 + timeGap3);
        var time4 = now + TimeSpan.FromTicks(timeGap1 + timeGap2 + timeGap3 + timeGap4);

        var timeline1 = Timeline.FromInstants(time1, time3);
        var timeline2 = Timeline.FromInstants(time2, time4);

        var timelines = new[] { timeline1, timeline2 };

        var observable = timelines.ToInstantObservable(now, scheduler, emitInstantUponSubscribe: false);

        observable.Subscribe(results.Add);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks);

        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(timeGap1 - 1);
        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { time1 }, results);

        scheduler.AdvanceBy(timeGap2 - 1);
        Assert.HasCount(1, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { time1, time2 }, results);

        scheduler.AdvanceBy(timeGap3 - 1);
        Assert.HasCount(2, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { time1, time2, time3 }, results);

        scheduler.AdvanceBy(timeGap4 - 1);
        Assert.HasCount(3, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { time1, time2, time3, time4 }, results);
    }

    [TestMethod]
    public void ToInstantObservable_IncludingCurrentSample()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;
        const int timeGap3 = timeGap1 + timeGap2;
        const int timeGap4 = timeGap2 - timeGap1;

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<DateTime>();

        var time1 = now + TimeSpan.FromTicks(timeGap1);
        var time2 = now + TimeSpan.FromTicks(timeGap1 + timeGap2);
        var time3 = now + TimeSpan.FromTicks(timeGap1 + timeGap2 + timeGap3);
        var time4 = now + TimeSpan.FromTicks(timeGap1 + timeGap2 + timeGap3 + timeGap4);

        var timeline1 = Timeline.FromInstants(time1, time3);
        var timeline2 = Timeline.FromInstants(time2, time4);

        var timelines = new[] { timeline1, timeline2 };

        var observable = timelines.ToInstantObservable(now, scheduler);

        // The first result should only be emitted after Subscribe is called.
        Assert.IsFalse(results.Any());

        observable.Subscribe(results.Add);

        // The observable should have provided the current time.
        CollectionAssert.AreEqual(new[] { now }, results);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks);

        Assert.HasCount(1, results);

        scheduler.AdvanceBy(timeGap1 - 1);
        Assert.HasCount(1, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { now, time1 }, results);

        scheduler.AdvanceBy(timeGap2 - 1);
        Assert.HasCount(2, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { now, time1, time2 }, results);

        scheduler.AdvanceBy(timeGap3 - 1);
        Assert.HasCount(3, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { now, time1, time2, time3 }, results);

        scheduler.AdvanceBy(timeGap4 - 1);
        Assert.HasCount(4, results);

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { now, time1, time2, time3, time4 }, results);
    }
}