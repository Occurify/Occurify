using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class TimelineTakeLastWithinTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetPreviousUtcInstant(string source, string periods, int takeCount, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, periods, takeCount, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetNextUtcInstant(string source, string periods, int takeCount, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, periods, takeCount, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void IsInstant(string source, string periods, int takeCount, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, periods, takeCount, expected);
    }

    private void ExecuteTest(TimelineMethods method, string source, string periods, int takeCount, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Periods:  \"{periods}\"");
        Console.WriteLine($"Take:     \"{takeCount}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var timeline = helper.CreateTimeline(source);
        var periodTimeline = helper.CreatePeriodTimeline(periods);

        // Act
        var result = timeline.TakeLastWithin(periodTimeline, takeCount);

        // Assert
        var actual = helper.TimelineToString(result, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/Timeline.TakeLastWithin.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<TimelineTakeWithinTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineTakeLastWithinTests)}."),
                tc.Periods ?? throw new InvalidOperationException(
                    $"{nameof(tc.Periods)} of null is not supported in {nameof(TimelineTakeLastWithinTests)}."),
                tc.Take ?? throw new InvalidOperationException(
                    $"{nameof(tc.Take)} of null is not supported in {nameof(TimelineTakeLastWithinTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineTakeLastWithinTests)}.")
            })).ToArray();
    }
}