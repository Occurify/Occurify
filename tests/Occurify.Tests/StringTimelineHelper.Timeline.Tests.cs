using Occurify.Tests.Helpers;
using Occurify.Tests.StringHelper;

namespace Occurify.Tests;

[TestClass]
public class StringTimelineHelperTimelineTests
{
    [TestMethod]
    [DataRow(new int[0], " ")]
    [DataRow(new[] { 0 }, "|")]
    [DataRow(new[] { 0 }, "| ")]
    [DataRow(new[] { 0, 2 }, "| |")]

    [DataRow(new[] { -1 }, "|_ ", 1)]
    [DataRow(new[] { -100 }, "|_ ", 1)]
    [DataRow(new[] { -1, 0 }, "|_|", 1)]
    [DataRow(new[] { -100, 0 }, "|_|", 1)]

    [DataRow(new[] { 1 }, " ", 1)]
    public void TimelineToString_GetPreviousUtcInstant(int[] instants, string expectedTimeline, int? convertLength = null)
    {
        // Arrange
        convertLength ??= expectedTimeline.Length;
        var helper = new StringTimelineHelper();
        var timeline = Timeline.FromInstants(instants.Select(i => helper.Origin.AddTicks(i)));

        // Act
        var actual =
            helper.TimelineToString(timeline, convertLength.Value, TimelineMethods.GetPreviousUtcInstant);

        // Assert
        Assert.AreEqual(expectedTimeline, actual);
    }

    [TestMethod]
    public void TimelineToString_GetPreviousUtcInstant_OriginTooLate_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var helper = new StringTimelineHelper(new DateTime(DateTime.MaxValue.Ticks - 3));
        var timeline = Timeline.Empty();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            helper.TimelineToString(timeline, 3, TimelineMethods.GetPreviousUtcInstant), "Origin + convertLength cannot be larger or equal to DateTime.MaxValue as we need to start one tick after the origin + convertLength to use GetPreviousUtcInstant.");
    }

    [TestMethod]
    public void TimelineToString_GetPreviousUtcInstant_OriginNotTooLate()
    {
        // Arrange
        var helper = new StringTimelineHelper(new DateTime(DateTime.MaxValue.Ticks - 3, DateTimeKind.Utc));
        var timeline = Timeline.Empty();

        // Act
        helper.TimelineToString(timeline, 2, TimelineMethods.GetPreviousUtcInstant);
    }

    [TestMethod]
    [DataRow(new int[0], " ")]
    [DataRow(new[] { 0 }, "|")]
    [DataRow(new[] { 0 }, "| ")]
    [DataRow(new[] { 0, 2 }, "| |")]

    [DataRow(new[] { 1 }, " _|", 1)]
    [DataRow(new[] { 100 }, " _|", 1)]
    [DataRow(new[] { 0, 1 }, "|_|", 1)]
    [DataRow(new[] { 0, 100 }, "|_|", 1)]

    [DataRow(new[] { -1 }, " ", 1)]
    public void TimelineToString_GetNextUtcInstant(int[] instants, string expectedTimeline, int? convertLength = null)
    {
        // Arrange
        convertLength ??= expectedTimeline.Length;
        var helper = new StringTimelineHelper();
        var timeline = Timeline.FromInstants(instants.Select(i => helper.Origin.AddTicks(i)));

        // Act
        var actual =
            helper.TimelineToString(timeline, convertLength.Value, TimelineMethods.GetNextUtcInstant);

        // Assert
        Assert.AreEqual(expectedTimeline, actual);
    }

    [TestMethod]
    public void TimelineToString_GetNextUtcInstant_OriginTooEarly_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var helper = new StringTimelineHelper(DateTimeHelper.MinValueUtc);
        var timeline = Timeline.Empty();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            helper.TimelineToString(timeline, 1, TimelineMethods.GetNextUtcInstant), "Origin cannot be DateTime.MinValue as we need to start one tick before the origin to use GetNextUtcInstant.");
    }

    [TestMethod]
    public void TimelineToString_GetNextUtcInstant_OriginNotTooEarly()
    {
        // Arrange
        var helper = new StringTimelineHelper(new DateTime(1, DateTimeKind.Utc));
        var timeline = Timeline.Empty();

        // Act
        helper.TimelineToString(timeline, 1, TimelineMethods.GetNextUtcInstant);
    }

    [TestMethod]
    public void TimelineToString_GetNextUtcInstant_OriginTooLate_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var helper = new StringTimelineHelper(new DateTime(DateTime.MaxValue.Ticks - 3));
        var timeline = Timeline.Empty();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            helper.TimelineToString(timeline, 4, TimelineMethods.GetNextUtcInstant), "Origin + convertLength cannot be larger than DateTime.MaxValue.");
    }

    [TestMethod]
    public void TimelineToString_GetNextUtcInstant_OriginNotTooLate()
    {
        // Arrange
        var helper = new StringTimelineHelper(new DateTime(DateTime.MaxValue.Ticks - 3, DateTimeKind.Utc));
        var timeline = Timeline.Empty();

        // Act
        helper.TimelineToString(timeline, 3, TimelineMethods.GetNextUtcInstant);
    }

    [TestMethod]
    [DataRow(new int[0], " ")]
    [DataRow(new[] { 0 }, "|")]
    [DataRow(new[] { 0 }, "| ")]
    [DataRow(new[] { 0, 2 }, "| |")]
    public void TimelineToString_IsInstant(int[] instants, string expectedTimeline)
    {
        // Arrange
        var helper = new StringTimelineHelper();
        var timeline = Timeline.FromInstants(instants.Select(i => helper.Origin.AddTicks(i)));

        // Act
        var actual =
            helper.TimelineToString(timeline, expectedTimeline.Length, TimelineMethods.IsInstant);

        // Assert
        Assert.AreEqual(expectedTimeline, actual);
    }

    [TestMethod]
    [DataRow(" ")]
    [DataRow("|")]
    [DataRow("| |")]
    public void CreateTimeline(string timeline)
    {
        // Arrange
        var helper = new StringTimelineHelper();

        // Act
        var parsedPipeline = helper.CreateTimeline(timeline);

        // Assert
        var timelineStr = helper.TimelineToString(parsedPipeline, timeline.Length, TimelineMethods.IsInstant);
        Assert.AreEqual(timeline, timelineStr);
    }
}