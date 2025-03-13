using Occurify.Extensions;

namespace Occurify.Tests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void AddOrNullOnOverflow_AddPositive()
        {
            // Arrange
            var dateTime = DateTime.UtcNow;
            var amountToAdd = TimeSpan.FromHours(1);

            // Act
            var result = dateTime.AddOrNullOnOverflow(amountToAdd);

            // Assert
            Assert.AreEqual(dateTime + amountToAdd, result);
        }

        [TestMethod]
        public void AddOrNullOnOverflow_AddPositive_Overflows()
        {
            // Arrange
            var dateTime = DateTime.MaxValue;
            var amountToAdd = TimeSpan.FromTicks(1);

            // Act
            var result = dateTime.AddOrNullOnOverflow(amountToAdd);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AddOrNullOnOverflow_AddNegative()
        {
            // Arrange
            var dateTime = DateTime.UtcNow;
            var amountToAdd = TimeSpan.FromHours(1);

            // Act
            var result = dateTime.AddOrNullOnOverflow(-amountToAdd);

            // Assert
            Assert.AreEqual(dateTime - amountToAdd, result);
        }

        [TestMethod]
        public void AddOrNullOnOverflow_AddNegative_Overflows()
        {
            // Arrange
            var dateTime = DateTime.MinValue;
            var amountToAdd = TimeSpan.FromTicks(1);

            // Act
            var result = dateTime.AddOrNullOnOverflow(-amountToAdd);

            // Assert
            Assert.IsNull(result);
        }
    }
}
