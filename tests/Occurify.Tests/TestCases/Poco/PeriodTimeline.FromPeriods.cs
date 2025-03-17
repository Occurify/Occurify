using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodTimelineFromPeriodsTestCase
{
    [JsonProperty("periods")]
    public string[]? Periods { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}