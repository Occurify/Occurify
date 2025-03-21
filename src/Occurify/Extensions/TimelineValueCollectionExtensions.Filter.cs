
namespace Occurify.Extensions;

public static partial class TimelineExtensions
{
    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> SkipWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.SkipWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> SkipWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<Period> mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.SkipWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> SkipWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, ITimeline mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.SkipWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> SkipWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IPeriodTimeline mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.SkipWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> SkipLastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.SkipLastWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> SkipLastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<Period> mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.SkipLastWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> SkipLastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, ITimeline mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.SkipLastWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> SkipLastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IPeriodTimeline mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.SkipLastWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> TakeWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.TakeWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> TakeWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<Period> mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.TakeWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> TakeWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, ITimeline mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.TakeWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> TakeWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IPeriodTimeline mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.TakeWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> TakeLastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.TakeLastWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> TakeLastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<Period> mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.TakeLastWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> TakeLastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, ITimeline mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.TakeLastWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> TakeLastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IPeriodTimeline mask, int count) =>
        source.ToDictionary(kvp => kvp.Key.TakeLastWithin(mask, count), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> FirstWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period mask) =>
        source.ToDictionary(kvp => kvp.Key.FirstWithin(mask), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> FirstWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<Period> mask) =>
        source.ToDictionary(kvp => kvp.Key.FirstWithin(mask), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> FirstWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, ITimeline mask) =>
        source.ToDictionary(kvp => kvp.Key.FirstWithin(mask), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> FirstWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IPeriodTimeline mask) =>
        source.ToDictionary(kvp => kvp.Key.FirstWithin(mask), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> LastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period mask) =>
        source.ToDictionary(kvp => kvp.Key.LastWithin(mask), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> LastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<Period> mask) =>
        source.ToDictionary(kvp => kvp.Key.LastWithin(mask), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> LastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, ITimeline mask) =>
        source.ToDictionary(kvp => kvp.Key.LastWithin(mask), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> LastWithin<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IPeriodTimeline mask) =>
        source.ToDictionary(kvp => kvp.Key.LastWithin(mask), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contain <paramref name="instantToContain"/> if it is also present in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instantToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<DateTime> instantsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, params DateTime[] instantsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, ITimeline instantsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<ITimeline> instantsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, params ITimeline[] instantsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Within<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Within<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<Period> mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Within<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, params Period[] mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Within<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IPeriodTimeline mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Outside<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Period mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Outside<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<Period> mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Outside<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, params Period[] mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Outside<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IPeriodTimeline mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that does not contain <paramref name="instantToExclude"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Without<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, DateTime instantToExclude) =>
        source.ToDictionary(kvp => kvp.Key.Without(instantToExclude), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Without<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<DateTime> instantsToExclude) =>
        source.ToDictionary(kvp => kvp.Key.Without(instantsToExclude), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Without<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, params DateTime[] instantsToExclude) =>
        source.ToDictionary(kvp => kvp.Key.Without(instantsToExclude), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Without<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, ITimeline instantsToExclude) =>
        source.ToDictionary(kvp => kvp.Key.Without(instantsToExclude), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Without<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, IEnumerable<ITimeline> instantsToExclude) =>
        source.ToDictionary(kvp => kvp.Key.Without(instantsToExclude), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> Without<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, params ITimeline[] instantsToExclude) =>
        source.ToDictionary(kvp => kvp.Key.Without(instantsToExclude), kvp => kvp.Value);

    /// <summary>
    /// Filters <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every instant, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic. 
    /// </summary>
    public static IEnumerable<KeyValuePair<ITimeline, TValue>> WhereInstants<TValue>(this IEnumerable<KeyValuePair<ITimeline, TValue>> source, Func<DateTime, bool> predicate) =>
        source.ToDictionary(kvp => kvp.Key.WhereInstants(predicate), kvp => kvp.Value);
}