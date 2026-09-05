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

        [TestMethod]
        public void AddOrNullOnOverflow_NegativeResult_IsNotOverflow()
        {
            Assert.AreEqual(TimeSpan.FromSeconds(-5), TimeSpan.FromSeconds(5).AddOrNullOnOverflow(TimeSpan.FromSeconds(-10)));
            Assert.AreEqual(TimeSpan.FromSeconds(-8), TimeSpan.FromSeconds(-5).AddOrNullOnOverflow(TimeSpan.FromSeconds(-3)));
            Assert.AreEqual(TimeSpan.FromSeconds(-2), TimeSpan.FromSeconds(-5).AddOrNullOnOverflow(TimeSpan.FromSeconds(3)));
        }

        [TestMethod]
        public void AddOrNullOnOverflow_Extremes()
        {
            Assert.AreEqual(TimeSpan.MinValue, TimeSpan.Zero.AddOrNullOnOverflow(TimeSpan.MinValue));
            Assert.AreEqual(TimeSpan.MaxValue, TimeSpan.Zero.AddOrNullOnOverflow(TimeSpan.MaxValue));
            Assert.IsNull(TimeSpan.MinValue.AddOrNullOnOverflow(TimeSpan.MinValue));
            Assert.IsNull(TimeSpan.MaxValue.AddOrNullOnOverflow(TimeSpan.MaxValue));
            Assert.AreEqual(TimeSpan.FromTicks(-1), TimeSpan.MinValue.AddOrNullOnOverflow(TimeSpan.MaxValue));
        }
    }
}
