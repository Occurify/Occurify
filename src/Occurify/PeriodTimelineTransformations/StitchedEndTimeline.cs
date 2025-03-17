
namespace Occurify.PeriodTimelineTransformations;

internal class StitchedEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;

    public StitchedEndTimeline(IPeriodTimeline source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? previousEnd, previousStart;
        do
        {
            previousEnd = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previousEnd == null)
            {
                return null;
            }

            previousStart = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            utcRelativeTo = previousEnd.Value;
        } while (previousStart != null && previousEnd == previousStart);

        return previousEnd;
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? nextEnd, nextStart;
        do
        {
            nextEnd = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            if (nextEnd == null)
            {
                return null;
            }

            nextStart = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            utcRelativeTo = nextEnd.Value;
        } while (nextStart != null && nextEnd == nextStart);

        return nextEnd;
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _source.EndTimeline.IsInstant(utcDateTime) && !_source.StartTimeline.IsInstant(utcDateTime);
    }
}