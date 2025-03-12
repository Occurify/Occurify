
using Occurify.Extensions;

namespace Occurify.PeriodTimelineTransformations;

internal class IntersectStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _periodsToIntersect;

    public IntersectStartTimeline(IPeriodTimeline source, IPeriodTimeline periodsToIntersect)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _periodsToIntersect = periodsToIntersect ?? throw new ArgumentNullException(nameof(periodsToIntersect));
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
            var previousIntersectStart = _periodsToIntersect.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previousBaseStart == null && previousIntersectStart == null)
            {
                return null;
            }

            if (previousBaseStart == previousIntersectStart)
            {
                return previousBaseStart;
            }

            if (previousIntersectStart != null && (previousBaseStart == null || previousIntersectStart > previousBaseStart))
            {
                if (_source.ContainsInstant(previousIntersectStart.Value))
                {
                    return previousIntersectStart;
                }

                utcRelativeTo = previousIntersectStart.Value;
            }
            else
            {
                if (_periodsToIntersect.ContainsInstant(previousBaseStart!.Value))
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
            var nextIntersectStart = _periodsToIntersect.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            if (nextBaseStart == null && nextIntersectStart == null)
            {
                return null;
            }

            if (nextBaseStart == nextIntersectStart)
            {
                return nextBaseStart;
            }

            if (nextIntersectStart != null && (nextBaseStart == null || nextIntersectStart < nextBaseStart))
            {
                if (_source.ContainsInstant(nextIntersectStart.Value))
                {
                    return nextIntersectStart;
                }

                utcRelativeTo = nextIntersectStart.Value;
            }
            else
            {
                if (_periodsToIntersect.ContainsInstant(nextBaseStart!.Value))
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
        var addendStartIsInstant = _periodsToIntersect.StartTimeline.IsInstant(utcDateTime);
        if (!baseStartIsInstant && !addendStartIsInstant)
        {
            return false;
        }

        if (baseStartIsInstant && addendStartIsInstant)
        {
            return true;
        }

        if (baseStartIsInstant && _periodsToIntersect.ContainsInstant(utcDateTime))
        {
            return true;
        }
        if (addendStartIsInstant && _source.ContainsInstant(utcDateTime))
        {
            return true;
        }

        return false;
    }
}