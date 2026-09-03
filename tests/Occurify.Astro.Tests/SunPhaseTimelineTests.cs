using Occurify.Extensions;

namespace Occurify.Astro.Tests
{
    [TestClass]
    public sealed class SunPhaseTimelineTests
    {
        [TestMethod]
        public void GetPreviousUtcInstant()
        {
            //Arrange
            const long expectedSunriseTicks = 599341223760027276;
            var date = new DateTime(1900, 3, 28, 23, 20, 0).AsUtcInstant();
            var coordinates = new Coordinates(82.5, 62.3);
            var sunrises = AstroInstants.SunPhases(coordinates, SunPhases.Sunrise);

            // Act
            var previousSunrise = sunrises.GetPreviousUtcInstant(date);

            // Assert
            Assert.IsNotNull(previousSunrise);
            Assert.AreEqual(DateTimeKind.Utc, previousSunrise.Value.Kind);
            Assert.AreEqual(expectedSunriseTicks, previousSunrise.Value.Ticks);
        }

        [TestMethod]
        public void GetPreviousUtcInstant_NoMoreSunsets()
        {
            // Arrange
            var date = new DateTime(1, 1, 1).AsUtcInstant();
            var coordinates = new Coordinates(82.5, 62.3);
            var sunrises = AstroInstants.SunPhases(coordinates, SunPhases.Sunrise);

            // Act
            var previousSunrise = sunrises.GetPreviousUtcInstant(date);

            // Assert
            Assert.IsNull(previousSunrise);
        }

        [TestMethod]
        public void GetNextUtcInstant()
        {
            // Arrange
            const long expectedSunriseTicks = 599341223760027276;
            var date = new DateTime(1900, 3, 28, 23, 15, 0).AsUtcInstant();
            var coordinates = new Coordinates(82.5, 62.3);
            var sunrises = AstroInstants.SunPhases(coordinates, SunPhases.Sunrise);

            // Act
            var nextSunrise = sunrises.GetNextUtcInstant(date);

            // Assert
            Assert.IsNotNull(nextSunrise);
            Assert.AreEqual(DateTimeKind.Utc, nextSunrise.Value.Kind);
            Assert.AreEqual(expectedSunriseTicks, nextSunrise.Value.Ticks);
        }

        [TestMethod]
        public void GetNextUtcInstant_NoMoreSunrise()
        {
            // Assert
            var date = new DateTime(9999, 12, 28, 23, 15, 0).AsUtcInstant();
            var coordinates = new Coordinates(82.5, 62.3);
            var sunrises = AstroInstants.SunPhases(coordinates, SunPhases.Sunrise);

            // Act
            var nextSunrise = sunrises.GetNextUtcInstant(date);

            // Assert
            Assert.IsNull(nextSunrise);
        }

        [TestMethod]
        public void IsInstant()
        {
            // Arrange
            const long expectedSunriseTicks = 599341223760027276;
            var date = new DateTime(expectedSunriseTicks, DateTimeKind.Utc);
            var coordinates = new Coordinates(82.5, 62.3);
            var sunrises = AstroInstants.SunPhases(coordinates, SunPhases.Sunrise);

            // Act
            var isInstant = sunrises.IsInstant(date);

            // Assert
            Assert.IsTrue(isInstant);
        }

        // Note: as we are using a third party library, we perform some simple sanity checks.
        [TestMethod]
        public void SunPhaseTimeline_RemainsConsistent_ForwardsBackwards()
        {
            // Arrange
            const int amountToCheck = 1000;
            var date = new DateTime(2025, 3, 10, 16, 21, 0).AsUtcInstant();
            var coordinates = new Coordinates(82.5, 62.3, 42.1337);
            var sunrises = AstroInstants.SunPhases(coordinates, SunPhases.Sunrise | SunPhases.Dawn);
            var collected = new List<DateTime>();

            // Act and Assert
            for (var i = 0; i < amountToCheck; i++)
            {
                var next = sunrises.GetNextUtcInstant(date);
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
                var previous = sunrises.GetPreviousUtcInstant(date);
                
                if (previous == null)
                {
                    Assert.Fail("Dates are expected in current range.");
                }

                Assert.IsTrue(sunrises.IsInstant(previous.Value));

                Assert.AreEqual(collected[i], previous.Value);
                date = previous.Value;
            }
        }

        [TestMethod]
        public void SunPhaseTimeline_RemainsConsistent_Forwards()
        {
            // Arrange
            const int amountToCheck = 1000;
            var date = new DateTime(2025, 3, 10, 16, 21, 0).AsUtcInstant();
            var coordinates = new Coordinates(82.5, 62.3, 42.1337);
            var sunrises = AstroInstants.SunPhases(coordinates, SunPhases.Sunrise | SunPhases.Dawn);

            // Act
            // Get initial sunrise
            var sunrise = sunrises.GetNextUtcInstant(date);
            Assert.IsNotNull(sunrise);

            // Check if all values before give the same result.
            date = sunrise.Value - TimeSpan.FromTicks(amountToCheck);

            // Assert
            for (var i = 0; i < amountToCheck; i++)
            {
                var next = sunrises.GetNextUtcInstant(date);
                Assert.AreEqual(next, sunrise);

                Assert.IsFalse(sunrises.IsInstant(date));

                date += TimeSpan.FromTicks(1);
            }
        }

        [TestMethod]
        public void SunPhaseTimeline_RemainsConsistent_BackwardsForwards()
        {
            // Arrange
            const int amountToCheck = 1000;
            var date = new DateTime(2025, 3, 10, 16, 21, 0).AsUtcInstant();
            var coordinates = new Coordinates(82.5, 62.3, 42.1337);
            var sunrises = AstroInstants.SunPhases(coordinates, SunPhases.Sunrise | SunPhases.Dawn);
            var collected = new List<DateTime>();

            // Act and Assert
            for (var i = 0; i < amountToCheck; i++)
            {
                var next = sunrises.GetPreviousUtcInstant(date);
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
                var previous = sunrises.GetNextUtcInstant(date);

                if (previous == null)
                {
                    Assert.Fail("Dates are expected in current range.");
                }

                Assert.IsTrue(sunrises.IsInstant(previous.Value));

                Assert.AreEqual(collected[i], previous.Value);
                date = previous.Value;
            }
        }

        [TestMethod]
        public void SunPhaseTimeline_RemainsConsistent_Backwards()
        {
            // Arrange
            const int amountToCheck = 1000;
            var date = new DateTime(2025, 3, 10, 16, 21, 0).AsUtcInstant();
            var coordinates = new Coordinates(82.5, 62.3, 42.1337);
            var sunrises = AstroInstants.SunPhases(coordinates, SunPhases.Sunrise | SunPhases.Dawn);

            // Act
            // Get initial sunrise
            var sunrise = sunrises.GetPreviousUtcInstant(date);
            Assert.IsNotNull(sunrise);

            // Check if all values after give the same result.
            date = sunrise.Value + TimeSpan.FromTicks(amountToCheck);

            // Assert
            for (var i = 0; i < amountToCheck; i++)
            {
                var next = sunrises.GetPreviousUtcInstant(date);
                Assert.AreEqual(next, sunrise);

                Assert.IsFalse(sunrises.IsInstant(date));

                date -= TimeSpan.FromTicks(1);
            }
        }
    }
}
