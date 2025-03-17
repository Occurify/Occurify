
using Occurify.Extensions;

namespace Occurify.TimeZones.Tests
{
    [TestClass]
    public class TimeZonePeriodsTests
    {
        private static readonly TimeZoneInfo DutchTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

        [TestMethod]
        public void Day_WithDaylightSavings()
        {
            // Act
            var day = TimeZonePeriods.Day(new DateTime(2024, 3, 31), DutchTimeZone); // Day of daylight savings in The Netherlands.

            // Assert
            var expectedLocalStart = new DateTime(2024, 3, 31);
            var expectedLocalEnd = new DateTime(2024, 4, 1);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(expectedLocalStart, DutchTimeZone), day.Start);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(expectedLocalEnd, DutchTimeZone), day.End);
        }

        [TestMethod]
        public void HoursContainingCron()
        {
            // Arrange
            var hours = TimeZonePeriods.Hours("5 4 * * *", DutchTimeZone);

            // Act
            var hour = hours.GetNextCompletePeriod(new DateTime(2024, 3, 11).AsUtcInstant());

            // Assert
            Assert.AreEqual(Period.Create(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 3, 11, 4, 0, 0), DutchTimeZone), TimeSpan.FromHours(1)), hour);
        }

        [TestMethod]
        public void HoursContainingCron_MinutesIgnored()
        {
            // Arrange
            var hoursWithoutMinutes = TimeZonePeriods.Hours("5 4 * * *", DutchTimeZone);
            var hoursWithMinutes = TimeZonePeriods.Hours("* 4 * * *", DutchTimeZone);
            var somePeriod = Period.Create(new DateTime(2024, 3, 11).AsUtcInstant(), TimeSpan.FromDays(5));

            // Act
            var collection1 = hoursWithoutMinutes.EnumeratePeriod(somePeriod).ToArray();
            var collection2 = hoursWithMinutes.EnumeratePeriod(somePeriod).ToArray();

            // Assert
            CollectionAssert.AreEqual(collection1, collection2);
        }
    }
}
