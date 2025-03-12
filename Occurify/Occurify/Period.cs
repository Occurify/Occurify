using System.Diagnostics.CodeAnalysis;

namespace Occurify;

public partial record Period(DateTime? Start, DateTime? End) : IComparable<Period>
{
    /// <summary>
    /// The duration of this period.
    /// <c>null</c> if either <c>Start</c> or <c>End</c> is <c>null</c>, meaning the duration of this period is infinite.
    /// </summary>
    public TimeSpan? Duration => End - Start;

    /// <summary>
    /// True if the period always started.
    /// (<c>Start == null</c>)
    /// </summary>
    [MemberNotNullWhen(false, nameof(Start))]
    public bool HasAlwaysStarted => Start == null;

    /// <summary>
    /// True if the period never ends.
    /// (<c>End == null</c>)
    /// </summary>
    [MemberNotNullWhen(false, nameof(End))]
    public bool NeverEnds => End == null;

    [MemberNotNullWhen(false, nameof(Start))]
    [MemberNotNullWhen(false, nameof(End))]
    public bool IsInfiniteInBothDirections => Start == null && End == null;

    public int CompareTo(Period? other)
    {
        if (other == null) return 1;

        var startComparison = Nullable.Compare(Start, other.Start);
        if (startComparison != 0)
        {
            return startComparison;
        }

        if (End == other.End)
        {
            return 0;
        }

        if (End == null)
        {
            return 1;
        }

        if (other.End == null)
        {
            return -1;
        }
        return End > other.End ? 1 : -1;
    }

    public override string ToString()
    {
        if (IsInfiniteInBothDirections)
        {
            return "∞";
        }

        if (HasAlwaysStarted)
        {
            return $"∞<->{End}";
        }

        if (NeverEnds)
        {
            return $"{Start}<->∞";
        }

        return $"{Start}<->{End}";
    }
}