using Microsoft.Reactive.Testing;
using Occurify.Extensions;
using Occurify.Reactive.Extensions;

namespace Occurify.Reactive.Tests;

[TestClass]
public class SchedulerClockTests
{
    private static readonly DateTime Start = new(2025, 3, 10, 16, 21, 0, DateTimeKind.Utc);
    private static readonly DateTime Instant1 = Start + TimeSpan.FromTicks(42);
    private static readonly DateTime Instant2 = Start + TimeSpan.FromTicks(1379);

    [TestMethod]
    public void ToInstantObservable_ReadsSchedulerClockUponSubscribe()
    {
        var scheduler = new TestScheduler();
        scheduler.AdvanceTo(Start.Ticks);
        var timeline = Timeline.FromInstants(Instant1, Instant2);
        var results = new List<DateTime>();

        var observable = timeline.ToInstantObservable(scheduler, emitInstantUponSubscribe: false);

        // The first instant passes before anybody subscribes; it must not be replayed.
        scheduler.AdvanceBy(100);
        observable.Subscribe(results.Add);
        Assert.IsFalse(results.Any());

        scheduler.AdvanceTo(Instant2.Ticks - 1);
        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(1);
        CollectionAssert.AreEqual(new[] { Instant2 }, results);
    }

    [TestMethod]
    public void ToInstantObservable_EmitsSchedulerNowUponSubscribe()
    {
        var scheduler = new TestScheduler();
        scheduler.AdvanceTo(Start.Ticks);
        var timeline = Timeline.FromInstants(Instant1, Instant2);
        var results = new List<DateTime>();

        var observable = timeline.ToInstantObservable(scheduler);

        scheduler.AdvanceBy(100);
        observable.Subscribe(results.Add);
        CollectionAssert.AreEqual(new[] { Start + TimeSpan.FromTicks(100) }, results);

        scheduler.AdvanceTo(Instant2.Ticks);
        CollectionAssert.AreEqual(new[] { Start + TimeSpan.FromTicks(100), Instant2 }, results);
    }

    [TestMethod]
    public void ToInstantObservable_EverySubscriptionStartsFromCurrentTime()
    {
        var scheduler = new TestScheduler();
        scheduler.AdvanceTo(Start.Ticks);
        var timeline = Timeline.FromInstants(Instant1, Instant2);
        var firstResults = new List<DateTime>();
        var secondResults = new List<DateTime>();

        var observable = timeline.ToInstantObservable(scheduler, emitInstantUponSubscribe: false);
        observable.Subscribe(firstResults.Add);
        scheduler.AdvanceTo(Instant1.Ticks + 1);
        observable.Subscribe(secondResults.Add);
        scheduler.AdvanceTo(Instant2.Ticks);

        CollectionAssert.AreEqual(new[] { Instant1, Instant2 }, firstResults);
        CollectionAssert.AreEqual(new[] { Instant2 }, secondResults);
    }

    [TestMethod]
    public void ToSampleObservable_ReadsSchedulerClockUponSubscribe()
    {
        var scheduler = new TestScheduler();
        scheduler.AdvanceTo(Start.Ticks);
        var periodTimeline = Instant1.To(Instant2).AsPeriodTimeline();
        var results = new List<PeriodTimelineSample>();

        var observable = periodTimeline.ToSampleObservable(scheduler);

        scheduler.AdvanceBy(100);
        observable.Subscribe(results.Add);

        Assert.HasCount(1, results);
        Assert.AreEqual(Start + TimeSpan.FromTicks(100), results[0].UtcSampleInstant);
        Assert.IsTrue(results[0].IsPeriod);

        scheduler.AdvanceTo(Instant2.Ticks);
        Assert.HasCount(2, results);
        Assert.AreEqual(Instant2, results[1].UtcSampleInstant);
        Assert.IsFalse(results[1].IsPeriod);
    }

    [TestMethod]
    public void ToInstantObservable_NonUtcRelativeTo_Throws()
    {
        var scheduler = new TestScheduler();
        var timeline = Timeline.FromInstants(Instant1, Instant2);
        var localRelativeTo = DateTime.SpecifyKind(Start, DateTimeKind.Local);

        Assert.ThrowsExactly<ArgumentException>(() => timeline.ToInstantObservable(localRelativeTo, scheduler));
        Assert.ThrowsExactly<ArgumentException>(() => Instant1.To(Instant2).AsPeriodTimeline().ToSampleObservable(localRelativeTo, scheduler));
        Assert.ThrowsExactly<ArgumentException>(() => new[] { timeline }.ToInstantObservable(localRelativeTo, scheduler));
    }

    [TestMethod]
    public void ToAnyAndAllBooleanObservable_EmptyCollection_EmitsConstantState()
    {
        var scheduler = new TestScheduler();
        var anyResults = new List<bool>();
        var allResults = new List<bool>();

        Array.Empty<IPeriodTimeline>().ToAnyBooleanObservable(scheduler).Subscribe(anyResults.Add);
        Array.Empty<IPeriodTimeline>().ToAllBooleanObservable(scheduler).Subscribe(allResults.Add);

        CollectionAssert.AreEqual(new[] { false }, anyResults);
        CollectionAssert.AreEqual(new[] { true }, allResults);
    }
}
