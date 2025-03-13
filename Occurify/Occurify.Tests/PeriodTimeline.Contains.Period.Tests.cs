using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.Helpers;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineContainsPeriodTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Contains(string source, string periods, bool expected)
    {
        ExecuteTest(source, periods, expected, DateTime.UtcNow);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Contains_StartAtMinValue(string source, string periods, bool expected)
    {
        ExecuteTest(source, periods, expected, DateTimeHelper.MinValueUtc);
    }

    public void ExecuteTest(string source, string periods, bool expected, DateTime origin)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Period:   \"{periods}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper(origin);

        var periodTimeline = helper.CreatePeriodTimeline(source);
        var period = helper.GetSinglePeriod(periods);

        // Act
        var actual = periodTimeline.ContainsPeriod(period);

        // Assert
        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Contains.Period.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineContainsPeriodTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineContainsPeriodTests)}."),
                tc.Period ?? throw new InvalidOperationException(
                    $"{nameof(tc.Period)} of null is not supported in {nameof(PeriodTimelineContainsPeriodTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineContainsPeriodTests)}.")
            })).ToArray();
    }
}