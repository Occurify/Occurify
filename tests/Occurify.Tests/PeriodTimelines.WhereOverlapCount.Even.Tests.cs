using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelinesWhereOverlapCountEvenTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void WhereOverlapCountEven_GetPreviousUtcInstant(string[] source, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void WhereOverlapCountEven_GetNextUtcInstant(string[] source, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void WhereOverlapCountEven_IsInstant(string[] periods, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, periods, expected);
    }

    private void ExecuteTest(TimelineMethods method, string[] source, string expected)
    {
        Console.WriteLine($"Source:   \"{source.FirstOrDefault() ?? ""}\"");
        foreach (var period in source.Skip(1))
        {
            Console.WriteLine($"          \"{period}\"");
        }
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimelines = source.Select(p => helper.CreatePeriodTimeline(p));

        // Act
        var result = periodTimelines.WhereOverlapCount(i => i > 0 && i % 2 == 0);

        // Assert
        var actualPeriodTimeline = helper.PeriodTimelineToString(result, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actualPeriodTimeline}\"");
        Assert.AreEqual(expected, actualPeriodTimeline);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimelines.WhereOverlapCount.Even.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelinesWhereOverlapTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelinesWhereOverlapCountEvenTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelinesWhereOverlapCountEvenTests)}.")
            })).ToArray();
    }
}