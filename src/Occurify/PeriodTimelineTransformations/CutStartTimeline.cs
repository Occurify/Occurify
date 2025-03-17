
using Occurify.Extensions;

namespace Occurify.PeriodTimelineTransformations;

internal class CutStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly ITimeline _cutInstants;

    public CutStartTimeline(IPeriodTimeline source, ITimeline cutInstants)
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
            var previousStart = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            var previousCut = _cutInstants.GetPreviousUtcInstant(utcRelativeTo);
            if (previousStart == null && previousCut == null)
            {
                return null;
            }

            if (previousCut != null && (previousStart == null || previousCut > previousStart))
            {
                if (_source.ContainsInstant(previousCut.Value))
                {
                    return previousCut.Value;
                }

                utcRelativeTo = previousCut.Value;
            }
            else
            {
                return previousStart;
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
            var nextStart = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            var nextCut = _cutInstants.GetNextUtcInstant(utcRelativeTo);
            if (nextStart == null && nextCut == null)
            {
                return null;
            }

            if (nextCut != null && (nextStart == null || nextCut < nextStart))
            {
                if (_source.ContainsInstant(nextCut.Value))
                {
                    return nextCut.Value;
                }

                utcRelativeTo = nextCut.Value;
            }
            else
            {
                return nextStart;
            }
        } while (true);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _source.StartTimeline.IsInstant(utcDateTime) ||
               (_cutInstants.IsInstant(utcDateTime) && _source.ContainsInstant(utcDateTime));
    }
}