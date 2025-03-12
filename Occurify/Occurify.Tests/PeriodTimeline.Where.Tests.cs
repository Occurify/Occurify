using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineWhereTests
{
    [DataTestMethod]
    [DynamicData(nameof(LargerThan2TestCaseSource), DynamicDataSourceType.Method)]
    public void LargerThan2_GetPreviousUtcInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, p => p.Duration == null || p.Duration > TimeSpan.FromTicks(2), expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(LargerThan2TestCaseSource), DynamicDataSourceType.Method)]
    public void LargerThan2_GetNextUtcInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, p => p.Duration == null || p.Duration > TimeSpan.FromTicks(2), expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(LargerThan2TestCaseSource), DynamicDataSourceType.Method)]
    public void LargerThan2_IsInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, p => p.Duration == null || p.Duration > TimeSpan.FromTicks(2), expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(SmallerThan2TestCaseSource), DynamicDataSourceType.Method)]
    public void SmallerThan2_GetPreviousUtcInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, p => p.Duration < TimeSpan.FromTicks(2), expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(SmallerThan2TestCaseSource), DynamicDataSourceType.Method)]
    public void SmallerThan2_GetNextUtcInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, p => p.Duration < TimeSpan.FromTicks(2), expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(SmallerThan2TestCaseSource), DynamicDataSourceType.Method)]
    public void SmallerThan2_IsInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, p => p.Duration < TimeSpan.FromTicks(2), expected);
    }

    private void ExecuteTest(TimelineMethods method, string source, Func<Period, bool> predicate, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);

        // Act
        var cutPeriodTimeline = periodTimeline.WherePeriods(predicate);

        // Assert
        var actual = helper.PeriodTimelineToString(cutPeriodTimeline, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> LargerThan2TestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.LargerThan2?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineWhereTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineWhereTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.LargerThan2)} of null is not supported in {nameof(PeriodTimelineWhereTests)}.");
    }

    private static IEnumerable<object[]> SmallerThan2TestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.SmallerThan2?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineWhereTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineWhereTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.SmallerThan2)} of null is not supported in {nameof(PeriodTimelineWhereTests)}.");
    }

    private static PeriodTimelineWhereTestCases ReadTestCases()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Where.json");
        var json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<PeriodTimelineWhereTestCases>(json) ??
               throw new InvalidOperationException("Was unable to load test cases.");
    }
}