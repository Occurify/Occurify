using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class TimelineSkipWithinTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void GetPreviousUtcInstant(string source, string periods, int skipCount, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, periods, skipCount, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void GetNextUtcInstant(string source, string periods, int skipCount, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, periods, skipCount, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void IsInstant(string source, string periods, int skipCount, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, periods, skipCount, expected);
    }

    private void ExecuteTest(TimelineMethods method, string source, string periods, int skipCount, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Periods:  \"{periods}\"");
        Console.WriteLine($"Skip:     \"{skipCount}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var timeline = helper.CreateTimeline(source);
        var periodTimeline = helper.CreatePeriodTimeline(periods);

        // Act
        var result = timeline.SkipWithin(periodTimeline, skipCount);

        // Assert
        var actual = helper.TimelineToString(result, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/Timeline.SkipWithin.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<TimelineSkipWithinTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineSkipWithinTests)}."),
                tc.Periods ?? throw new InvalidOperationException(
                    $"{nameof(tc.Periods)} of null is not supported in {nameof(TimelineSkipWithinTests)}."),
                tc.Skip ?? throw new InvalidOperationException(
                    $"{nameof(tc.Skip)} of null is not supported in {nameof(TimelineSkipWithinTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineSkipWithinTests)}.")
            })).ToArray();
    }
}