using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class TimelineWithoutTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetPreviousUtcInstant(string source, string instants, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, instants, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetNextUtcInstant(string source, string instants, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, instants, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void IsInstant(string source, string instants, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, instants, expected);
    }

    private void ExecuteTest(TimelineMethods method, string source, string instants, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Periods:  \"{instants}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var timeline = helper.CreateTimeline(source);
        var instantsToExclude = helper.CreateTimeline(instants);

        // Act
        var result = timeline.Without(instantsToExclude);

        // Assert
        var actual = helper.TimelineToString(result, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/Timeline.Without.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<TimelineWithoutTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineWithoutTests)}."),
            tc.Instants ?? throw new InvalidOperationException(
                $"{nameof(tc.Instants)} of null is not supported in {nameof(TimelineWithoutTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineWithoutTests)}.")
        }).ToArray();
    }
}