using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineFromPeriodsTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void AsPeriodTimeline_GetPreviousUtcInstant(string[] periods, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, periods, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void AsPeriodTimeline_GetNextUtcInstant(string[] periods, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, periods, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void AsPeriodTimeline_IsInstant(string[] periods, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, periods, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Merge_GetPreviousUtcInstant(string[] periods, string expected)
    {
        Execute_MergingGivesSameResult(TimelineMethods.GetPreviousUtcInstant, periods, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Merge_GetNextUtcInstant(string[] periods, string expected)
    {
        Execute_MergingGivesSameResult(TimelineMethods.GetNextUtcInstant, periods, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Merge_IsInstant(string[] periods, string expected)
    {
        Execute_MergingGivesSameResult(TimelineMethods.IsInstant, periods, expected);
    }

    private void ExecuteTest(TimelineMethods method, string[] periods, string expected)
    {
        Console.WriteLine($"Periods:  \"{periods.FirstOrDefault() ?? ""}\"");
        foreach (var period in periods.Skip(1))
        {
            Console.WriteLine($"          \"{period}\"");
        }
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var parsedPeriods = periods.Select(helper.GetSinglePeriod);

        // Act
        var periodTimeline = parsedPeriods.AsPeriodTimeline();

        // Assert
        var actualPeriodTimeline = helper.PeriodTimelineToString(periodTimeline, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actualPeriodTimeline}\"");
        Assert.AreEqual(expected, actualPeriodTimeline);
    }

    private void Execute_MergingGivesSameResult(TimelineMethods method, string[] periods, string expected)
    {
        Console.WriteLine($"Periods:  \"{periods.FirstOrDefault() ?? ""}\"");
        foreach (var period in periods.Skip(1))
        {
            Console.WriteLine($"          \"{period}\"");
        }
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var parsedPeriods = periods.Select(helper.GetSinglePeriod).ToArray();

        if (parsedPeriods.Any(p => p.IsInfiniteInBothDirections))
        {
            // This indeed means that there is slightly different behaviour in the PeriodProvider.Merge method: It does not throw an exception when resulting in an infinite period but rather returns a period that starts at MinValue and never ends.
            // This is by design, as it prevents unexpected behaviour when using Merge. As a future improvement, methods like Merge, Stitch and possibly Invert should have the option to also throw an exception instead.
            return;
        }
        if (!parsedPeriods.Any())
        {
            return;
        }

        // Act
        var periodTimeline = parsedPeriods.Select(pp => pp.AsPeriodTimeline()).Merge();

        // Assert
        var actualPeriodTimeline = helper.PeriodTimelineToString(periodTimeline, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actualPeriodTimeline}\"");
        Assert.AreEqual(expected, actualPeriodTimeline);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.FromPeriods.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineFromPeriodsTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Periods ?? throw new InvalidOperationException(
                    $"{nameof(tc.Periods)} of null is not supported in {nameof(PeriodTimelineFromPeriodsTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineFromPeriodsTests)}.")
            })).ToArray();
    }
}