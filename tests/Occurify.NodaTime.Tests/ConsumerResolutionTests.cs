using NodaTime;
using Occurify.Extensions;
using Occurify.NodaTime.Extensions;
using static Occurify.NodaTime.Tests.TestData;

namespace Occurify.NodaTime.Tests;

/// <summary>
/// Proves that a consumer importing both <c>Occurify.Extensions</c> and <c>Occurify.NodaTime.Extensions</c>
/// can call the core and the NodaTime overloads side by side without ambiguity.
/// </summary>
[TestClass]
public class ConsumerResolutionTests
{
    private static readonly ITimeline Timeline12 = Timeline.FromInstants(Utc(2025, 1, 1), Utc(2025, 1, 2));
    private static readonly Period Period12 = PeriodOf(Utc(2025, 1, 1), Utc(2025, 1, 2));
    private static readonly Interval Interval12 = Between(At(2025, 1, 1), At(2025, 1, 2));

    [TestMethod]
    public void Within_PeriodAndInterval_BothResolve()
    {
        CollectionAssert.AreEqual(new[] { Utc(2025, 1, 1) }, Timeline12.Within(Period12).ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 1) }, Timeline12.Within(Interval12).EnumerateInstants().ToArray());
    }

    [TestMethod]
    public void Lookups_DateTimeAndInstant_Coexist()
    {
        DateTime? next = Timeline12.GetNextUtcInstant(Utc(2025, 1, 1));
        Instant? nextInstant = Timeline12.GetNextInstant(At(2025, 1, 1));

        Assert.AreEqual(Utc(2025, 1, 2), next);
        Assert.AreEqual(At(2025, 1, 2), nextInstant);
    }

    [TestMethod]
    public void WhereInstants_TypedAndBodyTypedLambdas_Resolve()
    {
        var byInstant = Timeline12.WhereInstants((Instant i) => i > At(2025, 1, 1));
        var byDateTime = Timeline12.WhereInstants((DateTime d) => d > Utc(2025, 1, 1));
        var inferred = Timeline12.WhereInstants(i => i > At(2025, 1, 1));

        CollectionAssert.AreEqual(new[] { At(2025, 1, 2) }, byInstant.EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 2) }, byDateTime.EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 2) }, inferred.EnumerateInstants().ToArray());
    }

    [TestMethod]
    public void ContainsPeriod_NullInstantBounds_ResolveWithCast()
    {
        Assert.IsTrue(Period12.ContainsPeriod((Instant?)null, At(2025, 1, 1, 12), PeriodIncludeOptions.StartPartialAllowed));
        Assert.IsTrue(Period12.ContainsPeriod((DateTime?)null, Utc(2025, 1, 1, 12), PeriodIncludeOptions.StartPartialAllowed));
    }

    [TestMethod]
    public void TotalDuration_ConvertsViaToDuration()
    {
        Duration? total = Period12.AsPeriodTimeline().TotalDuration().ToDuration();

        Assert.AreEqual(Duration.FromDays(1), total);
    }

    [TestMethod]
    public void ParameterlessCoreOverloads_WinOverNodaTimeParamsOverloads()
    {
        var timelines = new[] { Period12.AsPeriodTimeline(), Period12.AsPeriodTimeline() };
        var periods = new[] { Period12 };

        IPeriodTimeline intersected = timelines.IntersectPeriods();
        IPeriodTimeline merged = periods.Merge();

        CollectionAssert.AreEqual(new[] { Interval12 }, intersected.EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Interval12 }, merged.EnumerateIntervals().ToArray());
    }

    [TestMethod]
    public void DualEnumerateSurface()
    {
        var periodTimeline = Period12.AsPeriodTimeline();

        CollectionAssert.AreEqual(new[] { Period12 }, periodTimeline.Enumerate().ToArray());
        CollectionAssert.AreEqual(new[] { Interval12 }, periodTimeline.EnumerateIntervals().ToArray());
        CollectionAssert.AreEqual(new[] { Utc(2025, 1, 1), Utc(2025, 1, 2) }, Timeline12.Enumerate().ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 1), At(2025, 1, 2) }, Timeline12.EnumerateInstants().ToArray());
    }
}
