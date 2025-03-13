using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class TimelineOutsideTests
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
        Console.WriteLine($"Periods:  \"{periods}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var sourceTimeline = helper.CreateTimeline(source);
        var periodTimeline = helper.CreatePeriodTimeline(periods);

        // Act
        var result = sourceTimeline.Outside(periodTimeline);

        // Assert
        var actual = helper.TimelineToString(result, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/Timeline.Outside.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<TimelineOutsideTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases => cases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineOutsideTests)}."),
            tc.Period ?? throw new InvalidOperationException(
                $"{nameof(tc.Period)} of null is not supported in {nameof(TimelineOutsideTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineOutsideTests)}.")
        })).ToArray();
    }
}