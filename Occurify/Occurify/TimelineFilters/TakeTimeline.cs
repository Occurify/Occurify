﻿using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.TimelineFilters;

internal class TakeTimeline : Timeline
{
    private readonly ITimeline _source;
    private readonly IPeriodTimeline _mask;
    private readonly int _count;

    public TakeTimeline(ITimeline source, IPeriodTimeline mask, int count)
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

        if (_count == 0)
        {
            return null;
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
                DateTime? lastInstant = null;
                foreach (var instant in _source.EnumerateFrom(period.Start ?? DateTimeHelper.MinValueUtc).Take(_count))
                {
                    if (instant == previous)
                    {
                        return previous;
                    }
                    lastInstant = instant;
                }

                return lastInstant;
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

        if (_count == 0)
        {
            return null;
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
                if (_source.EnumerateFrom(period.Start ?? DateTimeHelper.MinValueUtc).Take(_count).Any(instant => instant == next))
                {
                    return next;
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

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        if (_count == 0)
        {
            return false;
        }
        if (!_source.IsInstant(utcDateTime))
        {
            return false;
        }
        if (!_mask.TryGetPeriod(utcDateTime, out var period))
        {
            return false;
        }

        return _source.EnumerateFrom(period.Start ?? DateTimeHelper.MinValueUtc).Take(_count).Any(instant => instant == utcDateTime);
    }
}