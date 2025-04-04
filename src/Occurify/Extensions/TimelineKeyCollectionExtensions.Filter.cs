
namespace Occurify.Extensions;

public static partial class TimelineKeyCollectionExtensions
{
    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> SkipWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Period mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SkipWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> SkipWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Period> mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SkipWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> SkipWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, ITimeline mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SkipWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> SkipWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IPeriodTimeline mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SkipWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> SkipLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Period mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SkipLastWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> SkipLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Period> mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SkipLastWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> SkipLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, ITimeline mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SkipLastWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> in which the last <paramref name="count"/> amount of instants of every timeline in <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> SkipLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IPeriodTimeline mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SkipLastWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> TakeWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Period mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.TakeWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> TakeWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Period> mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.TakeWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> TakeWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, ITimeline mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.TakeWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> TakeWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IPeriodTimeline mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.TakeWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> TakeLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Period mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.TakeLastWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> TakeLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Period> mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.TakeLastWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> TakeLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, ITimeline mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.TakeLastWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last <paramref name="count"/> instants of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> TakeLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IPeriodTimeline mask, int count) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.TakeLastWithin(mask, count));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> FirstWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Period mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.FirstWithin(mask));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> FirstWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Period> mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.FirstWithin(mask));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> FirstWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, ITimeline mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.FirstWithin(mask));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the first instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> FirstWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IPeriodTimeline mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.FirstWithin(mask));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> LastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Period mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.LastWithin(mask));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> LastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Period> mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.LastWithin(mask));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> LastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, ITimeline mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.LastWithin(mask));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contains the last instant of every timeline in <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// This method is applied to individual timelines in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> LastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IPeriodTimeline mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.LastWithin(mask));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that contain <paramref name="instantToContain"/> if it is also present in <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instantToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<DateTime> instantsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantsToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, params DateTime[] instantsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantsToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, ITimeline instantsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantsToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<ITimeline> instantsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantsToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, params ITimeline[] instantsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantsToContain));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Within<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Period mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Within<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Period> mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Within<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, params Period[] mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Within<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IPeriodTimeline mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Period mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Period> mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, params Period[] mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IPeriodTimeline mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Returns a timeline <see cref="IEnumerable{KeyValuePair}"/> that does not contain <paramref name="instantToExclude"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Without<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, DateTime instantToExclude) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(instantToExclude));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Without<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<DateTime> instantsToExclude) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(instantsToExclude));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Without<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, params DateTime[] instantsToExclude) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(instantsToExclude));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Without<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, ITimeline instantsToExclude) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(instantsToExclude));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Without<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<ITimeline> instantsToExclude) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(instantsToExclude));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> Without<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, params ITimeline[] instantsToExclude) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(instantsToExclude));

    /// <summary>
    /// Filters <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every instant, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic. 
    /// </summary>
    public static IEnumerable<KeyValuePair<TKey, ITimeline>> WhereInstants<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Func<DateTime, bool> predicate) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.WhereInstants(predicate));
}