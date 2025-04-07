using Occurify.Tests.Helpers;

namespace Occurify.Tests
{
    [TestClass]
    public class PeriodicTimelineTests
    {
        private const long MaxUtc = 3155378975999999999L;
        private const long NoonOfLastDay = 3155378544000000000L;
        private const long NoonOfFirstDay = 432000000000L;

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(MaxUtc)]
        public void SaturatedTimeline_GetPreviousUtcInstant(long origin)
        {
            // Arrange
            var now = DateTime.UtcNow;
            var timeline = Timeline.Periodic(new (origin, DateTimeKind.Utc), TimeSpan.FromTicks(1));

            // Act
            var result = timeline.GetPreviousUtcInstant(now);

            // Assert
            Assert.AreEqual(now - TimeSpan.FromTicks(1), result);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(MaxUtc)]
        public void SaturatedTimeline_GetPreviousUtcInstant_MinValue_Null(long origin)
        {
            // Arrange
            var timeline = Timeline.Periodic(new(origin, DateTimeKind.Utc), TimeSpan.FromTicks(1));

            // Act
            var result = timeline.GetPreviousUtcInstant(DateTimeHelper.MinValueUtc);

            // Assert
            Assert.IsNull(result);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(MaxUtc)]
        public void SaturatedTimeline_GetNextUtcInstant(long origin)
        {
            // Arrange
            var now = DateTime.UtcNow;
            var timeline = Timeline.Periodic(new(origin, DateTimeKind.Utc), TimeSpan.FromTicks(1));

            // Act
            var result = timeline.GetNextUtcInstant(now);

            // Assert
            Assert.AreEqual(now + TimeSpan.FromTicks(1), result);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(MaxUtc)]
        public void SaturatedTimeline_GetNextUtcInstant_MaxValue_Null(long origin)
        {
            // Arrange
            var timeline = Timeline.Periodic(new(origin, DateTimeKind.Utc), TimeSpan.FromTicks(1));

            // Act
            var result = timeline.GetNextUtcInstant(DateTimeHelper.MaxValueUtc);

            // Assert
            Assert.IsNull(result);
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(0, MaxUtc)]
        [DataRow(MaxUtc, 0)]
        [DataRow(MaxUtc, MaxUtc)]
        public void SaturatedTimeline_IsInstant(long origin, long utcTime)
        {
            // Arrange
            var timeline = Timeline.Periodic(new(origin, DateTimeKind.Utc), TimeSpan.FromTicks(1));

            // Act
            var result = timeline.IsInstant(new(utcTime, DateTimeKind.Utc));

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DayTimeline_GetPreviousUtcInstant_OriginZero()
        {
            // Arrange
            var startOfToday = DateTime.UtcNow.Date;
            var noon = startOfToday.Date.AddHours(12);
            var timeline = Timeline.Periodic(DateTimeHelper.MinValueUtc, TimeSpan.FromDays(1));

            // Act
            var result = timeline.GetPreviousUtcInstant(noon);

            // Assert
            Assert.AreEqual(startOfToday, result);
        }

        [TestMethod]
        public void DayTimeline_GetPreviousUtcInstant_OriginMaxValue()
        {
            // Arrange
            var startOfToday = DateTime.UtcNow.Date;
            var noon = startOfToday.Date.AddHours(12);
            var timeline = Timeline.Periodic(DateTimeHelper.MaxValueUtc, TimeSpan.FromDays(1));

            // Act
            var result = timeline.GetPreviousUtcInstant(noon);

            // Assert
            Assert.AreEqual(startOfToday - TimeSpan.FromTicks(1), result);
        }

        [DataTestMethod]
        [DataRow(NoonOfFirstDay)]
        [DataRow(NoonOfLastDay)]
        public void DayTimeline_GetPreviousUtcInstant_OriginNoon(long origin)
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var startOfTomorrow = utcNow.Date.AddDays(1);
            var noon = utcNow.Date.AddHours(12);
            var timeline = Timeline.Periodic(new DateTime(origin, DateTimeKind.Utc), TimeSpan.FromDays(1));

            // Act
            var result = timeline.GetPreviousUtcInstant(startOfTomorrow);

            // Assert
            Assert.AreEqual(noon, result);
        }

        [TestMethod]
        public void DayTimeline_GetPreviousUtcInstant_FromInstantOnTimeline()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var noon = utcNow.Date.AddHours(12);
            var timeline = Timeline.Periodic(new DateTime(NoonOfFirstDay, DateTimeKind.Utc), TimeSpan.FromDays(1));

