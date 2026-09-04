using NodaTime;
using Occurify.Extensions;
using Occurify.NodaTime.Extensions;
using static Occurify.NodaTime.Tests.TestData;

namespace Occurify.NodaTime.Tests;

[TestClass]
public class PeriodExtensionsTests
{
    private static readonly Period Outer = PeriodOf(Utc(2025, 1, 10), Utc(2025, 1, 20));

    [TestMethod]
    public void ContainsInstant_Instant()
    {
        Assert.IsTrue(Outer.ContainsInstant(At(2025, 1, 10)));
        Assert.IsFalse(Outer.ContainsInstant(At(2025, 1, 20)));
        Assert.IsTrue(Outer.ContainsAnyInstant(new[] { At(2025, 1, 1), At(2025, 1, 19) }));
    }

    [TestMethod]
    public void ContainsPeriod_Interval_IncludeOptions()
    {
        var overlappingStart = Between(At(2025, 1, 5), At(2025, 1, 15));

        Assert.IsFalse(Outer.ContainsPeriod(overlappingStart));
        Assert.IsTrue(Outer.ContainsPeriod(overlappingStart, PeriodIncludeOptions.StartPartialAllowed));
        Assert.IsFalse(Outer.ContainsPeriod(overlappingStart, PeriodIncludeOptions.EndPartialAllowed));
        Assert.IsTrue(Outer.ContainsPeriod(overlappingStart, PeriodIncludeOptions.PartialAllowed));
        Assert.IsTrue(Outer.ContainsPeriod(At(2025, 1, 12), At(2025, 1, 13)));
        Assert.IsFalse(Outer.ContainsPeriod(At(2025, 1, 12), null));
    }

    [TestMethod]
    public void Excludes_Interval()
    {
        Assert.IsTrue(Outer.Excludes(Between(At(2025, 1, 20), At(2025, 1, 25))));
        Assert.IsFalse(Outer.Excludes(Between(At(2025, 1, 19), At(2025, 1, 25))));
        Assert.IsTrue(Outer.Excludes(At(2025, 1, 9)));
    }

    [TestMethod]
    public void Offset_Duration_MatchesTimeSpanOffset()
    {
        Assert.AreEqual(Outer.Offset(TimeSpan.FromHours(6)), Outer.Offset(Duration.FromHours(6)));
        Assert.AreEqual(PeriodOf(null, null), PeriodOf(null, null).Offset(Duration.FromDays(1)));
    }

    [TestMethod]
    public void Offset_Duration_Overflow_Throws()
    {
        var period = PeriodOf(Utc(9999, 12, 31), null);

        Assert.ThrowsExactly<OverflowException>(() => period.Offset(Duration.FromDays(1)));
    }

    [TestMethod]
    public void IntersectPeriod_Merge_Subtract_Interval()
    {
        var other = Between(At(2025, 1, 15), At(2025, 1, 25));

        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 15), At(2025, 1, 20)) }, Outer.IntersectPeriod(other).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 10), At(2025, 1, 25)) }, Outer.Merge(other).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 10), At(2025, 1, 15)) }, Outer.Subtract(other).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 10), At(2025, 1, 15)) }, Outer.Subtract(other, Between(At(2025, 1, 5), At(2025, 1, 6))).EnumerateIntervals().ToArray());
    }

    [TestMethod]
    public void Cut_Instant()
    {
        var cut = Outer.Cut(At(2025, 1, 15)).EnumerateIntervals().ToArray();

        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 10), At(2025, 1, 15)), Between(At(2025, 1, 15), At(2025, 1, 20)) }, cut);
    }

    [TestMethod]
    public void Collection_Merge_IntersectPeriods_Subtract_Interval()
    {
        var periods = new[] { Outer, PeriodOf(Utc(2025, 2, 1), Utc(2025, 2, 2)) };

        CollectionAssert.AreEqual(
            new[] { Between(At(2025, 1, 10), At(2025, 1, 25)), Between(At(2025, 2, 1), At(2025, 2, 2)) },
            periods.Merge(Between(At(2025, 1, 15), At(2025, 1, 25))).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(
            new[] { Between(At(2025, 1, 15), At(2025, 1, 20)) },
            periods.IntersectPeriods(Between(At(2025, 1, 15), At(2025, 1, 25))).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(
            new[] { Between(At(2025, 1, 10), At(2025, 1, 15)), Between(At(2025, 2, 1), At(2025, 2, 2)) },
            periods.Subtract(Between(At(2025, 1, 15), At(2025, 1, 25))).EnumerateIntervals().ToArray());
    }

    [TestMethod]
    public void Collection_ContainsPeriod_Excludes_Offset()
    {
        var periods = new[] { Outer, PeriodOf(Utc(2025, 2, 1), Utc(2025, 2, 2)) };

        Assert.IsTrue(periods.ContainsPeriod(Between(At(2025, 2, 1), At(2025, 2, 2))));
        Assert.IsFalse(periods.ContainsPeriod(Between(At(2025, 1, 19), At(2025, 1, 21))));
        Assert.IsTrue(periods.ContainsPeriod(Between(At(2025, 1, 19), At(2025, 1, 21)), PeriodIncludeOptions.PartialAllowed));
        Assert.IsTrue(periods.Excludes(Between(At(2025, 1, 20), At(2025, 2, 1))));
        Assert.IsTrue(periods.ContainsInstant(At(2025, 2, 1)));
        CollectionAssert.AreEqual(periods.Offset(TimeSpan.FromDays(1)).ToArray(), periods.Offset(Duration.FromDays(1)).ToArray());
    }
}
