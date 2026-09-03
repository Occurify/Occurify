using Occurify.Extensions;
using Occurify.Tests.Helpers;
using Occurify.Tests.StringHelper;

namespace Occurify.Tests;

[TestClass]
public class StringTimelineHelperPeriodTimelineTests
{
    [TestMethod]
    [DataRow(new int[0], new int[0], " ")]

    // Start only
    [DataRow(new[] { 0 }, new int[0], "<")]
    [DataRow(new[] { 0 }, new int[0], "< ")]

    [DataRow(new[] { -1 }, new int[0], "<_ ", 1)]
    [DataRow(new[] { -100 }, new int[0], "<_ ", 1)]

    [DataRow(new[] { 1 }, new int[0], " ", 1)]

    // End only
    [DataRow(new int[0], new[] { 0 }, ">")]
    [DataRow(new int[0], new[] { 0 }, "> ")]

    [DataRow(new int[0], new[] { -1 }, ">_ ", 1)]
    [DataRow(new int[0], new[] { -100 }, ">_ ", 1)]

    [DataRow(new int[0], new[] { 1 }, " ", 1)]

    // Combined
    [DataRow(new[] { 0 }, new[] { 0 }, "X")]
    [DataRow(new[] { 0 }, new[] { 0 }, "X ")]
    [DataRow(new[] { 0 }, new[] { 2 }, "< >")]
    [DataRow(new[] { 2 }, new[] { 0 }, "> <")]
    [DataRow(new[] { 0, 2 }, new[] { 0, 2 }, "X X")]
    [DataRow(new[] { -1 }, new[] { 0 }, "<_>", 1)]
    [DataRow(new[] { -100 }, new[] { 0 }, "<_>", 1)]
    [DataRow(new[] { 0 }, new[] { -1 }, ">_<", 1)]
    [DataRow(new[] { 0 }, new[] { -100 }, ">_<", 1)]

    [DataRow(new[] { -1 }, new[] { -1 }, "X_ ", 1)]
    [DataRow(new[] { -100 }, new[] { -100 }, "X_ ", 1)]
    [DataRow(new[] { -1, 0 }, new[] { -1, 0 }, "X_X", 1)]
    [DataRow(new[] { -100, 0 }, new[] { -100, 0 }, "X_X", 1)]

    [DataRow(new[] { 1 }, new[] { 1 }, " ", 1)]
    public void PeriodTimelineToString_GetPreviousUtcInstant(int[] startInstants, int[] endInstants, string expectedTimeline, int? convertLength = null)
    {
        // Arrange
        convertLength ??= expectedTimeline.Length;
        var helper = new StringTimelineHelper();
        var periodTimeline = startInstants.Select(i => helper.Origin.AddTicks(i))
            .To(endInstants.Select(i => helper.Origin.AddTicks(i)));

        // Act
        var actual =
            helper.PeriodTimelineToString(periodTimeline, convertLength.Value, TimelineMethods.GetPreviousUtcInstant);

        // Assert
        Assert.AreEqual(expectedTimeline, actual);
    }

