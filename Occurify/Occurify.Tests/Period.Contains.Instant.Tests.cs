using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodContainsInstantTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void Contains(string period, string instant, bool expected)
    {
        Console.WriteLine($"Periods:  \"{period}\"");
        Console.WriteLine($"Instant:  \"{instant}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        var helper = new StringTimelineHelper();

        var parsedPeriod = helper.GetSinglePeriod(period);

        var parsedInstant = helper.GetSingleInstant(instant);

        var actual = parsedPeriod.ContainsInstant(parsedInstant);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/Period.Contains.Instant.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodContainsInstantTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodContainsPeriodTests)}."),
            tc.Instant ?? throw new InvalidOperationException(
                $"{nameof(tc.Instant)} of null is not supported in {nameof(PeriodContainsPeriodTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodContainsPeriodTests)}.")
        }).ToArray();
    }
}