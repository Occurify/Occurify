using Occurify.Extensions;

namespace Occurify.TimeZones.Tests
{
    [TestClass]
    public class TimeZoneInstantsTests
    {
        private static readonly TimeZoneInfo DutchTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

        [TestMethod]
        public void DailyAt_InvalidTime_Substituted()
        {
            // Arrange
            // Period contains daylight savings in The Netherlands.
            var periodStart = DateTime.SpecifyKind(new DateTime(2024, 3, 30), DateTimeKind.Utc);
            var periodEnd = DateTime.SpecifyKind(new DateTime(2024, 4, 03), DateTimeKind.Utc);

            // Act
            var daily = TimeZoneInstants.DailyAt(2, 30, DutchTimeZone);
            var results = daily.EnumeratePeriod(periodStart.To(periodEnd)).ToArray();

            // Assert
            var expectedLocal = new []
            {
                new DateTime(2024, 3, 30, 2, 30, 0),
                new DateTime(2024, 3, 31, 3, 0, 0),
                new DateTime(2024, 4, 1, 2, 30, 0),
                new DateTime(2024, 4, 2, 2, 30, 0)
            };
            var expectedUtc = expectedLocal.Select(dt => TimeZoneInfo.ConvertTimeToUtc(dt, DutchTimeZone)).ToArray();
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
            var daily = TimeZoneInstants.StartOfDays([DayOfWeek.Monday, DayOfWeek.Friday, DayOfWeek.Tuesday], DutchTimeZone);
            var results = daily.EnumeratePeriod(periodStart.To(periodEnd)).ToArray();

            // Assert
            var expectedLocal = new[]
            {
                new DateTime(2024, 3, 29),
                new DateTime(2024, 4, 1),
                new DateTime(2024, 4, 2)
            };
            var expectedUtc = expectedLocal.Select(dt => TimeZoneInfo.ConvertTimeToUtc(dt, DutchTimeZone)).ToArray();
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

        [TestMethod]
        public void StartOfDay_ReturnsStartOfSameDay()
        {
            var expectedStart = TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 3, 31), DutchTimeZone);
            var expectedEnd = TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 4, 1), DutchTimeZone);

            Assert.AreEqual(expectedStart, TimeZoneInstants.StartOfDay(new DateTime(2024, 3, 31), DutchTimeZone));
            Assert.AreEqual(expectedStart, TimeZoneInstants.StartOfDay(new DateTime(2024, 3, 31, 18, 0, 0), DutchTimeZone));
            Assert.AreEqual(expectedStart, TimeZoneInstants.StartOfDay(31, 3, 2024, DutchTimeZone));
            Assert.AreEqual(expectedEnd, TimeZoneInstants.EndOfDay(new DateTime(2024, 3, 31), DutchTimeZone));
            Assert.AreEqual(expectedEnd, TimeZoneInstants.EndOfDay(31, 3, 2024, DutchTimeZone));
        }

        [TestMethod]
        public void StartOfWeekMonthYear_OnBoundaryDay_ReturnsStartOfThatPeriod()
        {
            var monday = new DateTime(2024, 4, 1);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(monday, DutchTimeZone), TimeZoneInstants.StartOfWeek(monday, DutchTimeZone));
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 4, 8), DutchTimeZone), TimeZoneInstants.EndOfWeek(monday, DutchTimeZone));

            var firstOfMonth = new DateTime(2024, 4, 1);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(firstOfMonth, DutchTimeZone), TimeZoneInstants.StartOfMonth(firstOfMonth, DutchTimeZone));
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 5, 1), DutchTimeZone), TimeZoneInstants.EndOfMonth(firstOfMonth, DutchTimeZone));

            var firstOfYear = new DateTime(2024, 1, 1);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(firstOfYear, DutchTimeZone), TimeZoneInstants.StartOfYear(firstOfYear, DutchTimeZone));
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2025, 1, 1), DutchTimeZone), TimeZoneInstants.EndOfYear(firstOfYear, DutchTimeZone));
        }

        [TestMethod]
        public void IntOverloads_UseProvidedTimeZone()
        {
            Assert.AreEqual(new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInstants.StartOfYear(2025, TimeZoneInfo.Utc));
            Assert.AreEqual(new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInstants.EndOfYear(2025, TimeZoneInfo.Utc));
            Assert.AreEqual(new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInstants.StartOfMonth(2, 2025, TimeZoneInfo.Utc));
            Assert.AreEqual(new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInstants.EndOfMonth(2, 2025, TimeZoneInfo.Utc));
            Assert.AreEqual(new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInstants.StartOfDay(1, 2, 2025, TimeZoneInfo.Utc));
            Assert.AreEqual(new DateTime(2025, 2, 2, 0, 0, 0, DateTimeKind.Utc), TimeZoneInstants.EndOfDay(1, 2, 2025, TimeZoneInfo.Utc));

            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2025, 1, 1), DutchTimeZone), TimeZoneInstants.StartOfYear(2025, DutchTimeZone));
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2025, 2, 1), DutchTimeZone), TimeZoneInstants.StartOfMonth(2, 2025, DutchTimeZone));
        }

        [TestMethod]
        public void EndOfMonths_NovemberAndDecember()
        {
            var endOfNovember = TimeZoneInstants.EndOfMonths(new[] { 11 }, TimeZoneInfo.Utc);
            Assert.AreEqual(new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc), endOfNovember.GetNextUtcInstant(new DateTime(2024, 11, 15, 0, 0, 0, DateTimeKind.Utc)));

            var endOfDecember = TimeZoneInstants.EndOfMonths(new[] { 12 }, TimeZoneInfo.Utc);
            Assert.AreEqual(new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), endOfDecember.GetNextUtcInstant(new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)));
        }

        [TestMethod]
        public void DailyAt_WithMilliseconds_UsesProvidedTimeZone()
        {
            var relativeTo = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);

            var utc = TimeZoneInstants.DailyAt(new TimeOnly(9, 0, 0, 500), TimeZoneInfo.Utc);
            Assert.AreEqual(new DateTime(2024, 6, 1, 9, 0, 0, 500, DateTimeKind.Utc), utc.GetNextUtcInstant(relativeTo));

            var dutch = TimeZoneInstants.DailyAt(new TimeOnly(9, 0, 0, 500), DutchTimeZone);
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(new DateTime(2024, 6, 1, 9, 0, 0, 500), DutchTimeZone), dutch.GetNextUtcInstant(relativeTo));
        }

        [TestMethod]
        public void StartOfDaysAndMonths_Empty_Throws()
        {
            Assert.ThrowsExactly<ArgumentException>(() => TimeZoneInstants.StartOfDays(Array.Empty<DayOfWeek>(), TimeZoneInfo.Utc));
            Assert.ThrowsExactly<ArgumentException>(() => TimeZoneInstants.StartOfMonths(Array.Empty<int>(), TimeZoneInfo.Utc));
        }
    }
}
