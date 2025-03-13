using Occurify.Extensions;

namespace Occurify.TimelineFilters;

internal class WithinTimeline : Timeline
{
    private readonly ITimeline _source;
    private readonly IPeriodTimeline _mask;

    public WithinTimeline(ITimeline source, IPeriodTimeline mask)
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

        do
        {
            var previous = _source.GetPreviousUtcInstant(utcRelativeTo);
            if (previous == null)
            {
                return null;
            }

            if (_mask.ContainsInstant(previous.Value))
            {
                return previous;
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

            if (_mask.ContainsInstant(next.Value))
            {
                return next;
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

        return _source.IsInstant(utcDateTime) && _mask.ContainsInstant(utcDateTime);
    }
}