    [TestMethod]
    public void PeriodTimelineToString_GetPreviousUtcInstant_OriginTooLate_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var helper = new StringTimelineHelper(new DateTime(DateTime.MaxValue.Ticks - 3));
        var timeline = PeriodTimeline.Empty();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            helper.PeriodTimelineToString(timeline, 3, TimelineMethods.GetPreviousUtcInstant), "Origin + convertLength cannot be larger or equal to DateTime.MaxValue as we need to start one tick after the origin + convertLength to use GetPreviousUtcInstant.");
    }

    [TestMethod]
    public void PeriodTimelineToString_GetPreviousUtcInstant_OriginNotTooLate()
    {
        // Arrange
        var helper = new StringTimelineHelper(new DateTime(DateTime.MaxValue.Ticks - 3, DateTimeKind.Utc));
        var timeline = PeriodTimeline.Empty();

        // Act
        helper.PeriodTimelineToString(timeline, 2, TimelineMethods.GetPreviousUtcInstant);
    }

    [TestMethod]
    [DataRow(new int[0], new int[0], " ")]

    // Start only
    [DataRow(new[] { 0 }, new int[0], "<")]
    [DataRow(new[] { 0 }, new int[0], "< ")]

    [DataRow(new[] { 1 }, new int[0], " _<", 1)]
    [DataRow(new[] { 100 }, new int[0], " _<", 1)]

    [DataRow(new[] { -1 }, new int[0], " ", 1)]

    // End only
    [DataRow(new int[0], new[] { 0 }, ">")]
    [DataRow(new int[0], new[] { 0 }, "> ")]

    [DataRow(new int[0], new[] { 1 }, " _>", 1)]
    [DataRow(new int[0], new[] { 100 }, " _>", 1)]

    [DataRow(new int[0], new[] { -1 }, " ", 1)]

    // Combined
    [DataRow(new[] { 0 }, new[] { 0 }, "X")]
    [DataRow(new[] { 0 }, new[] { 0 }, "X ")]
    [DataRow(new[] { 0 }, new[] { 2 }, "< >")]
    [DataRow(new[] { 2 }, new[] { 0 }, "> <")]
    [DataRow(new[] { 0, 2 }, new[] { 0, 2 }, "X X")]
    [DataRow(new[] { 0 }, new[] { 1 }, "<_>", 1)]
    [DataRow(new[] { 0 }, new[] { 100 }, "<_>", 1)]
    [DataRow(new[] { 1 }, new[] { 0 }, ">_<", 1)]
    [DataRow(new[] { 100 }, new[] { 0 }, ">_<", 1)]

    [DataRow(new[] { 1 }, new[] { 1 }, " _X", 1)]
    [DataRow(new[] { 100 }, new[] { 100 }, " _X", 1)]
    [DataRow(new[] { 0, 1 }, new[] { 0, 1 }, "X_X", 1)]
    [DataRow(new[] { 0, 100 }, new[] { 0, 100 }, "X_X", 1)]

    [DataRow(new[] { -1 }, new[] { -1 }, " ", 1)]
    public void PeriodTimelineToString_GetNextUtcInstant(int[] startInstants, int[] endInstants, string expectedTimeline, int? convertLength = null)
    {
        // Arrange
        convertLength ??= expectedTimeline.Length;
        var helper = new StringTimelineHelper();
        var periodTimeline = startInstants.Select(i => helper.Origin.AddTicks(i))
            .To(endInstants.Select(i => helper.Origin.AddTicks(i)));

        // Act
        var actual =
            helper.PeriodTimelineToString(periodTimeline, convertLength.Value, TimelineMethods.GetNextUtcInstant);

        // Assert
        Assert.AreEqual(expectedTimeline, actual);
    }

    [TestMethod]
    public void PeriodTimelineToString_GetNextUtcInstant_OriginTooEarly_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var helper = new StringTimelineHelper(DateTimeHelper.MinValueUtc);
        var timeline = PeriodTimeline.Empty();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            helper.PeriodTimelineToString(timeline, 1, TimelineMethods.GetNextUtcInstant), "Origin cannot be DateTime.MinValue as we need to start one tick before the origin to use GetNextUtcInstant.");
    }

    [TestMethod]
    public void PeriodTimelineToString_GetNextUtcInstant_OriginNotTooEarly()
    {
        // Arrange
        var helper = new StringTimelineHelper(new DateTime(1, DateTimeKind.Utc));
        var timeline = PeriodTimeline.Empty();

        // Act
        helper.PeriodTimelineToString(timeline, 1, TimelineMethods.GetNextUtcInstant);
    }

    [TestMethod]
    public void PeriodTimelineToString_GetNextUtcInstant_OriginTooLate_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var helper = new StringTimelineHelper(new DateTime(DateTime.MaxValue.Ticks - 3));
        var timeline = PeriodTimeline.Empty();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            helper.PeriodTimelineToString(timeline, 4, TimelineMethods.GetNextUtcInstant), "Origin + convertLength cannot be larger than DateTime.MaxValue.");
    }

    [TestMethod]
    public void PeriodTimelineToString_GetNextUtcInstant_OriginNotTooLate()
    {
        // Arrange
        var helper = new StringTimelineHelper(new DateTime(DateTime.MaxValue.Ticks - 3, DateTimeKind.Utc));
        var timeline = PeriodTimeline.Empty();

        // Act
        helper.PeriodTimelineToString(timeline, 3, TimelineMethods.GetNextUtcInstant);
    }

    [TestMethod]
    [DataRow(new int[0], new int[0], " ")]

    // Start only
    [DataRow(new[] { 0 }, new int[0], "<")]
    [DataRow(new[] { 0 }, new int[0], "< ")]

    // End only
    [DataRow(new int[0], new[] { 0 }, ">")]
    [DataRow(new int[0], new[] { 0 }, "> ")]

    // Combined
    [DataRow(new[] { 0 }, new[] { 0 }, "X")]
    [DataRow(new[] { 0 }, new[] { 0 }, "X ")]
    [DataRow(new[] { 0 }, new[] { 2 }, "< >")]
    [DataRow(new[] { 2 }, new[] { 0 }, "> <")]
    [DataRow(new[] { 0, 2 }, new[] { 0, 2 }, "X X")]
    public void PeriodTimelineToString_IsInstant(int[] startInstants, int[] endInstants, string expectedTimeline)
    {
        // Arrange
        var helper = new StringTimelineHelper();
        var periodTimeline = startInstants.Select(i => helper.Origin.AddTicks(i))
            .To(endInstants.Select(i => helper.Origin.AddTicks(i)));

        // Act
        var actual =
            helper.PeriodTimelineToString(periodTimeline, expectedTimeline.Length, TimelineMethods.IsInstant);

        // Assert
        Assert.AreEqual(expectedTimeline, actual);
    }

    [TestMethod]
    [DataRow(" ")]
    [DataRow("<")]
    [DataRow(">")]
    [DataRow("X")]
    [DataRow("< >")]
    [DataRow("> <")]
    [DataRow("X X")]
    public void CreatePeriodTimeline(string timeline)
    {
        // Arrange
        var helper = new StringTimelineHelper();

        // Act
        var parsedPipeline = helper.CreatePeriodTimeline(timeline);

        // Assert
        var timelineStr = helper.PeriodTimelineToString(parsedPipeline, timeline.Length, TimelineMethods.IsInstant);
        Assert.AreEqual(timeline, timelineStr);
    }
}