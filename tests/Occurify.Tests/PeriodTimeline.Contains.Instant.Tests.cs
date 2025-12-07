using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineContainsInstantTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void Contains(string source, string instant, bool expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Instant:  \"{instant}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);
        var parsedInstant = helper.GetSingleInstant(instant);

        // Act
        var actual = periodTimeline.ContainsInstant(parsedInstant);

        // Assert
        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Contains.Instant.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineContainsInstantTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineContainsInstantTests)}."),
            tc.Instant ?? throw new InvalidOperationException(
                $"{nameof(tc.Instant)} of null is not supported in {nameof(PeriodTimelineContainsInstantTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineContainsInstantTests)}.")
        }).ToArray();
    }
}