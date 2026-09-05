using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineNormalizeTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetPreviousUtcInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetNextUtcInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void IsInstant(string source, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, expected);
    }

    [TestMethod]
    [Timeout(10000, CooperativeCancellation = true)]
    public void Normalize_PreviousInstant_DoesNotWalkEveryStart()
    {
        // Hourly starts since the beginning of time and a single end: only the very first start is a valid period start.
        // Finding it backwards must not visit every hourly start in between.
        var origin = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var end = new DateTime(2020, 1, 1, 0, 30, 0, DateTimeKind.Utc);
        var starts = Timeline.Periodic(origin, TimeSpan.FromHours(1));
        var firstStart = starts.GetCurrentOrNextUtcInstant(new DateTime(0, DateTimeKind.Utc))!.Value;
        var startAfterEnd = starts.GetNextUtcInstant(end)!.Value;

        var periodTimeline = starts.To(end.AsTimeline());
        var ends = end.AsTimeline().To(starts);

        CollectionAssert.AreEqual(
            new[] { Period.Create(startAfterEnd, null), Period.Create(firstStart, end) },
            periodTimeline.EnumerateBackwards().Take(2).ToArray());
        Assert.AreEqual(Period.Create(firstStart, end), periodTimeline.Enumerate().First());
        Assert.AreEqual(Period.Create(firstStart, end), periodTimeline.GetPreviousCompletePeriod(end.AddYears(1)));
        Assert.AreEqual(Period.Create(end, startAfterEnd), ends.GetPreviousCompletePeriod(end.AddYears(1)));
    }

    private void ExecuteTest(TimelineMethods method, string source, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);

        // Act
        var normalizedPeriodTimeline = periodTimeline.Normalize();

        // Assert
        var actual = helper.PeriodTimelineToString(normalizedPeriodTimeline, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Normalize.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineNormalizeTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException($"Input of null is not supported in {nameof(PeriodTimelineNormalizeTests)}."),
            tc.Expected ?? throw new InvalidOperationException($"Expected of null is not supported in {nameof(PeriodTimelineNormalizeTests)}.")
        }).ToArray();
    }
}