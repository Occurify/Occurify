using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.TimelineFilters;

internal class SkipTimeline : Timeline
{
    private readonly ITimeline _source;
    private readonly IPeriodTimeline _mask;
    private readonly int _count;

    public SkipTimeline(ITimeline source, IPeriodTimeline mask, int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _mask = mask ?? throw new ArgumentNullException(nameof(mask));
        _count = count;
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        do
        {
            var previous = _source.GetPreviousUtcInstant(utcRelativeTo);
            if (previous == null)
            {
                return null;
            }

            if (_mask.TryGetPeriod(previous.Value, out var period))
            {
                if (_source.EnumerateFrom(period.Start ?? DateTimeHelper.MinValueUtc).Take(_count).All(i => i != previous))
                {
                    return previous;
                }
                // At this point we didn't find any match in the current period. We set the previous to the start of the period so the next logic can look from there.
                previous = period.Start;
                if (previous == null)
                {
                    return null;
                }
            }

            // If the previous instant is not in any mask period, we can optimize by starting to look from the end of the previous mask period. This way we can skip any instants we know for sure are outside the mask.
            var previousMaskPeriodEnd = _mask.EndTimeline.GetCurrentOrPreviousUtcInstant(previous.Value);
            if (previousMaskPeriodEnd == null)
            {
                return null;
            }

            utcRelativeTo = previousMaskPeriodEnd.Value;
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
            var next = _source.GetNextUtcInstant(utcRelativeTo);
            if (next == null)
            {
                return null;
            }

            if (_mask.TryGetPeriod(next.Value, out var period))
            {
                if (TryFindNextInstantInPeriod(period, next.Value, out DateTime? actual))
                {
                    return actual;
                }

                // At this point we didn't find any match in the current period. We set the next to the end of the period so the next logic can look from there.
                next = period.End;
                if (next == null)
                {
                    return null;
                }
            }

            // If the next instant is not in any mask period, we can optimize by starting to look from just before the start of the next mask period. This way we can skip any instants we know for sure are outside the mask.
            var nextMaskPeriodStart = _mask.StartTimeline.GetCurrentOrNextUtcInstant(next.Value);
            if (nextMaskPeriodStart == null)
            {
                return null;
            }
            utcRelativeTo = nextMaskPeriodStart.Value.AddTicks(-1); // note: this could be equal to next.Value, which is fine.
        }
        while (true);
    }

    private bool TryFindNextInstantInPeriod(Period period, DateTime proposed, out DateTime? actual)
    {
        DateTime? lastInstantToSkip = null;
        var count = 0;
        // The reason we use take here instead of skip, is so we can stop iterating the moment we find an instant that doesn't fit in the period.
        foreach (var instant in _source.EnumerateFrom(period.Start ?? DateTimeHelper.MinValueUtc).Take(_count))
        {
            if (!period.ContainsInstant(instant))
            {
                actual = null;
                return false;
            }
            count++;
            lastInstantToSkip = instant;
        }

        if (count != _count)
        {
            // There were not enough instants left
            actual = null;
            return true;
        }

        if (lastInstantToSkip == null || lastInstantToSkip < proposed)
        {
            // Proposed falls out of the skip range, so it's valid.
            actual = proposed;
            return true;
        }

        // Proposed falls in the skip range, so we check the next value.
        var instantAfterSkip = _source.GetNextUtcInstant(lastInstantToSkip.Value);
        if (instantAfterSkip == null)
        {
            actual = null;
            return true;
        }
        if (period.ContainsInstant(instantAfterSkip.Value))
        {
            actual = instantAfterSkip;
            return true;
        }

        actual = null;
        return false;
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        if (!_source.IsInstant(utcDateTime))
        {
            return false;
        }
        if (!_mask.TryGetPeriod(utcDateTime, out var period))
        {
            return false;
        }

        return _source.EnumerateFrom(period.Start ?? DateTimeHelper.MinValueUtc).Take(_count).All(instant => instant != utcDateTime);
    }
}