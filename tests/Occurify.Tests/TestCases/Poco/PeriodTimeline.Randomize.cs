using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodTimelineRandomizeTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }

    [JsonProperty("maxDeviationBefore")]
    public int? MaxDeviationBefore { get; set; }

    [JsonProperty("maxDeviationAfter")]
    public int? MaxDeviationAfter { get; set; }

    [JsonProperty("randomResults")]
    public PeriodTimelineRandomResult[]? RandomResults { get; set; }
}

public class PeriodTimelineRandomResult
{
    [JsonProperty("index")]
    public int? Index { get; set; }

    [JsonProperty("result")]
    public double? Result { get; set; }
}