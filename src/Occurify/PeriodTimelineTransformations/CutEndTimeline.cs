
using Occurify.Extensions;

namespace Occurify.PeriodTimelineTransformations;

internal class CutEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly ITimeline _cutInstants;

    public CutEndTimeline(IPeriodTimeline source, ITimeline cutInstants)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _cutInstants = cutInstants ?? throw new ArgumentNullException(nameof(cutInstants));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        do
        {
            var previousEnd = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            var previousCut = _cutInstants.GetPreviousUtcInstant(utcRelativeTo);
            if (previousEnd == null && previousCut == null)
            {
                return null;
            }

            if (previousCut != null && (previousEnd == null || previousCut > previousEnd))
            {
                if (_source.ContainsEnd(previousCut.Value))
                {
                    return previousCut.Value;
                }

                utcRelativeTo = previousCut.Value;
            }
            else
            {
                return previousEnd;
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
            var nextEnd = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            var nextCut = _cutInstants.GetNextUtcInstant(utcRelativeTo);
            if (nextEnd == null && nextCut == null)
            {
                return null;
            }

            if (nextCut != null && (nextEnd == null || nextCut < nextEnd))
            {
                if (_source.ContainsEnd(nextCut.Value))
                {
                    return nextCut.Value;
                }

                utcRelativeTo = nextCut.Value;
            }
            else
            {
                return nextEnd;
            }
        } while (true);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _source.EndTimeline.IsInstant(utcDateTime) ||
               (_cutInstants.IsInstant(utcDateTime) && _source.ContainsEnd(utcDateTime));
    }
}