using Occurify.Extensions;

namespace Occurify.PeriodTimelineFilters;

internal class WithinStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _mask;

    public WithinStartTimeline(IPeriodTimeline source, IPeriodTimeline mask)
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
            previous = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previous == null)
            {
                return null;
            }

            var endOfPrevious = _source.EndTimeline.GetNextUtcInstant(previous.Value);
            period = Period.Create(previous, endOfPrevious);

            if (!_mask.TryGetPeriod(previous.Value, out maskPeriod))
            {
                // If the previous instant is not in any mask period, we can optimize by starting to look one tick from the end of the previous mask period. This way we can skip any instants we know for sure are outside the mask.
                var previousMaskPeriodEnd = _mask.EndTimeline.GetCurrentOrPreviousUtcInstant(previous.Value);
                if (previousMaskPeriodEnd == null)
                {
                    return null;
                }
                // Add one tick to make sure we don't miss any instants that are on the end of the mask.
                previousMaskPeriodEnd = previousMaskPeriodEnd.Value.AddTicks(1);
                utcRelativeTo = previous.Value < previousMaskPeriodEnd.Value ? previous.Value : previousMaskPeriodEnd.Value;
            }
            else
            {
                utcRelativeTo = previous.Value;
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
            next = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            if (next == null)
            {
                return null;
            }

            var endOfNext = _source.EndTimeline.GetNextUtcInstant(next.Value);
            period = Period.Create(next, endOfNext);

            if (endOfNext == null)
            {
                return _mask.ContainsPeriod(period) ? next : null;
            }

            if (!_mask.TryGetPeriod(endOfNext.Value.AddTicks(-1), out maskPeriod))
            {
                // If the end of the next instant is not in any mask period, we can optimize by starting to look from just before the start of the next mask period. This way we can skip any instants we know for sure are outside the mask.
                var nextMaskPeriodStart = _mask.StartTimeline.GetCurrentOrNextUtcInstant(endOfNext.Value);
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

        if (!_source.StartTimeline.IsInstant(utcDateTime))
        {
            return false;
        }

        return _mask.ContainsPeriod(Period.Create(utcDateTime, _source.EndTimeline.GetNextUtcInstant(utcDateTime)));
    }
}