using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.PeriodTimelineTransformations;

internal class NormalizedEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;

    public NormalizedEndTimeline(IPeriodTimeline source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        var previousEnd = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
        if (previousEnd == null)
        {
            return null;
        }
        if (IsValidEndOfPeriod(previousEnd.Value))
        {
            return previousEnd;
        }

        // previousEnd is part of a run of ends without a start in between. The valid end of that run is the first
        // end after the last start before it (or the very first end if there is no such start).
        // Mirrors GetNextUtcInstant, which jumps to the next start instead of visiting every end in the run.
        var previousStart = _source.StartTimeline.GetPreviousUtcInstant(previousEnd.Value);
        if (previousStart == null)
        {
            return _source.EndTimeline.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc);
        }
        return _source.EndTimeline.GetNextUtcInstant(previousStart.Value);
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        var nextEnd = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
        if (nextEnd == null)
        {
            return null;
        }
        if (IsValidEndOfPeriod(nextEnd.Value))
        {
            return nextEnd;
        }

        var nextStart = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
        return nextStart == null ? null : _source.EndTimeline.GetNextUtcInstant(nextStart.Value);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _source.EndTimeline.IsInstant(utcDateTime) && IsValidEndOfPeriod(utcDateTime);
    }

    private bool IsValidEndOfPeriod(DateTime utcDateTime)
    {
        var previousStart = _source.StartTimeline.GetPreviousUtcInstant(utcDateTime);
        var previousEnd = _source.EndTimeline.GetPreviousUtcInstant(utcDateTime);
        if (previousEnd == null)
        {
            // There is no previous end.
            return true;
        }

        if (previousStart == null)
        {
            // There is a previous end.
            return false;
        }

        return previousEnd <= previousStart;
    }
}