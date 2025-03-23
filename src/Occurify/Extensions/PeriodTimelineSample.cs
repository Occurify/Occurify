using System.Diagnostics.CodeAnalysis;

namespace Occurify.Extensions;

/// <summary>
/// Represents a single sample taken from a <see cref="IPeriodTimeline"/>.
/// </summary>
public record PeriodTimelineSample
{
    internal PeriodTimelineSample(DateTime utcSampleInstant, Period? period, Period? gap)
    {
        if ((period == null) == (gap == null))
        {
            throw new ArgumentException($"Either {nameof(period)} or {nameof(gap)} has to be set.");
        }
        UtcSampleInstant = utcSampleInstant;
        Period = period;
        Gap = gap;
    }

    /// <summary>
    /// The UTC time at which the sample was taken.
    /// </summary>
    public DateTime UtcSampleInstant { get; }

    /// <summary>
    /// The period at the sample instant. Null if the instant is not on a period.
    /// </summary>
    public Period? Period { get; }

    /// <summary>
    /// The period between periods at the sample instant. Null if the instant is not on a gap.
    /// </summary>
    public Period? Gap { get; }

    /// <summary>
    /// Whether the sample was taken on a period.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Period))]
    [MemberNotNullWhen(false, nameof(Gap))]
    public bool IsPeriod => Period != null;

    /// <summary>
    /// Whether the sample was taken between periods.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Gap))]
    [MemberNotNullWhen(false, nameof(Period))]
    public bool IsGap => Gap != null;

    /// <summary>
    /// UTC start of the period or gap sampled in this sample.
    /// </summary>
    public DateTime? Start => IsPeriod ? Period.Start : Gap.Start;

    /// <summary>
    /// UTC end of the period or gap sampled in this sample.
    /// </summary>
    public DateTime? End => IsPeriod ? Period.End : Gap.End;
}