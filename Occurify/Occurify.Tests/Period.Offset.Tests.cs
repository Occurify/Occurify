﻿
using Occurify.Extensions;

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
            var start = DateTime.MaxValue - TimeSpan.FromHours(1);
            var period = Period.Create(start, DateTime.MaxValue);
            var amountToAdd = TimeSpan.FromTicks(1);

            // Act
            period = period.Offset(amountToAdd);

            // Assert
            Assert.AreEqual(start + amountToAdd, period.Start);
            Assert.IsNull(period.End);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException), "Start is not allowed to overflow DateTime.MaxValue.")]
        public void Offset_AddPositive_OverflowsStartAndEnd()
        {
            // Arrange
            var start = DateTime.MaxValue - TimeSpan.FromTicks(1);
            var period = Period.Create(start, DateTime.MaxValue);
            var amountToAdd = TimeSpan.FromTicks(2);

            // Act
            _ = period.Offset(amountToAdd);
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
            var end = DateTime.MinValue + TimeSpan.FromHours(1);
            var period = Period.Create(DateTime.MinValue, end);
            var amountToAdd = TimeSpan.FromTicks(1);

            // Act
            period = period.Offset(-amountToAdd);

            // Assert
            Assert.IsNull(period.Start);
            Assert.AreEqual(end - amountToAdd, period.End);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException), "End is not allowed to overflow DateTime.MinValue.")]
        public void Offset_AddNegative_OverflowsStartAndEnd()
        {
            // Arrange
            var end = DateTime.MinValue + TimeSpan.FromTicks(1);
            var period = Period.Create(DateTime.MinValue, end);
            var amountToAdd = TimeSpan.FromTicks(2);

            // Act
            _ = period.Offset(-amountToAdd);
        }
    }
}
