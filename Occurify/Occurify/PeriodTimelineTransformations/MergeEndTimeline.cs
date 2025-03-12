
using Occurify.Extensions;

namespace Occurify.PeriodTimelineTransformations;

internal class MergeEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _periodsToAdd;

    public MergeEndTimeline(IPeriodTimeline source, IPeriodTimeline periodsToAdd)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _periodsToAdd = periodsToAdd ?? throw new ArgumentNullException(nameof(periodsToAdd));
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
            var previousAddendEnd = _periodsToAdd.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previousBaseEnd == null && previousAddendEnd == null)
            {
                return null;
            }

            if (previousBaseEnd == previousAddendEnd)
            {
                return previousBaseEnd;
            }

            if (previousAddendEnd != null && (previousBaseEnd == null || previousAddendEnd > previousBaseEnd))
            {
                if (!_source.ContainsEnd(previousAddendEnd.Value))
                {
                    return previousAddendEnd;
                }

                utcRelativeTo = previousAddendEnd.Value;
            }
            else
            {
                if (!_periodsToAdd.ContainsEnd(previousBaseEnd!.Value))
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
            var nextAddendEnd = _periodsToAdd.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            if (nextBaseEnd == null && nextAddendEnd == null)
            {
                return null;
            }

            if (nextBaseEnd == nextAddendEnd)
            {
                return nextBaseEnd;
            }

            if (nextAddendEnd != null && (nextBaseEnd == null || nextAddendEnd < nextBaseEnd))
            {
                if (!_source.ContainsEnd(nextAddendEnd.Value))
                {
                    return nextAddendEnd;
                }

                utcRelativeTo = nextAddendEnd.Value;
            }
            else
            {
                if (!_periodsToAdd.ContainsEnd(nextBaseEnd!.Value))
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
        var addendEndIsInstant = _periodsToAdd.EndTimeline.IsInstant(utcDateTime);
        if (!baseEndIsInstant && !addendEndIsInstant)
        {
            return false;
        }

        if (baseEndIsInstant && addendEndIsInstant)
        {
            return true;
        }

        if (baseEndIsInstant && !_periodsToAdd.ContainsEnd(utcDateTime))
        {
            return true;
        }
        if (addendEndIsInstant && !_source.ContainsEnd(utcDateTime))
        {
            return true;
        }

        return false;
    }
}