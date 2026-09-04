using NodaTime;
using Occurify.Extensions;
using Occurify.NodaTime.Extensions;
using static Occurify.NodaTime.Tests.TestData;

namespace Occurify.NodaTime.Tests;

[TestClass]
public class TimelineExtensionsTests
{
    private static readonly ITimeline Timeline5 = Timeline.FromInstants(Utc(2025, 1, 1), Utc(2025, 1, 2), Utc(2025, 1, 3), Utc(2025, 1, 4), Utc(2025, 1, 5));

    private static Instant[] Days(params int[] days) => days.Select(d => At(2025, 1, d)).ToArray();

    [TestMethod]
    public void EnumerateInstants_AllForms()
    {
        CollectionAssert.AreEqual(Days(1, 2, 3, 4, 5), Timeline5.EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(5, 4, 3, 2, 1), Timeline5.EnumerateInstantsBackwards().ToArray());
        CollectionAssert.AreEqual(Days(3, 4, 5), Timeline5.EnumerateInstantsFrom(At(2025, 1, 3)).ToArray());
        CollectionAssert.AreEqual(Days(1, 2), Timeline5.EnumerateInstantsTo(At(2025, 1, 3)).ToArray());
        CollectionAssert.AreEqual(Days(5, 4, 3), Timeline5.EnumerateInstantsBackwardsTo(At(2025, 1, 3)).ToArray());
        CollectionAssert.AreEqual(Days(2, 1), Timeline5.EnumerateInstantsBackwardsFrom(At(2025, 1, 3)).ToArray());
        CollectionAssert.AreEqual(Days(2, 3), Timeline5.EnumerateInstantRange(At(2025, 1, 2), At(2025, 1, 4)).ToArray());
        CollectionAssert.AreEqual(Days(3, 2), Timeline5.EnumerateInstantRangeBackwards(At(2025, 1, 2), At(2025, 1, 4)).ToArray());
        CollectionAssert.AreEqual(Days(2, 3), Timeline5.EnumerateInstants(Between(At(2025, 1, 2), At(2025, 1, 4))).ToArray());
        CollectionAssert.AreEqual(Days(3, 2), Timeline5.EnumerateInstantsBackwards(Between(At(2025, 1, 2), At(2025, 1, 4))).ToArray());
        CollectionAssert.AreEqual(Days(1, 2), Timeline5.EnumerateInstants(Between(null, At(2025, 1, 3))).ToArray());
        CollectionAssert.AreEqual(Days(5, 4), Timeline5.EnumerateInstantsBackwards(Between(At(2025, 1, 4), null)).ToArray());
    }

    [TestMethod]
    public void Filters_Interval()
    {
        var mask = Between(At(2025, 1, 2), At(2025, 1, 5));

        CollectionAssert.AreEqual(Days(2, 3, 4), Timeline5.Within(mask).EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(1, 5), Timeline5.Outside(mask).EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(3, 4), Timeline5.SkipWithin(mask, 1).EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(2), Timeline5.TakeWithin(mask, 1).EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(4), Timeline5.LastWithin(new[] { mask }).EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(2, 3, 4), Timeline5.Within(mask, Between(At(2025, 1, 3), At(2025, 1, 4))).EnumerateInstants().ToArray());
    }

    [TestMethod]
    public void Filters_Instant()
    {
        CollectionAssert.AreEqual(Days(3), Timeline5.Containing(At(2025, 1, 3)).EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(1, 5), Timeline5.Containing(At(2025, 1, 1), At(2025, 1, 5), At(2025, 1, 9)).EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(1, 2, 4, 5), Timeline5.Without(At(2025, 1, 3)).EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(2, 3), Timeline5.Without(new[] { At(2025, 1, 1), At(2025, 1, 4), At(2025, 1, 5) }).EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(2, 4), Timeline5.WhereInstants((Instant i) => i.InUtc().Day % 2 == 0).EnumerateInstants().ToArray());
    }

    [TestMethod]
    public void Offset_Randomize_Duration()
    {
        CollectionAssert.AreEqual(Days(2, 3, 4, 5, 6), Timeline5.Offset(Duration.FromDays(1)).EnumerateInstants().ToArray());

        var randomized = Timeline5.Randomize(42, Duration.FromHours(1)).EnumerateInstants().ToArray();
        CollectionAssert.AreEqual(randomized, Timeline5.Randomize(42, Duration.FromHours(1)).EnumerateInstants().ToArray());
        Assert.HasCount(5, randomized);
        Assert.AreEqual(5, Timeline5.Randomize(Duration.FromHours(1), Duration.FromHours(2)).EnumerateInstants().Count());
    }

    [TestMethod]
    public void Lookups_Instant()
    {
        Assert.AreEqual(At(2025, 1, 2), Timeline5.GetPreviousInstant(At(2025, 1, 3)));
        Assert.AreEqual(At(2025, 1, 4), Timeline5.GetNextInstant(At(2025, 1, 3)));
        Assert.AreEqual(At(2025, 1, 3), Timeline5.GetCurrentOrPreviousInstant(At(2025, 1, 3)));
        Assert.AreEqual(At(2025, 1, 3), Timeline5.GetCurrentOrNextInstant(At(2025, 1, 3)));
        Assert.AreEqual(At(2025, 1, 4), Timeline5.GetCurrentOrNextInstant(At(2025, 1, 3, 12)));
        Assert.IsNull(Timeline5.GetPreviousInstant(At(2025, 1, 1)));
        Assert.IsNull(Timeline5.GetNextInstant(At(2025, 1, 5)));
        Assert.AreEqual(Duration.FromHours(12), Timeline5.GetTimeToNextInstant(At(2025, 1, 1, 12)));
        Assert.AreEqual(Duration.FromHours(12), Timeline5.GetTimeSincePreviousInstant(At(2025, 1, 1, 12)));
        Assert.IsNull(Timeline5.GetTimeToNextInstant(At(2025, 1, 5)));
        Assert.IsTrue(Timeline5.ContainsInstant(At(2025, 1, 5)));
        Assert.IsFalse(Timeline5.ContainsInstant(At(2025, 1, 6)));
    }

    [TestMethod]
    public void Combine_To()
    {
        CollectionAssert.AreEqual(Days(1, 2, 3, 4, 5, 6), Timeline5.Combine(At(2025, 1, 6)).EnumerateInstants().ToArray());
        CollectionAssert.AreEqual(Days(1, 2, 3, 4, 5, 6, 7), Timeline5.Combine(At(2025, 1, 7), At(2025, 1, 6)).EnumerateInstants().ToArray());

        var periods = Timeline.FromInstants(Utc(2025, 1, 1), Utc(2025, 1, 3)).To(At(2025, 1, 2), At(2025, 1, 4)).EnumerateIntervals().ToArray();
        CollectionAssert.AreEqual(new[] { Between(At(2025, 1, 1), At(2025, 1, 2)), Between(At(2025, 1, 3), At(2025, 1, 4)) }, periods);
    }
}
