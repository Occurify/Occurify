using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineSubtractTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void GetPreviousUtcInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, periods, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void GetNextUtcInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, periods, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void IsInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, periods, expected);
    }
        
    private void ExecuteTest(TimelineMethods method, string source, string periods, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Period:   \"{periods}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimelineMinuend = helper.CreatePeriodTimeline(source);
        var periodTimelineSubtrahend = helper.CreatePeriodTimeline(periods);

        // Act
        var subtractedPeriodTimeline = periodTimelineMinuend.Subtract(periodTimelineSubtrahend);

        // Assert
        var actual = helper.PeriodTimelineToString(subtractedPeriodTimeline, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Subtract.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineSubtractTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineSubtractTests)}."),
                tc.Periods ?? throw new InvalidOperationException(
                    $"{nameof(tc.Periods)} of null is not supported in {nameof(PeriodTimelineSubtractTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineSubtractTests)}.")
            })).ToArray();
    }
}