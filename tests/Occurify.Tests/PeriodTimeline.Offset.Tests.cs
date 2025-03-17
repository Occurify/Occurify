using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineOffsetTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void GetPreviousUtcInstant(string source, int offset, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, offset, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void GetNextUtcInstant(string source, int offset, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, offset, expected);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void IsInstant(string source, int offset, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, offset, expected);
    }

    private void ExecuteTest(TimelineMethods method, string source, int offset, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);

        // Act
        var offsetPeriodTimeline = periodTimeline.OffsetTicks(offset);

        // Assert
        var actual = helper.PeriodTimelineToString(offsetPeriodTimeline, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Offset.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineOffsetTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineOffsetTests)}."),
            tc.Offset ?? throw new InvalidOperationException(
                $"{nameof(tc.Offset)} of null is not supported in {nameof(PeriodTimelineOffsetTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineOffsetTests)}.")
        }).ToArray();
    }
}