using Occurify.Extensions;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineCollectionMergeTests
{
    [TestMethod]
    public void Merge_EmptyCollection_IsEmpty()
    {
        Assert.IsTrue(Array.Empty<IPeriodTimeline>().Merge().IsEmpty());
    }

    [TestMethod]
    public void IntersectPeriods_EmptyCollection_IsEmpty()
    {
        Assert.IsTrue(Array.Empty<IPeriodTimeline>().IntersectPeriods().IsEmpty());
        Assert.IsTrue(Array.Empty<Period>().IntersectPeriods().IsEmpty());
    }

    [TestMethod]
    public void TotalDuration_EmptyCollection_IsZero()
    {
        Assert.AreEqual(TimeSpan.Zero, Array.Empty<IPeriodTimeline>().TotalDuration());
        Assert.AreEqual(TimeSpan.Zero, Array.Empty<IPeriodTimeline>().TotalDuration(addIndividualTimelineDurations: true));
    }

    [TestMethod]
    public void Outside_EmptyMask_ReturnsDistinctTimelinePerEntry()
    {
        var timeline = Period.Create(DateTime.UtcNow, TimeSpan.FromHours(1)).AsPeriodTimeline();
        var source = new List<KeyValuePair<IPeriodTimeline, int>>
        {
            new(timeline, 1),
            new(timeline, 2)
        };

        var result = source.Outside(PeriodTimeline.Empty());

        Assert.HasCount(2, result);
    }
}
