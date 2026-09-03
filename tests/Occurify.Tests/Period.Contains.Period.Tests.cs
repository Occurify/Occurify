using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodContainsPeriodTests
{
    [TestMethod]
    [DynamicData(nameof(CompleteOnlyTestCaseSource))]
    public void Contains_CompleteOnly(string period, string otherPeriod, bool expected)
    {
        ExecuteTest(period, otherPeriod, PeriodIncludeOptions.CompleteOnly, expected);
    }

    [TestMethod]
    [DynamicData(nameof(StartPartialAllowedTestCaseSource))]
    public void Contains_StartPartialAllowed(string period, string otherPeriod, bool expected)
    {
        ExecuteTest(period, otherPeriod, PeriodIncludeOptions.StartPartialAllowed, expected);
    }

    [TestMethod]
    [DynamicData(nameof(EndPartialAllowedTestCaseSource))]
    public void Contains_EndPartialAllowed(string period, string otherPeriod, bool expected)
    {
        ExecuteTest(period, otherPeriod, PeriodIncludeOptions.EndPartialAllowed, expected);
    }

    [TestMethod]
    [DynamicData(nameof(PartialAllowedTestCaseSource))]
    public void Contains_PartialAllowed(string period, string otherPeriod, bool expected)
    {
        ExecuteTest(period, otherPeriod, PeriodIncludeOptions.PartialAllowed, expected);
    }

    public void ExecuteTest(string period, string otherPeriod, PeriodIncludeOptions periodIncludeOption, bool expected)
    {
        Console.WriteLine($"Period1:  \"{period}\"");
        Console.WriteLine($"Period2:  \"{otherPeriod}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        var helper = new StringTimelineHelper();

        var parsedPeriod = helper.GetSinglePeriod(period);

        var parsedOtherPeriod = helper.GetSinglePeriod(otherPeriod);

        var actual = parsedPeriod.ContainsPeriod(parsedOtherPeriod, periodIncludeOption);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> CompleteOnlyTestCaseSource() => ParseTestCases("TestCases/Period.Contains.Period.CompleteOnly.json");
    private static IEnumerable<object[]> StartPartialAllowedTestCaseSource() => ParseTestCases("TestCases/Period.Contains.Period.StartPartialAllowed.json");
    private static IEnumerable<object[]> EndPartialAllowedTestCaseSource() => ParseTestCases("TestCases/Period.Contains.Period.EndPartialAllowed.json");
    private static IEnumerable<object[]> PartialAllowedTestCaseSource() => ParseTestCases("TestCases/Period.Contains.Period.PartialAllowed.json");

    private static IEnumerable<object[]> ParseTestCases(string file)
    {
        using var r = new StreamReader(file);
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodContainsPeriodTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Period1 ?? throw new InvalidOperationException(
                    $"{nameof(tc.Period1)} of null is not supported in {nameof(PeriodContainsPeriodTests)}."),
                tc.Period2 ?? throw new InvalidOperationException(
                    $"{nameof(tc.Period2)} of null is not supported in {nameof(PeriodContainsPeriodTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodContainsPeriodTests)}.")
            })).ToArray();
    }
}