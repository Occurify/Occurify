namespace Occurify;

/// <summary>
/// Interface representing a timeline of periods (<see cref="Period"/>).
/// </summary>
public partial interface IPeriodTimeline : IEnumerable<Period>
{
    /// <summary>
    /// The start instances for periods on this timeline.
    /// </summary>
    ITimeline StartTimeline { get; }

    /// <summary>
    /// The end instances for periods on this timeline.
    /// </summary>
    ITimeline EndTimeline { get; }
}