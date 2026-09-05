
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

        [TestMethod]
        public void Workdays_Containing_ExcludesWeekends()
        {
            var noon = TimeZoneInstants.FromCron("0 12 * * *", TimeZoneInfo.Utc);
            var mondayToMonday = Period.Create(new DateTime(2024, 6, 3, 0, 0, 0, DateTimeKind.Utc), TimeSpan.FromDays(7));

            var workdays = TimeZonePeriods.Workdays(noon, TimeZoneInfo.Utc).EnumeratePeriod(mondayToMonday).ToArray();

            Assert.HasCount(5, workdays);
            Assert.IsTrue(workdays.All(p => p.Start!.Value.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday)));
        }

        [TestMethod]
        public void IntOverloads_UseProvidedTimeZone()
        {
            Assert.AreEqual(
                Period.Create(new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc)),
                TimeZonePeriods.Month(2, 2025, TimeZoneInfo.Utc));
            Assert.AreEqual(
                Period.Create(new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)),
                TimeZonePeriods.Year(2025, TimeZoneInfo.Utc));
            Assert.AreEqual(
                Period.Create(new DateTime(2024, 3, 31, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc)),
                TimeZonePeriods.Day(31, 3, 2024, TimeZoneInfo.Utc));
            Assert.AreEqual(
                Period.Create(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 3, 31), DutchTimeZone), TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 4, 1), DutchTimeZone)),
                TimeZonePeriods.Day(31, 3, 2024, DutchTimeZone));

            var february = TimeZonePeriods.Months(2, TimeZoneInfo.Utc).GetNextCompletePeriod(new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            Assert.AreEqual(
                Period.Create(new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc)),
                february);
        }

        [TestMethod]
        public void ByUtc_NearLocalBoundary_UsesFullInstant()
        {
            // Dutch time is UTC+2 in summer, so these UTC instants already fall in the next local week/month/year.
            var week = TimeZonePeriods.WeekByUtc(new DateTime(2024, 6, 9, 22, 30, 0, DateTimeKind.Utc), DutchTimeZone);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 6, 10), DutchTimeZone), week.Start);

            var month = TimeZonePeriods.MonthByUtc(new DateTime(2024, 6, 30, 22, 30, 0, DateTimeKind.Utc), DutchTimeZone);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 7, 1), DutchTimeZone), month.Start);

            var year = TimeZonePeriods.YearByUtc(new DateTime(2024, 12, 31, 23, 30, 0, DateTimeKind.Utc), DutchTimeZone);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2025, 1, 1), DutchTimeZone), year.Start);
        }

        [TestMethod]
        public void ByUtc_LocalTimeZoneOverloads_MatchExplicitLocalTimeZone()
        {
            var utc = new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Utc);

            Assert.AreEqual(TimeZonePeriods.WeekByUtc(utc, TimeZoneInfo.Local), TimeZonePeriods.WeekByUtc(utc));
            Assert.AreEqual(TimeZonePeriods.MonthByUtc(utc, TimeZoneInfo.Local), TimeZonePeriods.MonthByUtc(utc));
            Assert.AreEqual(TimeZonePeriods.YearByUtc(utc, TimeZoneInfo.Local), TimeZonePeriods.YearByUtc(utc));
        }

        [TestMethod]
        public void Day_IgnoresDateTimeKind()
        {
            var expected = Period.Create(new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 16, 0, 0, 0, DateTimeKind.Utc));

            Assert.AreEqual(expected, TimeZonePeriods.Day(new DateTime(2024, 6, 15, 8, 0, 0, DateTimeKind.Local), TimeZoneInfo.Utc));
            Assert.AreEqual(expected, TimeZonePeriods.Day(new DateTime(2024, 6, 15, 8, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Utc));
            Assert.AreEqual(expected, TimeZonePeriods.Day(new DateTime(2024, 6, 15, 8, 0, 0, DateTimeKind.Unspecified), TimeZoneInfo.Utc));
        }

        [TestMethod]
        public void Day_MidnightInDaylightSavingsGap()
        {
            TimeZoneInfo santiago;
            try
            {
                santiago = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                Assert.Inconclusive("Time zone 'Pacific SA Standard Time' is not available on this machine.");
                return;
            }

            var localMidnight = new DateTime(2024, 9, 8);
            if (!santiago.IsInvalidTime(localMidnight))
            {
                Assert.Inconclusive("Time zone data on this machine does not place the 2024 daylight saving transition of Santiago at midnight.");
            }

            var day = TimeZonePeriods.Day(localMidnight, santiago);

            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 9, 8, 1, 0, 0), santiago), day.Start);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 9, 9), santiago), day.End);
        }
    }
}
