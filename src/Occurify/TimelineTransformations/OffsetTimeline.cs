using Occurify.Extensions;
using Occurify.Helpers;

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

        if (_offset == TimeSpan.Zero)
        {
            return _source.GetPreviousUtcInstant(utcRelativeTo);
        }

        if (_offset > TimeSpan.Zero)
        {
            if (utcRelativeTo.Ticks < _offset.Ticks)
            {
                return null;
            }
        }
        else if (DateTime.MaxValue.Ticks - utcRelativeTo.Ticks < (-_offset).Ticks)
        {
            return _source.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc)?.AddOrNullOnOverflow(_offset);
        }

        return _source.GetPreviousUtcInstant(utcRelativeTo - _offset)?.AddOrNullOnOverflow(_offset);
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        if (_offset == TimeSpan.Zero)
        {
            return _source.GetNextUtcInstant(utcRelativeTo);
        }

        if (_offset > TimeSpan.Zero)
        {
            if (utcRelativeTo.Ticks < _offset.Ticks)
            {
                return _source.GetCurrentOrNextUtcInstant(DateTimeHelper.MinValueUtc)?.AddOrNullOnOverflow(_offset);
            }
        }
        else if (DateTime.MaxValue.Ticks - utcRelativeTo.Ticks < (-_offset).Ticks)
        {
            return null;
        }

        return _source.GetNextUtcInstant(utcRelativeTo - _offset)?.AddOrNullOnOverflow(_offset);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        if (_offset == TimeSpan.Zero)
        {
            return _source.IsInstant(utcDateTime);
        }

        if (_offset > TimeSpan.Zero)
        {
            if (utcDateTime.Ticks < _offset.Ticks)
            {
                return false;
            }
        }
        else if (DateTime.MaxValue.Ticks - utcDateTime.Ticks < (-_offset).Ticks)
        {
            return false;
        }

        return _source.IsInstant(utcDateTime - _offset);
    }
}