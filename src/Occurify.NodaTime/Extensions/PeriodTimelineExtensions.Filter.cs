using NodaTime;
using Occurify.NodaTime.Extensions;
using Occurify.PeriodTimelineFilters;
using System.Threading.Tasks;

namespace Occurify.Extensions;

public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals are inside <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Within(this IPeriodTimeline source, Interval mask) =>
        source.Within(mask.ToPeriod());

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Within(this IPeriodTimeline source, IEnumerable<Interval> mask) =>
        source.Within(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Within(this IPeriodTimeline source, params Interval[] mask) =>
        source.Within(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals not in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Outside(this IPeriodTimeline source, Interval mask) =>
        source.Outside(mask.ToPeriod());

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Outside(this IPeriodTimeline source, IEnumerable<Interval> mask) =>
        source.Outside(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Outside(this IPeriodTimeline source, params Interval[] mask) =>
        source.Outside(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals contain <paramref name="intervalToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, Interval intervalToContain) =>
        source.Containing(intervalToContain.ToPeriod());

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals contain any of the intervals in <paramref name="intervalsToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, IEnumerable<Interval> intervalsToContain) =>
        source.Containing(intervalsToContain.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals contain any of the intervals in <paramref name="intervalsToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, params Interval[] intervalsToContain) =>
        source.Containing(intervalsToContain.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals contain <paramref name="instantToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, Instant instantToContain) =>
        source.Containing(instantToContain.ToDateTimeUtc());

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, IEnumerable<Instant> instantsToContain) =>
        source.Containing(instantsToContain.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, params Instant[] instantsToContain) =>
        source.Containing(instantsToContain.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals do not contain <paramref name="intervalNotToContain"/>.
    /// </summary>
    public static IPeriodTimeline Without(this IPeriodTimeline source, Interval intervalNotToContain) =>
        source.Without(intervalNotToContain.ToPeriod());

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals do not contain any of the intervals in <paramref name="intervalsNotToContain"/>.
    /// </summary>
    public static IPeriodTimeline Without(this IPeriodTimeline source, IEnumerable<Interval> intervalsNotToContain) =>
        source.Without(intervalsNotToContain.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which intervals do not contain any of the intervals in <paramref name="intervalsNotToContain"/>.
    /// </summary>
    public static IPeriodTimeline Without(this IPeriodTimeline source, params Interval[] intervalsNotToContain) =>
        source.Without(intervalsNotToContain.Select(i => i.ToPeriod()));
}