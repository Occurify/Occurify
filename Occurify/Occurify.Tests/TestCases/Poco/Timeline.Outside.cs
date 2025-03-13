using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class TimelineOutsideTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("period  ")]
    public string? Period { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}