            // Act
            var result = timeline.GetPreviousUtcInstant(noon);

            // Assert
            Assert.AreEqual(noon - TimeSpan.FromDays(1), result);
        }

        [TestMethod]
        public void GetPreviousUtcInstant_LessThanOnePeriodFromOrigin()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var timeline = Timeline.Periodic(utcNow, TimeSpan.FromSeconds(2));

            // Act
            var result = timeline.GetPreviousUtcInstant(utcNow - TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(utcNow - TimeSpan.FromSeconds(2), result);
        }

        [TestMethod]
        public void GetPreviousUtcInstant_LessThanNegativeOnePeriodFromOrigin()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var timeline = Timeline.Periodic(utcNow, TimeSpan.FromSeconds(2));

            // Act
            var result = timeline.GetPreviousUtcInstant(utcNow + TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(utcNow, result);
        }

        [TestMethod]
        public void DayTimeline_GetNextUtcInstant_OriginZero()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var startOfTomorrow = utcNow.Date.AddDays(1);
            var noon = utcNow.Date.AddHours(12);
            var timeline = Timeline.Periodic(DateTimeHelper.MinValueUtc, TimeSpan.FromDays(1));

            // Act
            var result = timeline.GetNextUtcInstant(noon);

            // Assert
            Assert.AreEqual(startOfTomorrow, result);
        }

        [TestMethod]
        public void DayTimeline_GetNextUtcInstant_OriginMaxValue()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var startOfTomorrow = utcNow.Date.AddDays(1);
            var noon = utcNow.Date.AddHours(12);
            var timeline = Timeline.Periodic(DateTimeHelper.MaxValueUtc, TimeSpan.FromDays(1));

            // Act
            var result = timeline.GetNextUtcInstant(noon);

            // Assert
            Assert.AreEqual(startOfTomorrow - TimeSpan.FromTicks(1), result);
        }

        [DataTestMethod]
        [DataRow(NoonOfFirstDay)]
        [DataRow(NoonOfLastDay)]
        public void DayTimeline_GetNextUtcInstant_OriginNoon(long origin)
        {
            // Arrange
            var startOfToday = DateTime.UtcNow.Date;
            var noon = startOfToday.AddHours(12);
            var timeline = Timeline.Periodic(new DateTime(origin, DateTimeKind.Utc), TimeSpan.FromDays(1));

            // Act
            var result = timeline.GetNextUtcInstant(startOfToday);

            // Assert
            Assert.AreEqual(noon, result);
        }

        [TestMethod]
        public void DayTimeline_GetNextUtcInstant_FromInstantOnTimeline()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var noon = utcNow.Date.AddHours(12);
            var timeline = Timeline.Periodic(new DateTime(NoonOfFirstDay, DateTimeKind.Utc), TimeSpan.FromDays(1));

            // Act
            var result = timeline.GetNextUtcInstant(noon);

            // Assert
            Assert.AreEqual(noon + TimeSpan.FromDays(1), result);
        }

        [TestMethod]
        public void GetNextUtcInstant_LessThanOnePeriodFromOrigin()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var timeline = Timeline.Periodic(utcNow, TimeSpan.FromSeconds(2));

            // Act
            var result = timeline.GetNextUtcInstant(utcNow + TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(utcNow + TimeSpan.FromSeconds(2), result);
        }

        [TestMethod]
        public void GetNextUtcInstant_LessThanNegativeOnePeriodFromOrigin()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var timeline = Timeline.Periodic(utcNow, TimeSpan.FromSeconds(2));

            // Act
            var result = timeline.GetNextUtcInstant(utcNow - TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(utcNow, result);
        }

        [TestMethod]
        public void DayTimeline_IsInstant_OriginZero()
        {
            // Arrange
            var startOfToday = DateTime.UtcNow.Date;
            var timeline = Timeline.Periodic(DateTimeHelper.MinValueUtc, TimeSpan.FromDays(1));

            // Act
            var result = timeline.IsInstant(startOfToday);

            // Assert
            Assert.IsTrue(result);
        }

        
        [DataTestMethod]
        [DataRow(NoonOfFirstDay)]
        [DataRow(NoonOfLastDay)]
        public void DayTimeline_IsInstant_OriginNoon(long origin)
        {
            // Arrange
            var noon = DateTime.UtcNow.Date.AddHours(12);
            var timeline = Timeline.Periodic(new DateTime(origin, DateTimeKind.Utc), TimeSpan.FromDays(1));

            // Act
            var result = timeline.IsInstant(noon);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
