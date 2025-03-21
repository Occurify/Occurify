
namespace Occurify.Extensions;

/// <summary>
/// Represents a single sample taken from a <see cref="IEnumerable{KeyValuePair}"/> with <see cref="ITimeline"/> as key.
/// </summary>
public record TimelineValueCollectionSample<TValue>
{
    internal TimelineValueCollectionSample(KeyValuePair<ITimeline, TValue>[] source, TimelineCollectionSample timelineCollectionSample)
    {
        UtcSampleInstant = timelineCollectionSample.UtcSampleInstant;
        Samples = source.ToDictionary(kvp => kvp.Key, kvp => (kvp.Value, timelineCollectionSample.Samples[kvp.Key]));
        TimelinesWithInstantOnSampleLocation = source.Where(kvp => timelineCollectionSample.Samples[kvp.Key].SampleIsInstant).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        Previous = timelineCollectionSample.Previous;
        TimelinesOnPrevious = source.Where(kvp => timelineCollectionSample.Samples[kvp.Key].Previous == Previous).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        Next = timelineCollectionSample.Next;
        TimelinesOnNext = source.Where(kvp => timelineCollectionSample.Samples[kvp.Key].Next == Next).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    /// <summary>
    /// The UTC time at which the sample was taken.
    /// </summary>
    public DateTime UtcSampleInstant { get; }

    /// <summary>
    /// The individual samples.
    /// </summary>
    public IReadOnlyDictionary<ITimeline, (TValue, TimelineSample)> Samples { get; }

    /// <summary>
    /// The timelines that have an instant on the sample location.
    /// </summary>
    public IReadOnlyDictionary<ITimeline, TValue> TimelinesWithInstantOnSampleLocation { get; }

    /// <summary>
    /// The instant before the sample instant. Null if the sample instant is the earliest instant on the timeline.
    /// </summary>
    public DateTime? Previous { get; }

    /// <summary>
    /// The timelines that have their previous instant on the instant before the sample instant.
    /// </summary>
    public IReadOnlyDictionary<ITimeline, TValue> TimelinesOnPrevious { get; }

    /// <summary>
    /// The instant after the sample instant. Null if the sample instant is the latest instant on the timeline.
    /// </summary>
    public DateTime? Next { get; }

    /// <summary>
    /// The timelines that have their next instant on the instant after the sample instant.
    /// </summary>
    public IReadOnlyDictionary<ITimeline, TValue> TimelinesOnNext { get; }
}