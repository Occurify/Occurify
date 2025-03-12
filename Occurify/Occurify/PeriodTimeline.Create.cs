using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify;

public partial class PeriodTimeline
{
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with a single period <paramref name="period"/>.
    /// </summary>
    public static IPeriodTimeline FromPeriod(Period period)
    {
        if (period.IsInfiniteInBothDirections)
        {
            throw new ArgumentException("Infinite period cannot be represented as a period timeline", nameof(period));
        }

        return new PeriodTimeline(
            period.Start.AsTimeline(),
            period.End.AsTimeline());
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods <paramref name="periods"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline FromPeriods(IEnumerable<Period> periods) => PeriodTimelineHelper.CreatePeriodTimelineFromPeriods(periods);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods <paramref name="periods"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline FromPeriods(params Period[] periods) => PeriodTimelineHelper.CreatePeriodTimelineFromPeriods(periods);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with two periods: One from <c>null</c> to <paramref name="instant"/> and one from <paramref name="instant"/> to <c>null</c>.
    /// </summary>
    public static IPeriodTimeline FromInstantAsConsecutive(DateTime instant) =>
        FromInstantsAsConsecutive(instant.AsTimeline());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending with instants in <paramref name="timeline"/>.
    /// </summary>
    public static IPeriodTimeline FromInstantsAsConsecutive(ITimeline timeline) => new PeriodTimeline(timeline, timeline);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending with instants in <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline FromInstantsAsConsecutive(IEnumerable<DateTime> instants) => FromInstantsAsConsecutive(instants.AsTimeline());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending with instants in <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline FromInstantsAsConsecutive(params DateTime[] instants) =>
        FromInstantsAsConsecutive(instants.AsTimeline());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="periodStartTimeline"/> and ending with <paramref name="periodEndTimeline"/>.
    /// The result is Normalized.
    /// </summary>
    public static IPeriodTimeline Between(ITimeline periodStartTimeline, ITimeline periodEndTimeline) => new PeriodTimeline(periodStartTimeline, periodEndTimeline).Normalize();

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending with instants in <paramref name="timeline"/>.
    /// </summary>
    public static IPeriodTimeline Consecutive(ITimeline timeline) => timeline.AsConsecutivePeriodTimeline();

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending <paramref name="period"/> time apart.
    /// <see cref="DateTime.UtcNow"/> will be set as the origin. Meaning that a period starts at exactly that moment, and the periods are calculated from that instant.
    /// </summary>
    public static IPeriodTimeline Periodic(TimeSpan period) => Timeline.Periodic(period).AsConsecutivePeriodTimeline();

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending <paramref name="period"/> time apart.
    /// <paramref name="origin"/> will be set as the origin. Meaning that a period starts at exactly that moment, and the periods are calculated from that instant.
    /// </summary>
    public static IPeriodTimeline Periodic(DateTime origin, TimeSpan period) => Timeline.Periodic(origin, period).AsConsecutivePeriodTimeline();

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> without any periods.
    /// </summary>
    public static IPeriodTimeline Empty() => new PeriodTimeline(Timeline.Empty(), Timeline.Empty());
}