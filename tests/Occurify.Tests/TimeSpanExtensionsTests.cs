using Occurify.Extensions;

namespace Occurify.Tests
{
    [TestClass]
    public class TimeSpanExtensionsTests
    {
        [TestMethod]
        public void AddOrNullOnOverflow_AddPositive()
        {
            // Arrange
            var timeSpan = TimeSpan.FromHours(2);
            var amountToAdd = TimeSpan.FromHours(1);

            // Act
            var result = timeSpan.AddOrNullOnOverflow(amountToAdd);

            // Assert
            Assert.AreEqual(timeSpan + amountToAdd, result);
        }

        [TestMethod]
        public void AddOrNullOnOverflow_AddPositive_Overflows()
        {
            // Arrange
            var timeSpan = TimeSpan.MaxValue;
            var amountToAdd = TimeSpan.FromTicks(1);

            // Act
            var result = timeSpan.AddOrNullOnOverflow(amountToAdd);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AddOrNullOnOverflow_AddNegative()
        {
            // Arrange
            var timeSpan = TimeSpan.FromHours(2);
            var amountToAdd = TimeSpan.FromHours(1);

            // Act
            var result = timeSpan.AddOrNullOnOverflow(-amountToAdd);

            // Assert
            Assert.AreEqual(timeSpan - amountToAdd, result);
        }

        [TestMethod]
        public void AddOrNullOnOverflow_AddNegative_Overflows()
        {
            // Arrange
            var timeSpan = TimeSpan.MinValue;
            var amountToAdd = TimeSpan.FromTicks(1);

            // Act
            var result = timeSpan.AddOrNullOnOverflow(-amountToAdd);

            // Assert
            Assert.IsNull(result);
        }
    }
}
