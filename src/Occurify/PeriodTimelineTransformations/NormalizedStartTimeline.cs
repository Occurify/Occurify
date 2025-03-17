namespace Occurify.PeriodTimelineTransformations;

internal class NormalizedStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;

    public NormalizedStartTimeline(IPeriodTimeline source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        var previousStart = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
        while (previousStart != null && !IsValidStartOfPeriod(previousStart.Value))
        {
            previousStart = _source.StartTimeline.GetPreviousUtcInstant(previousStart.Value);
        }
        return previousStart;
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        var nextStart = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
        if (nextStart == null)
        {
            return null;
        }
        if (IsValidStartOfPeriod(nextStart.Value))
        {
            return nextStart;
        }

        var nextEnd = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
        if (nextEnd == null)
        {
            return null;
        }
        return _source.StartTimeline.IsInstant(nextEnd.Value) ? nextEnd : _source.StartTimeline.GetNextUtcInstant(nextEnd.Value);
    }
        
    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _source.StartTimeline.IsInstant(utcDateTime) && IsValidStartOfPeriod(utcDateTime);
    }

    private bool IsValidStartOfPeriod(DateTime utcDateTime)
    {
        var previousStart = _source.StartTimeline.GetPreviousUtcInstant(utcDateTime);
        var previousEnd = _source.EndTimeline.IsInstant(utcDateTime) ? utcDateTime : _source.EndTimeline.GetPreviousUtcInstant(utcDateTime);
        if (previousEnd == null)
        {
            if (previousStart == null)
            {
                // utcDateTime is the first instant in both timelines.
                return true;
            }
            // There is no previous end, but there is an earlier start. This means utcDateTime is not valid.
            return false;
        }

        return previousStart == null || previousEnd > previousStart;
    }
}