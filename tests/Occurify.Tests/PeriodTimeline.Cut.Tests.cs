using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineCutTests
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
        
    private void ExecuteTest(TimelineMethods method, string source, string instants, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Instants: \"{instants}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);
        var cutTimeline = helper.CreateTimeline(instants);

        // Act
        var cutPeriodTimeline = periodTimeline.Cut(cutTimeline);

        // Assert
        var actual = helper.PeriodTimelineToString(cutPeriodTimeline, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Cut.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineCutTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineCutTests)}."),
            tc.Instants ?? throw new InvalidOperationException(
                $"{nameof(tc.Instants)} of null is not supported in {nameof(PeriodTimelineCutTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineCutTests)}.")
        }).ToArray();
    }
}