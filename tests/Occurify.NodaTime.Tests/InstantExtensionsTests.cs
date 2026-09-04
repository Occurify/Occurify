using NodaTime;
using Occurify.Extensions;
using Occurify.NodaTime.Extensions;
using static Occurify.NodaTime.Tests.TestData;

namespace Occurify.NodaTime.Tests;

[TestClass]
public class InstantExtensionsTests
{
    private static readonly Interval Interval = Between(At(2025, 1, 10), At(2025, 1, 20));
    private static readonly Period Period = PeriodOf(Utc(2025, 1, 10), Utc(2025, 1, 20));

    [TestMethod]
    public void IsWithin_IsOutside_AllMaskKinds()
    {
        var inside = At(2025, 1, 15);
        var outside = At(2025, 1, 25);

        Assert.IsTrue(inside.IsWithin(Interval));
        Assert.IsTrue(inside.IsWithin(Period));
        Assert.IsTrue(inside.IsWithin(Period.AsPeriodTimeline()));
        Assert.IsTrue(inside.IsWithin(new[] { Period.AsPeriodTimeline() }));
        Assert.IsFalse(outside.IsWithin(Interval));

        Assert.IsTrue(outside.IsOutside(Interval));
        Assert.IsTrue(outside.IsOutside(Period));
        Assert.IsTrue(outside.IsOutside(Period.AsPeriodTimeline()));
        Assert.IsTrue(outside.IsOutside(new[] { Period.AsPeriodTimeline() }));
        Assert.IsFalse(inside.IsOutside(Interval));
    }

    [TestMethod]
    [Timeout(5000, CooperativeCancellation = true)]
    public void IsOutside_InfinitePeriodTimeline_DoesNotEnumerate()
    {
        var everything = PeriodTimeline.Periodic(TimeSpan.FromHours(1));

        Assert.IsFalse(At(2025, 1, 15).IsOutside(everything));
        Assert.IsFalse(At(2025, 1, 15).IsOutside(new[] { everything }));
        Assert.IsTrue(At(2025, 1, 15).IsWithin(everything));
    }

    [TestMethod]
    public void IsOnTimeline_IsOnAnyTimeline()
    {
        var timeline = Timeline.FromInstants(Utc(2025, 1, 15));

        Assert.IsTrue(At(2025, 1, 15).IsOnTimeline(timeline));
        Assert.IsFalse(At(2025, 1, 16).IsOnTimeline(timeline));
        Assert.IsTrue(At(2025, 1, 15).IsOnAnyTimeline(Timeline.Empty(), timeline));
        Assert.IsFalse(At(2025, 1, 16).IsOnAnyTimeline(new[] { Timeline.Empty(), timeline }));
    }

    [TestMethod]
    public void To_CreatesPeriod()
    {
        Assert.AreEqual(Period, At(2025, 1, 10).To(At(2025, 1, 20)));
        Assert.AreEqual(PeriodOf(Utc(2025, 1, 10), null), At(2025, 1, 10).To(null));
        Assert.AreEqual(PeriodOf(null, Utc(2025, 1, 20)), ((Instant?)null).To(At(2025, 1, 20)));
        Assert.AreEqual(Period, At(2025, 1, 10).ToPeriodWithDuration(Duration.FromDays(10)));
    }

    [TestMethod]
    public void AsTimeline_Combine()
    {
        CollectionAssert.AreEqual(new[] { At(2025, 1, 10) }, At(2025, 1, 10).AsTimeline().EnumerateInstants().ToArray());
        Assert.IsTrue(((Instant?)null).AsTimeline().IsEmpty());
        CollectionAssert.AreEqual(
            new[] { At(2025, 1, 10), At(2025, 1, 11), At(2025, 1, 12) },
            At(2025, 1, 12).Combine(At(2025, 1, 10), At(2025, 1, 11)).EnumerateInstants().ToArray());
    }

    [TestMethod]
    public void AsConsecutivePeriodTimeline()
    {
        var intervals = At(2025, 1, 10).AsConsecutivePeriodTimeline().EnumerateIntervals().ToArray();

        CollectionAssert.AreEqual(new[] { Between(null, At(2025, 1, 10)), Between(At(2025, 1, 10), null) }, intervals);
    }

    [TestMethod]
    public void Collection_Within_Outside()
    {
        var instants = new[] { At(2025, 1, 5), At(2025, 1, 15), At(2025, 1, 25) };

        CollectionAssert.AreEqual(new[] { At(2025, 1, 15) }, instants.Within(Interval).ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 15) }, instants.Within(Period).ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 5), At(2025, 1, 25) }, instants.Outside(Interval).ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 5), At(2025, 1, 25) }, instants.Outside(Period.AsPeriodTimeline()).ToArray());
    }

    [TestMethod]
    public void Collection_AsTimeline_To()
    {
        var instants = new[] { At(2025, 1, 5), At(2025, 1, 15) };

        CollectionAssert.AreEqual(instants, instants.AsTimeline().EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(
            new[] { Between(At(2025, 1, 5), At(2025, 1, 10)), Between(At(2025, 1, 15), At(2025, 1, 20)) },
            instants.To(At(2025, 1, 10), At(2025, 1, 20)).EnumerateIntervals().ToArray());
    }
}
