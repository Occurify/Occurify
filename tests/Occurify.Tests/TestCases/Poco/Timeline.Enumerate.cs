using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class TimelineEnumerateTestCases
{
    public TimelineEnumerateTestCase[]? Enumerate { get; set; }
    public TimelineEnumerateFromTestCase[]? EnumerateFrom { get; set; }
    public TimelineEnumerateToTestCase[]? EnumerateTo { get; set; }
    public TimelineEnumerateRangeTestCase[]? EnumerateRange { get; set; }
    public TimelineEnumeratePeriodTestCase[]? EnumeratePeriod { get; set; }
}

public class TimelineEnumerateTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}

public class TimelineEnumerateFromTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("from     ")]
    public string? From { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}

public class TimelineEnumerateToTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("to       ")]
    public string? To { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}

public class TimelineEnumerateRangeTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("range    ")]
    public string? Range { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}

public class TimelineEnumeratePeriodTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("period   ")]
    public string? Period { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}