using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineEnumerateTests
{
    [TestMethod]
    [DynamicData(nameof(EnumerateTestCaseSource))]
    public void Enumerate(string source, string expected)
    {
        ExecuteEnumerateTest(source, expected, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateTestCaseSource))]
    public void EnumerateBackwards(string source, string expected)
    {
        ExecuteEnumerateTest(source, expected, true);
    }

    private void ExecuteEnumerateTest(string source, string expected, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var period = helper.CreatePeriodTimeline(source);

        // Act
        var resultingArray = backwards ? period.EnumerateBackwards().ToArray() : period.ToArray();

        // Assert
        var resultingPeriodTimeline = resultingArray.AsPeriodTimeline();
        var actualPeriodsTimeline = helper.PeriodTimelineToString(resultingPeriodTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);
        
        Console.WriteLine($"Actual:   \"{actualPeriodsTimeline}\"");
        Assert.AreEqual(expected, actualPeriodsTimeline);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateFromTestCaseSource))]
    public void EnumerateFrom(string source, string from, string expected)
    {
        ExecuteEnumerateFrom(source, from, expected, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateFromTestCaseSource))]
    public void EnumerateBackwardsTo(string source, string from, string expected)
    {
        ExecuteEnumerateFrom(source, from, expected, true);
    }

    private void ExecuteEnumerateFrom(string source, string from, string expected, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"From:     \"{from}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var period = helper.CreatePeriodTimeline(source);
        var instant = helper.GetSingleInstant(from);

        // Act
        var resultingArray = backwards ? period.EnumerateBackwardsTo(instant).ToArray() : period.EnumerateFrom(instant).ToArray();

        // Assert
        var resultingPeriodTimeline = resultingArray.AsPeriodTimeline();
        var actualPeriodsTimeline = helper.PeriodTimelineToString(resultingPeriodTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualPeriodsTimeline}\"");
        Assert.AreEqual(expected, actualPeriodsTimeline);
    }

    [TestMethod] [DynamicData(nameof(EnumerateFromIncludingPartialTestCaseSource))]
    public void EnumerateFromIncludingPartial(string source, string from, string expected)
    {
        ExecuteEnumerateFromIncludingPartial(source, from, expected, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateFromIncludingPartialTestCaseSource))]
    public void EnumerateBackwardsToIncludingPartial(string source, string from, string expected)
    {
        ExecuteEnumerateFromIncludingPartial(source, from, expected, true);
    }

    private void ExecuteEnumerateFromIncludingPartial(string source, string from, string expected, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"From:     \"{from}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var period = helper.CreatePeriodTimeline(source);
        var instant = helper.GetSingleInstant(from);

        // Act
        var resultingArray = backwards ? period.EnumerateBackwardsToIncludingPartial(instant).ToArray() : period.EnumerateFromIncludingPartial(instant).ToArray();

        // Assert
        var resultingPeriodTimeline = resultingArray.AsPeriodTimeline();
        var actualPeriodsTimeline = helper.PeriodTimelineToString(resultingPeriodTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualPeriodsTimeline}\"");
        Assert.AreEqual(expected, actualPeriodsTimeline);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateToTestCaseSource))]
    public void EnumerateTo(string source, string to, string expected)
    {
        ExecuteEnumerateTo(source, to, expected, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateToTestCaseSource))]
    public void EnumerateBackwardsFrom(string source, string to, string expected)
    {
        ExecuteEnumerateTo(source, to, expected, true);
    }

    private void ExecuteEnumerateTo(string source, string to, string expected, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"To:       \"{to}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var period = helper.CreatePeriodTimeline(source);
        var instant = helper.GetSingleInstant(to);

        // Act
        var resultingArray = backwards ? period.EnumerateBackwardsFrom(instant).ToArray() : period.EnumerateTo(instant).ToArray();
        
        // Assert
        var resultingPeriodTimeline = resultingArray.AsPeriodTimeline();
        var actualPeriodsTimeline = helper.PeriodTimelineToString(resultingPeriodTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualPeriodsTimeline}\"");
        Assert.AreEqual(expected, actualPeriodsTimeline);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateToIncludingPartialTestCaseSource))]
    public void EnumerateToIncludingPartial(string source, string from, string expected)
    {
        ExecuteEnumerateToIncludingPartial(source, from, expected, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateToIncludingPartialTestCaseSource))]
    public void EnumerateBackwardsFromIncludingPartial(string source, string from, string expected)
    {
        ExecuteEnumerateToIncludingPartial(source, from, expected, true);
    }

    private void ExecuteEnumerateToIncludingPartial(string source, string from, string expected, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"To:       \"{from}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var period = helper.CreatePeriodTimeline(source);
        var instant = helper.GetSingleInstant(from);

        // Act
        var resultingArray = backwards ? period.EnumerateBackwardsFromIncludingPartial(instant).ToArray() : period.EnumerateToIncludingPartial(instant).ToArray();

        // Assert
        var resultingPeriodTimeline = resultingArray.AsPeriodTimeline();
        var actualPeriodsTimeline = helper.PeriodTimelineToString(resultingPeriodTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualPeriodsTimeline}\"");
        Assert.AreEqual(expected, actualPeriodsTimeline);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateRangeCompleteOnlyTestCaseSource))]
    public void EnumerateRangeCompleteOnly(string source, string range, string expected)
    {
        ExecuteEnumerateRange(source, range, expected, PeriodIncludeOptions.CompleteOnly, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateRangeCompleteOnlyTestCaseSource))]
    public void EnumerateRangeBackwardsCompleteOnly(string source, string range, string expected)
    {
        ExecuteEnumerateRange(source, range, expected, PeriodIncludeOptions.CompleteOnly, true);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateRangeStartPartialAllowedTestCaseSource))]
    public void EnumerateRangeStartPartialAllowed(string source, string range, string expected)
    {
        ExecuteEnumerateRange(source, range, expected, PeriodIncludeOptions.StartPartialAllowed, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateRangeStartPartialAllowedTestCaseSource))]
    public void EnumerateRangeBackwardsStartPartialAllowed(string source, string range, string expected)
    {
        ExecuteEnumerateRange(source, range, expected, PeriodIncludeOptions.StartPartialAllowed, true);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateRangeEndPartialAllowedTestCaseSource))]
    public void EnumerateRangeEndPartialAllowed(string source, string range, string expected)
    {
        ExecuteEnumerateRange(source, range, expected, PeriodIncludeOptions.EndPartialAllowed, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateRangeEndPartialAllowedTestCaseSource))]
    public void EnumerateRangeBackwardsEndPartialAllowed(string source, string range, string expected)
    {
        ExecuteEnumerateRange(source, range, expected, PeriodIncludeOptions.EndPartialAllowed, true);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateRangePartialAllowedTestCaseSource))]
    public void EnumerateRangePartialAllowed(string source, string range, string expected)
    {
        ExecuteEnumerateRange(source, range, expected, PeriodIncludeOptions.PartialAllowed, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateRangePartialAllowedTestCaseSource))]
    public void EnumerateRangeBackwardsPartialAllowed(string source, string range, string expected)
    {
        ExecuteEnumerateRange(source, range, expected, PeriodIncludeOptions.PartialAllowed, true);
    }

    private void ExecuteEnumerateRange(string source, string range, string expected, PeriodIncludeOptions periodIncludeOption, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Range:    \"{range}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var period = helper.CreatePeriodTimeline(source);
        var instants = helper.GetInstants(range).ToArray();
        if (instants.Length != 2)
        {
            Assert.Fail("Two instants expected for range.");
        }

        var rangeStart = instants.First();
        var rangeEnd = instants.Last();

        // Act
        var resultingArray = backwards ? period.EnumerateRangeBackwards(rangeStart, rangeEnd, periodIncludeOption).ToArray() : period.EnumerateRange(rangeStart, rangeEnd, periodIncludeOption).ToArray();

        //Assert
        var resultingPeriodTimeline = resultingArray.AsPeriodTimeline();
        var actualPeriodsTimeline = helper.PeriodTimelineToString(resultingPeriodTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualPeriodsTimeline}\"");
        Assert.AreEqual(expected, actualPeriodsTimeline);
    }

    [TestMethod]
    [DynamicData(nameof(EnumeratePeriodCompleteOnlyTestCaseSource))]
    public void EnumeratePeriodCompleteOnly(string source, string range, string expected)
    {
        ExecuteEnumeratePeriod(source, range, expected, PeriodIncludeOptions.CompleteOnly, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumeratePeriodCompleteOnlyTestCaseSource))]
    public void EnumeratePeriodBackwardsCompleteOnly(string source, string range, string expected)
    {
        ExecuteEnumeratePeriod(source, range, expected, PeriodIncludeOptions.CompleteOnly, true);
    }

    [TestMethod]
    [DynamicData(nameof(EnumeratePeriodStartPartialAllowedTestCaseSource))]
    public void EnumeratePeriodStartPartialAllowed(string source, string period, string expected)
    {
        ExecuteEnumeratePeriod(source, period, expected, PeriodIncludeOptions.StartPartialAllowed, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumeratePeriodStartPartialAllowedTestCaseSource))]
    public void EnumeratePeriodBackwardsStartPartialAllowed(string source, string period, string expected)
    {
        ExecuteEnumeratePeriod(source, period, expected, PeriodIncludeOptions.StartPartialAllowed, true);
    }

    [TestMethod]
    [DynamicData(nameof(EnumeratePeriodEndPartialAllowedTestCaseSource))]
    public void EnumeratePeriodEndPartialAllowed(string source, string period, string expected)
    {
        ExecuteEnumeratePeriod(source, period, expected, PeriodIncludeOptions.EndPartialAllowed, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumeratePeriodEndPartialAllowedTestCaseSource))]
    public void EnumeratePeriodBackwardsEndPartialAllowed(string source, string period, string expected)
    {
        ExecuteEnumeratePeriod(source, period, expected, PeriodIncludeOptions.EndPartialAllowed, true);
    }

    [TestMethod]
    [DynamicData(nameof(EnumeratePeriodPartialAllowedTestCaseSource))]
    public void EnumeratePeriodPartialAllowed(string source, string period, string expected)
    {
        ExecuteEnumeratePeriod(source, period, expected, PeriodIncludeOptions.PartialAllowed, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumeratePeriodPartialAllowedTestCaseSource))]
    public void EnumeratePeriodBackwardsPartialAllowed(string source, string period, string expected)
    {
        ExecuteEnumeratePeriod(source, period, expected, PeriodIncludeOptions.PartialAllowed, true);
    }

    private void ExecuteEnumeratePeriod(string source, string period, string expected, PeriodIncludeOptions periodIncludeOption, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Period:   \"{period}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);
        var parsedPeriod = helper.GetSinglePeriod(period);

        // Act
        var resultingArray = backwards ? periodTimeline.EnumeratePeriodBackwards(parsedPeriod, periodIncludeOption).ToArray() : periodTimeline.EnumeratePeriod(parsedPeriod, periodIncludeOption).ToArray();

        // Assert
        var resultingPeriodTimeline = resultingArray.AsPeriodTimeline();
        var actualPeriodsTimeline = helper.PeriodTimelineToString(resultingPeriodTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualPeriodsTimeline}\"");
        Assert.AreEqual(expected, actualPeriodsTimeline);
    }

    private static IEnumerable<object[]> EnumerateTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.Enumerate?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateFromTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateFrom?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.From ?? throw new InvalidOperationException(
                $"{nameof(tc.From)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateFromIncludingPartialTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateFromIncludingPartial?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.From ?? throw new InvalidOperationException(
                $"{nameof(tc.From)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateToTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateTo?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.To ?? throw new InvalidOperationException(
                $"{nameof(tc.To)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateToIncludingPartialTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateToIncludingPartial?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.To ?? throw new InvalidOperationException(
                $"{nameof(tc.To)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateRangeCompleteOnlyTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateRangeCompleteOnly?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Range ?? throw new InvalidOperationException(
                $"{nameof(tc.Range)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateRangeStartPartialAllowedTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateRangeStartPartialAllowed?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Range ?? throw new InvalidOperationException(
                $"{nameof(tc.Range)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateRangeEndPartialAllowedTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateRangeEndPartialAllowed?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Range ?? throw new InvalidOperationException(
                $"{nameof(tc.Range)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateRangePartialAllowedTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateRangePartialAllowed?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Range ?? throw new InvalidOperationException(
                $"{nameof(tc.Range)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumeratePeriodCompleteOnlyTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumeratePeriodCompleteOnly?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Period ?? throw new InvalidOperationException(
                $"{nameof(tc.Period)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumeratePeriodStartPartialAllowedTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumeratePeriodStartPartialAllowed?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Period ?? throw new InvalidOperationException(
                $"{nameof(tc.Period)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumeratePeriodEndPartialAllowedTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumeratePeriodEndPartialAllowed?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Period ?? throw new InvalidOperationException(
                $"{nameof(tc.Period)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumeratePeriodPartialAllowedTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumeratePeriodPartialAllowed?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Period ?? throw new InvalidOperationException(
                $"{nameof(tc.Period)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(PeriodTimelineEnumerateTests)}.");
    }

    private static PeriodTimelineEnumerateTestCases ReadTestCases()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Enumerate.json");
        var json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<PeriodTimelineEnumerateTestCases>(json) ??
               throw new InvalidOperationException("Was unable to load test cases.");
    }
}