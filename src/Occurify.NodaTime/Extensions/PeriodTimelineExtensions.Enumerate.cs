using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Period> Enumerate(this IPeriodTimeline source) =>
        source.EnumerateFromIncludingPartial(DateTimeHelper.MinValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwards(this IPeriodTimeline source) =>
        source.EnumerateBackwardsFromIncludingPartial(DateTimeHelper.MaxValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcStart"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Period> EnumerateFrom(this IPeriodTimeline source, DateTime utcStart)
    {
        var current = source.GetNextCompletePeriod(utcStart);
        while (current != null)
        {
            yield return current;
            if (current.End == null)
            {
                break;
            }
            current = source.GetNextCompletePeriod(current.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcEnd"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsTo(this IPeriodTimeline source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(p => p.Start >= utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcStart"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Period> EnumerateFromIncludingPartial(this IPeriodTimeline source, DateTime utcStart)
    {
        if (source.TryGetPeriod(utcStart, out var period) && (period.Start == null ||
                                                                      period.Start < utcStart))
        {
            yield return period;
        }
            
        var current = source.GetNextCompletePeriod(utcStart);
        while (current != null)
        {
            yield return current;
            if (current.End == null)
            {
                break;
            }
            current = source.GetNextCompletePeriod(current.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcEnd"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsToIncludingPartial(this IPeriodTimeline source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(p => p.End == null || p.End > utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcEnd"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Period> EnumerateTo(this IPeriodTimeline source, DateTime utcEnd) =>
        source.TakeWhile(p => p.End <= utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcStart"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsFrom(this IPeriodTimeline source, DateTime utcStart)
    {
        var current = source.GetPreviousCompletePeriod(utcStart);
        while (current != null)
        {
            yield return current;
            if (current.Start == null)
            {
                break;
            }
            current = source.GetPreviousCompletePeriod(current.Start.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcEnd"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<Period> EnumerateToIncludingPartial(this IPeriodTimeline source, DateTime utcEnd) =>
        source.TakeWhile(p => p.Start == null || p.Start < utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcStart"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<Period> EnumerateBackwardsFromIncludingPartial(this IPeriodTimeline source, DateTime utcStart)
    {
        if (source.TryGetPeriod(utcStart, out var period) &&
            (period.Start == null || 
             period.Start < utcStart) &&
            (period.End == null ||
             period.End > utcStart))
        {
            yield return period;
        }

        var current = source.GetPreviousCompletePeriod(utcStart);
        while (current != null)
        {
            yield return current;
            if (current.Start == null)
            {
                break;
            }
            current = source.GetPreviousCompletePeriod(current.Start.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// </summary>
    public static IEnumerable<Period> EnumerateRange(this IPeriodTimeline source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        if (utcStart == utcEnd)
        {
            yield break;
        }

        if (utcStart > utcEnd)
        {
            (utcEnd, utcStart) = (utcStart, utcEnd);
        }

        if (periodIncludeOptions.AllowsStartPartial())
        {
            if (source.TryGetPeriod(utcStart, out var period) &&
                (period.Start == null ||
                 period.Start < utcStart))
            {
                yield return period;
            }
        }
            
        var current = source.GetNextCompletePeriod(utcStart);
        while (current != null &&
               ((periodIncludeOptions.AllowsEndPartial() && current.Start < utcEnd) ||
                (!periodIncludeOptions.AllowsEndPartial() && current.End <= utcEnd)))
        {
            yield return current;
            if (current.End == null)
            {
                yield break;
            }
            current = source.GetNextCompletePeriod(current.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// </summary>
    public static IEnumerable<Period> EnumerateRangeBackwards(this IPeriodTimeline source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        if (utcStart == utcEnd)
        {
            yield break;
        }

        if (utcStart > utcEnd)
        {
            (utcEnd, utcStart) = (utcStart, utcEnd);
        }

        if (periodIncludeOptions.AllowsEndPartial())
        {
            if (source.TryGetPeriod(utcEnd, out var period) &&
                (period.Start == null || 
                 period.Start < utcEnd) &&
                (period.End == null ||
                 period.End > utcEnd))
            {
                yield return period;
            }
        }

        var current = source.GetPreviousCompletePeriod(utcEnd);
        while (current != null &&
               ((periodIncludeOptions.AllowsStartPartial() && current.End > utcStart) ||
                (!periodIncludeOptions.AllowsStartPartial() && current.Start >= utcStart)))
        {
            yield return current;
            if (current.Start == null)
            {
                yield break;
            }
            current = source.GetPreviousCompletePeriod(current.Start.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// </summary>
    public static IEnumerable<Period> EnumeratePeriod(this IPeriodTimeline source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        if (period.Start != null && period.End != null)
        {
            return source.EnumerateRange(period.Start.Value, period.End.Value, periodIncludeOptions);
        }

        if (period.End != null)
        {
            return periodIncludeOptions.AllowsEndPartial()
                ? source.EnumerateToIncludingPartial(period.End.Value)
                : source.EnumerateTo(period.End.Value);
        }

        if (period.Start != null)
        {
            return periodIncludeOptions.AllowsStartPartial()
                ? source.EnumerateFromIncludingPartial(period.Start.Value)
                : source.EnumerateFrom(period.Start.Value);
        }

        return source;
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// </summary>
    public static IEnumerable<Period> EnumeratePeriodBackwards(this IPeriodTimeline source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        if (period.Start != null && period.End != null)
        {
            return source.EnumerateRangeBackwards(period.Start.Value, period.End.Value, periodIncludeOptions);
        }

        if (period.End != null)
        {
            return periodIncludeOptions.AllowsEndPartial()
                ? source.EnumerateBackwardsFromIncludingPartial(period.End.Value)
                : source.EnumerateBackwardsFrom(period.End.Value);
        }

        if (period.Start != null)
        {
            return periodIncludeOptions.AllowsStartPartial()
                ? source.EnumerateBackwardsToIncludingPartial(period.Start.Value)
                : source.EnumerateBackwardsTo(period.Start.Value);
        }

        return source.EnumerateBackwards();
    }
}