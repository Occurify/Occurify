using Occurify.Helpers;

namespace Occurify.Extensions;

public static partial class TimelineExtensions
{
    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<DateTime> Enumerate(this ITimeline source) =>
        source.EnumerateFrom(DateTimeHelper.MinValueUtc);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateBackwards(this ITimeline source)
    {
        var current = source.GetCurrentOrPreviousUtcInstant(DateTimeHelper.MaxValueUtc);

        while (current != null)
        {
            yield return current.Value;
            current = source.GetPreviousUtcInstant(current.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="utcStart"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateFrom(this ITimeline source, DateTime utcStart)
    {
        var current = source.GetCurrentOrNextUtcInstant(utcStart);
        while (current != null)
        {
            yield return current.Value;
            current = source.GetNextUtcInstant(current.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur on or after <paramref name="utcEnd"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateBackwardsTo(this ITimeline source, DateTime utcEnd) =>
        source.EnumerateBackwards().TakeWhile(i => i >= utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="utcEnd"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateTo(this ITimeline source, DateTime utcEnd) =>
        source.TakeWhile(p => p < utcEnd);

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur earlier than <paramref name="utcStart"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateBackwardsFrom(this ITimeline source, DateTime utcStart)
    {
        var current = source.GetPreviousUtcInstant(utcStart);

        while (current != null)
        {
            yield return current.Value;
            current = source.GetPreviousUtcInstant(current.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateRange(this ITimeline source, DateTime utcStart, DateTime utcEnd)
    {
        if (utcStart == utcEnd)
        {
            yield break;
        }

        if (utcStart > utcEnd)
        {
            (utcEnd, utcStart) = (utcStart, utcEnd);
        }

        var current = source.GetCurrentOrNextUtcInstant(utcStart);
        while (current != null && current.Value < utcEnd)
        {
            yield return current.Value;
            current = source.GetNextUtcInstant(current.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur between <paramref name="utcStart"/> and <paramref name="utcEnd"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<DateTime> EnumerateRangeBackwards(this ITimeline source, DateTime utcStart, DateTime utcEnd)
    {
        if (utcStart == utcEnd)
        {
            yield break;
        }

        if (utcStart > utcEnd)
        {
            (utcEnd, utcStart) = (utcStart, utcEnd);
        }

        var current = source.GetPreviousUtcInstant(utcEnd);
        while (current != null && current.Value >= utcStart)
        {
            yield return current.Value;
            current = source.GetPreviousUtcInstant(current.Value);
        }
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="period"/> from earliest to latest.
    /// </summary>
    public static IEnumerable<DateTime> EnumeratePeriod(this ITimeline source, Period period)
    {
        if (period.Start != null && period.End != null)
        {
            return source.EnumerateRange(period.Start.Value, period.End.Value);
        }

        if (period.End != null)
        {
            return source.EnumerateTo(period.End.Value);
        }

        if (period.Start != null)
        {
            return source.EnumerateFrom(period.Start.Value);
        }

        return source;
    }

    /// <summary>
    /// Enumerates all instants on <paramref name="source"/> that occur in <paramref name="period"/> from latest to earliest.
    /// </summary>
    public static IEnumerable<DateTime> EnumeratePeriodBackwards(this ITimeline source, Period period)
    {
        if (period.Start != null && period.End != null)
        {
            return source.EnumerateRangeBackwards(period.Start.Value, period.End.Value);
        }

        if (period.End != null)
        {
            return source.EnumerateBackwardsFrom(period.End.Value);
        }

        if (period.Start != null)
        {
            return source.EnumerateBackwardsTo(period.Start.Value);
        }

        return source.EnumerateBackwards();
    }
}