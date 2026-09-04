using NodaTime;
using Occurify.Extensions;
using Occurify.NodaTime.Extensions;
using static Occurify.NodaTime.Tests.TestData;

namespace Occurify.NodaTime.Tests;

[TestClass]
public class TimelineKeyValueCollectionExtensionsTests
{
    private static readonly ITimeline A = Timeline.FromInstants(Utc(2025, 1, 1), Utc(2025, 1, 3));
    private static readonly ITimeline B = Timeline.FromInstants(Utc(2025, 1, 2), Utc(2025, 1, 3));
    private static readonly Dictionary<string, ITimeline> ByKey = new() { ["a"] = A, ["b"] = B };
    private static readonly Dictionary<ITimeline, int> ByTimeline = new() { [A] = 1, [B] = 2 };

    [TestMethod]
    public void Key_GetKeysAt()
    {
        CollectionAssert.AreEqual(new[] { "a", "b" }, ByKey.GetKeysAtInstant(At(2025, 1, 3)));
        CollectionAssert.AreEqual(new[] { "b" }, ByKey.GetKeysAtInstant(At(2025, 1, 2)));

        var next = ByKey.GetKeysAtNextInstant(At(2025, 1, 1));
        Assert.AreEqual(At(2025, 1, 2), next.Key);
        CollectionAssert.AreEqual(new[] { "b" }, next.Value);

        var currentOrPrevious = ByKey.GetKeysAtCurrentOrPreviousInstant(At(2025, 1, 3));
        Assert.AreEqual(At(2025, 1, 3), currentOrPrevious.Key);
        CollectionAssert.AreEqual(new[] { "a", "b" }, currentOrPrevious.Value);

        var none = ByKey.GetKeysAtPreviousInstant(At(2025, 1, 1));
        Assert.IsNull(none.Key);
        Assert.IsEmpty(none.Value);
    }

    [TestMethod]
    public void Key_GetTimelinesAt_Lookups()
    {
        var timelines = ByKey.GetTimelinesAtInstant(At(2025, 1, 3)).ToArray();
        CollectionAssert.AreEqual(new[] { "a", "b" }, timelines.Select(kvp => kvp.Key).ToArray());

        var next = ByKey.GetTimelinesAtNextInstant(At(2025, 1, 2));
        Assert.AreEqual(At(2025, 1, 3), next.Key);
        Assert.HasCount(2, next.Value);

        Assert.AreEqual(At(2025, 1, 2), ByKey.GetPreviousInstant(At(2025, 1, 3)));
        Assert.AreEqual(At(2025, 1, 3), ByKey.GetCurrentOrNextInstant(At(2025, 1, 3)));
        Assert.AreEqual(Duration.FromDays(1), ByKey.GetTimeToNextInstant(At(2025, 1, 2)));
        Assert.IsTrue(ByKey.ContainsInstant(At(2025, 1, 1)));
        Assert.IsFalse(ByKey.IsInstant(At(2025, 1, 4)));
    }

    [TestMethod]
    public void Key_EnumerateInstants()
    {
        var enumerated = ByKey.EnumerateInstants().ToArray();

        CollectionAssert.AreEqual(new[] { At(2025, 1, 1), At(2025, 1, 2), At(2025, 1, 3) }, enumerated.Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(new[] { "a", "b" }, enumerated[2].Value);
        CollectionAssert.AreEqual(new[] { At(2025, 1, 2), At(2025, 1, 3) }, ByKey.EnumerateInstantsFrom(At(2025, 1, 2)).Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 2) }, ByKey.EnumerateInstants(Between(At(2025, 1, 2), At(2025, 1, 3))).Select(kvp => kvp.Key).ToArray());
    }

    [TestMethod]
    public void Key_Filter_Transform()
    {
        var within = ByKey.Within(Between(At(2025, 1, 2), At(2025, 1, 4)));
        CollectionAssert.AreEqual(new[] { At(2025, 1, 3) }, within["a"].EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 2), At(2025, 1, 3) }, within["b"].EnumerateInstants().ToArray());

        var offset = ByKey.Offset(Duration.FromDays(1));
        CollectionAssert.AreEqual(new[] { At(2025, 1, 2), At(2025, 1, 4) }, offset["a"].EnumerateInstants().ToArray());

        var where = ByKey.WhereInstants((Instant i) => i.InUtc().Day == 1);
        CollectionAssert.AreEqual(new[] { At(2025, 1, 1) }, where["a"].EnumerateInstants().ToArray());
        Assert.IsTrue(where["b"].IsEmpty());
    }

    [TestMethod]
    public void Value_GetValuesAt()
    {
        CollectionAssert.AreEqual(new[] { 1, 2 }, ByTimeline.GetValuesAtInstant(At(2025, 1, 3)));

        var next = ByTimeline.GetValuesAtNextInstant(At(2025, 1, 1));
        Assert.AreEqual(At(2025, 1, 2), next.Key);
        CollectionAssert.AreEqual(new[] { 2 }, next.Value);

        var previous = ByTimeline.GetValuesAtPreviousInstant(At(2025, 1, 2));
        Assert.AreEqual(At(2025, 1, 1), previous.Key);
        CollectionAssert.AreEqual(new[] { 1 }, previous.Value);

        var timelines = ByTimeline.GetTimelinesAtCurrentOrNextInstant(At(2025, 1, 2));
        Assert.AreEqual(At(2025, 1, 2), timelines.Key);
        CollectionAssert.AreEqual(new[] { 2 }, timelines.Value.Select(kvp => kvp.Value).ToArray());
    }

    [TestMethod]
    public void Value_Enumerate_Filter_Transform()
    {
        var enumerated = ByTimeline.EnumerateInstantsBackwards().ToArray();
        CollectionAssert.AreEqual(new[] { At(2025, 1, 3), At(2025, 1, 2), At(2025, 1, 1) }, enumerated.Select(kvp => kvp.Key).ToArray());
        CollectionAssert.AreEqual(new[] { 1, 2 }, enumerated[0].Value);

        var within = ByTimeline.Within(Between(At(2025, 1, 1), At(2025, 1, 3)));
        CollectionAssert.AreEqual(new[] { 1, 2 }, within.Values.ToArray());
        CollectionAssert.AreEqual(new[] { At(2025, 1, 1), At(2025, 1, 2) }, within.Keys.EnumerateInstants().ToArray());

        var randomized = ByTimeline.Randomize(7, Duration.FromHours(1));
        Assert.HasCount(2, randomized);
        Assert.AreEqual(3, ByTimeline.Keys.Offset(Duration.FromDays(1)).EnumerateInstants().Count());
    }
}
