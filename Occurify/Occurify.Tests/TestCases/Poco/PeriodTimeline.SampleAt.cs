using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodTimelineSampleAtTestCase
{

    [JsonProperty("source        ")]
    public string? Source { get; set; }

    [JsonProperty("instant       ")]
    public string? Instant { get; set; }

    [JsonProperty("expectedPeriod")]
    public string? ExpectedPeriod { get; set; }

    [JsonProperty("expectedType  ")]
    public string? ExpectedType { get; set; }
}