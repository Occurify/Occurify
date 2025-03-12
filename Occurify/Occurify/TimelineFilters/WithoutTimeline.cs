namespace Occurify.TimelineFilters;

internal class WithoutTimeline : Timeline
{
    private readonly ITimeline _source;
    private readonly ITimeline _instantsToExclude;

    public WithoutTimeline(ITimeline source, ITimeline instantsToExclude)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _instantsToExclude = instantsToExclude ?? throw new ArgumentNullException(nameof(instantsToExclude));
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
        } while (_instantsToExclude.IsInstant(previous.Value));

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
        } while (_instantsToExclude.IsInstant(next.Value));

        return next;
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _source.IsInstant(utcDateTime) && !_instantsToExclude.IsInstant(utcDateTime);
    }
}