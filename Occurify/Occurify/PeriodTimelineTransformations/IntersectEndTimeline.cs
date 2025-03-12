
using Occurify.Extensions;

namespace Occurify.PeriodTimelineTransformations;

internal class IntersectEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _periodsToIntersect;

    public IntersectEndTimeline(IPeriodTimeline source, IPeriodTimeline periodsToIntersect)
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
            var previousBaseEnd = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            var previousIntersectEnd = _periodsToIntersect.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previousBaseEnd == null && previousIntersectEnd == null)
            {
                return null;
            }

            if (previousBaseEnd == previousIntersectEnd)
            {
                return previousBaseEnd;
            }

            if (previousIntersectEnd != null && (previousBaseEnd == null || previousIntersectEnd > previousBaseEnd))
            {
                if (_source.ContainsEnd(previousIntersectEnd.Value))
                {
                    return previousIntersectEnd;
                }

                utcRelativeTo = previousIntersectEnd.Value;
            }
            else
            {
                if (_periodsToIntersect.ContainsEnd(previousBaseEnd!.Value))
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
            var nextIntersectEnd = _periodsToIntersect.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            if (nextBaseEnd == null && nextIntersectEnd == null)
            {
                return null;
            }

            if (nextBaseEnd == nextIntersectEnd)
            {
                return nextBaseEnd;
            }

            if (nextIntersectEnd != null && (nextBaseEnd == null || nextIntersectEnd < nextBaseEnd))
            {
                if (_source.ContainsEnd(nextIntersectEnd.Value))
                {
                    return nextIntersectEnd;
                }

                utcRelativeTo = nextIntersectEnd.Value;
            }
            else
            {
                if (_periodsToIntersect.ContainsEnd(nextBaseEnd!.Value))
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
        var addendEndIsInstant = _periodsToIntersect.EndTimeline.IsInstant(utcDateTime);
        if (!baseEndIsInstant && !addendEndIsInstant)
        {
            return false;
        }

        if (baseEndIsInstant && addendEndIsInstant)
        {
            return true;
        }

        if (baseEndIsInstant && _periodsToIntersect.ContainsEnd(utcDateTime))
        {
            return true;
        }
        if (addendEndIsInstant && _source.ContainsEnd(utcDateTime))
        {
            return true;
        }

        return false;
    }
}