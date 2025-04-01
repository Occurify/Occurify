
namespace Occurify.Extensions;

/// <summary>
/// Represents a single sample taken from a <see cref="IEnumerable{ITimeline}"/>.
/// </summary>
public record TimelineCollectionSample
{
    internal TimelineCollectionSample(DateTime utcSampleInstant, Dictionary<ITimeline, TimelineSample> samples)
    {
        UtcSampleInstant = utcSampleInstant;
        Samples = samples;
        TimelinesWithInstantOnSampleLocation = samples.Where(kvp => kvp.Value.SampleIsInstant).Select(kvp => kvp.Key).ToArray();
        Previous = samples.Values.Select(s => s.Previous).OrderAndPutNullFirst().Last();
        Next = samples.Values.Select(s => s.Next).OrderAndPutNullLast().First();
        TimelinesOnPrevious = samples.Where(kvp => kvp.Value.Previous == Previous).Select(kvp => kvp.Key).ToArray();
        TimelinesOnNext = samples.Where(kvp => kvp.Value.Next == Next).Select(kvp => kvp.Key).ToArray();
    }

    /// <summary>
    /// The UTC time at which the sample was taken.
    /// </summary>
    public DateTime UtcSampleInstant { get; }

    /// <summary>
    /// The individual samples.
    /// </summary>
    public IReadOnlyDictionary<ITimeline, TimelineSample> Samples { get; }

    /// <summary>
    /// The timelines that have an instant on the sample location.
    /// </summary>
    public IReadOnlyCollection<ITimeline> TimelinesWithInstantOnSampleLocation { get; }

    /// <summary>
    /// The instant before the sample instant. Null if the sample instant is the earliest instant on the timeline.
    /// </summary>
    public DateTime? Previous { get; }

    /// <summary>
    /// The timelines that have their previous instant on the instant before the sample instant.
    /// </summary>
    public IReadOnlyCollection<ITimeline> TimelinesOnPrevious { get; }

    /// <summary>
    /// The instant after the sample instant. Null if the sample instant is the latest instant on the timeline.
    /// </summary>
    public DateTime? Next { get; }

    /// <summary>
    /// The timelines that have their next instant on the instant after the sample instant.
    /// </summary>
    public IReadOnlyCollection<ITimeline> TimelinesOnNext { get; }
}