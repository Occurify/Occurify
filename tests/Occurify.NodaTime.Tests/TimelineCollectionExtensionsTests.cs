using NodaTime;
using Occurify.Extensions;
using Occurify.NodaTime.Extensions;
using static Occurify.NodaTime.Tests.TestData;

namespace Occurify.NodaTime.Tests;

[TestClass]
public class TimelineCollectionExtensionsTests
{
    private static readonly ITimeline A = Timeline.FromInstants(Utc(2025, 1, 1), Utc(2025, 1, 3));
    private static readonly ITimeline B = Timeline.FromInstants(Utc(2025, 1, 2), Utc(2025, 1, 3));
    private static readonly ITimeline[] Timelines = { A, B };

    [TestMethod]
    public void EnumerateInstants_MergesAndOrders()
    {
        CollectionAssert.AreEqual(new[] { At(2025, 1, 1), At(2025, 1, 2), At(2025, 1, 3) }, Timelines.EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 2), At(2025, 1, 3) }, Timelines.EnumerateInstantsFrom(At(2025, 1, 2)).ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 3), At(2025, 1, 2), At(2025, 1, 1) }, Timelines.EnumerateInstantsBackwards().ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 1), At(2025, 1, 2) }, Timelines.EnumerateInstants(Between(At(2025, 1, 1), At(2025, 1, 3))).ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 2) }, Timelines.EnumerateInstantsRange(At(2025, 1, 2), At(2025, 1, 3)).ToArray());
    }

    [TestMethod]
    public void Filters()
    {
        var within = Timelines.Within(Between(At(2025, 1, 2), At(2025, 1, 4))).ToArray();
        Assert.HasCount(2, within);
        CollectionAssert.AreEqual(new[] { At(2025, 1, 3) }, within[0].EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 2), At(2025, 1, 3) }, within[1].EnumerateInstants().ToArray());

        var without = Timelines.Without(At(2025, 1, 3)).ToArray();
        CollectionAssert.AreEqual(new[] { At(2025, 1, 1) }, without[0].EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 2) }, without[1].EnumerateInstants().ToArray());

        var where = Timelines.WhereInstants((Instant i) => i.InUtc().Day == 3).ToArray();
        CollectionAssert.AreEqual(new[] { At(2025, 1, 3) }, where[0].EnumerateInstants().ToArray());
    }

    [TestMethod]
    public void Offset_Duration()
    {
        var offset = Timelines.Offset(Duration.FromDays(1)).ToArray();

        CollectionAssert.AreEqual(new[] { At(2025, 1, 2), At(2025, 1, 4) }, offset[0].EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 3), At(2025, 1, 4) }, offset[1].EnumerateInstants().ToArray());
    }

    [TestMethod]
    public void Lookups()
    {
        Assert.AreEqual(At(2025, 1, 2), Timelines.GetNextInstant(At(2025, 1, 1)));
        Assert.AreEqual(At(2025, 1, 2), Timelines.GetPreviousInstant(At(2025, 1, 3)));
        Assert.AreEqual(At(2025, 1, 3), Timelines.GetCurrentOrNextInstant(At(2025, 1, 3)));
        Assert.AreEqual(At(2025, 1, 3), Timelines.GetCurrentOrPreviousInstant(At(2025, 1, 4)));
        Assert.AreEqual(Duration.FromDays(1), Timelines.GetTimeToNextInstant(At(2025, 1, 1)));
        Assert.AreEqual(Duration.FromDays(1), Timelines.GetTimeSincePreviousInstant(At(2025, 1, 3)));
        Assert.IsTrue(Timelines.ContainsInstant(At(2025, 1, 2)));
        Assert.IsTrue(Timelines.IsInstant(At(2025, 1, 2)));
        Assert.IsFalse(Timelines.ContainsInstant(At(2025, 1, 4)));
    }

    [TestMethod]
    public void GetTimelinesAt()
    {
        CollectionAssert.AreEqual(new[] { A, B }, Timelines.GetTimelinesAtInstant(At(2025, 1, 3)).ToArray());
        CollectionAssert.AreEqual(new[] { A }, Timelines.GetTimelinesAtInstant(At(2025, 1, 1)).ToArray());

        var next = Timelines.GetTimelinesAtNextInstant(At(2025, 1, 1));
        Assert.AreEqual(At(2025, 1, 2), next.Key);
        CollectionAssert.AreEqual(new[] { B }, next.Value);

        var previous = Timelines.GetTimelinesAtCurrentOrPreviousInstant(At(2025, 1, 3));
        Assert.AreEqual(At(2025, 1, 3), previous.Key);
        CollectionAssert.AreEqual(new[] { A, B }, previous.Value);

        var none = Timelines.GetTimelinesAtNextInstant(At(2025, 1, 3));
        Assert.IsNull(none.Key);
        Assert.IsEmpty(none.Value);
    }
}
