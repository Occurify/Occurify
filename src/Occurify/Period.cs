using System.Diagnostics.CodeAnalysis;

namespace Occurify;

/// <summary>
/// Represents a period of time.
/// </summary>
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

    /// <summary>
    /// True when the period is infinite in both directions.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Start))]
    [MemberNotNullWhen(false, nameof(End))]
    public bool IsInfiniteInBothDirections => Start == null && End == null;

    /// <summary>
    /// Compare this period to another period.
    /// </summary>
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

    /// <inheritdoc />
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