using Occurify.Extensions;

namespace Occurify.Timelines;

internal class PeriodicTimeline : Timeline
{
    private readonly DateTime _utcOrigin;
    private readonly TimeSpan _period;

    public PeriodicTimeline(DateTime utcOrigin, TimeSpan period)
    {
        if (utcOrigin.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcOrigin)} should be UTC time.");
        }
        if (period <= TimeSpan.Zero)
        {
            throw new ArgumentException($"{nameof(period)} should be larger than zero.");
        }
        _utcOrigin = utcOrigin;
        _period = period;
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        var timeDifference = utcRelativeTo - _utcOrigin;
        var periodsElapsedSincePeriod = timeDifference.Ticks / _period.Ticks;

        if (timeDifference.Ticks % _period.Ticks == 0)
        {
            return _utcOrigin.AddOrNullOnOverflow(TimeSpan.FromTicks(_period.Ticks * (periodsElapsedSincePeriod - 1)));
        }

        if (timeDifference.Ticks < 0)
        {
            periodsElapsedSincePeriod--;
        }

        return _utcOrigin.AddOrNullOnOverflow(TimeSpan.FromTicks(_period.Ticks * periodsElapsedSincePeriod));
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        var timeDifference = utcRelativeTo - _utcOrigin;
        var periodsElapsedSincePeriod = timeDifference.Ticks / _period.Ticks;

        if (timeDifference.Ticks % _period.Ticks == 0)
        {
            return _utcOrigin.AddOrNullOnOverflow(TimeSpan.FromTicks(_period.Ticks * (periodsElapsedSincePeriod + 1)));
        }

        if (timeDifference.Ticks > 0)
        {
            periodsElapsedSincePeriod++;
        }

        return _utcOrigin.AddOrNullOnOverflow(TimeSpan.FromTicks(_period.Ticks * periodsElapsedSincePeriod));
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        var timeDifference = utcDateTime - _utcOrigin;
        return timeDifference.Ticks % _period.Ticks == 0;
    }
}