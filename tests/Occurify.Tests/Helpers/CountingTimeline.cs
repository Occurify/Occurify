namespace Occurify.Tests.Helpers;

/// <summary>
/// Wraps a timeline and counts how often each member is called, so tests can assert on the amount of work an operation performs.
/// </summary>
internal class CountingTimeline : Timeline
{
    private readonly ITimeline _source;

    public CountingTimeline(ITimeline source)
    {
        _source = source;
    }

    public int GetPreviousUtcInstantCalls { get; private set; }
    public int GetNextUtcInstantCalls { get; private set; }
    public int IsInstantCalls { get; private set; }

    public int TotalCalls => GetPreviousUtcInstantCalls + GetNextUtcInstantCalls + IsInstantCalls;

    public void Reset()
    {
        GetPreviousUtcInstantCalls = 0;
        GetNextUtcInstantCalls = 0;
        IsInstantCalls = 0;
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        GetPreviousUtcInstantCalls++;
        return _source.GetPreviousUtcInstant(utcRelativeTo);
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        GetNextUtcInstantCalls++;
        return _source.GetNextUtcInstant(utcRelativeTo);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        IsInstantCalls++;
        return _source.IsInstant(utcDateTime);
    }
}
