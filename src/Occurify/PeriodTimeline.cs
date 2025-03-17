using System.Collections;
using Occurify.Extensions;

namespace Occurify;

public partial class PeriodTimeline : IPeriodTimeline
{
    internal PeriodTimeline(ITimeline startTimeline, ITimeline endTimeline)
    {
        StartTimeline = startTimeline;
        EndTimeline = endTimeline;
    }

    /// <inheritdoc />
    public ITimeline StartTimeline { get; }

    /// <inheritdoc />
    public ITimeline EndTimeline { get; }

    /// <inheritdoc />
    public IEnumerator<Period> GetEnumerator()
    {
        return this.Enumerate().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"{StartTimeline} to {EndTimeline}";
    }
}