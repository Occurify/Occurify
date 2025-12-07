using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineIntersectTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetPreviousUtcInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, periods , expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetNextUtcInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, periods , expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void IsInstant(string source, string periods, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, periods , expected);
    }
        
    private void ExecuteTest(TimelineMethods method, string source, string periods, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Periods:  \"{periods}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var parsedPeriods = helper.CreatePeriodTimeline(source  );
        var periodsToIntersect = helper.CreatePeriodTimeline(periods );

        // Act
        var intersections = parsedPeriods.IntersectPeriods(periodsToIntersect);

        // Assert
        var actualPeriodTimeline = helper.PeriodTimelineToString(intersections, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actualPeriodTimeline}\"");
        Assert.AreEqual(expected, actualPeriodTimeline);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Intersect.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineIntersectTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineIntersectTests)}."),
                tc.Periods ?? throw new InvalidOperationException(
                    $"{nameof(tc.Periods)} of null is not supported in {nameof(PeriodTimelineIntersectTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineIntersectTests)}.")
            })).ToArray();
    }
}