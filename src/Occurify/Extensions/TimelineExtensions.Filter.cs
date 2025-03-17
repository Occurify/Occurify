using Occurify.TimelineFilters;

namespace Occurify.Extensions;

public static partial class TimelineExtensions
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the first <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// </summary>
    public static ITimeline SkipWithin(this ITimeline source, Period mask, int count)
    {
        return source.SkipWithin(mask.AsPeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the first <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// </summary>
    public static ITimeline SkipWithin(this ITimeline source, IEnumerable<Period> mask, int count)
    {
        return source.SkipWithin(mask.AsPeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the first <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// </summary>
    public static ITimeline SkipWithin(this ITimeline source, ITimeline mask, int count)
    {
        return source.SkipWithin(mask.AsConsecutivePeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the first <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/> are bypassed.
    /// </summary>
    public static ITimeline SkipWithin(this ITimeline source, IPeriodTimeline mask, int count)
    {
        return new SkipTimeline(source, mask, count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the last <paramref name="count"/> amount of instants of <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// </summary>
    public static ITimeline SkipLastWithin(this ITimeline source, Period mask, int count)
    {
        return source.SkipLastWithin(mask.AsPeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the last <paramref name="count"/> amount of instants of <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// </summary>
    public static ITimeline SkipLastWithin(this ITimeline source, IEnumerable<Period> mask, int count)
    {
        return source.SkipLastWithin(mask.AsPeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the last <paramref name="count"/> amount of instants of <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// </summary>
    public static ITimeline SkipLastWithin(this ITimeline source, ITimeline mask, int count)
    {
        return source.SkipLastWithin(mask.AsConsecutivePeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the last <paramref name="count"/> amount of instants of <paramref name="source"/> within every period <paramref name="mask"/> are omitted.
    /// </summary>
    public static ITimeline SkipLastWithin(this ITimeline source, IPeriodTimeline mask, int count)
    {
        return new SkipLastTimeline(source, mask, count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeWithin(this ITimeline source, Period mask, int count)
    {
        return source.TakeWithin(mask.AsPeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeWithin(this ITimeline source, IEnumerable<Period> mask, int count)
    {
        return source.TakeWithin(mask.AsPeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeWithin(this ITimeline source, ITimeline mask, int count)
    {
        return source.TakeWithin(mask.AsConsecutivePeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeWithin(this ITimeline source, IPeriodTimeline mask, int count)
    {
        return new TakeTimeline(source, mask, count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeLastWithin(this ITimeline source, Period mask, int count)
    {
        return source.TakeLastWithin(mask.AsPeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeLastWithin(this ITimeline source, IEnumerable<Period> mask, int count)
    {
        return source.TakeLastWithin(mask.AsPeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeLastWithin(this ITimeline source, ITimeline mask, int count)
    {
        return source.TakeLastWithin(mask.AsConsecutivePeriodTimeline(), count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last <paramref name="count"/> instants of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeLastWithin(this ITimeline source, IPeriodTimeline mask, int count)
    {
        return new TakeLastTimeline(source, mask, count);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first instant of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline FirstWithin(this ITimeline source, Period mask) =>
        source.TakeWithin(mask, 1);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first instant of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline FirstWithin(this ITimeline source, IEnumerable<Period> mask) =>
        source.TakeWithin(mask, 1);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first instant of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline FirstWithin(this ITimeline source, ITimeline mask) =>
        source.TakeWithin(mask, 1);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first instant of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline FirstWithin(this ITimeline source, IPeriodTimeline mask) =>
        source.TakeWithin(mask, 1);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last instant of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline LastWithin(this ITimeline source, Period mask) =>
        source.TakeLastWithin(mask, 1);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last instant of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline LastWithin(this ITimeline source, IEnumerable<Period> mask) =>
        source.TakeLastWithin(mask, 1);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last instant of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline LastWithin(this ITimeline source, ITimeline mask) =>
        source.TakeLastWithin(mask, 1);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last instant of <paramref name="source"/> within every period in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline LastWithin(this ITimeline source, IPeriodTimeline mask) =>
        source.TakeLastWithin(mask, 1);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains <paramref name="instantToContain"/> if it is also present in <paramref name="source"/>.
    /// </summary>
    public static ITimeline Containing(this ITimeline source, DateTime instantToContain)
    {
        return new ContainingTimeline(source, instantToContain.AsTimeline());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static ITimeline Containing(this ITimeline source, IEnumerable<DateTime> instantsToContain)
    {
        return new ContainingTimeline(source, instantsToContain.AsTimeline());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static ITimeline Containing(this ITimeline source, params DateTime[] instantsToContain)
    {
        return new ContainingTimeline(source, instantsToContain.AsTimeline());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static ITimeline Containing(this ITimeline source, ITimeline instantsToContain)
    {
        return new ContainingTimeline(source, instantsToContain);
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static ITimeline Containing(this ITimeline source, IEnumerable<ITimeline> instantsToContain)
    {
        return new ContainingTimeline(source, instantsToContain.Combine());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static ITimeline Containing(this ITimeline source, params ITimeline[] instantsToContain)
    {
        return new ContainingTimeline(source, instantsToContain.Combine());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Within(this ITimeline source, Period mask)
    {
        return new WithinTimeline(source, mask.AsPeriodTimeline());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Within(this ITimeline source, IEnumerable<Period> mask)
    {
        return new WithinTimeline(source, mask.AsPeriodTimeline());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Within(this ITimeline source, params Period[] mask)
    {
        return new WithinTimeline(source, mask.AsPeriodTimeline());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Within(this ITimeline source, IPeriodTimeline mask)
    {
        return new WithinTimeline(source, mask);
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Outside(this ITimeline source, Period mask)
    {
        return source.Within(mask.AsPeriodTimeline().Invert());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Outside(this ITimeline source, IEnumerable<Period> mask)
    {
        return source.Within(mask.AsPeriodTimeline().Invert());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Outside(this ITimeline source, params Period[] mask)
    {
        return source.Within(mask.AsPeriodTimeline().Invert());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Outside(this ITimeline source, IPeriodTimeline mask)
    {
        return source.Within(mask.Invert());
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that does not contain <paramref name="instantToExclude"/>.
    /// </summary>
    public static ITimeline Without(this ITimeline source, DateTime instantToExclude)
    {
        return new WithoutTimeline(source, instantToExclude.AsTimeline());
    }

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static ITimeline Without(this ITimeline source, IEnumerable<DateTime> instantsToExclude)
    {
        return new WithoutTimeline(source, instantsToExclude.AsTimeline());
    }

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static ITimeline Without(this ITimeline source, params DateTime[] instantsToExclude)
    {
        return new WithoutTimeline(source, instantsToExclude.AsTimeline());
    }

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static ITimeline Without(this ITimeline source, ITimeline instantsToExclude)
    {
        return new WithoutTimeline(source, instantsToExclude);
    }

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static ITimeline Without(this ITimeline source, IEnumerable<ITimeline> instantsToExclude)
    {
        return new WithoutTimeline(source, instantsToExclude.Combine());
    }

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static ITimeline Without(this ITimeline source, params ITimeline[] instantsToExclude)
    {
        return new WithoutTimeline(source, instantsToExclude.Combine());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every instant, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic. 
    /// </summary>
    public static ITimeline WhereInstants(this ITimeline source, Func<DateTime, bool> predicate)
    {
        return new WhereTimeline(source, predicate);
    }
}