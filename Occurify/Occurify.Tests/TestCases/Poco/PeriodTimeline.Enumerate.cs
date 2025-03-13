using Newtonsoft.Json;

namespace Occurify.Tests.TestCases.Poco;

public class PeriodTimelineEnumerateTestCases
{
    public PeriodTimelineEnumerateTestCase[]? Enumerate { get; set; }
    public PeriodTimelineEnumerateFromTestCase[]? EnumerateFrom { get; set; }
    public PeriodTimelineEnumerateFromTestCase[]? EnumerateFromIncludingPartial { get; set; }
    public PeriodTimelineEnumerateToTestCase[]? EnumerateTo { get; set; }
    public PeriodTimelineEnumerateToTestCase[]? EnumerateToIncludingPartial { get; set; }
    public PeriodTimelineEnumerateRangeTestCase[]? EnumerateRangeCompleteOnly { get; set; }
    public PeriodTimelineEnumerateRangeTestCase[]? EnumerateRangeStartPartialAllowed { get; set; }
    public PeriodTimelineEnumerateRangeTestCase[]? EnumerateRangeEndPartialAllowed { get; set; }
    public PeriodTimelineEnumerateRangeTestCase[]? EnumerateRangePartialAllowed { get; set; }
    public PeriodTimelineEnumeratePeriodTestCase[]? EnumeratePeriodCompleteOnly { get; set; }
    public PeriodTimelineEnumeratePeriodTestCase[]? EnumeratePeriodStartPartialAllowed { get; set; }
    public PeriodTimelineEnumeratePeriodTestCase[]? EnumeratePeriodEndPartialAllowed { get; set; }
    public PeriodTimelineEnumeratePeriodTestCase[]? EnumeratePeriodPartialAllowed { get; set; }
}

public class PeriodTimelineEnumerateTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}

public class PeriodTimelineEnumerateFromTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("from    ")]
    public string? From { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}

public class PeriodTimelineEnumerateToTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("to      ")]
    public string? To { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}

public class PeriodTimelineEnumerateRangeTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("range   ")]
    public string? Range { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}

public class PeriodTimelineEnumeratePeriodTestCase
{
    [JsonProperty("source  ")]
    public string? Source { get; set; }

    [JsonProperty("period  ")]
    public string? Period { get; set; }

    [JsonProperty("expected")]
    public string? Expected { get; set; }
}