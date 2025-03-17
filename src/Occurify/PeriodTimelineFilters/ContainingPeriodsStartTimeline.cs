using Occurify.Extensions;

namespace Occurify.PeriodTimelineFilters;

internal class ContainingPeriodsStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly IPeriodTimeline _periodsToContain;

    public ContainingPeriodsStartTimeline(IPeriodTimeline source, IPeriodTimeline periodsToContain)
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
            previous = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
            if (previous == null)
            {
                return null;
            }

            var endOfPrevious = _source.EndTimeline.GetNextUtcInstant(previous.Value);
            period = Period.Create(previous, endOfPrevious);

            var startOfPeriodToCheck =
                _periodsToContain.StartTimeline.GetCurrentOrNextUtcInstant(previous.Value);

            periodThatShouldBeContained = startOfPeriodToCheck == null
                ? null
                : Period.Create(startOfPeriodToCheck,
                    _periodsToContain.EndTimeline.GetNextUtcInstant(startOfPeriodToCheck.Value));

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
            next = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
            if (next == null)
            {
                return null;
            }

            var endOfNext = _source.EndTimeline.GetNextUtcInstant(next.Value);
            period = Period.Create(next, endOfNext);

            var startOfPeriodToCheck =
                _periodsToContain.StartTimeline.GetCurrentOrNextUtcInstant(next.Value);

            periodThatShouldBeContained = startOfPeriodToCheck == null
                ? null
                : Period.Create(startOfPeriodToCheck,
                    _periodsToContain.EndTimeline.GetNextUtcInstant(startOfPeriodToCheck.Value));

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

        if (!_source.StartTimeline.IsInstant(utcDateTime))
        {
            return false;
        }

        var startOfPeriodToCheck = _periodsToContain.StartTimeline.GetCurrentOrNextUtcInstant(utcDateTime);
        if (startOfPeriodToCheck == null)
        {
            return false;
        }

        return Period.Create(utcDateTime, _source.EndTimeline.GetNextUtcInstant(utcDateTime))
            .ContainsPeriod(Period.Create(startOfPeriodToCheck,
                _periodsToContain.EndTimeline.GetNextUtcInstant(startOfPeriodToCheck.Value)));
    }
}