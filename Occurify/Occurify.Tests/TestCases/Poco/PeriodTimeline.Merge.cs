using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodTimelineMergeTestCase
{

    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("periods ")]
    public string? Periods { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}