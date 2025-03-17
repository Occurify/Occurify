
using Occurify.Extensions;

namespace Occurify.PeriodTimelineTransformations;

internal class SubtractEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _subtrahend;

    public SubtractEndTimeline(IPeriodTimeline source, IPeriodTimeline subtrahend)
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
            var previousBaseEnd = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            var previousSubtrahendStart = _subtrahend.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previousBaseEnd == null && previousSubtrahendStart == null)
            {
                return null;
            }

            if (previousSubtrahendStart != null && (previousBaseEnd == null || previousSubtrahendStart > previousBaseEnd))
            {
                if (!_subtrahend.EndTimeline.IsInstant(previousSubtrahendStart.Value) && // If the subtrahend starts at the same time as it ends, we don't need to end the period here.
                    _source.ContainsEnd(previousSubtrahendStart.Value))
                {
                    return previousSubtrahendStart;
                }

                utcRelativeTo = previousSubtrahendStart.Value;
            }
            else
            {
                if (!_subtrahend.ContainsEnd(previousBaseEnd!.Value))
                {
                    return previousBaseEnd;
                }

                utcRelativeTo = previousBaseEnd.Value;
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
            var nextBaseEnd = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            var nextSubtrahendStart = _subtrahend.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            if (nextBaseEnd == null && nextSubtrahendStart == null)
            {
                return null;
            }

            if (nextSubtrahendStart != null && (nextBaseEnd == null || nextSubtrahendStart < nextBaseEnd))
            {
                if (!_subtrahend.EndTimeline.IsInstant(nextSubtrahendStart.Value) && // If the subtrahend starts at the same time as it ends, we don't need to end the period here.
                    _source.ContainsEnd(nextSubtrahendStart.Value))
                {
                    return nextSubtrahendStart;
                }

                utcRelativeTo = nextSubtrahendStart.Value;
            }
            else
            {
                if (!_subtrahend.ContainsEnd(nextBaseEnd!.Value))
                {
                    return nextBaseEnd;
                }

                utcRelativeTo = nextBaseEnd.Value;
            }
        } while (true);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        var baseEndIsInstant = _source.EndTimeline.IsInstant(utcDateTime);
        var subtrahendStartIsInstant = _subtrahend.StartTimeline.IsInstant(utcDateTime);
        if (!baseEndIsInstant && !subtrahendStartIsInstant)
        {
            return false;
        }

        if (baseEndIsInstant && !_subtrahend.ContainsEnd(utcDateTime))
        {
            return true;
        }
        if (subtrahendStartIsInstant && _source.ContainsEnd(utcDateTime) && !_subtrahend.EndTimeline.IsInstant(utcDateTime))
        {
            return true;
        }

        return false;
    }
}