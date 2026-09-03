using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.Helpers;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class TimelineRandomizeTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetPreviousUtcInstant(string source, string expected, int maxDeviationBefore, int maxDeviationAfter, TimelineRandomResult[] randomResults)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, expected, maxDeviationBefore, maxDeviationAfter, randomResults);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetNextUtcInstant(string source, string expected, int maxDeviationBefore, int maxDeviationAfter, TimelineRandomResult[] randomResults)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, expected, maxDeviationBefore, maxDeviationAfter, randomResults);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void IsInstant(string source, string expected, int maxDeviationBefore, int maxDeviationAfter, TimelineRandomResult[] randomResults)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, expected, maxDeviationBefore, maxDeviationAfter, randomResults);
    }
        
    private void ExecuteTest(TimelineMethods method, string source, string expected, int maxDeviationBefore, int maxDeviationAfter, TimelineRandomResult[] randomResults)
    {
        Console.WriteLine($"Source:               \"{source}\"");
        Console.WriteLine($"Max Deviation Before: \"{maxDeviationBefore}\"");
        Console.WriteLine($"Max Deviation After:  \"{maxDeviationAfter}\"");
        Console.WriteLine($"Random Results:       \"{string.Join(", ", randomResults.Select(r => $"{r.Index}: {r.Result}"))}\"");
        Console.WriteLine($"Expected:             \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var timeline = helper.CreateTimeline(source);

        const int seed = 1337;
        var randomLookup = randomResults.ToDictionary(r =>
        {
            var index = r.Index ?? throw new InvalidOperationException(
                $"Random {nameof(r.Index)} of null is not supported in {nameof(TimelineRandomizeTests)}.");
            var dateTime = helper.Origin + TimeSpan.FromTicks(index);
            return dateTime.GetHashCode() ^ seed;
        }, r => r.Result ?? throw new InvalidOperationException(
            $"Random {nameof(r.Result)} of null is not supported in {nameof(TimelineRandomizeTests)}."));
            
        // Act
        var randomizedTimeline = timeline.Randomize(
            seed, 
            TimeSpan.FromTicks(maxDeviationBefore), 
            TimeSpan.FromTicks(maxDeviationAfter),
            s => randomLookup[s]);

        // Assert
        var actual = helper.TimelineToString(randomizedTimeline, expected.Length, method);
        
        Console.WriteLine($"Actual:               \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Randomize_OutOfRange_MinValue()
    {
        // Arrange
        var timeline = DateTimeHelper.MinValueUtc.AsTimeline();
        const int seed = 1337;

        // Act
        var result = timeline.Randomize(seed, TimeSpan.FromTicks(1), TimeSpan.Zero, _ => 0);

        // Assert
        Assert.AreEqual(result.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc), DateTimeHelper.MinValueUtc);
        Assert.AreEqual(result.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc), DateTimeHelper.MinValueUtc);
        Assert.IsTrue(result.IsInstant(DateTimeHelper.MinValueUtc));
    }

    [TestMethod]
    public void Randomize_OutOfRange_MaxValue()
    {
        // Arrange
        var timeline = DateTimeHelper.MaxValueUtc.AsTimeline();
        const int seed = 1337;

        // Act
        var result = timeline.Randomize(seed, TimeSpan.Zero, TimeSpan.FromTicks(1), _ => 1);

        // Assert
        Assert.AreEqual(result.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc), DateTimeHelper.MaxValueUtc);
        Assert.AreEqual(result.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc), DateTimeHelper.MaxValueUtc);
        Assert.IsTrue(result.IsInstant(DateTimeHelper.MaxValueUtc));
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/Timeline.Randomize.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<TimelineRandomizeTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineRandomizeTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineRandomizeTests)}."),
                tc.MaxDeviationBefore ?? throw new InvalidOperationException(
                    $"{nameof(tc.MaxDeviationBefore)} of null is not supported in {nameof(TimelineRandomizeTests)}."),
                tc.MaxDeviationAfter ?? throw new InvalidOperationException(
                    $"{nameof(tc.MaxDeviationAfter)} of null is not supported in {nameof(TimelineRandomizeTests)}."),
                tc.RandomResults ?? throw new InvalidOperationException(
                    $"{nameof(tc.RandomResults)} of null is not supported in {nameof(TimelineRandomizeTests)}.")
            })).ToArray();
    }
}