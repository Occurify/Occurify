using Occurify.Extensions;

namespace Occurify.PeriodTimelineFilters;

internal class WithoutStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _periodsNotToContain;

    public WithoutStartTimeline(IPeriodTimeline source, IPeriodTimeline periodsNotToContain)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _periodsNotToContain = periodsNotToContain ?? throw new ArgumentNullException(nameof(periodsNotToContain));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? previous;
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

            utcRelativeTo = previous.Value;

        } while (_periodsNotToContain.EnumeratePeriod(period, PeriodIncludeOptions.PartialAllowed).Any());

        return previous;
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? next;
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

            utcRelativeTo = next.Value;

        } while (_periodsNotToContain.EnumeratePeriod(period, PeriodIncludeOptions.PartialAllowed).Any());

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

        var period = Period.Create(utcDateTime, _source.EndTimeline.GetNextUtcInstant(utcDateTime));

        return !_periodsNotToContain.EnumeratePeriod(period, PeriodIncludeOptions.PartialAllowed).Any();
    }
}