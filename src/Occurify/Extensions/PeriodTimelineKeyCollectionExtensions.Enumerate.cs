using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class PeriodTimelineKeyCollectionExtensions
{
    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> Enumerate<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) =>
        source.EnumerateFromIncludingPartial(DateTimeHelper.MinValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source) =>
        source.EnumerateBackwardsFromIncludingPartial(DateTimeHelper.MaxValueUtc);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcStart"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateFrom<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetKeysAtNextCompletePeriod(utcStart);

        while (current.Key != null)
        {
            yield return new KeyValuePair<Period, TKey[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                break;
            }
            current = source.GetKeysAtNextCompletePeriod(current.Key.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that start on or after <paramref name="utcEnd"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsTo<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(p => p.Key.Start >= utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcStart"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateFromIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetKeysAtNextPeriodIncludingPartial(utcStart);

        while (current.Key != null)
        {
            yield return new KeyValuePair<Period, TKey[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                break;
            }
            current = source.GetKeysAtNextCompletePeriod(current.Key.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or start after <paramref name="utcEnd"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsToIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(p => p.Key.End == null || p.Key.End > utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcEnd"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateTo<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcEnd) =>
        source.Enumerate().TakeWhile(p => p.Key.End <= utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that end before <paramref name="utcStart"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsFrom<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetKeysAtPreviousCompletePeriod(utcStart);

        while (current.Key != null)
        {
            yield return new KeyValuePair<Period, TKey[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                break;
            }
            current = source.GetKeysAtPreviousCompletePeriod(current.Key.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcEnd"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateToIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcEnd) =>
        source.Enumerate().TakeWhile(p => p.Key.Start == null || p.Key.Start < utcEnd);

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> that include or end before <paramref name="utcStart"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateBackwardsFromIncludingPartial<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart)
    {
        source = source.ToArray();
        var current = source.GetKeysAtPreviousPeriodIncludingPartial(utcStart);

        while (current.Key != null)
        {
            yield return new KeyValuePair<Period, TKey[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                break;
            }
            current = source.GetKeysAtPreviousCompletePeriod(current.Key.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateRange<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
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
        KeyValuePair<Period?, TKey[]> current;
        if (periodIncludeOptions.AllowsStartPartial())
        {
            current = source.GetKeysAtNextPeriodIncludingPartial(utcStart);
            if (current.Key == null)
            {
                yield break;
            }
            yield return new KeyValuePair<Period, TKey[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                yield break;
            }

            utcStart = current.Key.End.Value;
        }

        current = source.GetKeysAtNextCompletePeriod(utcStart);
        while (current.Key != null &&
               ((periodIncludeOptions.AllowsEndPartial() && current.Key.Start < utcEnd) ||
                (!periodIncludeOptions.AllowsEndPartial() && current.Key.End <= utcEnd)))
        {
            yield return new KeyValuePair<Period, TKey[]>(current.Key, current.Value);
            if (current.Key.End == null)
            {
                yield break;
            }
            current = source.GetKeysAtNextCompletePeriod(current.Key.End.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around <paramref name="utcStart"/> or <paramref name="utcEnd"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumerateRangeBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime utcStart, DateTime utcEnd, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
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
        KeyValuePair<Period?, TKey[]> current;
        if (periodIncludeOptions.AllowsEndPartial())
        {
            current = source.GetKeysAtPreviousPeriodIncludingPartial(utcEnd);
            if (current.Key == null)
            {
                yield break;
            }
            yield return new KeyValuePair<Period, TKey[]>(current.Key, current.Value);
            if (current.Key.Start == null)
            {
                yield break;
            }

            utcEnd = current.Key.Start.Value;
        }

        current = source.GetKeysAtPreviousCompletePeriod(utcEnd);
        while (current.Key != null &&
               ((periodIncludeOptions.AllowsStartPartial() && current.Key.End > utcStart) ||
                (!periodIncludeOptions.AllowsStartPartial() && current.Key.Start >= utcStart)))
        {
            yield return new KeyValuePair<Period, TKey[]>(current.Key, current.Value);
            if (current.Key.Start == null)
            {
                yield break;
            }
            current = source.GetKeysAtPreviousCompletePeriod(current.Key.Start.Value);
        }
    }

    /// <summary>
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from earliest to latest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumeratePeriod<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
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
    /// Enumerates all periods on <paramref name="source"/> within <paramref name="period"/> from latest to earliest and returns the period along with the keys of the timelines that include this exact period.
    /// <paramref name="periodIncludeOptions"/> defines inclusion of periods around the start and end of <paramref name="period"/>.
    /// Periods are ordered using <see cref="Period.CompareTo"/>. Duplicates are removed.
    /// </summary>
    public static IEnumerable<KeyValuePair<Period, TKey[]>> EnumeratePeriodBackwards<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period period, PeriodIncludeOptions periodIncludeOptions = PeriodIncludeOptions.CompleteOnly)
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