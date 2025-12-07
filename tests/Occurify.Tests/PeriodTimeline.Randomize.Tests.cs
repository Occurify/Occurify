using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.Helpers;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineRandomizeTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetPreviousUtcInstant(string source, string expected, int maxDeviationBefore, int maxDeviationAfter, PeriodTimelineRandomResult[] randomResults)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, expected, maxDeviationBefore, maxDeviationAfter, randomResults);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetNextUtcInstant(string source, string expected, int maxDeviationBefore, int maxDeviationAfter, PeriodTimelineRandomResult[] randomResults)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, expected, maxDeviationBefore, maxDeviationAfter, randomResults);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void IsInstant(string source, string expected, int maxDeviationBefore, int maxDeviationAfter, PeriodTimelineRandomResult[] randomResults)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, expected, maxDeviationBefore, maxDeviationAfter, randomResults);
    }
        
    private void ExecuteTest(TimelineMethods method, string source, string expected, int maxDeviationBefore, int maxDeviationAfter, PeriodTimelineRandomResult[] randomResults)
    {
        Console.WriteLine($"Source:               \"{source}\"");
        Console.WriteLine($"Max Deviation Before: \"{maxDeviationBefore}\"");
        Console.WriteLine($"Max Deviation After:  \"{maxDeviationAfter}\"");
        Console.WriteLine($"Random Results:       \"{string.Join(", ", randomResults.Select(r => $"{r.Index}: {r.Result}"))}\"");
        Console.WriteLine($"Expected:             \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);

        const int seed = 1337;
        var randomLookup = randomResults.ToDictionary(r =>
        {
            var index = r.Index ?? throw new InvalidOperationException(
                $"Random {nameof(r.Index)} of null is not supported in {nameof(PeriodTimelineRandomizeTests)}.");
            var dateTime = helper.Origin + TimeSpan.FromTicks(index);
            return dateTime.GetHashCode() ^ seed;
        }, r => r.Result ?? throw new InvalidOperationException(
            $"Random {nameof(r.Result)} of null is not supported in {nameof(PeriodTimelineRandomizeTests)}."));
            
        // Act
        var randomizedPeriodTimeline = periodTimeline.Randomize(
            seed, 
            TimeSpan.FromTicks(maxDeviationBefore), 
            TimeSpan.FromTicks(maxDeviationAfter),
            s => randomLookup[s]);

        // Assert
        var actual = helper.PeriodTimelineToString(randomizedPeriodTimeline, expected.Length, method);

        Console.WriteLine($"Actual:               \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Randomize_OutOfRange_MinValue()
    {
        // Arrange
        var periodTimeline = DateTimeHelper.MinValueUtc.To(DateTimeHelper.MinValueUtc + TimeSpan.FromTicks(1)).AsPeriodTimeline();
        const int seed = 1337;

        // Act
        var result = periodTimeline.Randomize(seed, TimeSpan.FromTicks(1), TimeSpan.Zero, _ => 0);

        // Assert
        Assert.AreEqual(result.StartTimeline.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc), DateTimeHelper.MinValueUtc);
        Assert.AreEqual(result.StartTimeline.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc), DateTimeHelper.MinValueUtc);
        Assert.IsTrue(result.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc));
    }

    [TestMethod]
    public void Randomize_OutOfRange_MaxValue()
    {
        // Arrange
        var periodTimeline = (DateTimeHelper.MaxValueUtc - TimeSpan.FromTicks(1)).To(DateTimeHelper.MaxValueUtc).AsPeriodTimeline();
        const int seed = 1337;

        // Act
        var result = periodTimeline.Randomize(seed, TimeSpan.Zero, TimeSpan.FromTicks(1), _ => 1);

        // Assert
        Assert.AreEqual(result.EndTimeline.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc), DateTimeHelper.MaxValueUtc);
        Assert.AreEqual(result.EndTimeline.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc), DateTimeHelper.MaxValueUtc);
        Assert.IsTrue(result.EndTimeline.IsInstant(DateTimeHelper.MaxValueUtc));
    }

    [TestMethod]
    public void Randomize_SameSeed_Equal()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var periodTimeline = now.To(now + TimeSpan.FromHours(1)).AsPeriodTimeline();
        const int seed = 1337;

        // Act
        var result1 = periodTimeline.Randomize(seed, TimeSpan.FromHours(1));
        var result2 = periodTimeline.Randomize(seed, TimeSpan.FromHours(1));

        // Assert
        CollectionAssert.AreEqual(result1.ToArray(), result2.ToArray());
    }

    [TestMethod]
    public void Randomize_DifferentSeed_NotEqual()
    {
        // Arrange
        var dateTime = new DateTime(133742, DateTimeKind.Utc);
        var periodTimeline = dateTime.To(dateTime + TimeSpan.FromHours(1)).AsPeriodTimeline();
        const int seed1 = 42;
        const int seed2 = 1337;

        // Act
        var result1 = periodTimeline.Randomize(seed1, TimeSpan.FromHours(1));
        var result2 = periodTimeline.Randomize(seed2, TimeSpan.FromHours(1));

        // Assert
        CollectionAssert.AreNotEqual(result1.ToArray(), result2.ToArray());
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Randomize.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineRandomizeTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineRandomizeTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineRandomizeTests)}."),
                tc.MaxDeviationBefore ?? throw new InvalidOperationException(
                    $"{nameof(tc.MaxDeviationBefore)} of null is not supported in {nameof(PeriodTimelineRandomizeTests)}."),
                tc.MaxDeviationAfter ?? throw new InvalidOperationException(
                    $"{nameof(tc.MaxDeviationAfter)} of null is not supported in {nameof(PeriodTimelineRandomizeTests)}."),
                tc.RandomResults ?? throw new InvalidOperationException(
                    $"{nameof(tc.RandomResults)} of null is not supported in {nameof(PeriodTimelineRandomizeTests)}.")
            })).ToArray();
    }
}