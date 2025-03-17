using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodTimelineCutTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("instants")]
    public string? Instants { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}