using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class TimelineCombineTestCase
{
    [JsonProperty("source")]
    public string[]? Source { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}