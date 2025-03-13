using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodContainsPeriodTestCase
{
    [JsonProperty("period1")]
    public string? Period1 { get; set; }

    [JsonProperty("period2")]
    public string? Period2 { get; set; }

    [JsonProperty("expected")]
    public bool? Expected { get; set; }
}