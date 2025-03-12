using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.Helpers;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineInvertTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Invert_GetPreviousUtcInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, 1, source, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Invert_GetNextUtcInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, 1, source, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Invert_IsInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, 1, source, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Invert_Twice_GetPreviousUtcInstant(string source, string expected)
    {
        // As InvertedStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, 2, source, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Invert_Twice_GetNextUtcInstant(string source, string expected)
    {
        // As InvertedStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        ExecuteTest(TimelineMethods.GetNextUtcInstant, 2, source, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Invert_Twice_IsInstant(string source, string expected)
    {
        // As InvertedStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
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
        var invertedPeriods = periodTimeline.Invert();

        // Assert
        string? actual = null;
        for (var i = 0; i < methodCalls; i++)
        {
            actual = helper.PeriodTimelineToString(invertedPeriods, expected.Length, method);
        }

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmptyTimeline_GetPreviousUtcInstant()
    {
        // Arrange
        var emptyPeriodTimeline = PeriodTimeline.Empty();
        var invertedPeriodTimeline = emptyPeriodTimeline.Invert();

        // Act
        var start = invertedPeriodTimeline.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
        var end = invertedPeriodTimeline.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        // Assert
        Assert.AreEqual(DateTimeHelper.MinValueUtc, start);
        Assert.IsNull(end);
    }

    [TestMethod]
    public void EmptyTimeline_GetPreviousUtcInstant_Twice()
    {
        // As InvertedStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        // Arrange
        var emptyPeriodTimeline = PeriodTimeline.Empty();
        var invertedPeriodTimeline = emptyPeriodTimeline.Invert();

        // Act
        invertedPeriodTimeline.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
        var start = invertedPeriodTimeline.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        invertedPeriodTimeline.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
        var end = invertedPeriodTimeline.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        // Assert
        Assert.AreEqual(DateTimeHelper.MinValueUtc, start);
        Assert.IsNull(end);
    }

    [TestMethod]
    public void EmptyTimeline_GetNextUtcInstant()
    {
        // Arrange
        var emptyPeriodTimeline = PeriodTimeline.Empty();
        var invertedPeriodTimeline = emptyPeriodTimeline.Invert();

        // Act
        var start = invertedPeriodTimeline.StartTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);
        var end = invertedPeriodTimeline.EndTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);

        // Assert
        // The actual start is skipped.
        Assert.IsNull(start);
        Assert.IsNull(end);
    }

    [TestMethod]
    public void EmptyTimeline_GetNextUtcInstant_Twice()
    {
        // As InvertedStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        // Arrange
        var emptyPeriodTimeline = PeriodTimeline.Empty();
        var invertedPeriodTimeline = emptyPeriodTimeline.Invert();

        // Act
        invertedPeriodTimeline.StartTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);
        var start = invertedPeriodTimeline.StartTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);

        invertedPeriodTimeline.EndTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);
        var end = invertedPeriodTimeline.EndTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);

        // Assert
        // The actual start is skipped.
        Assert.IsNull(start);
        Assert.IsNull(end);
    }

    [TestMethod]
    public void EmptyTimeline_IsInstant()
    {
        // Arrange
        var emptyPeriodTimeline = PeriodTimeline.Empty();
        var invertedPeriodTimeline = emptyPeriodTimeline.Invert();

        // Act
        var firstInstantIsStart = invertedPeriodTimeline.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc);
        var firstInstantIsEnd = invertedPeriodTimeline.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc);

        // Assert
        Assert.IsTrue(firstInstantIsStart);
        Assert.IsFalse(firstInstantIsEnd);
    }

    [TestMethod]
    public void EmptyTimeline_IsInstant_Twice()
    {
        // As InvertedStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        // Arrange
        var emptyPeriodTimeline = PeriodTimeline.Empty();
        var invertedPeriodTimeline = emptyPeriodTimeline.Invert();

        // Act
        invertedPeriodTimeline.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc);
        var firstInstantIsStart = invertedPeriodTimeline.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc);

        invertedPeriodTimeline.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc);
        var firstInstantIsEnd = invertedPeriodTimeline.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc);

        // Assert
        Assert.IsTrue(firstInstantIsStart);
        Assert.IsFalse(firstInstantIsEnd);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Invert.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineInvertTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException($"Input of null is not supported in {nameof(PeriodTimelineInvertTests)}."),
            tc.Expected ?? throw new InvalidOperationException($"Expected of null is not supported in {nameof(PeriodTimelineInvertTests)}.")
        }).ToArray();
    }
}