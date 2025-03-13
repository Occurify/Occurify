using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodTimelineWithoutTestCase
{

    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("periods ")]
    public string? Periods { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}