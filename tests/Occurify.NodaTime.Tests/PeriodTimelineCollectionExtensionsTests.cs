using NodaTime;
using Occurify.Extensions;
using Occurify.NodaTime.Extensions;
using static Occurify.NodaTime.Tests.TestData;

namespace Occurify.NodaTime.Tests;

[TestClass]
public class PeriodTimelineCollectionExtensionsTests
{
    private static readonly IPeriodTimeline P1 = PeriodTimeline.FromPeriods(PeriodOf(Utc(2025, 1, 1), Utc(2025, 1, 2)), PeriodOf(Utc(2025, 1, 5), Utc(2025, 1, 6)));
    private static readonly IPeriodTimeline P2 = PeriodTimeline.FromPeriods(PeriodOf(Utc(2025, 1, 3), Utc(2025, 1, 4)));
    private static readonly IPeriodTimeline[] Timelines = { P1, P2 };
    private static readonly Dictionary<string, IPeriodTimeline> ByKey = new() { ["p1"] = P1, ["p2"] = P2 };
    private static readonly Dictionary<IPeriodTimeline, int> ByTimeline = new() { [P1] = 1, [P2] = 2 };

    private static Interval Day(int day) => Between(At(2025, 1, day), At(2025, 1, day + 1));

    [TestMethod]
    public void Collection_Enumerate()
    {
        CollectionAssert.AreEqual(new[] { Day(1), Day(3), Day(5) }, Timelines.EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Day(5), Day(3), Day(1) }, Timelines.EnumerateIntervalsBackwards().ToArray());
        CollectionAssert.AreEqual(new[] { Day(3), Day(5) }, Timelines.EnumerateIntervalsFrom(At(2025, 1, 2)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(1), Day(3) }, Timelines.EnumerateIntervals(Between(At(2025, 1, 1), At(2025, 1, 4))).ToArray());
        CollectionAssert.AreEqual(
            new[] { PeriodOf(Utc(2025, 1, 1), Utc(2025, 1, 2)), PeriodOf(Utc(2025, 1, 3), Utc(2025, 1, 4)) },
            Timelines.EnumeratePeriod(Between(At(2025, 1, 1), At(2025, 1, 4))).ToArray());
        CollectionAssert.AreEqual(new[] { Day(3) }, Timelines.EnumerateIntervalRange(At(2025, 1, 2), At(2025, 1, 5)).ToArray());
    }

    [TestMethod]
    public void Collection_Lookups()
    {
        Assert.AreEqual(Day(3), Timelines.GetNextCompleteInterval(At(2025, 1, 2)));
        Assert.AreEqual(Day(1), Timelines.GetPreviousCompleteInterval(At(2025, 1, 3, 12)));
        Assert.AreEqual(Day(3), Timelines.GetNextIntervalIncludingPartial(At(2025, 1, 3, 12)));
        Assert.IsNull(Timelines.GetNextCompleteInterval(At(2025, 1, 6)));

        var next = Timelines.GetTimelinesAtNextCompleteInterval(At(2025, 1, 2));
        Assert.AreEqual(Day(3), next.Key);
        CollectionAssert.AreEqual(new[] { P2 }, next.Value);

        var none = Timelines.GetTimelinesAtNextCompleteInterval(At(2025, 1, 6));
        Assert.IsNull(none.Key);
        Assert.IsEmpty(none.Value);

        Assert.IsTrue(Timelines.ContainsInstant(At(2025, 1, 3, 12)));
        Assert.IsTrue(Timelines.ContainsPeriod(Between(At(2025, 1, 3), At(2025, 1, 3, 12))));
        Assert.IsTrue(Timelines.ContainsExactPeriod(Day(5)));
        CollectionAssert.AreEqual(new[] { P1 }, Timelines.GetTimelinesAtExactPeriod(Day(5)).ToArray());
    }

    [TestMethod]
    public void Collection_Filter_Transform_Sample()
    {
        var within = Timelines.Within(Between(At(2025, 1, 1), At(2025, 1, 4))).ToArray();
        CollectionAssert.AreEqual(new[] { Day(1) }, within[0].EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Day(3) }, within[1].EnumerateIntervals().ToArray());

        var intersected = Timelines.IntersectPeriod(Between(At(2025, 1, 1, 12), At(2025, 1, 3, 12))).ToArray();
        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 1, 12), At(2025, 1, 2)) }, intersected[0].EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 3), At(2025, 1, 3, 12)) }, intersected[1].EnumerateIntervals().ToArray());

        var offset = Timelines.Offset(Duration.FromDays(1)).ToArray();
        CollectionAssert.AreEqual(new[] { Day(4) }, offset[1].EnumerateIntervals().ToArray());

        var samples = Timelines.SampleAt(At(2025, 1, 1, 12)).ToArray();
        Assert.IsTrue(samples[0].IsPeriod);
        Assert.IsTrue(samples[1].IsGap);
        Assert.AreEqual(Day(1), samples[0].ToInterval());
    }

    [TestMethod]
    public void Key_Lookups()
    {
        CollectionAssert.AreEqual(new[] { "p2" }, ByKey.GetKeysAtInstant(At(2025, 1, 3, 12)));
        CollectionAssert.AreEqual(new[] { "p1" }, ByKey.GetKeysAtPeriod(Between(At(2025, 1, 1, 6), At(2025, 1, 1, 12))));
        CollectionAssert.AreEqual(new[] { "p2" }, ByKey.GetKeysAtExactPeriod(Day(3)));

        var next = ByKey.GetKeysAtNextCompleteInterval(At(2025, 1, 2));
        Assert.AreEqual(Day(3), next.Key);
        CollectionAssert.AreEqual(new[] { "p2" }, next.Value);

        var previous = ByKey.GetKeysAtPreviousIntervalIncludingPartial(At(2025, 1, 5, 12));
        Assert.AreEqual(Day(5), previous.Key);
        CollectionAssert.AreEqual(new[] { "p1" }, previous.Value);

        var timelines = ByKey.GetTimelinesAtPreviousCompleteInterval(At(2025, 1, 4, 12));
        Assert.AreEqual(Day(3), timelines.Key);
        CollectionAssert.AreEqual(new[] { "p2" }, timelines.Value.Select(kvp => kvp.Key).ToArray());

        Assert.AreEqual(Day(3), ByKey.GetNextCompleteInterval(At(2025, 1, 2)));
        Assert.IsTrue(ByKey.ContainsPeriod(Day(5)));
    }

    [TestMethod]
    public void Key_Enumerate_Filter_Transform()
    {
        var enumerated = ByKey.EnumerateIntervalsFrom(At(2025, 1, 1)).ToArray();
        CollectionAssert.AreEqual(new[] { Day(1), Day(3), Day(5) }, enumerated.Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(new[] { "p2" }, enumerated[1].Value);
        Assert.AreEqual(3, ByKey.EnumerateIntervals().Count());
        CollectionAssert.AreEqual(new[] { Day(1), Day(3) }, ByKey.EnumerateIntervals(Between(At(2025, 1, 1), At(2025, 1, 4))).Select(kvp => kvp.Key).ToArray());

        var within = ByKey.Within(Between(At(2025, 1, 1), At(2025, 1, 4)));
        CollectionAssert.AreEqual(new[] { Day(1) }, within["p1"].EnumerateIntervals().ToArray());

        var intersected = ByKey.IntersectPeriod(Between(At(2025, 1, 5, 12), At(2025, 1, 7)));
        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 5, 12), At(2025, 1, 6)) }, intersected["p1"].EnumerateIntervals().ToArray());
        Assert.IsTrue(intersected["p2"].IsEmpty());

        var samples = ByKey.SampleAt(At(2025, 1, 3, 12)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        Assert.IsTrue(samples["p2"].IsPeriod);
        Assert.AreEqual(Day(3), samples["p2"].ToInterval());
    }

    [TestMethod]
    public void Value_Lookups_Enumerate()
    {
        CollectionAssert.AreEqual(new[] { 2 }, ByTimeline.GetValuesAtInstant(At(2025, 1, 3, 12)));
        CollectionAssert.AreEqual(new[] { 1 }, ByTimeline.GetValuesAtExactPeriod(Day(1)));
        CollectionAssert.AreEqual(new[] { 1 }, ByTimeline.GetValuesAtPeriod(Between(At(2025, 1, 5, 6), At(2025, 1, 5, 12))));

        var previous = ByTimeline.GetValuesAtPreviousCompleteInterval(At(2025, 1, 4, 12));
        Assert.AreEqual(Day(3), previous.Key);
        CollectionAssert.AreEqual(new[] { 2 }, previous.Value);

        var enumerated = ByTimeline.EnumerateIntervalsBackwards().ToArray();
        CollectionAssert.AreEqual(new[] { Day(5), Day(3), Day(1) }, enumerated.Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(new[] { 1 }, enumerated[0].Value);

        var within = ByTimeline.Within(Between(At(2025, 1, 3), At(2025, 1, 6)));
        CollectionAssert.AreEqual(new[] { 1, 2 }, within.Values.ToArray());
        CollectionAssert.AreEqual(new[] { Day(3), Day(5) }, within.Keys.EnumerateIntervals().ToArray());
    }
}
