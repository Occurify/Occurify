using Occurify.Extensions;

namespace Occurify.Tests;

[TestClass]
public class TimelineUtilsTests
{
    [TestMethod]
    public void GetTimeToNextInstant()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var timeline = utcNow.AsTimeline();
        var duration = TimeSpan.FromHours(1);

        // Act
        var result = timeline.GetTimeToNextInstant(utcNow - duration);

        // Assert
        Assert.AreEqual(duration, result);
    }

    [TestMethod]
    public void GetTimeToNextInstant_NoInstant_Null()
    {
        // Arrange
        var timeline = Timeline.Empty();

        // Act
        var result = timeline.GetTimeToNextInstant(DateTime.UtcNow);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetTimeSincePreviousInstant()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var timeline = utcNow.AsTimeline();
        var duration = TimeSpan.FromHours(1);

        // Act
        var result = timeline.GetTimeSincePreviousInstant(utcNow + duration);

        // Assert
        Assert.AreEqual(duration, result);
    }

    [TestMethod]
    public void GetTimeSincePreviousInstant_NoInstant_Null()
    {
        // Arrange
        var timeline = Timeline.Empty();

        // Act
        var result = timeline.GetTimeSincePreviousInstant(DateTime.UtcNow);

        // Assert
        Assert.IsNull(result);
    }
}