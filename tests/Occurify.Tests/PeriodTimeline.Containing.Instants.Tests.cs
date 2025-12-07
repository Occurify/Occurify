using Newtonsoft.Json;
using Occurify.Extensions;
using Occurify.Tests.StringHelper;
using Occurify.Tests.TestCases.Poco;

namespace Occurify.Tests;

[TestClass]
public class PeriodTimelineContainingInstantsTests
{
    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetPreviousUtcInstant(string source, string instants, string expected)
    {
        ExecuteTest(TimelineMethods.GetPreviousUtcInstant, source, instants, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void GetNextUtcInstant(string source, string instants, string expected)
    {
        ExecuteTest(TimelineMethods.GetNextUtcInstant, source, instants, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TestCaseSource))]
    public void IsInstant(string source, string instants, string expected)
    {
        ExecuteTest(TimelineMethods.IsInstant, source, instants, expected);
    }
        
    private void ExecuteTest(TimelineMethods method, string source, string instants, string expected)
    {
        Console.WriteLine($"Source:   \"{source}\"");
        Console.WriteLine($"Instants: \"{instants}\"");
        Console.WriteLine($"Expected: \"{expected}\"");

        // Arrange
        var helper = new StringTimelineHelper();

        var periodTimeline = helper.CreatePeriodTimeline(source);
        var instantsTimeline = helper.CreateTimeline(instants);

        // Act
        var filteredPeriodTimeline = periodTimeline.Containing(instantsTimeline);

        // Assert
        var actualPeriodTimeline = helper.PeriodTimelineToString(filteredPeriodTimeline, expected.Length, method);

        Console.WriteLine($"Actual:   \"{actualPeriodTimeline}\"");
        Assert.AreEqual(expected, actualPeriodTimeline);
    }

    private static IEnumerable<object[]> TestCaseSource()
    {
        using var r = new StreamReader("TestCases/PeriodTimeline.Containing.Instants.json");
        var json = r.ReadToEnd();
        var testCases = JsonConvert.DeserializeObject<PeriodTimelineContainingInstantsTestCase[][]>(json) ?? throw new InvalidOperationException("Was unable to load test cases.");
        return testCases.SelectMany(cases =>
            cases.Select(tc => new object[]
            {
                tc.Source ?? throw new InvalidOperationException(
                    $"{nameof(tc.Source)} of null is not supported in {nameof(PeriodTimelineContainingInstantsTestCase)}."),
                tc.Instants ?? throw new InvalidOperationException(
                    $"{nameof(tc.Instants)} of null is not supported in {nameof(PeriodTimelineContainingInstantsTestCase)}."),
                tc.Expected ?? throw new InvalidOperationException(
                    $"{nameof(tc.Expected)} of null is not supported in {nameof(PeriodTimelineContainingInstantsTestCase)}.")
            })).ToArray();
    }
}