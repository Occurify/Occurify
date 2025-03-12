using System.Collections;
using Occurify.Extensions;

namespace Occurify;

public abstract partial class Timeline : ITimeline
{
    public abstract DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo);

    public abstract DateTime? GetNextUtcInstant(DateTime utcRelativeTo);

    public abstract bool IsInstant(DateTime utcDateTime);

    public IEnumerator<DateTime> GetEnumerator()
    {
        return this.Enumerate().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}