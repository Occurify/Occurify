using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class TimelineContainingTestCase
{
    [JsonProperty("source  ")]
    public string? Timeline { get; set; }

    [JsonProperty("instants")]
    public string? Instants { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}