using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.Tests
{
    [TestClass]
    public class PeriodOffsetTests
    {
        [TestMethod]
        public void Offset_AddPositive()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var period = Period.Create(utcNow, TimeSpan.FromHours(1));
            var amountToAdd = TimeSpan.FromHours(2);

            // Act
            period = period.Offset(amountToAdd);

            // Assert
            Assert.AreEqual(utcNow + amountToAdd, period.Start);
            Assert.AreEqual(utcNow + TimeSpan.FromHours(1) + amountToAdd, period.End);
        }

        [TestMethod]
        public void Offset_AddPositive_OverflowsEnd()
        {
            // Arrange
            var start = DateTimeHelper.MaxValueUtc - TimeSpan.FromHours(1);
            var period = Period.Create(start, DateTimeHelper.MaxValueUtc);
            var amountToAdd = TimeSpan.FromTicks(1);

            // Act
            period = period.Offset(amountToAdd);

            // Assert
            Assert.AreEqual(start + amountToAdd, period.Start);
            Assert.IsNull(period.End);
        }

        [TestMethod]
        public void Offset_AddPositive_OverflowsStartAndEnd_ThrowsOverflowException()
        {
            // Arrange
            var start = DateTimeHelper.MaxValueUtc - TimeSpan.FromTicks(1);
            var period = Period.Create(start, DateTimeHelper.MaxValueUtc);
            var amountToAdd = TimeSpan.FromTicks(2);

            // Act & Assert
            Assert.Throws<OverflowException>(() =>
                period.Offset(amountToAdd), "Start is not allowed to overflow DateTime.MaxValue.");
        }

        [TestMethod]
        public void Offset_AddNegative()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var period = Period.Create(utcNow, TimeSpan.FromHours(1));
            var amountToAdd = TimeSpan.FromHours(2);

            // Act
            period = period.Offset(-amountToAdd);

            // Assert
            Assert.AreEqual(utcNow - amountToAdd, period.Start);
            Assert.AreEqual(utcNow + TimeSpan.FromHours(1) - amountToAdd, period.End);
        }

        [TestMethod]
        public void Offset_AddNegative_OverflowsStart()
        {
            // Arrange
            var end = DateTimeHelper.MinValueUtc + TimeSpan.FromHours(1);
            var period = Period.Create(DateTimeHelper.MinValueUtc, end);
            var amountToAdd = TimeSpan.FromTicks(1);

            // Act
            period = period.Offset(-amountToAdd);

            // Assert
            Assert.IsNull(period.Start);
            Assert.AreEqual(end - amountToAdd, period.End);
        }

        [TestMethod]
        public void Offset_AddNegative_OverflowsStartAndEnd_ThrowsOverflowException()
        {
            // Arrange
            var end = DateTimeHelper.MinValueUtc + TimeSpan.FromTicks(1);
            var period = Period.Create(DateTimeHelper.MinValueUtc, end);
            var amountToAdd = TimeSpan.FromTicks(2);

            // Act & Assert
            Assert.Throws<OverflowException>(() =>
                period.Offset(-amountToAdd), "End is not allowed to overflow DateTime.MinValue.");
        }
    }
}
