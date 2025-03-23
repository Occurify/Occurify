namespace Occurify.Extensions;

/// <summary>
/// Represents a timeline entry containing an instant along with the timelines that include this instant and their corresponding value.
/// </summary>
public record TimelineValueCollectionEntry<TValue>(
    DateTime Instant,
    IEnumerable<KeyValuePair<ITimeline, TValue>> TimelinesWithInstantOnLocation);