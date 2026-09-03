using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class TimelineEnumerateTests
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

        var timeline = helper.CreateTimeline(source);

        // Act
        var resultingArray = backwards ? timeline.EnumerateBackwards().ToArray() : timeline.ToArray();

        // Assert
        var resultingTimeline = resultingArray.AsTimeline();
        var actualInstantsTimeline =
            helper.TimelineToString(resultingTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualInstantsTimeline}\"");
        Assert.AreEqual(expected, actualInstantsTimeline);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateFromTestCaseSource))]
    public void EnumerateFrom(string source, string fromInstant, string expected)
    {
        ExecuteEnumerateFrom(source, fromInstant, expected, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateFromTestCaseSource))]
    public void EnumerateBackwardsTo(string source, string fromInstant, string expected)
    {
        ExecuteEnumerateFrom(source, fromInstant, expected, true);
    }

    private void ExecuteEnumerateFrom(string source, string fromInstant, string expected, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"From:     \"{fromInstant}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreateTimeline(source);
        var instant = helper.GetSingleInstant(fromInstant);

        // Act
        var resultingArray = backwards ? periodTimeline.EnumerateBackwardsTo(instant).ToArray() : periodTimeline.EnumerateFrom(instant).ToArray();

        // Assert
        var resultingTimeline = resultingArray.AsTimeline();
        var actualInstantsTimeline =
            helper.TimelineToString(resultingTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualInstantsTimeline}\"");
        Assert.AreEqual(expected, actualInstantsTimeline);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateToTestCaseSource))]
    public void EnumerateTo(string source, string toInstant, string expected)
    {
        ExecuteEnumerateTo(source, toInstant, expected, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateToTestCaseSource))]
    public void EnumerateBackwardsFrom(string source, string toInstant, string expected)
    {
        ExecuteEnumerateTo(source, toInstant, expected, true);
    }

    private void ExecuteEnumerateTo(string source, string toInstant, string expected, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"To:       \"{toInstant}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var timeline = helper.CreateTimeline(source);
        var instant = helper.GetSingleInstant(toInstant);

        // Act
        var resultingArray = backwards ? timeline.EnumerateBackwardsFrom(instant).ToArray() : timeline.EnumerateTo(instant).ToArray();

        // Assert
        var resultingTimeline = resultingArray.AsTimeline();
        var actualInstantsTimeline =
            helper.TimelineToString(resultingTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualInstantsTimeline}\"");
        Assert.AreEqual(expected, actualInstantsTimeline);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateRangeTestCaseSource))]
    public void EnumerateRange(string source, string range, string expected)
    {
        ExecuteEnumerateRange(source, range, expected, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumerateRangeTestCaseSource))]
    public void EnumerateRangeBackwards(string source, string range, string expected)
    {
        ExecuteEnumerateRange(source, range, expected, true);
    }

    private void ExecuteEnumerateRange(string source, string range, string expected, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Range:    \"{range}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var timeline = helper.CreateTimeline(source);
        var instants = helper.GetInstants(range).ToArray();
        if (instants.Length != 2)
        {
            Assert.Fail("Two instants expected for range.");
        }

        var rangeStart = instants.First();
        var rangeEnd = instants.Last();

        // Act
        var resultingArray = backwards ? timeline.EnumerateRangeBackwards(rangeStart, rangeEnd).ToArray() : timeline.EnumerateRange(rangeStart, rangeEnd).ToArray();

        // Assert
        var resultingPeriodTimeline = resultingArray.AsTimeline();
        var actualInstantsTimeline =
            helper.TimelineToString(resultingPeriodTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualInstantsTimeline}\"");
        Assert.AreEqual(expected, actualInstantsTimeline);
    }

    [TestMethod]
    [DynamicData(nameof(EnumeratePeriodTestCaseSource))]
    public void EnumeratePeriod(string source, string period, string expected)
    {
        ExecuteEnumeratePeriod(source, period, expected, false);
    }

    [TestMethod]
    [DynamicData(nameof(EnumeratePeriodTestCaseSource))]
    public void EnumeratePeriodBackwards(string source, string period, string expected)
    {
        ExecuteEnumeratePeriod(source, period, expected, true);
    }

    private void ExecuteEnumeratePeriod(string source, string period, string expected, bool backwards)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Period:   \"{period}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var timeline = helper.CreateTimeline(source);
        var parsedPeriod = helper.GetSinglePeriod(period);

        // Act
        var resultingArray = backwards ? timeline.EnumeratePeriodBackwards(parsedPeriod).ToArray() : timeline.EnumeratePeriod(parsedPeriod).ToArray();

        // Assert
        var resultingPeriodTimeline = resultingArray.AsTimeline();
        var actualInstantsTimeline =
            helper.TimelineToString(resultingPeriodTimeline, expected.Length, TimelineMethods.GetNextUtcInstant);

        Console.WriteLine($"Actual:   \"{actualInstantsTimeline}\"");
        Assert.AreEqual(expected, actualInstantsTimeline);
    }

    private static IEnumerable<object[]> EnumerateTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.Enumerate?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(TimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateFromTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateFrom?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineEnumerateTests)}."),
            tc.From ?? throw new InvalidOperationException(
                $"{nameof(tc.From)} of null is not supported in {nameof(TimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(TimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateToTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateTo?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineEnumerateTests)}."),
            tc.To ?? throw new InvalidOperationException(
                $"{nameof(tc.To)} of null is not supported in {nameof(TimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(TimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumerateRangeTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumerateRange?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineEnumerateTests)}."),
            tc.Range ?? throw new InvalidOperationException(
                $"{nameof(tc.Range)} of null is not supported in {nameof(TimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(TimelineEnumerateTests)}.");
    }

    private static IEnumerable<object[]> EnumeratePeriodTestCaseSource()
    {
        var testCases = ReadTestCases();
        return testCases.EnumeratePeriod?.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineEnumerateTests)}."),
            tc.Period ?? throw new InvalidOperationException(
                $"{nameof(tc.Period)} of null is not supported in {nameof(TimelineEnumerateTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineEnumerateTests)}.")
        }).ToArray() ?? throw new InvalidOperationException(
            $"{nameof(testCases.Enumerate)} of null is not supported in {nameof(TimelineEnumerateTests)}.");
    }

    private static TimelineEnumerateTestCases ReadTestCases()
    {
        using var r = new StreamReader("TestCases/Timeline.Enumerate.json");
        var json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<TimelineEnumerateTestCases>(json) ??
               throw new InvalidOperationException("Was unable to load test cases.");
    }
}