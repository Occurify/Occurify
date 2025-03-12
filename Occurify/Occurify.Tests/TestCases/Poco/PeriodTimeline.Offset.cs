using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodTimelineOffsetTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("offset  ")]
    public int? Offset { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}