using Occurify.PeriodTimelineFilters;

namespace Occurify.Extensions;

public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Filters <paramref name="source"/> based on which periods are inside <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Within(this IPeriodTimeline source, Period mask)
    {
        var periodMask = mask.AsPeriodTimeline();
        return new PeriodTimeline(
            new WithinStartTimeline(source, periodMask),
            new WithinEndTimeline(source, periodMask));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Within(this IPeriodTimeline source, IEnumerable<Period> mask)
    {
        var periodMask = mask.AsPeriodTimeline();
        return new PeriodTimeline(
            new WithinStartTimeline(source, periodMask),
            new WithinEndTimeline(source, periodMask));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Within(this IPeriodTimeline source, params Period[] mask)
    {
        var periodMask = mask.AsPeriodTimeline();
        return new PeriodTimeline(
            new WithinStartTimeline(source, periodMask),
            new WithinEndTimeline(source, periodMask));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Within(this IPeriodTimeline source, IPeriodTimeline mask)
    {
        return new PeriodTimeline(
            new WithinStartTimeline(source, mask),
            new WithinEndTimeline(source, mask));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods not in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Outside(this IPeriodTimeline source, Period mask)
    {
        var periodMask = mask.AsPeriodTimeline();
        if (periodMask.IsEmpty())
        {
            return source;
        }

        return source.Within(periodMask.Invert());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Outside(this IPeriodTimeline source, IEnumerable<Period> mask)
    {
        var periodMask = mask.AsPeriodTimeline();
        if (periodMask.IsEmpty())
        {
            return source;
        }

        return source.Within(periodMask.Invert());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Outside(this IPeriodTimeline source, params Period[] mask)
    {
        var periodMask = mask.AsPeriodTimeline();
        if (periodMask.IsEmpty())
        {
            return source;
        }

        return source.Within(periodMask.Invert());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IPeriodTimeline Outside(this IPeriodTimeline source, IPeriodTimeline mask)
    {
        if (mask.IsEmpty())
        {
            return source;
        }

        return source.Within(mask.Invert());
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods contain <paramref name="periodToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, Period periodToContain)
    {
        var periodToContainTimeline = periodToContain.AsPeriodTimeline();
        return new PeriodTimeline(
            new ContainingPeriodsStartTimeline(source, periodToContainTimeline),
            new ContainingPeriodsEndTimeline(source, periodToContainTimeline));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, IEnumerable<Period> periodsToContain)
    {
        var periodsToContainTimeline = periodsToContain.AsPeriodTimeline();
        return new PeriodTimeline(
            new ContainingPeriodsStartTimeline(source, periodsToContainTimeline),
            new ContainingPeriodsEndTimeline(source, periodsToContainTimeline));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, params Period[] periodsToContain)
    {
        var periodsToContainTimeline = periodsToContain.AsPeriodTimeline();
        return new PeriodTimeline(
            new ContainingPeriodsStartTimeline(source, periodsToContainTimeline),
            new ContainingPeriodsEndTimeline(source, periodsToContainTimeline));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, IPeriodTimeline periodsToContain)
    {
        return new PeriodTimeline(
            new ContainingPeriodsStartTimeline(source, periodsToContain),
            new ContainingPeriodsEndTimeline(source, periodsToContain));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods contain <paramref name="instantToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, DateTime instantToContain)
    {
        var instantToContainTimeline = instantToContain.AsTimeline();
        return new PeriodTimeline(
            new ContainingInstantsStartTimeline(source, instantToContainTimeline),
            new ContainingInstantsEndTimeline(source, instantToContainTimeline));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, IEnumerable<DateTime> instantsToContain)
    {
        var instantsToContainTimeline = instantsToContain.AsTimeline();
        return new PeriodTimeline(
            new ContainingInstantsStartTimeline(source, instantsToContainTimeline),
            new ContainingInstantsEndTimeline(source, instantsToContainTimeline));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, params DateTime[] instantsToContain)
    {
        var instantsToContainTimeline = instantsToContain.AsTimeline();
        return new PeriodTimeline(
            new ContainingInstantsStartTimeline(source, instantsToContainTimeline),
            new ContainingInstantsEndTimeline(source, instantsToContainTimeline));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IPeriodTimeline Containing(this IPeriodTimeline source, ITimeline instantsToContain)
    {
        return new PeriodTimeline(
            new ContainingInstantsStartTimeline(source, instantsToContain),
            new ContainingInstantsEndTimeline(source, instantsToContain));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods do not contain <paramref name="periodNotToContain"/>.
    /// </summary>
    public static IPeriodTimeline Without(this IPeriodTimeline source, Period periodNotToContain)
    {
        var periodNotToContainTimeline = periodNotToContain.AsPeriodTimeline();
        return new PeriodTimeline(
            new WithoutStartTimeline(source, periodNotToContainTimeline),
            new WithoutEndTimeline(source, periodNotToContainTimeline));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static IPeriodTimeline Without(this IPeriodTimeline source, IEnumerable<Period> periodsNotToContain)
    {
        var periodsNotToContainTimeline = periodsNotToContain.AsPeriodTimeline();
        return new PeriodTimeline(
            new WithoutStartTimeline(source, periodsNotToContainTimeline),
            new WithoutEndTimeline(source, periodsNotToContainTimeline));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static IPeriodTimeline Without(this IPeriodTimeline source, params Period[] periodsNotToContain)
    {
        var periodsNotToContainTimeline = periodsNotToContain.AsPeriodTimeline();
        return new PeriodTimeline(
            new WithoutStartTimeline(source, periodsNotToContainTimeline),
            new WithoutEndTimeline(source, periodsNotToContainTimeline));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static IPeriodTimeline Without(this IPeriodTimeline source, IPeriodTimeline periodsNotToContain)
    {
        return new PeriodTimeline(
            new WithoutStartTimeline(source, periodsNotToContain),
            new WithoutEndTimeline(source, periodsNotToContain));
    }

    /// <summary>
    /// Filters <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every period, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic. 
    /// </summary>
    public static IPeriodTimeline WherePeriods(this IPeriodTimeline source, Func<Period, bool> predicate)
    {
        return new PeriodTimeline(
            new WhereStartTimeline(source, predicate),
            new WhereEndTimeline(source, predicate));
    }
}