namespace Occurify.TimelineTransformations;

internal class CompositeTimeline : Timeline
{
    private readonly ITimeline[] _timelines;

    public CompositeTimeline(IEnumerable<ITimeline> timelines)
        : this(timelines.ToArray())
    {
    }

    public CompositeTimeline(params ITimeline[] timelines)
    {
        if (timelines == null || !timelines.Any())
            throw new ArgumentException("At least one ITimeline is required.");

        _timelines = timelines;
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        return _timelines.Max(tl => tl.GetPreviousUtcInstant(utcRelativeTo));
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        return _timelines.Min(tl => tl.GetNextUtcInstant(utcRelativeTo));
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _timelines.Any(p => p.IsInstant(utcDateTime));
    }
}