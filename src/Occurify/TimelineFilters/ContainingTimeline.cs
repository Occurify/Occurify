using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.TimelineFilters;

internal class ContainingTimeline : Timeline
{
    private readonly ITimeline _source;
    private readonly ITimeline _instantsToContain;

    public ContainingTimeline(ITimeline source, ITimeline instantsToContain)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _instantsToContain = instantsToContain ?? throw new ArgumentNullException(nameof(instantsToContain));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        if (utcRelativeTo == DateTime.MinValue)
        {
            return null;
        }

        utcRelativeTo -= TimeSpan.FromTicks(1);

        do
        {
            var previousSource = _source.GetCurrentOrPreviousUtcInstant(utcRelativeTo);
            if (previousSource == null)
            {
                return null;
            }

            var previousInstantToContain = _instantsToContain.GetCurrentOrPreviousUtcInstant(utcRelativeTo);
            if (previousInstantToContain == null)
            {
                return null;
            }

            if (previousSource == previousInstantToContain)
            {
                return previousSource;
            }

            utcRelativeTo = DateTimeHelper.GetEarliest(previousSource.Value, previousInstantToContain.Value);
        } while (true);
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        if (utcRelativeTo == DateTime.MaxValue)
        {
            return null;
        }

        utcRelativeTo += TimeSpan.FromTicks(1);

        do
        {
            var nextSource = _source.GetCurrentOrNextUtcInstant(utcRelativeTo);
            if (nextSource == null)
            {
                return null;
            }

            var nextInstantToContain = _instantsToContain.GetCurrentOrNextUtcInstant(utcRelativeTo);
            if (nextInstantToContain == null)
            {
                return null;
            }

            if (nextSource == nextInstantToContain)
            {
                return nextSource;
            }

            utcRelativeTo = DateTimeHelper.GetLatest(nextSource.Value, nextInstantToContain.Value);
        } while (true);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _source.IsInstant(utcDateTime) && _instantsToContain.IsInstant(utcDateTime);
    }
}