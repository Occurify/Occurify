using Occurify.Helpers;

namespace Occurify.Tests;

[TestClass]
public class PeriodCreateTests
{
    [TestMethod]
    public void Create_EndBeforeStart_Throws()
    {
        var utc = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        Assert.ThrowsExactly<ArgumentException>(() => Period.Create(utc, utc.AddTicks(-1)));
        Assert.ThrowsExactly<ArgumentException>(() => Period.Create(utc, TimeSpan.FromTicks(-1)));
    }

    [TestMethod]
    public void Create_NonUtc_Throws()
    {
        var utc = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var local = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Local);
        var unspecified = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

        Assert.ThrowsExactly<ArgumentException>(() => Period.Create(local, utc.AddHours(1)));
        Assert.ThrowsExactly<ArgumentException>(() => Period.Create(utc, unspecified.AddHours(1)));
        Assert.ThrowsExactly<ArgumentException>(() => Period.Create(local, null));
        Assert.ThrowsExactly<ArgumentException>(() => Period.Create(null, unspecified));
        Assert.ThrowsExactly<ArgumentException>(() => Period.Create(local, TimeSpan.FromHours(1)));
        Assert.ThrowsExactly<ArgumentException>(() => Period.Create(DateTime.MinValue, DateTime.MaxValue));
    }

    [TestMethod]
    public void Create_Utc_IsAllowed()
    {
        var utc = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        Assert.AreEqual(utc, Period.Create(utc, null).Start);
        Assert.AreEqual(utc, Period.Create(null, utc).End);
        Assert.IsNull(Period.Create(null, null).Start);
        Assert.AreEqual(DateTimeHelper.MaxValueUtc, Period.Create(DateTimeHelper.MinValueUtc, DateTimeHelper.MaxValueUtc).End);
    }

    [TestMethod]
    public void Create_ZeroDuration_IsAllowed()
    {
        var utc = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        Assert.AreEqual(TimeSpan.Zero, Period.Create(utc, utc).Duration);
        Assert.AreEqual(TimeSpan.Zero, Period.Create(utc, TimeSpan.Zero).Duration);
    }
}
