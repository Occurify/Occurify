
using Occurify.Extensions;

namespace Occurify.TimeZones.Tests
{
    [TestClass]
    public class TimeZonePeriodsTests
    {
        [TestMethod]
        public void Day_WithDaylightSavings()
        {
            // Arrange
            var netherlandsTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

            // Act
            var day = TimeZonePeriods.Day(new DateTime(2024, 3, 31), netherlandsTimeZone); // Day of daylight savings in The Netherlands.

            // Assert
            var expectedLocalStart = new DateTime(2024, 3, 31);
            var expectedLocalEnd = new DateTime(2024, 4, 1);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(expectedLocalStart, netherlandsTimeZone), day.Start);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(expectedLocalEnd, netherlandsTimeZone), day.End);
        }

        [TestMethod]
        public void HoursContainingCron()
        {
            // Arrange
            var netherlandsTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            var hours = TimeZonePeriods.Hours("5 4 * * *", netherlandsTimeZone);

            // Act
            var hour = hours.GetNextCompletePeriod(new DateTime(2024, 3, 11).AsUtcInstant());

            // Assert
            Assert.AreEqual(Period.Create(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 3, 11, 4, 0, 0)), TimeSpan.FromHours(1)), hour);
        }

        [TestMethod]
        public void HoursContainingCron_MinutesIgnored()
        {
            // Arrange
            var netherlandsTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            var hoursWithoutMinutes = TimeZonePeriods.Hours("5 4 * * *", netherlandsTimeZone);
            var hoursWithMinutes = TimeZonePeriods.Hours("* 4 * * *", netherlandsTimeZone);
            var somePeriod = Period.Create(new DateTime(2024, 3, 11).AsUtcInstant(), TimeSpan.FromDays(5));

            // Act
            var collection1 = hoursWithoutMinutes.EnumeratePeriod(somePeriod).ToArray();
            var collection2 = hoursWithMinutes.EnumeratePeriod(somePeriod).ToArray();

            // Assert
            CollectionAssert.AreEqual(collection1, collection2);
        }
    }
}
