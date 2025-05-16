using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Helpers;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelinesWhereOverlapCountTests
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

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void WhereOverlapCountEvenSameAsInvertedUneven_GetPreviousUtcInstant(string[] source, string _)
    {
        Execute_InvertedIsTheSameAsUneven(TimelineMethods.GetPreviousUtcInstant, source);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void WhereOverlapCountEvenSameAsInvertedUneven_GetNextUtcInstant(string[] source, string _)
    {
        Execute_InvertedIsTheSameAsUneven(TimelineMethods.GetNextUtcInstant, source);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestCaseSource), DynamicDataSourceType.Method)]
    public void WhereOverlapCountEvenSameAsInvertedUneven_IsInstant(string[] periods, string _)
    {
        Execute_InvertedIsTheSameAsUneven(TimelineMethods.IsInstant, periods);
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

    private void Execute_InvertedIsTheSameAsUneven(TimelineMethods method, string[] source)
    {
        if (!source.Any())
        {
            return;
        }
        Console.WriteLine($"Source:   \"{source.FirstOrDefault() ?? ""}\"");
        foreach (var period in source.Skip(1))
        {
            Console.WriteLine($"          \"{period}\"");
        }

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimelines = source.Select(p => helper.CreatePeriodTimeline(p)).ToArray();

        // Act
        var invertedResult = periodTimelines.WhereOverlapCount(i => i > 0 && i % 2 == 0).Invert();
        var overlapResult = periodTimelines.WhereOverlapCount(i => i <= 0 || i % 2 != 0);

        // Assert
        var invertedPeriodTimeline = helper.PeriodTimelineToString(invertedResult, source.First().Length, method);
        var overlapPeriodTimeline = helper.PeriodTimelineToString(overlapResult, source.First().Length, method);

        Console.WriteLine($"Inverted: \"{invertedPeriodTimeline}\"");
        Console.WriteLine($"Overlap:  \"{overlapPeriodTimeline}\"");
        Assert.AreEqual(invertedPeriodTimeline, overlapPeriodTimeline);
    }

    [TestMethod]
    public void GetPreviousUtcInstant_AtDateTimeMinValue_ReturnsNull()
    {
        // Arrange
        var emptyPeriodTimeline = PeriodTimeline.Empty();

        // Act
        var overlap = new[] { emptyPeriodTimeline }.WhereOverlapCount(_ => true);

        // Assert
        Assert.IsNull(overlap.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MinValueUtc));
        Assert.IsNull(overlap.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MinValueUtc));
    }

    [TestMethod]
    public void StartTimeline_IsInstant_StartsAtDateTimeMinValue_True()
    {
        // Arrange
        var startsAtMinValue = DateTimeHelper.MinValueUtc.To(DateTime.UtcNow).AsPeriodTimeline();

        // Act
        var overlap = new[] { startsAtMinValue }.WhereOverlapCount(n => n > 0);

        // Assert
        Assert.IsTrue(overlap.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc));
    }

    [TestMethod]
    public void StartTimeline_IsInstant_SinglePeriod_AlreadyStartedAtDateTimeMinValue_False()
    {
        // Arrange
        var alwaysStarted = Period.Create(null, DateTime.UtcNow).AsPeriodTimeline();

        // Act
        var overlap = new[] { alwaysStarted }.WhereOverlapCount(n => n > 0);

        // Assert
        Assert.IsFalse(overlap.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc));
    }

    [TestMethod]
    public void StartTimeline_IsInstant_MultiplePeriods_AlreadyStartedAtDateTimeMinValue_False()
    {
        // Arrange
        var startsAtMinValue = DateTimeHelper.MinValueUtc.To(DateTime.UtcNow).AsPeriodTimeline();
        var endsAtMinValue = Period.Create(null, DateTimeHelper.MinValueUtc).AsPeriodTimeline();

        // Act
        var overlap = new[] { startsAtMinValue, endsAtMinValue }.WhereOverlapCount(n => n > 0);

        // Assert
        Assert.IsFalse(overlap.StartTimeline.IsInstant(DateTimeHelper.MinValueUtc));
    }

    [TestMethod]
    public void StartTimeline_GetPreviousUtcInstant_StartsAtDateTimeMinValue()
    {
        // Arrange
        var startsAtMinValue = DateTimeHelper.MinValueUtc.To(DateTime.UtcNow).AsPeriodTimeline();

        // Act
        var overlap = new[] { startsAtMinValue }.WhereOverlapCount(n => n > 0);

        // Assert
        Assert.AreEqual(DateTime.MinValue, overlap.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MinValueUtc + TimeSpan.FromTicks(1)));
    }

    [TestMethod]
    public void StartTimeline_GetPreviousUtcInstant_SinglePeriod_AlreadyStartedAtDateTimeMinValue_Null()
    {
        // Arrange
        var alwaysStarted = Period.Create(null, DateTime.UtcNow).AsPeriodTimeline();

        // Act
        var overlap = new[] { alwaysStarted }.WhereOverlapCount(n => n > 0);

        // Assert
        Assert.IsNull(overlap.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MinValueUtc + TimeSpan.FromTicks(1)));
    }

    [TestMethod]
    public void StartTimeline_GetPreviousUtcInstant_MultiplePeriods_AlreadyStartedAtDateTimeMinValue_Null()
    {
        // Arrange
        var startsAtMinValue = DateTimeHelper.MinValueUtc.To(DateTime.UtcNow).AsPeriodTimeline();
        var endsAtMinValue = Period.Create(null, DateTimeHelper.MinValueUtc).AsPeriodTimeline();

        // Act
        var overlap = new[] { startsAtMinValue, endsAtMinValue }.WhereOverlapCount(n => n > 0);

        // Assert
        Assert.IsNull(overlap.StartTimeline.GetPreviousUtcInstant(DateTimeHelper.MinValueUtc + TimeSpan.FromTicks(1)));
    }

    [TestMethod]
    public void EndTimeline_IsInstant_EndsAtDateTimeMinValue_True()
    {
        // Arrange
        var startsAtMinValue = DateTimeHelper.MinValueUtc.To(DateTime.UtcNow).AsPeriodTimeline();

        // Act
        var overlap = new[] { startsAtMinValue }.WhereOverlapCount(n => n == 0);

        // Assert
        Assert.IsTrue(overlap.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc));
    }

    [TestMethod]
    public void EndTimeline_IsInstant_SinglePeriod_AlreadyEndedAtDateTimeMinValue_False()
    {
        // Arrange
        var alwaysStarted = Period.Create(null, DateTime.UtcNow).AsPeriodTimeline();

        // Act
        var overlap = new[] { alwaysStarted }.WhereOverlapCount(n => n == 0);

        // Assert
        Assert.IsFalse(overlap.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc));
    }

    [TestMethod]
    public void EndTimeline_IsInstant_MultiplePeriods_AlreadyEndedAtDateTimeMinValue_False()
    {
        // Arrange
        var startsAtMinValue = DateTimeHelper.MinValueUtc.To(DateTime.UtcNow).AsPeriodTimeline();
        var endsAtMinValue = Period.Create(null, DateTimeHelper.MinValueUtc).AsPeriodTimeline();

        // Act
        var overlap = new[] { startsAtMinValue, endsAtMinValue }.WhereOverlapCount(n => n == 0);

        // Assert
        Assert.IsFalse(overlap.EndTimeline.IsInstant(DateTimeHelper.MinValueUtc));
    }

    [TestMethod]
    public void EndTimeline_GetPreviousUtcInstant_EndsAtDateTimeMinValue()
    {
        // Arrange
        var startsAtMinValue = DateTimeHelper.MinValueUtc.To(DateTime.UtcNow).AsPeriodTimeline();

        // Act
        var overlap = new[] { startsAtMinValue }.WhereOverlapCount(n => n == 0);

        // Assert
        Assert.AreEqual(DateTime.MinValue, overlap.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MinValueUtc + TimeSpan.FromTicks(1)));
    }

    [TestMethod]
    public void EndTimeline_GetPreviousUtcInstant_SinglePeriod_AlreadyEndedAtDateTimeMinValue_Null()
    {
        // Arrange
        var alwaysStarted = Period.Create(null, DateTime.UtcNow).AsPeriodTimeline();

        // Act
        var overlap = new[] { alwaysStarted }.WhereOverlapCount(n => n == 0);

        // Assert
        Assert.IsNull(overlap.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MinValueUtc + TimeSpan.FromTicks(1)));
    }

    [TestMethod]
    public void EndTimeline_GetPreviousUtcInstant_MultiplePeriods_AlreadyEndedAtDateTimeMinValue_Null()
    {
        // Arrange
        var startsAtMinValue = DateTimeHelper.MinValueUtc.To(DateTime.UtcNow).AsPeriodTimeline();
        var endsAtMinValue = Period.Create(null, DateTimeHelper.MinValueUtc).AsPeriodTimeline();

        // Act
        var overlap = new[] { startsAtMinValue, endsAtMinValue }.WhereOverlapCount(n => n == 0);

        // Assert
        Assert.IsNull(overlap.EndTimeline.GetPreviousUtcInstant(DateTimeHelper.MinValueUtc + TimeSpan.FromTicks(1)));
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
                    $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelinesWhereOverlapCountTests)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelinesWhereOverlapCountTests)}.")
            })).ToArray();
    }
}