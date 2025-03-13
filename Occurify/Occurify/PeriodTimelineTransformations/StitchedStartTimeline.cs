using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.PeriodTimelineTransformations;

internal class StitchedStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;

    // Note: If the source only contains consecutive periods, the stitched state would mean a full period on the entire timeline. We achieve this by placing a single start at DateTime.MinValue.
    private bool? _isSourceConsecutivePeriodsOnly;

    public StitchedStartTimeline(IPeriodTimeline source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        if (IsSourceConsecutivePeriodsOnly())
        {
            return utcRelativeTo == DateTime.MinValue ? null : DateTimeHelper.MinValueUtc;
        }

        DateTime? previousStart, previousEnd;
        do
        {
            previousStart = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previousStart == null)
            {
                return null;
            }

            previousEnd = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            utcRelativeTo = previousStart.Value;
        } while (previousEnd != null && previousStart == previousEnd);

        return previousStart;
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        if (IsSourceConsecutivePeriodsOnly())
        {
            return null;
        }

        DateTime? nextStart, nextEnd;
        do
        {
            nextStart = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            if (nextStart == null)
            {
                return null;
            }

            nextEnd = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            utcRelativeTo = nextStart.Value;
        } while (nextEnd != null && nextStart == nextEnd);

        return nextStart;
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        if (IsSourceConsecutivePeriodsOnly())
        {
            return utcDateTime == DateTime.MinValue;
        }

        return _source.StartTimeline.IsInstant(utcDateTime) && !_source.EndTimeline.IsInstant(utcDateTime);
    }

    private bool IsSourceConsecutivePeriodsOnly()
    {
        if (_isSourceConsecutivePeriodsOnly != null)
        {
            return _isSourceConsecutivePeriodsOnly.Value;
        }

        var nextStart = _source.StartTimeline.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc);
        var nextEnd = _source.EndTimeline.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc);
        if (nextStart == null || nextEnd == null || nextStart != nextEnd)
        {
            _isSourceConsecutivePeriodsOnly = false;
            return false;
        }

        do
        {
            nextStart = _source.StartTimeline.GetNextUtcInstant(nextStart.Value);
            nextEnd = _source.EndTimeline.GetNextUtcInstant(nextEnd!.Value);

            if (nextStart != nextEnd)
            {
                _isSourceConsecutivePeriodsOnly = false;
                return false;
            }
        } while (nextStart != null);

        _isSourceConsecutivePeriodsOnly = true;
        return true;
    }
}