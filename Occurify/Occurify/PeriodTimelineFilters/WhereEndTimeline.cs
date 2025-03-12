namespace Occurify.PeriodTimelineFilters;

internal class WhereEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly Func<Period, bool> _predicate;

    public WhereEndTimeline(IPeriodTimeline source, Func<Period, bool> predicate)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
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
            previous = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previous == null)
            {
                return null;
            }

            var startOfNext = _source.StartTimeline.GetPreviousUtcInstant(previous.Value);
            period = Period.Create(startOfNext, previous);

            utcRelativeTo = previous.Value;

        } while (!_predicate(period));

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
            next = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            if (next == null)
            {
                return null;
            }

            var startOfNext = _source.StartTimeline.GetPreviousUtcInstant(next.Value);
            period = Period.Create(startOfNext, next);

            utcRelativeTo = next.Value;

        } while (!_predicate(period));

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

        return _predicate(Period.Create(_source.StartTimeline.GetPreviousUtcInstant(utcDateTime), utcDateTime));
    }
}