
using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.PeriodTimelineTransformations;

internal class MergeStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _periodsToAdd;

    // Note: If the result is full for the entire timeline, we have to return a single start at DateTime.MinValue.
    private bool? _isResultFull;

    public MergeStartTimeline(IPeriodTimeline source, IPeriodTimeline periodsToAdd)
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

        if (IsResultFull())
        {
            return utcRelativeTo == DateTime.MinValue ? null : DateTimeHelper.MinValueUtc;
        }

        return DeterminePreviousUtcInstant(utcRelativeTo);
    }

    private DateTime? DeterminePreviousUtcInstant(DateTime utcRelativeTo)
    {
        do
        {
            var previousSourceStart = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            var previousAddendStart = _periodsToAdd.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previousSourceStart == null && previousAddendStart == null)
            {
                return null;
            }

            if (previousSourceStart == previousAddendStart)
            {
                _isResultFull = false;
                return previousSourceStart;
            }

            if (previousAddendStart != null && (previousSourceStart == null || previousAddendStart > previousSourceStart))
            {
                if (!_source.ContainsInstant(previousAddendStart.Value))
                {
                    _isResultFull = false;
                    return previousAddendStart;
                }

                utcRelativeTo = previousAddendStart.Value;
            }
            else
            {
                if (!_periodsToAdd.ContainsInstant(previousSourceStart!.Value))
                {
                    _isResultFull = false;
                    return previousSourceStart;
                }

                utcRelativeTo = previousSourceStart.Value;
            }
        } while (true);
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        if (IsResultFull())
        {
            return null;
        }

        return DetermineNextUtcInstant(utcRelativeTo);
    }

    private DateTime? DetermineNextUtcInstant(DateTime utcRelativeTo)
    {
        do
        {
            var nextSourceStart = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            var nextAddendStart = _periodsToAdd.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            if (nextSourceStart == null && nextAddendStart == null)
            {
                return null;
            }

            if (nextSourceStart == nextAddendStart)
            {
                return nextSourceStart;
            }

            if (nextAddendStart != null && (nextSourceStart == null || nextAddendStart < nextSourceStart))
            {
                if (!_source.ContainsInstant(nextAddendStart.Value))
                {
                    return nextAddendStart;
                }

                utcRelativeTo = nextAddendStart.Value;
            }
            else
            {
                if (!_periodsToAdd.ContainsInstant(nextSourceStart!.Value))
                {
                    return nextSourceStart;
                }

                utcRelativeTo = nextSourceStart.Value;
            }
        } while (true);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        if (IsResultFull())
        {
            return utcDateTime == DateTime.MinValue;
        }

        var sourceStartIsInstant = _source.StartTimeline.IsInstant(utcDateTime);
        var addendStartIsInstant = _periodsToAdd.StartTimeline.IsInstant(utcDateTime);
        if (!sourceStartIsInstant && !addendStartIsInstant)
        {
            return false;
        }

        if (sourceStartIsInstant && addendStartIsInstant)
        {
            return true;
        }

        if (sourceStartIsInstant && !_periodsToAdd.ContainsInstant(utcDateTime))
        {
            return true;
        }
        if (addendStartIsInstant && !_source.ContainsInstant(utcDateTime))
        {
            return true;
        }

        return false;
    }

    private bool IsResultFull()
    {
        if (_isResultFull != null)
        {
            return _isResultFull.Value;
        }

        if (_source.IsEmpty() || _periodsToAdd.IsEmpty())
        {
            _isResultFull = false;
            return false;
        }

        if (DeterminePreviousUtcInstant(DateTimeHelper.MaxValueUtc) == null &&
            DetermineNextUtcInstant(DateTimeHelper.MaxValueUtc - TimeSpan.FromTicks(1)) == null &&
            new MergeEndTimeline(_source, _periodsToAdd).GetNextUtcInstant(DateTimeHelper.MinValueUtc) == null)
        {
            _isResultFull = true;
            return true;
        }

        _isResultFull = false;
        return false;
    }
}