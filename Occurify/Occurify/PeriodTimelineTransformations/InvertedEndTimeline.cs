
namespace Occurify.PeriodTimelineTransformations;

internal class InvertedEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;

    public InvertedEndTimeline(IPeriodTimeline source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? previousStart, previousEnd;
        do
        {
            previousStart = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previousStart == null)
            {
                return null;
            }

            previousEnd = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            utcRelativeTo = previousStart.Value;
        } while (previousEnd != null && previousStart == previousEnd);

        return previousStart;
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? nextStart, nextEnd;
        do
        {
            nextStart = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            if (nextStart == null)
            {
                return null;
            }

            nextEnd = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            utcRelativeTo = nextStart.Value;
        } while (nextEnd != null && nextStart == nextEnd);

        return nextStart;
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _source.StartTimeline.IsInstant(utcDateTime) && !_source.EndTimeline.IsInstant(utcDateTime);
    }
}