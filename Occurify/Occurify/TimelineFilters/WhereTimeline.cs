namespace Occurify.TimelineFilters;

internal class WhereTimeline : Timeline
{
    private readonly ITimeline _source;
    private readonly Func<DateTime, bool> _predicate;

    public WhereTimeline(ITimeline source, Func<DateTime, bool> predicate)
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
        do
        {
            previous = _source.GetPreviousUtcInstant(utcRelativeTo);
            if (previous == null)
            {
                return null;
            }

            utcRelativeTo = previous.Value;
        } while (!_predicate(previous.Value));

        return previous;
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? next;
        do
        {
            next = _source.GetNextUtcInstant(utcRelativeTo);
            if (next == null)
            {
                return null;
            }

            utcRelativeTo = next.Value;
        } while (!_predicate(next.Value));

        return next;
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _source.IsInstant(utcDateTime) && _predicate(utcDateTime);
    }
}