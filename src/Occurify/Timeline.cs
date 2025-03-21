using System.Collections;
using Occurify.Extensions;

namespace Occurify;

/// <summary>
/// Represents a timeline of instants.
/// </summary>
public abstract partial class Timeline : ITimeline
{
    /// <inheritdoc/>
    public abstract DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo);

    /// <inheritdoc/>
    public abstract DateTime? GetNextUtcInstant(DateTime utcRelativeTo);

    /// <inheritdoc/>
    public abstract bool IsInstant(DateTime utcDateTime);

    /// <inheritdoc/>
    public IEnumerator<DateTime> GetEnumerator()
    {
        return this.Enumerate().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}