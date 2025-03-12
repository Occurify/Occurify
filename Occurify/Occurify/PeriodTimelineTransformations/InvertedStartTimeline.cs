using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.PeriodTimelineTransformations;

internal class InvertedStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;

    // Note: If the source is empty, the inverted state would mean a full period on the entire timeline. We achieve this by placing a single start at DateTime.MinValue.
    private bool? _isSourceEmpty;

    public InvertedStartTimeline(IPeriodTimeline source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        if (IsSourceEmpty())
        {
            return utcRelativeTo == DateTime.MinValue ? null : DateTimeHelper.MinValueUtc;
        }

        DateTime? previousEnd, previousStart;
        do
        {
            previousEnd = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previousEnd == null)
            {
                return null;
            }

            previousStart = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            utcRelativeTo = previousEnd.Value;
        } while (previousStart != null && previousEnd == previousStart);

        return previousEnd;
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        if (IsSourceEmpty())
        {
            return null;
        }

        DateTime? nextEnd, nextStart;
        do
        {
            nextEnd = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            if (nextEnd == null)
            {
                return null;
            }

            nextStart = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            utcRelativeTo = nextEnd.Value;
        } while (nextStart != null && nextEnd == nextStart);

        return nextEnd;
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        if (IsSourceEmpty())
        {
            return utcDateTime == DateTime.MinValue;
        }

        return _source.EndTimeline.IsInstant(utcDateTime) && !_source.StartTimeline.IsInstant(utcDateTime);
    }

    private bool IsSourceEmpty()
    {
        if (_isSourceEmpty != null)
        {
            return _isSourceEmpty.Value;
        }

        _isSourceEmpty = _source.IsEmpty();
        return _isSourceEmpty.Value;
    }
}