using Occurify.Extensions;

namespace Occurify.PeriodTimelineFilters;

internal class ContainingPeriodsEndTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _periodsToContain;

    public ContainingPeriodsEndTimeline(IPeriodTimeline source, IPeriodTimeline periodsToContain)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _periodsToContain = periodsToContain ?? throw new ArgumentNullException(nameof(periodsToContain));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? previous;
        Period? periodThatShouldBeContained;
        Period period;
        do
        {
            previous = _source.EndTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previous == null)
            {
                return null;
            }

            var startOfPrevious = _source.StartTimeline.GetPreviousUtcInstant(previous.Value);
            period = Period.Create(startOfPrevious, previous);

            var endOfPeriodToCheck =
                _periodsToContain.EndTimeline.GetCurrentOrPreviousUtcInstant(previous.Value);

            periodThatShouldBeContained = endOfPeriodToCheck == null
                ? null
                : Period.Create(_periodsToContain.StartTimeline.GetPreviousUtcInstant(endOfPeriodToCheck.Value), endOfPeriodToCheck);

            utcRelativeTo = previous.Value;

        } while (periodThatShouldBeContained == null || !period.ContainsPeriod(periodThatShouldBeContained));

        return previous;
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        DateTime? next;
        Period? periodThatShouldBeContained;
        Period period;
        do
        {
            next = _source.EndTimeline.GetNextUtcInstant(utcRelativeTo);
            if (next == null)
            {
                return null;
            }

            var startOfNext = _source.StartTimeline.GetPreviousUtcInstant(next.Value);
            period = Period.Create(startOfNext, next);

            var endOfPeriodToCheck =
                _periodsToContain.EndTimeline.GetCurrentOrPreviousUtcInstant(next.Value);

            periodThatShouldBeContained = endOfPeriodToCheck == null
                ? null
                : Period.Create(_periodsToContain.StartTimeline.GetPreviousUtcInstant(endOfPeriodToCheck.Value), endOfPeriodToCheck);

            utcRelativeTo = next.Value;

        } while (periodThatShouldBeContained == null || !period.ContainsPeriod(periodThatShouldBeContained));

        return next;
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

        var endOfPeriodToCheck = _periodsToContain.EndTimeline.GetCurrentOrPreviousUtcInstant(utcDateTime);
        if (endOfPeriodToCheck == null)
        {
            return false;
        }

        return Period.Create(_source.StartTimeline.GetPreviousUtcInstant(utcDateTime), utcDateTime)
            .ContainsPeriod(Period.Create(_periodsToContain.StartTimeline.GetPreviousUtcInstant(endOfPeriodToCheck.Value), endOfPeriodToCheck));
    }
}