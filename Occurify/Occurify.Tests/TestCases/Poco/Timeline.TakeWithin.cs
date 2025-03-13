using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class TimelineTakeWithinTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("periods ")]
    public string? Periods { get; set; }

    [JsonProperty("take    ")]
    public int? Take { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}