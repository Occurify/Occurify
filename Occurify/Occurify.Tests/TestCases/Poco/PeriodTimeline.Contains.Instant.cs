using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodTimelineContainsInstantTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("instant ")]
    public string? Instant { get; set; }

    [JsonProperty("expected")]
    public bool? Expected { get; set; }
}