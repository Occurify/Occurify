using Occurify.PeriodTimelineTransformations;

namespace Occurify.Extensions;

public static partial class PeriodTimelineExtensions
{
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IPeriodTimeline source, DateTime instant)
    {
        var cutTimeline = instant.AsTimeline();
        return new PeriodTimeline(
            new CutStartTimeline(source, cutTimeline),
            new CutEndTimeline(source, cutTimeline));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IPeriodTimeline source, IEnumerable<DateTime> instants)
    {
        var cutTimeline = instants.AsTimeline();
        return new PeriodTimeline(
            new CutStartTimeline(source, cutTimeline),
            new CutEndTimeline(source, cutTimeline));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IPeriodTimeline source, params DateTime[] instants)
    {
        var cutTimeline = instants.AsTimeline();
        return new PeriodTimeline(
            new CutStartTimeline(source, cutTimeline),
            new CutEndTimeline(source, cutTimeline));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IPeriodTimeline source, ITimeline instants)
    {
        return new PeriodTimeline(
            new CutStartTimeline(source, instants),
            new CutEndTimeline(source, instants));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IPeriodTimeline source, IEnumerable<ITimeline> instants)
    {
        var cutTimeline = instants.Combine();
        return new PeriodTimeline(
            new CutStartTimeline(source, cutTimeline),
            new CutEndTimeline(source, cutTimeline));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IPeriodTimeline source, params ITimeline[] instants)
    {
        var cutTimeline = instants.Combine();
        return new PeriodTimeline(
            new CutStartTimeline(source, cutTimeline),
            new CutEndTimeline(source, cutTimeline));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all periods in <paramref name="source"/> with equal end and start instants are combined into a single period.
    /// </summary>
    public static IPeriodTimeline Stitch(this IPeriodTimeline source)
    {
        return new PeriodTimeline(
            new StitchedStartTimeline(source),
            new StitchedEndTimeline(source));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriod(this IPeriodTimeline source, Period periodToIntersect)
    {
        var periodsToIntersectTimeline = periodToIntersect.AsPeriodTimeline();
        return new PeriodTimeline(
            new IntersectStartTimeline(source, periodsToIntersectTimeline),
            new IntersectEndTimeline(source, periodsToIntersectTimeline));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IPeriodTimeline source, IEnumerable<Period> periodsToIntersect)
    {
        var periodsToIntersectTimeline = periodsToIntersect.AsPeriodTimeline();
        return new PeriodTimeline(
            new IntersectStartTimeline(source, periodsToIntersectTimeline),
            new IntersectEndTimeline(source, periodsToIntersectTimeline));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IPeriodTimeline source, params Period[] periodsToIntersect)
    {
        var periodsToIntersectTimeline = periodsToIntersect.AsPeriodTimeline();
        return new PeriodTimeline(
            new IntersectStartTimeline(source, periodsToIntersectTimeline),
            new IntersectEndTimeline(source, periodsToIntersectTimeline));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IPeriodTimeline source, IPeriodTimeline periodsToIntersect)
    {
        return new PeriodTimeline(
            new IntersectStartTimeline(source, periodsToIntersect),
            new IntersectEndTimeline(source, periodsToIntersect));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IPeriodTimeline source, IEnumerable<IPeriodTimeline> periodsToIntersect)
    {
        return periodsToIntersect.Aggregate(source, (current, pp) => current.IntersectPeriods(pp));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with the intersections of <paramref name="source"/> with <paramref name="periodsToIntersect"/>.
    /// </summary>
    public static IPeriodTimeline IntersectPeriods(this IPeriodTimeline source, params IPeriodTimeline[] periodsToIntersect)
    {
        return periodsToIntersect.Aggregate(source, (current, pp) => current.IntersectPeriods(pp));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> that is inverted of <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Invert(this IPeriodTimeline source)
    {
        return new PeriodTimeline(
            new InvertedStartTimeline(source),
            new InvertedEndTimeline(source));
    }

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with <paramref name="periodToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IPeriodTimeline source, Period periodToMerge)
    {
        var periodsToAddTimeline = periodToMerge.AsPeriodTimeline();
        return new PeriodTimeline(
            new MergeStartTimeline(source, periodsToAddTimeline),
            new MergeEndTimeline(source, periodsToAddTimeline));
    }

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IPeriodTimeline source, IEnumerable<Period> periodsToMerge)
    {
        var periodsToAddTimeline = periodsToMerge.AsPeriodTimeline();
        return new PeriodTimeline(
            new MergeStartTimeline(source, periodsToAddTimeline),
            new MergeEndTimeline(source, periodsToAddTimeline));
    }

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IPeriodTimeline source, params Period[] periodsToMerge)
    {
        var periodsToAddTimeline = periodsToMerge.AsPeriodTimeline();
        return new PeriodTimeline(
            new MergeStartTimeline(source, periodsToAddTimeline),
            new MergeEndTimeline(source, periodsToAddTimeline));
    }

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IPeriodTimeline source, IPeriodTimeline periodsToMerge)
    {
        return new PeriodTimeline(
            new MergeStartTimeline(source, periodsToMerge),
            new MergeEndTimeline(source, periodsToMerge));
    }

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IPeriodTimeline source, IEnumerable<IPeriodTimeline> periodsToMerge)
    {
        return periodsToMerge.Aggregate(source, (current, pp) => current.Merge(pp));
    }

    /// <summary>
    /// Merges all periods in <paramref name="source"/> with all periods in <paramref name="periodsToMerge"/>. Overlapping periods are combined.
    /// </summary>
    public static IPeriodTimeline Merge(this IPeriodTimeline source, params IPeriodTimeline[] periodsToMerge)
    {
        return periodsToMerge.Aggregate(source, (current, pp) => current.Merge(pp));
    }

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which all start instants are followed by an end instant and vice versa.
    /// </summary>
    public static IPeriodTimeline Normalize(this IPeriodTimeline source)
    {
        return new PeriodTimeline(
            new NormalizedStartTimeline(source),
            new NormalizedEndTimeline(source));
    }

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline Offset(this IPeriodTimeline source, TimeSpan offset)
    {
        return new PeriodTimeline(
            source.StartTimeline.Offset(offset),
            source.EndTimeline.Offset(offset));
    }

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="ticks"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline OffsetTicks(this IPeriodTimeline source, long ticks) => source.Offset(TimeSpan.FromTicks(ticks));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="microseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline OffsetMicroseconds(this IPeriodTimeline source, double microseconds) =>
#if NET7_0 || NET8_0 || NET9_0
        source.Offset(TimeSpan.FromMicroseconds(microseconds));
