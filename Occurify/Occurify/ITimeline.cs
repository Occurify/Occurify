namespace Occurify;

/// <summary>
/// Interface representing a timeline of instants (UTC <see cref="DateTime"/>).
/// </summary>
public partial interface ITimeline : IEnumerable<DateTime>
{
    /// <summary>
    /// Returns the previous instant relative to <paramref name="utcRelativeTo"/>.
    /// </summary>
    DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo);

    /// <summary>
    /// Returns the next instant relative to <paramref name="utcRelativeTo"/>.
    /// </summary>
    DateTime? GetNextUtcInstant(DateTime utcRelativeTo);

    /// <summary>
    /// Return whether the provided <paramref name="utcDateTime"/> is an instant on this timeline.
    /// </summary>
    bool IsInstant(DateTime utcDateTime);
}