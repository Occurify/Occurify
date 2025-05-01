using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineValueCollectionExtensions
{
    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> Enumerate<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.EnumerateFromIncludingPartial(DateTimeHelper.MinValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwards<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source) =>
        source.EnumerateBackwardsFromIncludingPartial(DateTimeHelper.MaxValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcStart"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateFrom<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetValuesAtNextCompletePeriod(utcStart);

        while (current.Key != null)
        {
            yield return new KeyValuePair<Period, TValue[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                break;
            }
            current = source.GetValuesAtNextCompletePeriod(current.Key.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcEnd"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsTo<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(p => p.Key.Start >= utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcStart"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateFromIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetValuesAtNextPeriodIncludingPartial(utcStart);

        while (current.Key != null)
        {
            yield return new KeyValuePair<Period, TValue[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                break;
            }
            current = source.GetValuesAtNextCompletePeriod(current.Key.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcEnd"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsToIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(p => p.Key.End == null || p.Key.End > utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcEnd"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateTo<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcEnd) =>
        source.Enumerate().TakeWhile(p => p.Key.End <= utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcStart"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsFrom<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetValuesAtPreviousCompletePeriod(utcStart);

        while (current.Key != null)
        {
            yield return new KeyValuePair<Period, TValue[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                break;
            }
            current = source.GetValuesAtPreviousCompletePeriod(current.Key.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcEnd"/> from earliest to latest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateToIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcEnd) =>
        source.Enumerate().TakeWhile(p => p.Key.Start == null || p.Key.Start < utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcStart"/> from latest to earliest.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateBackwardsFromIncludingPartial<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetValuesAtPreviousPeriodIncludingPartial(utcStart);

        while (current.Key != null)
        {
            yield return new KeyValuePair<Period, TValue[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                break;
            }
            current = source.GetValuesAtPreviousCompletePeriod(current.Key.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateRange<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        if (utcStart == utcEnd)
        {
            yield break;
        }

        if (utcStart > utcEnd)
        {
            (utcEnd, utcStart) = (utcStart, utcEnd);
        }

        source = source.ToArray();
        KeyValuePair<Period?, TValue[]> current;
        if (periodIncludeOptions.AllowsStartPartial())
        {
            current = source.GetValuesAtNextPeriodIncludingPartial(utcStart);
            if (current.Key == null)
            {
                yield break;
            }
            yield return new KeyValuePair<Period, TValue[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                yield break;
            }

            utcStart = current.Key.End.Value;
        }

        current = source.GetValuesAtNextCompletePeriod(utcStart);
        while (current.Key != null &&
               ((periodIncludeOptions.AllowsEndPartial() && current.Key.Start < utcEnd) ||
                (!periodIncludeOptions.AllowsEndPartial() && current.Key.End <= utcEnd)))
        {
            yield return new KeyValuePair<Period, TValue[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                yield break;
            }
            current = source.GetValuesAtNextCompletePeriod(current.Key.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumerateRangeBackwards<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
    {
        if (utcStart == utcEnd)
        {
            yield break;
        }

        if (utcStart > utcEnd)
        {
            (utcEnd, utcStart) = (utcStart, utcEnd);
        }

        source = source.ToArray();
        KeyValuePair<Period?, TValue[]> current;
        if (periodIncludeOptions.AllowsEndPartial())
        {
            current = source.GetValuesAtPreviousPeriodIncludingPartial(utcEnd);
            if (current.Key == null)
            {
                yield break;
            }
            yield return new KeyValuePair<Period, TValue[]>(current.Key, current.Value);
            if (current.Key.Start == null)
            {
                yield break;
            }

            utcEnd = current.Key.Start.Value;
        }

        current = source.GetValuesAtPreviousCompletePeriod(utcEnd);
        while (current.Key != null &&
               ((periodIncludeOptions.AllowsStartPartial() && current.Key.End > utcStart) ||
                (!periodIncludeOptions.AllowsStartPartial() && current.Key.Start >= utcStart)))
        {
            yield return new KeyValuePair<Period, TValue[]>(current.Key, current.Value);
            if (current.Key.Start == null)
            {
                yield break;
            }
            current = source.GetValuesAtPreviousCompletePeriod(current.Key.Start.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from earliest to latest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumeratePeriod<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
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

        return source.Enumerate();
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from latest to earliest.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TValue[]>> EnumeratePeriodBackwards<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
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