#else
        source.Offset(TimeSpan.FromTicks((long)(microseconds * 10)));
#endif

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="milliseconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline OffsetMilliseconds(this IPeriodTimeline source, double milliseconds) => source.Offset(TimeSpan.FromMilliseconds(milliseconds));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="seconds"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline OffsetSeconds(this IPeriodTimeline source, double seconds) => source.Offset(TimeSpan.FromSeconds(seconds));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="minutes"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline OffsetMinutes(this IPeriodTimeline source, double minutes) => source.Offset(TimeSpan.FromMinutes(minutes));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="hours"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline OffsetHours(this IPeriodTimeline source, double hours) => source.Offset(TimeSpan.FromHours(hours));

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="days"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IPeriodTimeline OffsetDays(this IPeriodTimeline source, double days) => source.Offset(TimeSpan.FromDays(days));

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// </summary>
    public static IPeriodTimeline Randomize(this IPeriodTimeline source, TimeSpan maxDeviation)
    {
        return source.Randomize(new Random().Next(), maxDeviation, maxDeviation, s => new Random(s).NextDouble());
    }

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviation"/> in both directions on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same seed, will result in the same output.
    /// </summary>
    public static IPeriodTimeline Randomize(this IPeriodTimeline source, int seed, TimeSpan maxDeviation)
    {
        return source.Randomize(seed, maxDeviation, maxDeviation, s => new Random(s).NextDouble());
    }

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// </summary>
    public static IPeriodTimeline Randomize(this IPeriodTimeline source, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter)
    {
        return source.Randomize(new Random().Next(), maxDeviationBefore, maxDeviationAfter, s => new Random(s).NextDouble());
    }

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IPeriodTimeline Randomize(this IPeriodTimeline source, int seed, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter)
    {
        return source.Randomize(seed, maxDeviationBefore, maxDeviationAfter, s => new Random(s).NextDouble());
    }

    /// <summary>
    /// Randomizes <paramref name="source"/> with <paramref name="maxDeviationBefore"/> towards the left and <paramref name="maxDeviationAfter"/> towards the right on the timeline.
    /// <paramref name="randomFunc"/> is to use input <c>int</c> as a seed and provide a random <c>double</c> between 0 and 1.
    /// This method will never result in a change of period count or in overlapping periods.
    /// Identical inputs with the same <paramref name="seed"/>, will result in the same output.
    /// </summary>
    public static IPeriodTimeline Randomize(this IPeriodTimeline source, int seed, TimeSpan maxDeviationBefore, TimeSpan maxDeviationAfter, Func<int, double> randomFunc)
    {
        return new PeriodTimeline(
            new RandomizedStartTimeline(source, seed, maxDeviationBefore, maxDeviationAfter, randomFunc),
            new RandomizedEndTimeline(source, seed, maxDeviationBefore, maxDeviationAfter, randomFunc));
    }

    /// <summary>
    /// Subtracts <paramref name="subtrahend"/> from all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IPeriodTimeline source, Period subtrahend)
    {
        var subtrahendPeriodTimeline = subtrahend.AsPeriodTimeline();
        return new PeriodTimeline(
            new SubtractStartTimeline(source, subtrahendPeriodTimeline),
            new SubtractEndTimeline(source, subtrahendPeriodTimeline));
    }

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IPeriodTimeline source, IEnumerable<Period> subtrahends)
    {
        var subtrahendPeriodTimeline = subtrahends.AsPeriodTimeline();
        return new PeriodTimeline(
            new SubtractStartTimeline(source, subtrahendPeriodTimeline),
            new SubtractEndTimeline(source, subtrahendPeriodTimeline));
    }

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IPeriodTimeline source, params Period[] subtrahends)
    {
        var subtrahendPeriodTimeline = subtrahends.AsPeriodTimeline();
        return new PeriodTimeline(
            new SubtractStartTimeline(source, subtrahendPeriodTimeline),
            new SubtractEndTimeline(source, subtrahendPeriodTimeline));
    }

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahend"/> from all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IPeriodTimeline source, IPeriodTimeline subtrahend)
    {
        return new PeriodTimeline(
            new SubtractStartTimeline(source, subtrahend),
            new SubtractEndTimeline(source, subtrahend));
    }

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IPeriodTimeline source, IEnumerable<IPeriodTimeline> subtrahends)
    {
        return subtrahends.Aggregate(source, (current, pp) => current.Subtract(pp));
    }

    /// <summary>
    /// Subtracts all periods in <paramref name="subtrahends"/> from all periods in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline Subtract(this IPeriodTimeline source, params IPeriodTimeline[] subtrahends)
    {
        return subtrahends.Aggregate(source, (current, pp) => current.Subtract(pp));
    }
}