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
    public void Create_ZeroDuration_IsAllowed()
    {
        var utc = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        Assert.AreEqual(TimeSpan.Zero, Period.Create(utc, utc).Duration);
        Assert.AreEqual(TimeSpan.Zero, Period.Create(utc, TimeSpan.Zero).Duration);
    }
}
