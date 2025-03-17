using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class TimelineWithinTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("periods ")]
    public string? Periods { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}