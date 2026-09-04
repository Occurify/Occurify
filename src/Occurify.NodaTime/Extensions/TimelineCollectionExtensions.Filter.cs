using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class TimelineCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the first <paramref name="count"/> instants of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/> are bypassed.
    /// </summary>
    public static IEnumerable<ITimeline> SkipWithin(this IEnumerable<ITimeline> source, Interval mask, int count) =>
        source.SkipWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the first <paramref name="count"/> instants of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/> are bypassed.
    /// </summary>
    public static IEnumerable<ITimeline> SkipWithin(this IEnumerable<ITimeline> source, IEnumerable<Interval> mask, int count) =>
        source.SkipWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the last <paramref name="count"/> instants of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/> are omitted.
    /// </summary>
    public static IEnumerable<ITimeline> SkipLastWithin(this IEnumerable<ITimeline> source, Interval mask, int count) =>
        source.SkipLastWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the last <paramref name="count"/> instants of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/> are omitted.
    /// </summary>
    public static IEnumerable<ITimeline> SkipLastWithin(this IEnumerable<ITimeline> source, IEnumerable<Interval> mask, int count) =>
        source.SkipLastWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first <paramref name="count"/> instants of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeWithin(this IEnumerable<ITimeline> source, Interval mask, int count) =>
        source.TakeWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first <paramref name="count"/> instants of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeWithin(this IEnumerable<ITimeline> source, IEnumerable<Interval> mask, int count) =>
        source.TakeWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last <paramref name="count"/> instants of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeLastWithin(this IEnumerable<ITimeline> source, Interval mask, int count) =>
        source.TakeLastWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last <paramref name="count"/> instants of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeLastWithin(this IEnumerable<ITimeline> source, IEnumerable<Interval> mask, int count) =>
        source.TakeLastWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first instant of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> FirstWithin(this IEnumerable<ITimeline> source, Interval mask) =>
        source.FirstWithin(mask.ToPeriod());

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first instant of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> FirstWithin(this IEnumerable<ITimeline> source, IEnumerable<Interval> mask) =>
        source.FirstWithin(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last instant of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> LastWithin(this IEnumerable<ITimeline> source, Interval mask) =>
        source.LastWithin(mask.ToPeriod());

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last instant of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> LastWithin(this IEnumerable<ITimeline> source, IEnumerable<Interval> mask) =>
        source.LastWithin(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains <paramref name="instantToContain"/> if it is also present in the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Containing(this IEnumerable<ITimeline> source, Instant instantToContain) =>
        source.Containing(instantToContain.ToDateTimeUtc());

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Containing(this IEnumerable<ITimeline> source, IEnumerable<Instant> instantsToContain) =>
        source.Containing(instantsToContain.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Containing(this IEnumerable<ITimeline> source, params Instant[] instantsToContain) =>
        source.Containing(instantsToContain.Select(i => i.ToDateTimeUtc()).ToArray());

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Within(this IEnumerable<ITimeline> source, Interval mask) =>
        source.Within(mask.ToPeriod());

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Within(this IEnumerable<ITimeline> source, IEnumerable<Interval> mask) =>
        source.Within(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Within(this IEnumerable<ITimeline> source, params Interval[] mask) =>
        source.Within(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are not inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Outside(this IEnumerable<ITimeline> source, Interval mask) =>
        source.Outside(mask.ToPeriod());

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Outside(this IEnumerable<ITimeline> source, IEnumerable<Interval> mask) =>
        source.Outside(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Outside(this IEnumerable<ITimeline> source, params Interval[] mask) =>
        source.Outside(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the timelines do not contain <paramref name="instantToExclude"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Without(this IEnumerable<ITimeline> source, Instant instantToExclude) =>
        source.Without(instantToExclude.ToDateTimeUtc());

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Without(this IEnumerable<ITimeline> source, IEnumerable<Instant> instantsToExclude) =>
        source.Without(instantsToExclude.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from the timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Without(this IEnumerable<ITimeline> source, params Instant[] instantsToExclude) =>
        source.Without(instantsToExclude.Select(i => i.ToDateTimeUtc()).ToArray());

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every instant, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic.
    /// </summary>
    /// <remarks>
    /// When both <c>Occurify.Extensions</c> and <c>Occurify.NodaTime.Extensions</c> are imported, type the lambda parameter (<c>(Instant i) => ...</c>) to disambiguate from the <see cref="DateTime"/> overload.
    /// </remarks>
    public static IEnumerable<ITimeline> WhereInstants(this IEnumerable<ITimeline> source, Func<Instant, bool> predicate) =>
        source.WhereInstants((DateTime dt) => predicate(dt.ToInstant()));
}
