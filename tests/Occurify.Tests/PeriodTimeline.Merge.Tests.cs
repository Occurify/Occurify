using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.Helpers;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineMergeTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Merge_GetPreviousUtcInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, 1, source, periods , expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Merge_GetNextUtcInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, 1, source, periods , expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Merge_IsInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, 1, source, periods , expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Merge_Twice_GetPreviousUtcInstant(string source, string periods, string expected)
    {
        // As MergeStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, 2, source, periods, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Merge_Twice_GetNextUtcInstant(string source, string periods, string expected)
    {
        // As MergeStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        ExecuteTest(TimelineMethods.GetNextUtcInstant, 2, source, periods, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Merge_Twice_IsInstant(string source, string periods, string expected)
    {
        // As MergeStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        ExecuteTest(TimelineMethods.IsInstant, 2, source, periods, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void AsPeriodTimeline_GetPreviousUtcInstant(string source, string periods, string expected)
    {
        Execute_AsPeriodTimelineGivesSameResult(TimelineMethods.GetPreviousUtcInstant, source, periods, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void AsPeriodTimeline_GetNextUtcInstant(string source, string periods, string expected)
    {
        Execute_AsPeriodTimelineGivesSameResult(TimelineMethods.GetNextUtcInstant, source, periods, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void AsPeriodTimeline_IsInstant(string source, string periods, string expected)
    {
        Execute_AsPeriodTimelineGivesSameResult(TimelineMethods.IsInstant, source, periods, expected);
    }

    private void ExecuteTest(TimelineMethods method, int methodCalls, string source, string periods, string expected)
    {
        Console.WriteLine($"Source:   \"{source  }\"");
        Console.WriteLine($"Periods:  \"{periods }\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);
        var periodsToMerge = helper.CreatePeriodTimeline(periods);

        // Act
        var mergedPeriodTimeline = periodTimeline.Merge(periodsToMerge);

        // Assert
        string? actual = null;
        for (var i = 0; i < methodCalls; i++)
        {
            actual = helper.PeriodTimelineToString(mergedPeriodTimeline, expected.Length, method);
        }

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private void Execute_AsPeriodTimelineGivesSameResult(TimelineMethods method, string source, string periods, string expected)
    {
        // AsPeriodTimeline internally uses slightly different (optimised) logic, but should give the same result.
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Periods:  \"{periods}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var sourcePeriods = helper.CreatePeriodTimeline(source);
        var periodsToMerge = helper.CreatePeriodTimeline(periods);
        var allPeriods = sourcePeriods.Concat(periodsToMerge).ToArray();

        // Act
        var periodTimeline = allPeriods.AsPeriodTimeline();

        // Assert
        var actual = helper.PeriodTimelineToString(periodTimeline, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DynamicData(nameof(FullTimelineTestData))]
    public void FullTimeline_GetPreviousUtcInstant(Period period1, Period period2)
    {
        // Arrange
        var mergedPeriodTimeline = period1.Merge(period2);

        // Act
        var start = mergedPeriodTimeline.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
        var end = mergedPeriodTimeline.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        // Assert
        Assert.AreEqual(DateTime.MinValue, start);
        Assert.IsNull(end);
    }

    [TestMethod]
    [DynamicData(nameof(FullTimelineTestData))]
    public void FullTimeline_GetPreviousUtcInstant_Twice(Period period1, Period period2)
    {
        // As MergeStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        // Arrange
        var mergedPeriodTimeline = period1.Merge(period2);

        // Act
        mergedPeriodTimeline.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
        var start = mergedPeriodTimeline.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        mergedPeriodTimeline.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);
        var end = mergedPeriodTimeline.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        // Assert
        Assert.AreEqual(DateTime.MinValue, start);
        Assert.IsNull(end);
    }

    [TestMethod]
    [DynamicData(nameof(FullTimelineTestData))]
    public void FullTimeline_GetNextUtcInstant(Period period1, Period period2)
    {
        // Arrange
        var mergedPeriodTimeline = period1.Merge(period2);

        // Act
        var start = mergedPeriodTimeline.StartTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);
        var end = mergedPeriodTimeline.EndTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);

        // Assert
        // The actual start is skipped.
        Assert.IsNull(start);
        Assert.IsNull(end);
    }

    [TestMethod]
    [DynamicData(nameof(FullTimelineTestData))]
    public void FullTimeline_GetNextUtcInstant_Twice(Period period1, Period period2)
    {
        // As MergeStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        // Arrange
        var mergedPeriodTimeline = period1.Merge(period2);

        // Act
        mergedPeriodTimeline.StartTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);
        var start = mergedPeriodTimeline.StartTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);

        mergedPeriodTimeline.EndTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);
        var end = mergedPeriodTimeline.EndTimeline.GetNextUtcInstant(DateTimeHelper.MinValueUtc);

        // Assert
        // The actual start is skipped.
        Assert.IsNull(start);
        Assert.IsNull(end);
    }

    [TestMethod]
    [DynamicData(nameof(FullTimelineTestData))]
    public void FullTimeline_IsInstant(Period period1, Period period2)
    {
        // Arrange
        var mergedPeriodTimeline = period1.Merge(period2);

        // Act
        var firstInstantIsStart = mergedPeriodTimeline.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc);
        var firstInstantIsEnd = mergedPeriodTimeline.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc);

        // Assert
        Assert.IsTrue(firstInstantIsStart);
        Assert.IsFalse(firstInstantIsEnd);
    }

    [TestMethod]
    [DynamicData(nameof(FullTimelineTestData))]
    public void FullTimeline_IsInstant_Twice(Period period1, Period period2)
    {
        // As MergeStartTimeline retains state, we call the methods reading the timeline twice to make sure it remains consistent.
        // Arrange
        var mergedPeriodTimeline = period1.Merge(period2);

        // Act
        mergedPeriodTimeline.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc);
        var firstInstantIsStart = mergedPeriodTimeline.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc);

        mergedPeriodTimeline.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc);
        var firstInstantIsEnd = mergedPeriodTimeline.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc);

        // Assert
        Assert.IsTrue(firstInstantIsStart);
        Assert.IsFalse(firstInstantIsEnd);
    }

    public static IEnumerable<object?[]> FullTimelineTestData()
    {
        yield return [Period.Create(null, new(42L, DateTimeKind.Utc)), Period.Create(new(41L, DateTimeKind.Utc), null)];
        yield return [Period.Create(new(41L, DateTimeKind.Utc), null), Period.Create(null, new(42L, DateTimeKind.Utc))];
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Merge.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineMergeTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineMergeTests)}."),
                tc.Periods ?? throw new InvalidOperationException(
                    $"{nameof(tc.Periods)} of null is not supported in {nameof(PeriodTimelineMergeTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineMergeTests)}.")
            })).ToArray();
    }
}