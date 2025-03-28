using Microsoft.Reactive.Testing;
using Occurify.Extensions;
using Occurify.Reactive.Extensions;

namespace Occurify.Reactive.Tests;

[TestClass]
public class TimelineValueCollectionExtensionsTests
{
    [TestMethod]
    public void ToSampleObservable()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;
        const int timeGap3 = timeGap1 + timeGap2;
        const int timeGap4 = timeGap2 - timeGap1;

        const string value1 = nameof(value1);
        const string value2 = nameof(value2);

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<TimelineValueCollectionSample<string>>();

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

        observable.Subscribe(results.Add);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks);

        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(timeGap1 - 1);
        Assert.IsFalse(results.Any());

        scheduler.AdvanceBy(1);
        AssertTimelineValueCollectionSampleCollections([
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time1, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time1, true, null, time3) },
                    { timeline2, new TimelineSample(time1, false, null, time2) }
                }))
        ], results);

        scheduler.AdvanceBy(timeGap2 - 1);
        Assert.AreEqual(1, results.Count);

        scheduler.AdvanceBy(1);
        AssertTimelineValueCollectionSampleCollections([
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time1, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time1, true, null, time3) },
                    { timeline2, new TimelineSample(time1, false, null, time2) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time2, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time2, false, time1, time3) },
                    { timeline2, new TimelineSample(time2, true, null, time4) }
                }))
        ], results);

        scheduler.AdvanceBy(timeGap3 - 1);
        Assert.AreEqual(2, results.Count);

        scheduler.AdvanceBy(1);
        AssertTimelineValueCollectionSampleCollections([
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time1, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time1, true, null, time3) },
                    { timeline2, new TimelineSample(time1, false, null, time2) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time2, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time2, false, time1, time3) },
                    { timeline2, new TimelineSample(time2, true, null, time4) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time3, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time3, true, time1, null) },
                    { timeline2, new TimelineSample(time3, false, time2, time4) }
                }))
        ], results);

        scheduler.AdvanceBy(timeGap4 - 1);
        Assert.AreEqual(3, results.Count);

        scheduler.AdvanceBy(1);
        AssertTimelineValueCollectionSampleCollections([
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time1, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time1, true, null, time3) },
                    { timeline2, new TimelineSample(time1, false, null, time2) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time2, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time2, false, time1, time3) },
                    { timeline2, new TimelineSample(time2, true, null, time4) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time3, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time3, true, time1, null) },
                    { timeline2, new TimelineSample(time3, false, time2, time4) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time4, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time4, false, time3, null) },
                    { timeline2, new TimelineSample(time4, true, time2, null) }
                }))
        ], results);
    }

    [TestMethod]
    public void ToSampleObservableIncludingCurrentInstant()
    {
        const int timeGap1 = 42;
        const int timeGap2 = 1337;
        const int timeGap3 = timeGap1 + timeGap2;
        const int timeGap4 = timeGap2 - timeGap1;

        const string value1 = nameof(value1);
        const string value2 = nameof(value2);

        var now = DateTime.UtcNow;
        var scheduler = new TestScheduler();
        var results = new List<TimelineValueCollectionSample<string>>();

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

        var observable = timelines.ToSampleObservableIncludingCurrentInstant(now, scheduler);

        // The first result should only be emitted after Subscribe is called.
        Assert.IsFalse(results.Any());

        observable.Subscribe(results.Add);

        // The observable should have provided the current time.
        AssertTimelineValueCollectionSampleCollections([
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(now, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(now, false, null, time1) },
                    { timeline2, new TimelineSample(now, false, null, time2) }
                }))
        ], results);

        // First set the current time. Note that we do this after creating the observable, as Observable.Generate also uses the scheduler for the first iteration, and this triggers that setup.
        scheduler.AdvanceTo(now.Ticks);

        Assert.AreEqual(1, results.Count);

        scheduler.AdvanceBy(timeGap1 - 1);
        Assert.AreEqual(1, results.Count);

        scheduler.AdvanceBy(1);
        AssertTimelineValueCollectionSampleCollections([
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(now, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(now, false, null, time1) },
                    { timeline2, new TimelineSample(now, false, null, time2) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time1, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time1, true, null, time3) },
                    { timeline2, new TimelineSample(time1, false, null, time2) }
                }))
        ], results);

        scheduler.AdvanceBy(timeGap2 - 1);
        Assert.AreEqual(2, results.Count);

        scheduler.AdvanceBy(1);
        AssertTimelineValueCollectionSampleCollections([
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(now, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(now, false, null, time1) },
                    { timeline2, new TimelineSample(now, false, null, time2) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time1, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time1, true, null, time3) },
                    { timeline2, new TimelineSample(time1, false, null, time2) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time2, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time2, false, time1, time3) },
                    { timeline2, new TimelineSample(time2, true, null, time4) }
                }))
        ], results);

        scheduler.AdvanceBy(timeGap3 - 1);
        Assert.AreEqual(3, results.Count);

        scheduler.AdvanceBy(1);
        AssertTimelineValueCollectionSampleCollections([
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(now, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(now, false, null, time1) },
                    { timeline2, new TimelineSample(now, false, null, time2) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time1, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time1, true, null, time3) },
                    { timeline2, new TimelineSample(time1, false, null, time2) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time2, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time2, false, time1, time3) },
                    { timeline2, new TimelineSample(time2, true, null, time4) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time3, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time3, true, time1, null) },
                    { timeline2, new TimelineSample(time3, false, time2, time4) }
                }))
        ], results);

        scheduler.AdvanceBy(timeGap4 - 1);
        Assert.AreEqual(4, results.Count);

        scheduler.AdvanceBy(1);
        AssertTimelineValueCollectionSampleCollections([
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(now, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(now, false, null, time1) },
                    { timeline2, new TimelineSample(now, false, null, time2) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time1, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time1, true, null, time3) },
                    { timeline2, new TimelineSample(time1, false, null, time2) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time2, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time2, false, time1, time3) },
                    { timeline2, new TimelineSample(time2, true, null, time4) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time3, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time3, true, time1, null) },
                    { timeline2, new TimelineSample(time3, false, time2, time4) }
                })),
            new TimelineValueCollectionSample<string>(timelines.ToArray(),
                new TimelineCollectionSample(time4, new Dictionary<ITimeline, TimelineSample>
                {
                    { timeline1, new TimelineSample(time4, false, time3, null) },
                    { timeline2, new TimelineSample(time4, true, time2, null) }
                }))
        ], results);
    }

    private void AssertTimelineValueCollectionSampleCollections(IEnumerable<TimelineValueCollectionSample<string>> expected, IEnumerable<TimelineValueCollectionSample<string>> actual)
    {
        expected = expected.ToArray();
        actual = actual.ToArray();

        Assert.AreEqual(expected.Count(), actual.Count());

        for (var i = 0; i < expected.Count(); i++)
        {
            var expectedSample = expected.ElementAt(i);
            var actualSample = actual.ElementAt(i);

            Assert.AreEqual(expectedSample.UtcSampleInstant, actualSample.UtcSampleInstant);
            Assert.AreEqual(expectedSample.Previous, actualSample.Previous);
            Assert.AreEqual(expectedSample.Next, actualSample.Next);
            CollectionAssert.AreEqual(expectedSample.TimelinesWithInstantOnSampleLocation.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), actualSample.TimelinesWithInstantOnSampleLocation.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            CollectionAssert.AreEqual(expectedSample.TimelinesOnPrevious.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), actualSample.TimelinesOnPrevious.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            CollectionAssert.AreEqual(expectedSample.TimelinesOnNext.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), actualSample.TimelinesOnNext.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            CollectionAssert.AreEqual(expectedSample.Samples.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), actualSample.Samples.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }
    }
}