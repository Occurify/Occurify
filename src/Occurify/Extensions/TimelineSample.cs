
namespace Occurify.Extensions;

/// <summary>
/// Represents a single sample taken from a <see cref="ITimeline"/>.
/// </summary>
public record TimelineSample
{
    internal TimelineSample(DateTime utcSampleInstant, bool sampleIsInstant, DateTime? previous, DateTime? next)
    {
        UtcSampleInstant = utcSampleInstant;
        SampleIsInstant = sampleIsInstant;
        Previous = previous;
        Next = next;
    }

    /// <summary>
    /// The UTC time at which the sample was taken.
    /// </summary>
    public DateTime UtcSampleInstant { get; }

    /// <summary>
    /// True if the sample was taken on an instant, false if it was taken between instants.
    /// </summary>
    public bool SampleIsInstant { get; }

    /// <summary>
    /// The instant before the sample instant. Null if the sample instant is the earliest instant on the timeline.
    /// </summary>
    public DateTime? Previous { get; }

    /// <summary>
    /// The instant after the sample instant. Null if the sample instant is the latest instant on the timeline.
    /// </summary>
    public DateTime? Next { get; }
}