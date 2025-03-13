using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineSampleAtTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void SampleAt(string source, string instant, string expected, string expectedType)
    {
        Console.WriteLine($"Source:          \"{source}\"");
        Console.WriteLine($"Instant:         \"{instant}\"");
        Console.WriteLine($"Expected Period: \"{expected}\"");
        Console.WriteLine($"Expected Type:   \"{expectedType}\"");

        var expectingPeriod = false;
        switch (expectedType)
        {
            case "gap":
                expectingPeriod = false;
                break;
            case "period":
                expectingPeriod = true;
                break;
            default:
                Assert.Fail("Expected Type should be either gap or period.");
                break;
        }

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);
        var parsedInstant = helper.GetSingleInstant(instant);
        var expectedPeriod = helper.GetSinglePeriod(expected);

        // Act
        var sample = periodTimeline.SampleAt(parsedInstant);

        // Assert
        Console.WriteLine($"Actual Type:     \"{(sample.IsPeriod ? "period" : "gap")}\"");
        if (expectingPeriod)
        {
            Assert.IsTrue(sample.IsPeriod);
            Assert.AreEqual(sample.Period, expectedPeriod);

            Console.WriteLine($"Actual period:   \"{(sample.Period.IsInfiniteInBothDirections ? 
                new string(' ', source.Length) : 
                helper.PeriodTimelineToString(sample.Period.AsPeriodTimeline(), source.Length, TimelineMethods.IsInstant))}\"");
        }
        else
        {
            Assert.IsTrue(sample.IsGap);
            Assert.AreEqual(sample.Gap, expectedPeriod);

            Console.WriteLine($"Actual gap:      \"{(sample.Gap.IsInfiniteInBothDirections ?
                new string(' ', source.Length) : 
                helper.PeriodTimelineToString(sample.Gap.AsPeriodTimeline(), source.Length, TimelineMethods.IsInstant))}\"");
        }
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.SampleAt.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineSampleAtTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineSampleAtTests)}."),
            tc.Instant ?? throw new InvalidOperationException(
                $"{nameof(tc.Instant)} of null is not supported in {nameof(PeriodTimelineSampleAtTests)}."),
            tc.ExpectedPeriod ?? throw new InvalidOperationException(
                $"{nameof(tc.ExpectedPeriod)} of null is not supported in {nameof(PeriodTimelineSampleAtTests)}."),
            tc.ExpectedType ?? throw new InvalidOperationException(
                $"{nameof(tc.ExpectedType)} of null is not supported in {nameof(PeriodTimelineSampleAtTests)}."),
        }).ToArray();
    }
}