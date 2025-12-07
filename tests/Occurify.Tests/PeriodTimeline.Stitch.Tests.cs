using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.Helpers;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineStitchTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Stitch_GetPreviousUtcInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, 1, source, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Stitch_GetNextUtcInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, 1, source, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Stitch_IsInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, 1, source, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Stitch_Twice_GetPreviousUtcInstant(string source, string expected)
    {
        // As StitchdStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, 2, source, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Stitch_Twice_GetNextUtcInstant(string source, string expected)
    {
        // As StitchdStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        ExecuteTest(TimelineMethods.GetNextUtcInstant, 2, source, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Stitch_Twice_IsInstant(string source, string expected)
    {
        // As StitchdStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        ExecuteTest(TimelineMethods.IsInstant, 2, source, expected);
    }

    public void ExecuteTest(TimelineMethods method, int methodCalls, string source, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);

        // Act
        var stitchdPeriodTimeline = periodTimeline.Stitch();

        // Assert
        string? actual = null;
        for (var i = 0; i < methodCalls; i++)
        {
            actual = helper.PeriodTimelineToString(stitchdPeriodTimeline, expected.Length, method);
        }

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }
        
    [TestMethod]
    public void ConsecutiveTimeline_GetPreviousUtcInstant()
    {
        // Arrange
        var timeline = Timeline.FromInstants(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, DateTime.UtcNow.AddDays(1));
        var consecutivePeriodTimeline = timeline.AsConsecutivePeriodTimeline();
        var stitchPeriodTimeline = consecutivePeriodTimeline.Stitch();

        // Act
        var start = stitchPeriodTimeline.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
        var end = stitchPeriodTimeline.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        // Assert
        Assert.AreEqual(DateTime.MinValue, start);
        Assert.IsNull(end);
    }

    [TestMethod]
    public void ConsecutiveTimeline_GetPreviousUtcInstant_Twice()
    {
        // As StitchdStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        // Arrange
        var timeline = Timeline.FromInstants(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1));
        var consecutivePeriodTimeline = timeline.AsConsecutivePeriodTimeline();
        var stitchPeriodTimeline = consecutivePeriodTimeline.Stitch();

        // Act
        stitchPeriodTimeline.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
        var start = stitchPeriodTimeline.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        stitchPeriodTimeline.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
        var end = stitchPeriodTimeline.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        // Assert
        Assert.AreEqual(DateTime.MinValue, start);
        Assert.IsNull(end);
    }

    [TestMethod]
    public void ConsecutiveTimeline_GetNextUtcInstant()
    {
        // Arrange
        var timeline = Timeline.FromInstants(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1));
        var consecutivePeriodTimeline = timeline.AsConsecutivePeriodTimeline();
        var stitchPeriodTimeline = consecutivePeriodTimeline.Stitch();

        // Act
        var start = stitchPeriodTimeline.StartTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);
        var end = stitchPeriodTimeline.EndTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);

        // Assert
        // The actual start is skipped.
        Assert.IsNull(start);
        Assert.IsNull(end);
    }

    [TestMethod]
    public void ConsecutiveTimeline_GetNextUtcInstant_Twice()
    {
        // As StitchdStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        // Arrange
        var timeline = Timeline.FromInstants(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1));
        var consecutivePeriodTimeline = timeline.AsConsecutivePeriodTimeline();
        var stitchPeriodTimeline = consecutivePeriodTimeline.Stitch();

        // Act
        stitchPeriodTimeline.StartTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);
        var start = stitchPeriodTimeline.StartTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);

        stitchPeriodTimeline.EndTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);
        var end = stitchPeriodTimeline.EndTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);

        // Assert
        // The actual start is skipped.
        Assert.IsNull(start);
        Assert.IsNull(end);
    }

    [TestMethod]
    public void ConsecutiveTimeline_IsInstant()
    {
        // Arrange
        var timeline = Timeline.FromInstants(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1));
        var consecutivePeriodTimeline = timeline.AsConsecutivePeriodTimeline();
        var stitchPeriodTimeline = consecutivePeriodTimeline.Stitch();

        // Act
        var firstInstantIsStart = stitchPeriodTimeline.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc);
        var firstInstantIsEnd = stitchPeriodTimeline.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc);

        // Assert
        Assert.IsTrue(firstInstantIsStart);
        Assert.IsFalse(firstInstantIsEnd);
    }

    [TestMethod]
    public void ConsecutiveTimeline_IsInstant_Twice()
    {
        // As StitchdStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        // Arrange
        var timeline = Timeline.FromInstants(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1));
        var consecutivePeriodTimeline = timeline.AsConsecutivePeriodTimeline();
        var stitchPeriodTimeline = consecutivePeriodTimeline.Stitch();

        // Act
        stitchPeriodTimeline.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc);
        var firstInstantIsStart = stitchPeriodTimeline.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc);

        stitchPeriodTimeline.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc);
        var firstInstantIsEnd = stitchPeriodTimeline.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc);

        // Assert
        Assert.IsTrue(firstInstantIsStart);
        Assert.IsFalse(firstInstantIsEnd);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Stitch.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineStitchTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException($"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineStitchTests)}."),
            tc.Expected ?? throw new InvalidOperationException($"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineStitchTests)}.")
        }).ToArray();
    }
}