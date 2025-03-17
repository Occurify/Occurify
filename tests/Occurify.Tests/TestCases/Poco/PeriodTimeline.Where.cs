using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodTimelineWhereTestCases
{
    [JsonProperty("largerThan2")]
    public PeriodTimelineWhereTestCase[]? LargerThan2 { get; set; }

    [JsonProperty("smallerThan2")]
    public PeriodTimelineWhereTestCase[]? SmallerThan2 { get; set; }
}

public class PeriodTimelineWhereTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}