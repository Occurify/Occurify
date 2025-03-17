using Occurify.Extensions;

namespace Occurify.PeriodTimelineFilters;

internal class WithinEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _mask;

    public WithinEndTimeline(IPeriodTimeline source, IPeriodTimeline mask)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _mask = mask ?? throw new ArgumentNullException(nameof(mask));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? previous;
        Period? maskPeriod;
        Period period;
        do
        {
            previous = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previous == null)
            {
                return null;
            }

            var startOfPrevious = _source.StartTimeline.GetPreviousUtcInstant(previous.Value);
            period = Period.Create(startOfPrevious, previous);

            if (startOfPrevious == null)
            {
                return _mask.ContainsPeriod(period) ? previous : null;
            }

            if (!_mask.TryGetPeriod(startOfPrevious.Value, out maskPeriod))
            {
                // If the start of the previous instant is not in any mask period, we can optimize by starting to look one tick from the end of the previous mask period. This way we can skip any instants we know for sure are outside the mask.
                var previousMaskPeriodEnd = _mask.EndTimeline.GetCurrentOrPreviousUtcInstant(startOfPrevious.Value);
                if (previousMaskPeriodEnd == null)
                {
                    return null;
                }

                // Add one tick to make sure we don't miss any instants that are on the end of the mask.
                utcRelativeTo = previousMaskPeriodEnd.Value.AddTicks(1);
            }
            else
            {
                utcRelativeTo = startOfPrevious.Value;
            }
        }
        while (maskPeriod == null || !_mask.ContainsPeriod(period));

        return previous;
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? next;
        Period? maskPeriod;
        Period period;
        do
        {
            next = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            if (next == null)
            {
                return null;
            }

            var startOfNext = _source.StartTimeline.GetPreviousUtcInstant(next.Value);
            period = Period.Create(startOfNext, next);

            if (!_mask.TryGetPeriod(next.Value.AddTicks(-1), out maskPeriod))
            {
                // If the next instant is not in any mask period, we can optimize by starting to look from just before the start of the next mask period. This way we can skip any instants we know for sure are outside the mask.
                var nextMaskPeriodStart = _mask.EndTimeline.GetCurrentOrNextUtcInstant(next.Value);
                if (nextMaskPeriodStart == null)
                {
                    return null;
                }
                utcRelativeTo = nextMaskPeriodStart.Value.AddTicks(-1); // note: this could be equal to next.Value, which is also fine.
            }
            else
            {
                utcRelativeTo = next.Value;
            }
        }
        while (maskPeriod == null || !_mask.ContainsPeriod(period));

        return next;
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        if (!_source.EndTimeline.IsInstant(utcDateTime))
        {
            return false;
        }

        return _mask.ContainsPeriod(Period.Create(_source.StartTimeline.GetPreviousUtcInstant(utcDateTime), utcDateTime));
    }
}