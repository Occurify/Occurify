using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.PeriodTimelineFilters;

internal class ContainingInstantsEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly ITimeline _instantsToContain;

    public ContainingInstantsEndTimeline(IPeriodTimeline source, ITimeline instantsToContain)
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

        do
        {
            var previous = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previous == null)
            {
                return null;
            }

            var startOfPrevious = _source.StartTimeline.GetPreviousUtcInstant(previous.Value);

            var previousInstant = _instantsToContain.GetPreviousUtcInstant(previous.Value);

            if (previousInstant == null)
            {
                return null;
            }
            if (startOfPrevious == null)
            {
                // There is another instant and the current period has always started.
                return previous;
            }
            if (previousInstant >= previous)
            {
                return previous;
            }

            // At this point we know there is another instant which is not in the current period. We immediately check if there is a period around that instant.
            var nextFromInstant = _source.EndTimeline.GetNextUtcInstant(previousInstant.Value);
            if (nextFromInstant == null)
            {
                throw new InvalidOperationException($"There should be an end instant after {previousInstant.Value}.");
            }

            var startOfNextFromInstant =
                _source.StartTimeline.GetPreviousUtcInstant(nextFromInstant.Value);

            if (startOfNextFromInstant == null ||
                new Period(startOfNextFromInstant, nextFromInstant).ContainsInstant(previousInstant
                    .Value))
            {
                return nextFromInstant;
            }

            utcRelativeTo = startOfNextFromInstant.Value + TimeSpan.FromTicks(1);

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
            var next = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            if (next == null)
            {
                return null;
            }

            var startOfNext = _source.StartTimeline.GetPreviousUtcInstant(next.Value);

            var nextInstant = _instantsToContain.GetCurrentOrNextUtcInstant(startOfNext ?? DateTimeHelper.MinValueUtc);

            if (nextInstant == null)
            {
                return null;
            }
            if (nextInstant < next)
            {
                return next;
            }

            utcRelativeTo = DateTimeHelper.GetLatest(next.Value, nextInstant.Value);

        } while (true);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        if (!_source.EndTimeline.IsInstant(utcDateTime))
        {
            return false;
        }

        return Period.Create(_source.StartTimeline.GetPreviousUtcInstant(utcDateTime), utcDateTime).ContainsAnyInstant(_instantsToContain);
    }
}