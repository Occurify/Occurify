using Occurify.Extensions;
using Occurify.TimeZones.Extensions;

namespace Occurify.TimeZones.Tests
{
    [TestClass]
    public class CronTimelineTests
    {
        private static readonly TimeZoneInfo NetherlandsTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

        [TestMethod]
        public void GetPreviousUtcInstant()
        {
            // Arrange
            var timeOnly = new TimeOnly(13, 37, 42);
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), TimeZoneInfo.Utc);
            var now = DateTime.UtcNow;
            var dateTime = now.Date + timeOnly.ToTimeSpan() + TimeSpan.FromHours(2);

            // Act
            var previousInstant = timeline.GetPreviousUtcInstant(dateTime);

            // Assert
            Assert.AreEqual(now.Date + timeOnly.ToTimeSpan(), previousInstant);
        }

        [TestMethod]
        public void GetPreviousUtcInstant_NoMoreResults()
        {
            // Arrange
            var timeOnly = new TimeOnly(13, 37, 42);
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), TimeZoneInfo.Utc);
            var dateTime = (DateTime.MinValue + timeOnly.ToTimeSpan()).AsUtcInstant();

            // Act
            var previousInstant = timeline.GetPreviousUtcInstant(dateTime);

            // Assert
            Assert.IsNull(previousInstant);
        }

        [TestMethod]
        public void GetNextUtcInstant()
        {
            // Arrange
            var timeOnly = new TimeOnly(13, 37, 42);
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), TimeZoneInfo.Utc);
            var now = DateTime.UtcNow;
            var dateTime = now.Date + timeOnly.ToTimeSpan() + TimeSpan.FromHours(2);

            // Act
            var nextInstant = timeline.GetNextUtcInstant(dateTime);

            // Assert
            Assert.AreEqual(now.Date.AddDays(1) + timeOnly.ToTimeSpan(), nextInstant);
        }

        [TestMethod]
        public void GetNextUtcInstant_NoMoreResults()
        {
            // Arrange
            var timeOnly = new TimeOnly(13, 37, 42);
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), TimeZoneInfo.Utc);
            var maxCronosTime = new DateTime(2500, 1, 1);
            var dateTime = (maxCronosTime - TimeSpan.FromDays(1) + timeOnly.ToTimeSpan()).AsUtcInstant();

            // Act
            var nextInstant = timeline.GetNextUtcInstant(dateTime);

            // Assert
            Assert.IsNull(nextInstant);
        }

        [TestMethod]
        public void IsInstant()
        {
            // Arrange
            var timeOnly = new TimeOnly(13, 37, 42);
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), TimeZoneInfo.Utc);
            var now = DateTime.UtcNow;

            // Act
            var isInstant = timeline.IsInstant(now.Date.AddDays(1) + timeOnly.ToTimeSpan());

            // Assert
            Assert.IsTrue(isInstant);
        }

        [TestMethod]
        public void GetNextUtcInstant_InvalidTime()
        {
            // Arrange
            var timeOnly = new TimeOnly(2, 30, 0); // Daylight savings in The Netherlands starts at 2am and turns the clock to 3am. 2:30 should be skipped.
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), NetherlandsTimeZone);

            // 5 minutes before daylight savings
            var localJustBeforeDaylightSavings = new DateTime(2024, 3, 31, 1, 55, 0);
            var utcJustBeforeDaylightSavings = TimeZoneInfo.ConvertTimeToUtc(localJustBeforeDaylightSavings, NetherlandsTimeZone);

            // Act
            var nextInstant = timeline.GetNextUtcInstant(utcJustBeforeDaylightSavings);

            // Assert
            var expectedLocalTime = new DateTime(2024, 3, 31, 3, 0, 0); // The corrected time.
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(expectedLocalTime, NetherlandsTimeZone), nextInstant);
        }

        [TestMethod]
        public void GetPreviousUtcInstant_InvalidTime_Substituted()
        {
            // Arrange
            var timeOnly = new TimeOnly(2, 30, 0); // Daylight savings in The Netherlands starts at 2am and turns the clock to 3am. 2:30 should be skipped.
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), NetherlandsTimeZone);

            // 35 minutes after daylight savings
            var localJustBeforeDaylightSavings = new DateTime(2024, 3, 31, 3, 35, 0);
            var utcJustBeforeDaylightSavings = TimeZoneInfo.ConvertTimeToUtc(localJustBeforeDaylightSavings, NetherlandsTimeZone);

            // Act
            var nextInstant = timeline.GetPreviousUtcInstant(utcJustBeforeDaylightSavings);

            // Assert
            var expectedLocalTime = new DateTime(2024, 3, 31, 3, 0, 0); // The corrected time.
            Assert.AreEqual(TimeZoneInfo.ConvertTimeToUtc(expectedLocalTime, NetherlandsTimeZone), nextInstant);
        }

        // Note: as we are using a third party library, we perform some simple sanity checks.
        [TestMethod]
        public void CronTimeline_RemainsConsistent_ForwardsBackwards()
        {
            // Arrange
            const int amountToCheck = 1000;
            var date = new DateTime(2025, 3, 10, 16, 21, 0).AsUtcInstant();
            var timeOnly = new TimeOnly(13, 37, 42);
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), TimeZoneInfo.Utc);
            var collected = new List<DateTime>();

            // Act and Assert
            for (var i = 0; i < amountToCheck; i++)
            {
                var next = timeline.GetNextUtcInstant(date);
                if (next == null)
                {
                    Assert.Fail("Dates are expected in current range.");
                }

                date = next.Value;
                collected.Add(date);
            }

            date += TimeSpan.FromTicks(1);

            for (var i = amountToCheck - 1; i >= 0; i--)
            {
                var previous = timeline.GetPreviousUtcInstant(date);

                if (previous == null)
                {
                    Assert.Fail("Dates are expected in current range.");
                }

                Assert.IsTrue(timeline.IsInstant(previous.Value));

                Assert.AreEqual(collected[i], previous.Value);
                date = previous.Value;
            }
        }

        [TestMethod]
        public void CronTimeline_RemainsConsistent_Forwards()
        {
            // Arrange
            const int amountToCheck = 1000;
            var date = new DateTime(2025, 3, 10, 16, 21, 0).AsUtcInstant();
            var timeOnly = new TimeOnly(13, 37, 42);
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), TimeZoneInfo.Utc);

            // Act
            // Get initial result
            var firstResult = timeline.GetNextUtcInstant(date);
            Assert.IsNotNull(firstResult);

            // Check if all values before give the same result.
            date = firstResult.Value - TimeSpan.FromTicks(amountToCheck);

            // Assert
            for (var i = 0; i < amountToCheck; i++)
            {
                var next = timeline.GetNextUtcInstant(date);
                Assert.AreEqual(next, firstResult);

                Assert.IsFalse(timeline.IsInstant(date));

                date += TimeSpan.FromTicks(1);
            }
        }

        [TestMethod]
        public void CronTimeline_RemainsConsistent_BackwardsForwards()
        {
            // Arrange
            const int amountToCheck = 1000;
            var date = new DateTime(2025, 3, 10, 16, 21, 0).AsUtcInstant();
            var timeOnly = new TimeOnly(13, 37, 42);
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), TimeZoneInfo.Utc);
            var collected = new List<DateTime>();

            // Act and Assert
            for (var i = 0; i < amountToCheck; i++)
            {
                var next = timeline.GetPreviousUtcInstant(date);
                if (next == null)
                {
                    Assert.Fail("Dates are expected in current range.");
                }

                date = next.Value;
                collected.Add(date);
            }

            date -= TimeSpan.FromTicks(1);

            for (var i = amountToCheck - 1; i >= 0; i--)
            {
                var previous = timeline.GetNextUtcInstant(date);

                if (previous == null)
                {
                    Assert.Fail("Dates are expected in current range.");
                }

                Assert.IsTrue(timeline.IsInstant(previous.Value));

                Assert.AreEqual(collected[i], previous.Value);
                date = previous.Value;
            }
        }

        [TestMethod]
        public void CronTimeline_RemainsConsistent_Backwards()
        {
            // Arrange
            const int amountToCheck = 1000;
            var date = new DateTime(2025, 3, 10, 16, 21, 0).AsUtcInstant();
            var timeOnly = new TimeOnly(13, 37, 42);
            var timeline = TimeZoneInstants.FromCron(timeOnly.ToCronExpression(), TimeZoneInfo.Utc);

            // Act
            // Get initial result
            var firstResult = timeline.GetPreviousUtcInstant(date);
            Assert.IsNotNull(firstResult);

            // Check if all values before give the same result.
            date = firstResult.Value + TimeSpan.FromTicks(amountToCheck);

            // Assert
            for (var i = 0; i < amountToCheck; i++)
            {
                var next = timeline.GetPreviousUtcInstant(date);
                Assert.AreEqual(next, firstResult);

                Assert.IsFalse(timeline.IsInstant(date));

                date -= TimeSpan.FromTicks(1);
            }
        }
    }
}
