
namespace Occurify.Tests
{
    [TestClass]
    public class PeriodDurationTests
    {
        [TestMethod]
        public void Duration_CreateUsingDuration()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var duration = TimeSpan.FromHours(1);

            // Act
            var period = Period.Create(utcNow, duration);

            // Assert
            Assert.AreEqual(duration, period.Duration);
        }

        [TestMethod]
        public void Duration_CreateUsingStartAndEnd()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var duration = TimeSpan.FromHours(1);

            // Act
            var period = Period.Create(utcNow, utcNow + duration);

            // Assert
            Assert.AreEqual(duration, period.Duration);
        }

        [TestMethod]
        public void Duration_NoStart_Infinite()
        {
            // Arrange/Act
            var period = Period.Create(null, DateTime.UtcNow);

            // Assert
            Assert.IsNull(period.Duration);
        }

        [TestMethod]
        public void Duration_NoEnd_Infinite()
        {
            // Arrange/Act
            var period = Period.Create(DateTime.UtcNow, null);

            // Assert
            Assert.IsNull(period.Duration);
        }

        [TestMethod]
        public void Duration_NoStartAndEnd_Infinite()
        {
            // Arrange/Act
            var period = Period.Create(null, null);

            // Assert
            Assert.IsNull(period.Duration);
        }
    }
}
