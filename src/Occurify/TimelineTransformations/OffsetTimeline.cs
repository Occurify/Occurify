using Occurify.Extensions;

namespace Occurify.TimelineTransformations;

internal class OffsetTimeline : Timeline
{
    private readonly ITimeline _source;
    private readonly TimeSpan _offset;

    public OffsetTimeline(ITimeline source, TimeSpan offset)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));

        _offset = offset;
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        var adjustedUtcRelativeTo = utcRelativeTo.AddOrNullOnOverflow(-_offset);
        if (adjustedUtcRelativeTo == null)
        {
            return null;
        }

        return _source.GetPreviousUtcInstant(adjustedUtcRelativeTo.Value)?.AddOrNullOnOverflow(_offset);
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        var adjustedUtcRelativeTo = utcRelativeTo.AddOrNullOnOverflow(-_offset);
        if (adjustedUtcRelativeTo == null)
        {
            return null;
        }

        return _source.GetNextUtcInstant(adjustedUtcRelativeTo.Value)?.AddOrNullOnOverflow(_offset);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        var offsetResult = utcDateTime.AddOrNullOnOverflow(-_offset);
        if (offsetResult == null)
        {
            return false;
        }

        return _source.IsInstant(offsetResult.Value);
    }
}