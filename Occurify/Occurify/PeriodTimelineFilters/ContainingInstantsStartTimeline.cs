using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.PeriodTimelineFilters;

internal class ContainingInstantsStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly ITimeline _instantsToContain;

    public ContainingInstantsStartTimeline(IPeriodTimeline source, ITimeline instantsToContain)
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
            var currentOrPrevious = _source.StartTimeline.GetCurrentOrPreviousUtcInstant(utcRelativeTo);
            if (currentOrPrevious == null)
            {
                return null;
            }

            var endOfPrevious = _source.EndTimeline.GetNextUtcInstant(currentOrPrevious.Value);

            var previousInstant = endOfPrevious == null ? 
                _instantsToContain.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc) : 
                _instantsToContain.GetPreviousUtcInstant(endOfPrevious.Value);

            if (previousInstant == null)
            {
                return null;
            }
            if (previousInstant >= currentOrPrevious)
            {
                return currentOrPrevious;
            }

            utcRelativeTo = DateTimeHelper.GetEarliest(currentOrPrevious.Value, previousInstant.Value);

        } while (true);
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        do
        {
            var next = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            if (next == null)
            {
                return null;
            }

            var endOfNext = _source.EndTimeline.GetNextUtcInstant(next.Value);

            var nextInstant = _instantsToContain.GetCurrentOrNextUtcInstant(next.Value);

            if (nextInstant == null)
            {
                return null;
            }
            if (endOfNext == null)
            {
                // There is another instant and the current period doesn't end.
                return next;
            }
            if (nextInstant < endOfNext)
            {
                return next;
            }

            // At this point we know there is another instant which is not in the current period. We immediately check if there is a period around that instant.
            var previousOrCurrentFromInstant = _source.StartTimeline.GetCurrentOrPreviousUtcInstant(nextInstant.Value);
            if (previousOrCurrentFromInstant == null)
            {
                throw new InvalidOperationException($"There should be a start instant before {nextInstant.Value}.");
            }

            var endOfPreviousOrCurrentFromInstant =
                _source.EndTimeline.GetNextUtcInstant(previousOrCurrentFromInstant.Value);

            if (endOfPreviousOrCurrentFromInstant == null || 
                new Period(previousOrCurrentFromInstant, endOfPreviousOrCurrentFromInstant).ContainsInstant(nextInstant
                    .Value))
            {
                return previousOrCurrentFromInstant;
            }

            utcRelativeTo = endOfPreviousOrCurrentFromInstant.Value - TimeSpan.FromTicks(1);

        } while (true);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        if (!_source.StartTimeline.IsInstant(utcDateTime))
        {
            return false;
        }

        return Period.Create(utcDateTime, _source.EndTimeline.GetNextUtcInstant(utcDateTime)).ContainsAnyInstant(_instantsToContain);
    }
}