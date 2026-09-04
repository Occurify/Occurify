using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="PeriodTimelineSample"/>.
/// </summary>
public static class PeriodTimelineSampleExtensions
{
    /// <summary>
    /// The instant at which <paramref name="sample"/> was taken.
    /// </summary>
    public static Instant SampleInstant(this PeriodTimelineSample sample) => sample.UtcSampleInstant.ToInstant();

    /// <summary>
    /// The period at the sample instant as an <see cref="Interval"/>. <c>null</c> if the instant is not on a period.
    /// </summary>
    public static Interval? PeriodAsInterval(this PeriodTimelineSample sample) => sample.Period?.ToInterval();

    /// <summary>
    /// The gap between periods at the sample instant as an <see cref="Interval"/>. <c>null</c> if the instant is not on a gap.
    /// </summary>
    public static Interval? GapAsInterval(this PeriodTimelineSample sample) => sample.Gap?.ToInterval();

    /// <summary>
    /// The period or gap sampled in <paramref name="sample"/> as an <see cref="Interval"/>.
    /// </summary>
    public static Interval ToInterval(this PeriodTimelineSample sample) =>
        sample.IsPeriod ? sample.Period.ToInterval() : sample.Gap.ToInterval();

    /// <summary>
    /// Start of the period or gap sampled in <paramref name="sample"/>. <c>null</c> if it has always started.
    /// </summary>
    public static Instant? IntervalStart(this PeriodTimelineSample sample) => sample.Start.ToInstant();

    /// <summary>
    /// End of the period or gap sampled in <paramref name="sample"/>. <c>null</c> if it never ends.
    /// </summary>
    public static Instant? IntervalEnd(this PeriodTimelineSample sample) => sample.End.ToInstant();
}
