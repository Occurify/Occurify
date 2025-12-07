using Microsoft.Reactive.Testing;
using Occurify.Reactive.Extensions;

namespace Occurify.Reactive.Tests;

[TestClass]
public class TimelineValueCollectionExtensionsTests
{
    [TestMethod]
    public void ToSampleObservable_ExcludingCurrentSample()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;
        const int timeGap3 = timeGap1 + timeGap2;
        const int timeGap4 = timeGap2 - timeGap1;

        const string value1 = nameof(value1);
        const string value2 = nameof(value2);

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<KeyValuePair<DateTime, string[]>>();

        var time1 = now + TimeSpan.FromTicks(timeGap1);
        var time2 = now + TimeSpan.FromTicks(timeGap1 + timeGap2);
        var time3 = now + TimeSpan.FromTicks(timeGap1 + timeGap2 + timeGap3);
        var time4 = now + TimeSpan.FromTicks(timeGap1 + timeGap2 + timeGap3 + timeGap4);

        var timeline1 = Timeline.FromInstants(time1, time3);
        var timeline2 = Timeline.FromInstants(time2, time4);

        var timelines = new Dictionary<ITimeline, string>
        {
            { timeline1, value1 },
            { timeline2, value2 },
        };

        var observable = timelines.ToSampleObservable(now, scheduler, emitSampleUponSubscribe: false);

        observable.Subscribe(results.Add);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks);

        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(timeGap1 - 1);
        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(1);
        AssertSampleCollections([
            new KeyValuePair<DateTime, string[]>(time1, [value1])
        ], results);

        scheduler.AdvanceBy(timeGap2 - 1);
        Assert.HasCount(1, results);

        scheduler.AdvanceBy(1);
        AssertSampleCollections([
            new KeyValuePair<DateTime, string[]>(time1, [value1]),
            new KeyValuePair<DateTime, string[]>(time2, [value2])
        ], results);

        scheduler.AdvanceBy(timeGap3 - 1);
        Assert.HasCount(2, results);

        scheduler.AdvanceBy(1);
        AssertSampleCollections([
            new KeyValuePair<DateTime, string[]>(time1, [value1]),
            new KeyValuePair<DateTime, string[]>(time2, [value2]),
            new KeyValuePair<DateTime, string[]>(time3, [value1])
        ], results);

        scheduler.AdvanceBy(timeGap4 - 1);
        Assert.HasCount(3, results);

        scheduler.AdvanceBy(1);
        AssertSampleCollections([
            new KeyValuePair<DateTime, string[]>(time1, [value1]),
            new KeyValuePair<DateTime, string[]>(time2, [value2]),
            new KeyValuePair<DateTime, string[]>(time3, [value1]),
            new KeyValuePair<DateTime, string[]>(time4, [value2])
        ], results);
    }

    [TestMethod]
    public void ToSampleObservable_IncludingCurrentSample()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;
        const int timeGap3 = timeGap1 + timeGap2;
        const int timeGap4 = timeGap2 - timeGap1;

        const string value1 = nameof(value1);
        const string value2 = nameof(value2);

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<KeyValuePair<DateTime, string[]>>();

        var time1 = now + TimeSpan.FromTicks(timeGap1);
        var time2 = now + TimeSpan.FromTicks(timeGap1 + timeGap2);
        var time3 = now + TimeSpan.FromTicks(timeGap1 + timeGap2 + timeGap3);
        var time4 = now + TimeSpan.FromTicks(timeGap1 + timeGap2 + timeGap3 + timeGap4);

        var timeline1 = Timeline.FromInstants(time1, time3);
        var timeline2 = Timeline.FromInstants(time2, time4);

        var timelines = new Dictionary<ITimeline, string>
        {
            { timeline1, value1 },
            { timeline2, value2 },
        };

        var observable = timelines.ToSampleObservable(now, scheduler);

        // The first result should only be emitted after Subscribe is called.
        Assert.IsFalse(results.Any());

        observable.Subscribe(results.Add);

        // The observable should have provided the current time.
        AssertSampleCollections([
            new KeyValuePair<DateTime, string[]>(now, [])
        ], results);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks);

        Assert.HasCount(1, results);

        scheduler.AdvanceBy(timeGap1 - 1);
        Assert.HasCount(1, results);

        scheduler.AdvanceBy(1);
        AssertSampleCollections([
            new KeyValuePair<DateTime, string[]>(now, []),
            new KeyValuePair<DateTime, string[]>(time1, [value1])
        ], results);

        scheduler.AdvanceBy(timeGap2 - 1);
        Assert.HasCount(2, results);

        scheduler.AdvanceBy(1);
        AssertSampleCollections([
            new KeyValuePair<DateTime, string[]>(now, []),
            new KeyValuePair<DateTime, string[]>(time1, [value1]),
            new KeyValuePair<DateTime, string[]>(time2, [value2])
        ], results);

        scheduler.AdvanceBy(timeGap3 - 1);
        Assert.HasCount(3, results);

        scheduler.AdvanceBy(1);
        AssertSampleCollections([
            new KeyValuePair<DateTime, string[]>(now, []),
            new KeyValuePair<DateTime, string[]>(time1, [value1]),
            new KeyValuePair<DateTime, string[]>(time2, [value2]),
            new KeyValuePair<DateTime, string[]>(time3, [value1])
        ], results);

        scheduler.AdvanceBy(timeGap4 - 1);
        Assert.HasCount(4, results);

        scheduler.AdvanceBy(1);
        AssertSampleCollections([
            new KeyValuePair<DateTime, string[]>(now, []),
            new KeyValuePair<DateTime, string[]>(time1, [value1]),
            new KeyValuePair<DateTime, string[]>(time2, [value2]),
            new KeyValuePair<DateTime, string[]>(time3, [value1]),
            new KeyValuePair<DateTime, string[]>(time4, [value2])
        ], results);
    }

    private void AssertSampleCollections(KeyValuePair<DateTime, string[]>[] expected, List<KeyValuePair<DateTime, string[]>> actual)
    {
        Assert.AreEqual(expected.Length, actual.Count);

        for (var i = 0; i < expected.Length; i++)
        {
            var expectedSample = expected[i];
            var actualSample = actual[i];

            Assert.AreEqual(expectedSample.Key, actualSample.Key);
            CollectionAssert.AreEqual(expectedSample.Value, actualSample.Value);
        }
    }
}