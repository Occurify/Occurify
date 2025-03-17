
using Occurify.Extensions;

namespace Occurify.PeriodTimelineTransformations;

internal class SubtractStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _subtrahend;

    public SubtractStartTimeline(IPeriodTimeline source, IPeriodTimeline subtrahend)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _subtrahend = subtrahend ?? throw new ArgumentNullException(nameof(subtrahend));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        do
        {
            var previousBaseStart = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            var previousSubtrahendEnd = _subtrahend.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previousBaseStart == null && previousSubtrahendEnd == null)
            {
                return null;
            }

            if (previousSubtrahendEnd != null && (previousBaseStart == null || previousSubtrahendEnd > previousBaseStart))
            {
                if (!_subtrahend.StartTimeline.IsInstant(previousSubtrahendEnd.Value) && // If the subtrahend starts at the same time as it ends, we don't need to end the period here.
                    _source.ContainsInstant(previousSubtrahendEnd.Value))
                {
                    return previousSubtrahendEnd;
                }

                utcRelativeTo = previousSubtrahendEnd.Value;
            }
            else
            {
                if (!_subtrahend.ContainsInstant(previousBaseStart!.Value))
                {
                    return previousBaseStart;
                }

                utcRelativeTo = previousBaseStart.Value;
            }
        } while (true);
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        do
        {
            var nextBaseStart = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            var nextSubtrahendEnd = _subtrahend.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            if (nextBaseStart == null && nextSubtrahendEnd == null)
            {
                return null;
            }

            if (nextSubtrahendEnd != null && (nextBaseStart == null || nextSubtrahendEnd < nextBaseStart))
            {
                if (!_subtrahend.StartTimeline.IsInstant(nextSubtrahendEnd.Value) && // If the subtrahend starts at the same time as it ends, we don't need to end the period here.
                    _source.ContainsInstant(nextSubtrahendEnd.Value))
                {
                    return nextSubtrahendEnd;
                }

                utcRelativeTo = nextSubtrahendEnd.Value;
            }
            else
            {
                if (!_subtrahend.ContainsInstant(nextBaseStart!.Value))
                {
                    return nextBaseStart;
                }

                utcRelativeTo = nextBaseStart.Value;
            }
        } while (true);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        var baseStartIsInstant = _source.StartTimeline.IsInstant(utcDateTime);
        var subtrahendEndIsInstant = _subtrahend.EndTimeline.IsInstant(utcDateTime);
        if (!baseStartIsInstant && !subtrahendEndIsInstant)
        {
            return false;
        }

        if (baseStartIsInstant && !_subtrahend.ContainsInstant(utcDateTime))
        {
            return true;
        }
        if (subtrahendEndIsInstant && _source.ContainsInstant(utcDateTime) && !_subtrahend.StartTimeline.IsInstant(utcDateTime))
        {
            return true;
        }

        return false;
    }
}