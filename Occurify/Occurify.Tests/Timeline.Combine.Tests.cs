using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class TimelineCombineTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void GetPreviousUtcInstant(string[] sources, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, sources, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void GetNextUtcInstant(string[] sources, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, sources, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void IsInstant(string[] sources, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, sources, expected);
    }

    private void ExecuteTest(TimelineMethods method, string[] sources, string expected)
    {
        if (sources.Length <= 1)
        {
            Assert.Fail("Combine requires at least two timelines.");
        }

        for (var index = 0; index < sources.Length; index++)
        {
            var timeline = sources[index];
            Console.WriteLine($"Timeline {index + 1}: \"{timeline}\"");
        }
        Console.WriteLine($"Expected:   \"{expected}\"");
            
        // Arrange
        var helper = new StringTimelineHelper();

        var initialTimeline = helper.CreateTimeline(sources.First());
        var timelinesToCombineWith =
            sources.Skip(1).Select(helper.CreateTimeline);

        // Act
        var combinedTimeline = initialTimeline.Combine(timelinesToCombineWith);

        // Assert
        var actual = helper.TimelineToString(combinedTimeline,
            expected.Length, method);
            
        Console.WriteLine($"Actual:     \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/Timeline.Combine.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<TimelineCombineTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineCombineTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineCombineTests)}.")
        }).ToArray();
    }
}