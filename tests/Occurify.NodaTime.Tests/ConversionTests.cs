using NodaTime;
using Occurify.NodaTime.Extensions;
using static Occurify.NodaTime.Tests.TestData;

namespace Occurify.NodaTime.Tests;

[TestClass]
public class ConversionTests
{
    [TestMethod]
    public void ToInstant_UtcDateTime_RoundTrips()
    {
        DateTime? dateTime = Utc(2025, 1, 1, 12, 30);

        var instant = dateTime.ToInstant();

        Assert.AreEqual(Instant.FromDateTimeUtc(dateTime.Value), instant);
        Assert.AreEqual(dateTime, instant!.Value.ToDateTimeUtc());
        Assert.AreEqual(DateTimeKind.Utc, instant.Value.ToDateTimeUtc().Kind);
    }

    [TestMethod]
    public void ToInstant_Null_ReturnsNull()
    {
        DateTime? dateTime = null;

        Assert.IsNull(dateTime.ToInstant());
    }

    [TestMethod]
    public void ToInstant_LocalKind_Throws()
    {
        DateTime? dateTime = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Local);

        Assert.ThrowsExactly<ArgumentException>(() => dateTime.ToInstant());
    }

    [TestMethod]
    public void ToInstant_UnspecifiedKind_Throws()
    {
        DateTime? dateTime = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

        Assert.ThrowsExactly<ArgumentException>(() => dateTime.ToInstant());
    }

    [TestMethod]
    public void ToDuration_TimeSpan_RoundTrips()
    {
        TimeSpan? timeSpan = TimeSpan.FromMinutes(90);

        var duration = timeSpan.ToDuration();

        Assert.AreEqual(Duration.FromMinutes(90), duration);
        Assert.AreEqual(timeSpan, duration!.Value.ToTimeSpan());
    }

    [TestMethod]
    public void ToDuration_Null_ReturnsNull()
    {
        TimeSpan? timeSpan = null;

        Assert.IsNull(timeSpan.ToDuration());
    }

    [TestMethod]
    public void ToInterval_BoundedPeriod_HasStartAndEnd()
    {
        var period = PeriodOf(Utc(2025, 1, 1), Utc(2025, 1, 2));

        var interval = period.ToInterval();

        Assert.IsTrue(interval.HasStart);
        Assert.IsTrue(interval.HasEnd);
        Assert.AreEqual(At(2025, 1, 1), interval.Start);
        Assert.AreEqual(At(2025, 1, 2), interval.End);
    }

    [TestMethod]
    public void ToInterval_OpenStart_HasNoStart()
    {
        var interval = PeriodOf(null, Utc(2025, 1, 2)).ToInterval();

        Assert.IsFalse(interval.HasStart);
        Assert.IsTrue(interval.HasEnd);
        Assert.AreEqual(At(2025, 1, 2), interval.End);
    }

    [TestMethod]
    public void ToInterval_OpenEnd_HasNoEnd()
    {
        var interval = PeriodOf(Utc(2025, 1, 1), null).ToInterval();

        Assert.IsTrue(interval.HasStart);
        Assert.IsFalse(interval.HasEnd);
        Assert.AreEqual(At(2025, 1, 1), interval.Start);
    }

    [TestMethod]
    public void ToInterval_InfinitePeriod_HasNeitherBound()
    {
        var interval = PeriodOf(null, null).ToInterval();

        Assert.IsFalse(interval.HasStart);
        Assert.IsFalse(interval.HasEnd);
    }

    [TestMethod]
    public void ToPeriod_RoundTripsAllShapes()
    {
        var periods = new[]
        {
            PeriodOf(Utc(2025, 1, 1), Utc(2025, 1, 2)),
            PeriodOf(null, Utc(2025, 1, 2)),
            PeriodOf(Utc(2025, 1, 1), null),
            PeriodOf(null, null)
        };

        foreach (var period in periods)
        {
            Assert.AreEqual(period, period.ToInterval().ToPeriod());
        }
    }

    [TestMethod]
    public void ToPeriod_InstantOutsideDateTimeRange_Throws()
    {
        var interval = Between(Instant.MinValue, null);

        Assert.ThrowsExactly<InvalidOperationException>(() => interval.ToPeriod());
    }
}
