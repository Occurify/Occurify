using Occurify.Extensions;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineKeyValueCollectionEnumerateBackwardsTests
{
    private static DateTime Utc(int day) => new(2025, 1, day, 0, 0, 0, DateTimeKind.Utc);

    private static readonly IPeriodTimeline First = PeriodTimeline.FromPeriods(new Period(Utc(1), Utc(2)), new Period(Utc(5), Utc(6)));
    private static readonly IPeriodTimeline Second = PeriodTimeline.FromPeriods(new Period(Utc(3), Utc(4)));

    private static readonly Period[] ExpectedBackwards =
    {
        new(Utc(5), Utc(6)),
        new(Utc(3), Utc(4)),
        new(Utc(1), Utc(2))
    };

    [TestMethod]
    [Timeout(10000, CooperativeCancellation = true)]
    public void KeyCollection_EnumerateBackwards_TerminatesAndOrdersLatestFirst()
    {
        var source = new Dictionary<string, IPeriodTimeline> { ["first"] = First, ["second"] = Second };

        var backwards = source.EnumerateBackwards().ToArray();
        var backwardsFrom = source.EnumerateBackwardsFrom(Utc(5)).ToArray();

        CollectionAssert.AreEqual(ExpectedBackwards, backwards.Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(new[] { "first" }, backwards[0].Value);
        CollectionAssert.AreEqual(ExpectedBackwards.Skip(1).ToArray(), backwardsFrom.Select(kvp => kvp.Key).ToArray());
    }

    [TestMethod]
    [Timeout(10000, CooperativeCancellation = true)]
    public void ValueCollection_EnumerateBackwards_TerminatesAndOrdersLatestFirst()
    {
        var source = new Dictionary<IPeriodTimeline, int> { [First] = 1, [Second] = 2 };

        var backwards = source.EnumerateBackwards().ToArray();
        var backwardsFrom = source.EnumerateBackwardsFromIncludingPartial(Utc(4)).ToArray();

        CollectionAssert.AreEqual(ExpectedBackwards, backwards.Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(new[] { 2 }, backwards[1].Value);
        CollectionAssert.AreEqual(ExpectedBackwards.Skip(1).ToArray(), backwardsFrom.Select(kvp => kvp.Key).ToArray());
    }

    private static readonly IPeriodTimeline OverlapA = PeriodTimeline.FromPeriods(new Period(Utc(1), Utc(3)));
    private static readonly IPeriodTimeline OverlapB = PeriodTimeline.FromPeriods(new Period(Utc(2), Utc(4)));

    [TestMethod]
    public void KeyCollection_OverlappingTimelines_EnumeratesEveryPeriod()
    {
        var source = new Dictionary<string, IPeriodTimeline> { ["a"] = OverlapA, ["b"] = OverlapB };
        var expected = new[] { new Period(Utc(1), Utc(3)), new Period(Utc(2), Utc(4)) };

        var from = source.EnumerateFrom(Utc(1)).ToArray();
        var range = source.EnumerateRange(Utc(1), Utc(4)).ToArray();
        var backwards = source.EnumerateBackwardsFrom(Utc(5)).ToArray();
        var to = source.EnumerateTo(Utc(4)).ToArray();

        CollectionAssert.AreEqual(expected, from.Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(new[] { "a" }, from[0].Value);
        CollectionAssert.AreEqual(new[] { "b" }, from[1].Value);
        CollectionAssert.AreEqual(expected, range.Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(Enumerable.Reverse(expected).ToArray(), backwards.Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(expected, to.Select(kvp => kvp.Key).ToArray());
    }

    [TestMethod]
    public void ValueCollection_OverlappingTimelines_EnumeratesEveryPeriod()
    {
        var source = new Dictionary<IPeriodTimeline, int> { [OverlapA] = 1, [OverlapB] = 2 };
        var expected = new[] { new Period(Utc(1), Utc(3)), new Period(Utc(2), Utc(4)) };

        var from = source.EnumerateFromIncludingPartial(Utc(1).AddDays(-1)).ToArray();
        var backwards = source.EnumerateRangeBackwards(Utc(1), Utc(4)).ToArray();

        CollectionAssert.AreEqual(expected, from.Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(new[] { 1 }, from[0].Value);
        CollectionAssert.AreEqual(new[] { 2 }, from[1].Value);
        CollectionAssert.AreEqual(Enumerable.Reverse(expected).ToArray(), backwards.Select(kvp => kvp.Key).ToArray());
    }
}
