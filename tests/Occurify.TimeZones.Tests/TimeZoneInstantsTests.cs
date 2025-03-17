using Occurify.Extensions;

namespace Occurify.TimeZones.Tests
{
    [TestClass]
    public class TimeZoneInstantsTests
    {
        private static readonly TimeZoneInfo NetherlandsTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

        [TestMethod]
        public void DailyAt_InvalidTime_Substituted()
        {
            // Arrange
            // Period contains daylight savings in The Netherlands.
            var periodStart = DateTime.SpecifyKind(new DateTime(2024, 3, 30), DateTimeKind.Utc);
            var periodEnd = DateTime.SpecifyKind(new DateTime(2024, 4, 03), DateTimeKind.Utc);

            // Act
            var daily = TimeZoneInstants.DailyAt(2, 30, NetherlandsTimeZone);
            var results = daily.EnumeratePeriod(periodStart.To(periodEnd)).ToArray();

            // Assert
            var expectedLocal = new []
            {
                new DateTime(2024, 3, 30, 2, 30, 0),
                new DateTime(2024, 3, 31, 3, 0, 0),
                new DateTime(2024, 4, 1, 2, 30, 0),
                new DateTime(2024, 4, 2, 2, 30, 0)
            };
            var expectedUtc = expectedLocal.Select(dt => TimeZoneInfo.ConvertTimeToUtc(dt, NetherlandsTimeZone)).ToArray();
            CollectionAssert.AreEqual(expectedUtc, results);
        }

        [TestMethod]
        public void DailyAt_StartOfDays()
        {
            // Arrange
            // Period contains daylight savings in The Netherlands.
            var periodStart = DateTime.SpecifyKind(new DateTime(2024, 3, 28), DateTimeKind.Utc);
            var periodEnd = DateTime.SpecifyKind(new DateTime(2024, 4, 03), DateTimeKind.Utc);

            // Act
            var daily = TimeZoneInstants.StartOfDays([DayOfWeek.Monday, DayOfWeek.Friday, DayOfWeek.Tuesday], NetherlandsTimeZone);
            var results = daily.EnumeratePeriod(periodStart.To(periodEnd)).ToArray();

            // Assert
            var expectedLocal = new[]
            {
                new DateTime(2024, 3, 29),
                new DateTime(2024, 4, 1),
                new DateTime(2024, 4, 2)
            };
            var expectedUtc = expectedLocal.Select(dt => TimeZoneInfo.ConvertTimeToUtc(dt, NetherlandsTimeZone)).ToArray();
            CollectionAssert.AreEqual(expectedUtc, results);
        }

        [TestMethod]
        public void Day()
        {
            // Arrange
            var eindhovenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

            // Act
            var day = TimeZonePeriods.Day(new DateTime(2024, 3, 31), eindhovenTimeZone); // Day of daylight savings in The Netherlands.

            // Assert
            var expectedLocalStart = new DateTime(2024, 3, 31);
            var expectedLocalEnd = new DateTime(2024, 4, 1);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(expectedLocalStart, eindhovenTimeZone), day.Start);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(expectedLocalEnd, eindhovenTimeZone), day.End);
        }
    }
}
