using NodaTime;
using Occurify.Extensions;
using Occurify.NodaTime.Extensions;
using static Occurify.NodaTime.Tests.TestData;

namespace Occurify.NodaTime.Tests;

[TestClass]
public class PeriodTimelineExtensionsTests
{
    private static readonly IPeriodTimeline Days135 = PeriodTimeline.FromPeriods(
        PeriodOf(Utc(2025, 1, 1), Utc(2025, 1, 2)),
        PeriodOf(Utc(2025, 1, 3), Utc(2025, 1, 4)),
        PeriodOf(Utc(2025, 1, 5), Utc(2025, 1, 6)));

    private static Interval Day(int day) => Between(At(2025, 1, day), At(2025, 1, day + 1));

    [TestMethod]
    public void EnumerateIntervals_AllForms()
    {
        CollectionAssert.AreEqual(new[] { Day(1), Day(3), Day(5) }, Days135.EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Day(5), Day(3), Day(1) }, Days135.EnumerateIntervalsBackwards().ToArray());
        CollectionAssert.AreEqual(new[] { Day(3), Day(5) }, Days135.EnumerateIntervalsFrom(At(2025, 1, 3)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(3), Day(5) }, Days135.EnumerateIntervalsFromIncludingPartial(At(2025, 1, 3, 12)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(5) }, Days135.EnumerateIntervalsFrom(At(2025, 1, 3, 12)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(1) }, Days135.EnumerateIntervalsTo(At(2025, 1, 3, 12)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(1), Day(3) }, Days135.EnumerateIntervalsToIncludingPartial(At(2025, 1, 3, 12)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(5), Day(3) }, Days135.EnumerateIntervalsBackwardsTo(At(2025, 1, 3)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(1) }, Days135.EnumerateIntervalsBackwardsFrom(At(2025, 1, 3, 12)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(3), Day(1) }, Days135.EnumerateIntervalsBackwardsFromIncludingPartial(At(2025, 1, 3, 12)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(1), Day(3) }, Days135.EnumerateIntervalsRange(At(2025, 1, 1), At(2025, 1, 4)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(3), Day(1) }, Days135.EnumerateIntervalsRangeBackwards(At(2025, 1, 1), At(2025, 1, 4)).ToArray());
        CollectionAssert.AreEqual(new[] { Day(3), Day(5) }, Days135.EnumerateIntervals(Between(At(2025, 1, 1, 12), At(2025, 1, 6))).ToArray());
        CollectionAssert.AreEqual(new[] { Day(1), Day(3), Day(5) }, Days135.EnumerateIntervals(Between(At(2025, 1, 1, 12), At(2025, 1, 6)), PeriodIncludeOptions.PartialAllowed).ToArray());
        CollectionAssert.AreEqual(new[] { Day(5), Day(3) }, Days135.EnumerateIntervalsBackwards(Between(At(2025, 1, 3), null)).ToArray());
    }

    [TestMethod]
    public void Enumerate_InstantArguments_StillReturnPeriods()
    {
        CollectionAssert.AreEqual(
            new[] { PeriodOf(Utc(2025, 1, 3), Utc(2025, 1, 4)), PeriodOf(Utc(2025, 1, 5), Utc(2025, 1, 6)) },
            Days135.EnumerateFrom(At(2025, 1, 3)).ToArray());
        CollectionAssert.AreEqual(
            new[] { PeriodOf(Utc(2025, 1, 1), Utc(2025, 1, 2)) },
            Days135.EnumeratePeriod(Between(null, At(2025, 1, 3))).ToArray());
    }

    [TestMethod]
    public void IntervalLookups()
    {
        Assert.AreEqual(Day(3), Days135.GetPreviousCompleteInterval(At(2025, 1, 4, 12)));
        Assert.IsNull(Days135.GetPreviousCompleteInterval(At(2025, 1, 1)));
        Assert.AreEqual(Day(3), Days135.GetPreviousIntervalIncludingPartial(At(2025, 1, 3, 12)));
        Assert.AreEqual(Day(5), Days135.GetNextCompleteInterval(At(2025, 1, 3, 12)));
        Assert.AreEqual(Day(3), Days135.GetNextIntervalIncludingPartial(At(2025, 1, 3, 12)));
        Assert.IsNull(Days135.GetNextCompleteInterval(At(2025, 1, 6)));

        Assert.AreEqual(PeriodOf(Utc(2025, 1, 3), Utc(2025, 1, 4)), Days135.GetNextPeriodIncludingPartial(At(2025, 1, 3, 12)));
        Assert.AreEqual(PeriodOf(Utc(2025, 1, 5), Utc(2025, 1, 6)), Days135.GetNextCompletePeriod(At(2025, 1, 3, 12)));
    }

    [TestMethod]
    public void Contains()
    {
        Assert.IsTrue(Days135.ContainsInstant(At(2025, 1, 1, 12)));
        Assert.IsFalse(Days135.ContainsInstant(At(2025, 1, 2)));
        Assert.IsTrue(Days135.ContainsPeriod(Between(At(2025, 1, 1, 6), At(2025, 1, 1, 12))));
        Assert.IsFalse(Days135.ContainsPeriod(Between(At(2025, 1, 1, 6), At(2025, 1, 3))));
        Assert.IsTrue(Days135.ContainsExactPeriod(Day(1)));
        Assert.IsFalse(Days135.ContainsExactPeriod(Between(At(2025, 1, 1), At(2025, 1, 1, 12))));
        Assert.IsTrue(Days135.TryGetPeriod(At(2025, 1, 1, 12), out var period));
        Assert.AreEqual(PeriodOf(Utc(2025, 1, 1), Utc(2025, 1, 2)), period);
        Assert.IsFalse(Days135.TryGetPeriod(At(2025, 1, 2, 12), out _));
    }

    [TestMethod]
    public void Filters()
    {
        var mask = Between(At(2025, 1, 1), At(2025, 1, 4));

        CollectionAssert.AreEqual(new[] { Day(1), Day(3) }, Days135.Within(mask).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Day(5) }, Days135.Outside(mask).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Day(3) }, Days135.Containing(At(2025, 1, 3, 12)).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Day(1), Day(5) }, Days135.Within(Day(1), Day(5)).EnumerateIntervals().ToArray());
    }

    [TestMethod]
    public void Transforms()
    {
        CollectionAssert.AreEqual(
            new[] { Between(At(2025, 1, 1, 12), At(2025, 1, 2)), Between(At(2025, 1, 3), At(2025, 1, 3, 12)) },
            Days135.IntersectPeriod(Between(At(2025, 1, 1, 12), At(2025, 1, 3, 12))).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(
            new[] { Day(1), Day(2), Day(3), Day(5) },
            Days135.Merge(Between(At(2025, 1, 2), At(2025, 1, 3))).EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(
            new[] { Between(At(2025, 1, 1), At(2025, 1, 1, 12)), Between(At(2025, 1, 5, 12), At(2025, 1, 6)) },
            Days135.Subtract(Between(At(2025, 1, 1, 12), At(2025, 1, 5, 12))).EnumerateIntervals().ToArray());
        Assert.AreEqual(4, Days135.Cut(At(2025, 1, 1, 12)).EnumerateIntervals().Count());
        CollectionAssert.AreEqual(new[] { Day(2), Day(4), Day(6) }, Days135.Offset(Duration.FromDays(1)).EnumerateIntervals().ToArray());
        Assert.AreEqual(3, Days135.Randomize(1, Duration.FromHours(1)).EnumerateIntervals().Count());
    }

    [TestMethod]
    public void SampleAt_OnPeriod()
    {
        var sample = Days135.SampleAt(At(2025, 1, 1, 12));

        Assert.IsTrue(sample.IsPeriod);
        Assert.AreEqual(At(2025, 1, 1, 12), sample.SampleInstant());
        Assert.AreEqual(Day(1), sample.PeriodAsInterval());
        Assert.IsNull(sample.GapAsInterval());
        Assert.AreEqual(Day(1), sample.ToInterval());
        Assert.AreEqual(At(2025, 1, 1), sample.StartInstant());
        Assert.AreEqual(At(2025, 1, 2), sample.EndInstant());
    }

    [TestMethod]
    public void SampleAt_OnGap()
    {
        var sample = Days135.SampleAt(At(2025, 1, 2, 12));

        Assert.IsTrue(sample.IsGap);
        Assert.AreEqual(Between(At(2025, 1, 2), At(2025, 1, 3)), sample.GapAsInterval());
        Assert.IsNull(sample.PeriodAsInterval());
        Assert.AreEqual(Between(At(2025, 1, 2), At(2025, 1, 3)), sample.ToInterval());
    }

    [TestMethod]
    public void SampleAt_OnOpenEndedGap()
    {
        var sample = Days135.SampleAt(At(2024, 12, 31));

        Assert.IsTrue(sample.IsGap);
        Assert.IsNull(sample.StartInstant());
        Assert.AreEqual(At(2025, 1, 1), sample.EndInstant());
        Assert.IsFalse(sample.ToInterval().HasStart);
    }
}
