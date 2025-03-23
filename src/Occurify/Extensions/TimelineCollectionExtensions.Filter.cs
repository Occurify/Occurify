
namespace Occurify.Extensions;

public static partial class TimelineExtensions
{
    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> SkipWithin(this IEnumerable<ITimeline> source, Period mask, int count) =>
        source.Select(tl => tl.SkipWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> SkipWithin(this IEnumerable<ITimeline> source, IEnumerable<Period> mask, int count) =>
        source.Select(tl => tl.SkipWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> SkipWithin(this IEnumerable<ITimeline> source, ITimeline mask, int count) =>
        source.Select(tl => tl.SkipWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> SkipWithin(this IEnumerable<ITimeline> source, IPeriodTimeline mask, int count) =>
        source.Select(tl => tl.SkipWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> SkipLastWithin(this IEnumerable<ITimeline> source, Period mask, int count) =>
        source.Select(tl => tl.SkipLastWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> SkipLastWithin(this IEnumerable<ITimeline> source, IEnumerable<Period> mask, int count) =>
        source.Select(tl => tl.SkipLastWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> SkipLastWithin(this IEnumerable<ITimeline> source, ITimeline mask, int count) =>
        source.Select(tl => tl.SkipLastWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> SkipLastWithin(this IEnumerable<ITimeline> source, IPeriodTimeline mask, int count) =>
        source.Select(tl => tl.SkipLastWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeWithin(this IEnumerable<ITimeline> source, Period mask, int count) =>
        source.Select(tl => tl.TakeWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeWithin(this IEnumerable<ITimeline> source, IEnumerable<Period> mask, int count) =>
        source.Select(tl => tl.TakeWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeWithin(this IEnumerable<ITimeline> source, ITimeline mask, int count) =>
        source.Select(tl => tl.TakeWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeWithin(this IEnumerable<ITimeline> source, IPeriodTimeline mask, int count) =>
        source.Select(tl => tl.TakeWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeLastWithin(this IEnumerable<ITimeline> source, Period mask, int count) =>
        source.Select(tl => tl.TakeLastWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeLastWithin(this IEnumerable<ITimeline> source, IEnumerable<Period> mask, int count) =>
        source.Select(tl => tl.TakeLastWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeLastWithin(this IEnumerable<ITimeline> source, ITimeline mask, int count) =>
        source.Select(tl => tl.TakeLastWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> TakeLastWithin(this IEnumerable<ITimeline> source, IPeriodTimeline mask, int count) =>
        source.Select(tl => tl.TakeLastWithin(mask, count));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> FirstWithin(this IEnumerable<ITimeline> source, Period mask) =>
        source.Select(tl => tl.FirstWithin(mask));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> FirstWithin(this IEnumerable<ITimeline> source, IEnumerable<Period> mask) =>
        source.Select(tl => tl.FirstWithin(mask));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> FirstWithin(this IEnumerable<ITimeline> source, ITimeline mask) =>
        source.Select(tl => tl.FirstWithin(mask));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> FirstWithin(this IEnumerable<ITimeline> source, IPeriodTimeline mask) =>
        source.Select(tl => tl.FirstWithin(mask));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> LastWithin(this IEnumerable<ITimeline> source, Period mask) =>
        source.Select(tl => tl.LastWithin(mask));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> LastWithin(this IEnumerable<ITimeline> source, IEnumerable<Period> mask) =>
        source.Select(tl => tl.LastWithin(mask));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> LastWithin(this IEnumerable<ITimeline> source, ITimeline mask) =>
        source.Select(tl => tl.LastWithin(mask));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> LastWithin(this IEnumerable<ITimeline> source, IPeriodTimeline mask) =>
        source.Select(tl => tl.LastWithin(mask));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that contain <paramref name="instantToContain"/> if it is also present in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Containing(this IEnumerable<ITimeline> source, DateTime instantToContain) =>
        source.Select(tl => tl.Containing(instantToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Containing(this IEnumerable<ITimeline> source, IEnumerable<DateTime> instantsToContain) =>
        source.Select(tl => tl.Containing(instantsToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Containing(this IEnumerable<ITimeline> source, params DateTime[] instantsToContain) =>
        source.Select(tl => tl.Containing(instantsToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Containing(this IEnumerable<ITimeline> source, ITimeline instantsToContain) =>
        source.Select(tl => tl.Containing(instantsToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Containing(this IEnumerable<ITimeline> source, IEnumerable<ITimeline> instantsToContain) =>
        source.Select(tl => tl.Containing(instantsToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Containing(this IEnumerable<ITimeline> source, params ITimeline[] instantsToContain) =>
        source.Select(tl => tl.Containing(instantsToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Within(this IEnumerable<ITimeline> source, Period mask) =>
        source.Select(tl => tl.Within(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Within(this IEnumerable<ITimeline> source, IEnumerable<Period> mask) =>
        source.Select(tl => tl.Within(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Within(this IEnumerable<ITimeline> source, params Period[] mask) =>
        source.Select(tl => tl.Within(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Within(this IEnumerable<ITimeline> source, IPeriodTimeline mask) =>
        source.Select(tl => tl.Within(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Outside(this IEnumerable<ITimeline> source, Period mask) =>
        source.Select(tl => tl.Outside(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Outside(this IEnumerable<ITimeline> source, IEnumerable<Period> mask) =>
        source.Select(tl => tl.Outside(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Outside(this IEnumerable<ITimeline> source, params Period[] mask) =>
        source.Select(tl => tl.Outside(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Outside(this IEnumerable<ITimeline> source, IPeriodTimeline mask) =>
        source.Select(tl => tl.Outside(mask));

    /// <summary>
    /// Returns a <see cref="IEnumerable{ITimeline}"/> that does not contain <paramref name="instantToExclude"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Without(this IEnumerable<ITimeline> source, DateTime instantToExclude) =>
        source.Select(tl => tl.Without(instantToExclude));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Without(this IEnumerable<ITimeline> source, IEnumerable<DateTime> instantsToExclude) =>
        source.Select(tl => tl.Without(instantsToExclude));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Without(this IEnumerable<ITimeline> source, params DateTime[] instantsToExclude) =>
        source.Select(tl => tl.Without(instantsToExclude));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Without(this IEnumerable<ITimeline> source, ITimeline instantsToExclude) =>
        source.Select(tl => tl.Without(instantsToExclude));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Without(this IEnumerable<ITimeline> source, IEnumerable<ITimeline> instantsToExclude) =>
        source.Select(tl => tl.Without(instantsToExclude));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<ITimeline> Without(this IEnumerable<ITimeline> source, params ITimeline[] instantsToExclude) =>
        source.Select(tl => tl.Without(instantsToExclude));

    /// <summary>
    /// Filters <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every instant, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic. 
    /// </summary>
    public static IEnumerable<ITimeline> WhereInstants(this IEnumerable<ITimeline> source, Func<DateTime, bool> predicate) =>
        source.Select(tl => tl.WhereInstants(predicate));
}