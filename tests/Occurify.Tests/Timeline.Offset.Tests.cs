using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.Helpers;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class TimelineOffsetTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetPreviousUtcInstant(string source, int offset, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, offset, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetNextUtcInstant(string source, int offset, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, offset, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void IsInstant(string source, int offset, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, offset, expected);
    }

    private void ExecuteTest(TimelineMethods method, string source, int offset, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Offset:   \"{offset}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var timeline = helper.CreateTimeline(source);

        // Act
        var offsetTimeline = timeline.OffsetTicks(offset);

        // Assert
        var actual = helper.TimelineToString(offsetTimeline, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actual}\"");
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Offset_OutOfRange_MinValue()
    {
        // Arrange
        var timeline = DateTimeHelper.MinValueUtc.AsTimeline();

        // Act
        var result = timeline.OffsetTicks(-1);

        // Assert
        Assert.IsNull(result.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc));
        Assert.IsNull(result.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc));
        Assert.IsFalse(result.IsInstant(DateTimeHelper.MinValueUtc));
    }

    [TestMethod]
    public void Offset_PartOutOfRange_MinValue_EnumerationFromBothDirections()
    {
        // Arrange
        var timeline = new[] { DateTimeHelper.MinValueUtc, DateTimeHelper.MinValueUtc + TimeSpan.FromTicks(1) }
            .AsTimeline();

        // Act
        var result = timeline.OffsetTicks(-1);

        // Assert
        CollectionAssert.AreEqual(new[] { DateTimeHelper.MinValueUtc }, result.ToArray());
        CollectionAssert.AreEqual(new[] { DateTimeHelper.MinValueUtc }, result.EnumerateBackwards().ToArray());
        Assert.IsTrue(result.IsInstant(DateTimeHelper.MinValueUtc));
        Assert.IsFalse(result.IsInstant(DateTimeHelper.MinValueUtc + TimeSpan.FromTicks(1)));
    }

    [TestMethod]
    public void Offset_OutOfRange_MaxValue()
    {
        // Arrange
        var timeline = DateTimeHelper.MaxValueUtc.AsTimeline();

        // Act
        var result = timeline.OffsetTicks(1);

        // Assert
        Assert.IsNull(result.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc));
        Assert.IsNull(result.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc));
        Assert.IsFalse(result.IsInstant(DateTimeHelper.MaxValueUtc));
    }

    [TestMethod]
    public void Offset_PartOutOfRange_MaxValue_EnumerationFromBothDirections()
    {
        // Arrange
        var timeline = new[] { DateTimeHelper.MaxValueUtc - TimeSpan.FromTicks(1), DateTimeHelper.MaxValueUtc }
            .AsTimeline();

        // Act
        var result = timeline.OffsetTicks(1);

        // Assert
        CollectionAssert.AreEqual(new[] { DateTimeHelper.MaxValueUtc }, result.ToArray());
        CollectionAssert.AreEqual(new[] { DateTimeHelper.MaxValueUtc }, result.EnumerateBackwards().ToArray());
        Assert.IsFalse(result.IsInstant(DateTimeHelper.MaxValueUtc - TimeSpan.FromTicks(1)));
        Assert.IsTrue(result.IsInstant(DateTimeHelper.MaxValueUtc));
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/Timeline.Offset.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<TimelineOffsetTestCase[]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.Select(tc => new object[]
        {
            tc.Source ?? throw new InvalidOperationException(
                $"{nameof(tc.Source)} of null is not supported in {nameof(TimelineOffsetTests)}."),
            tc.Offset ?? throw new InvalidOperationException(
                $"{nameof(tc.Offset)} of null is not supported in {nameof(TimelineOffsetTests)}."),
            tc.Expected ?? throw new InvalidOperationException(
                $"{nameof(tc.Expected)} of null is not supported in {nameof(TimelineOffsetTests)}.")
        }).ToArray();
    }
}