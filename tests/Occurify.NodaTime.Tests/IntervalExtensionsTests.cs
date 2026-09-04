using NodaTime;
using Occurify.Extensions;
using Occurify.NodaTime.Extensions;
using static Occurify.NodaTime.Tests.TestData;

namespace Occurify.NodaTime.Tests;

[TestClass]
public class IntervalExtensionsTests
{
    private static readonly Interval Outer = Between(At(2025, 1, 10), At(2025, 1, 20));

    [TestMethod]
    public void ContainsInstant_StartInclusiveEndExclusive()
    {
        Assert.IsTrue(Outer.ContainsInstant(At(2025, 1, 10)));
        Assert.IsTrue(Outer.ContainsInstant(At(2025, 1, 15)));
        Assert.IsFalse(Outer.ContainsInstant(At(2025, 1, 20)));
        Assert.IsFalse(Outer.ContainsInstant(At(2025, 1, 9)));
    }

    [TestMethod]
    public void ContainsInstant_OpenBounds()
    {
        Assert.IsTrue(Between(null, At(2025, 1, 20)).ContainsInstant(At(1900, 1, 1)));
        Assert.IsTrue(Between(At(2025, 1, 10), null).ContainsInstant(At(2999, 1, 1)));
        Assert.IsTrue(Between(null, null).ContainsInstant(At(2025, 1, 1)));
    }

    [TestMethod]
    [DataRow(12, 15, true, true, true, true, DisplayName = "inside")]
    [DataRow(5, 15, false, true, false, true, DisplayName = "overlapping start")]
    [DataRow(15, 25, false, false, true, true, DisplayName = "overlapping end")]
    [DataRow(5, 25, false, false, false, true, DisplayName = "enclosing")]
    [DataRow(25, 30, false, false, false, false, DisplayName = "disjoint")]
    [DataRow(20, 25, false, false, false, false, DisplayName = "touching end")]
    public void ContainsPeriod_AllIncludeOptions(int startDay, int endDay, bool completeOnly, bool startPartial, bool endPartial, bool partial)
    {
        var other = Between(At(2025, 1, startDay), At(2025, 1, endDay));

        Assert.AreEqual(completeOnly, Outer.ContainsPeriod(other), "CompleteOnly");
        Assert.AreEqual(startPartial, Outer.ContainsPeriod(other, PeriodIncludeOptions.StartPartialAllowed), "StartPartialAllowed");
        Assert.AreEqual(endPartial, Outer.ContainsPeriod(other, PeriodIncludeOptions.EndPartialAllowed), "EndPartialAllowed");
        Assert.AreEqual(partial, Outer.ContainsPeriod(other, PeriodIncludeOptions.PartialAllowed), "PartialAllowed");
    }

    [TestMethod]
    public void ContainsPeriod_InstantBounds_NullMeansOpen()
    {
        Assert.IsFalse(Outer.ContainsPeriod(null, At(2025, 1, 15)));
        Assert.IsTrue(Outer.ContainsPeriod(null, At(2025, 1, 15), PeriodIncludeOptions.StartPartialAllowed));
        Assert.IsTrue(Outer.ContainsPeriod(At(2025, 1, 15), null, PeriodIncludeOptions.EndPartialAllowed));
        Assert.IsFalse(Outer.ContainsPeriod(At(2025, 1, 15), null));
    }

    [TestMethod]
    public void Excludes_InstantAndInterval()
    {
        Assert.IsTrue(Outer.Excludes(At(2025, 1, 20)));
        Assert.IsFalse(Outer.Excludes(At(2025, 1, 19)));
        Assert.IsTrue(Outer.Excludes(Between(At(2025, 1, 20), At(2025, 1, 25))));
        Assert.IsFalse(Outer.Excludes(Between(At(2025, 1, 15), At(2025, 1, 25))));
    }

    [TestMethod]
    public void ContainsAnyInstant_Timeline()
    {
        var timeline = Timeline.FromInstants(Utc(2025, 1, 12), Utc(2025, 1, 30));

        Assert.IsTrue(Outer.ContainsAnyInstant(timeline));
        Assert.IsFalse(Between(At(2025, 1, 21), At(2025, 1, 25)).ContainsAnyInstant(timeline));
        Assert.IsTrue(Between(At(2025, 1, 20), null).ContainsAnyInstant(timeline));
        Assert.IsFalse(Between(null, At(2025, 1, 12)).ContainsAnyInstant(timeline));
    }

    [TestMethod]
    public void ContainsAnyInstant_Instants()
    {
        Assert.IsTrue(Outer.ContainsAnyInstant(new[] { At(2025, 1, 1), At(2025, 1, 15) }));
        Assert.IsFalse(Outer.ContainsAnyInstant(new[] { At(2025, 1, 1), At(2025, 1, 20) }));
    }

    [TestMethod]
    public void Offset_Duration_ShiftsBothBounds()
    {
        var offset = Outer.Offset(Duration.FromDays(1));

        Assert.AreEqual(Between(At(2025, 1, 11), At(2025, 1, 21)), offset);
    }

    [TestMethod]
    public void Offset_Duration_OpenBoundStaysOpen()
    {
        var offset = Between(At(2025, 1, 10), null).Offset(Duration.FromDays(-1));

        Assert.AreEqual(At(2025, 1, 9), offset.Start);
        Assert.IsFalse(offset.HasEnd);
    }

    [TestMethod]
    public void IntersectPeriod_Merge_Subtract()
    {
        var other = Between(At(2025, 1, 15), At(2025, 1, 25));

        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 15), At(2025, 1, 20)) }, Outer.IntersectPeriod(other).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 10), At(2025, 1, 25)) }, Outer.Merge(other).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 10), At(2025, 1, 15)) }, Outer.Subtract(other).EnumerateIntervals().ToArray());
    }

    [TestMethod]
    public void Collection_ContainsInstant_ContainsPeriod_Excludes()
    {
        var intervals = new[] { Outer, Between(At(2025, 2, 1), At(2025, 2, 2)) };

        Assert.IsTrue(intervals.ContainsInstant(At(2025, 2, 1)));
        Assert.IsFalse(intervals.ContainsInstant(At(2025, 1, 25)));
        Assert.IsTrue(intervals.ContainsPeriod(Between(At(2025, 1, 12), At(2025, 1, 13))));
        Assert.IsTrue(intervals.Excludes(Between(At(2025, 1, 20), At(2025, 2, 1))));
    }

    [TestMethod]
    public void Collection_TotalDuration()
    {
        var intervals = new[] { Outer, Between(At(2025, 1, 15), At(2025, 1, 25)) };

        Assert.AreEqual(Duration.FromDays(20), intervals.TotalDuration());
        Assert.AreEqual(Duration.FromDays(15), intervals.TotalDuration(mergeOverlapping: true));
        Assert.IsNull(new[] { Between(At(2025, 1, 1), null) }.TotalDuration());
    }

    [TestMethod]
    public void Collection_Offset_Merge_AsPeriodTimeline()
    {
        var intervals = new[] { Between(At(2025, 1, 1), At(2025, 1, 2)), Between(At(2025, 1, 2), At(2025, 1, 3)) };

        CollectionAssert.AreEqual(
            new[] { Between(At(2025, 1, 2), At(2025, 1, 3)), Between(At(2025, 1, 3), At(2025, 1, 4)) },
            intervals.Offset(Duration.FromDays(1)).ToArray());
        CollectionAssert.AreEqual(
            new[] { Between(At(2025, 1, 1), At(2025, 1, 3)) },
            intervals.AsPeriodTimeline().Stitch().EnumerateIntervals().ToArray());
    }
}
