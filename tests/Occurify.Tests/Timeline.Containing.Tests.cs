using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class TimelineContainingTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void GetPreviousUtcInstant(string source, string instants, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, instants, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void GetNextUtcInstant(string source, string instants, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, instants, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void IsInstant(string source, string instants, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, instants, expected);
    }

    private void ExecuteTest(TimelineMethods method, string source, string instants, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Instants: \"{instants}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var timeline = helper.CreateTimeline(source);
        var instantsTimeline = helper.CreateTimeline(instants);

        // Act
        var result = timeline.Containing(instantsTimeline);

        // Assert
        var actual = helper.TimelineToString(result, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/Timeline.Containing.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<TimelineContainingTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Timeline ?? throw new InvalidOperationException(
                $"{nameof(tc.Timeline)} of null is not supported in {nameof(TimelineContainingTests)}."),
            tc.Instants ?? throw new InvalidOperationException(
                $"{nameof(tc.Instants)} of null is not supported in {nameof(TimelineContainingTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineContainingTests)}.")
        }).ToArray();
    }
}