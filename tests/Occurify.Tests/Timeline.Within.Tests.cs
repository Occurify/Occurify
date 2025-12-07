using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class TimelineWithinTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetPreviousUtcInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, periods, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetNextUtcInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, periods, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
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

        var timeline = helper.CreateTimeline(source);
        var periodTimeline = helper.CreatePeriodTimeline(periods);

        // Act
        var withinTimeline = timeline.Within(periodTimeline);

        // Assert
        var actual = helper.TimelineToString(withinTimeline, expected.Length, method);
        
        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/Timeline.Within.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<TimelineWithinTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineWithinTests)}."),
                tc.Periods ?? throw new InvalidOperationException(
                    $"{nameof(tc.Periods)} of null is not supported in {nameof(TimelineWithinTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineWithinTests)}.")
            })).ToArray();
    }
}