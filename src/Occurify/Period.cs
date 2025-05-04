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

    /// <summary>
    /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the formatting conventions of the current culture.
    /// </summary>
    public override string ToString() => ToString(dt => dt.ToString(null, null));

    /// <summary>
    /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the specified culture-specific format information.
    /// </summary>
    public string ToString(IFormatProvider? provider) => ToString(dt => dt.ToString(provider));

    /// <summary>
    /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the specified format and the formatting conventions of the current culture.
    /// </summary>
    public string ToString(string? format) => ToString(dt => dt.ToString(format));

    /// <summary>
    /// Converts the value of the current <see cref="Period" /> object to its equivalent string representation using the specified format and culture-specific format information.
    /// </summary>
    public string ToString(string? format, IFormatProvider? provider) => ToString(dt => dt.ToString(format, provider));

    /// <summary>
    /// Converts the value of the current <see cref="Period" /> object to its equivalent long date string representation.
    /// </summary>
    public string ToLongDateString() => ToString(dt => dt.ToLongDateString());

    /// <summary>
    /// Converts the value of the current <see cref="Period" /> object to its equivalent long time string representation.
    /// </summary>
    public string ToLongTimeString() => ToString(dt => dt.ToLongTimeString());

    /// <summary>
    /// Converts the value of the current <see cref="Period" /> object to its equivalent short date string representation.
    /// </summary>
    public string ToShortDateString() => ToString(dt => dt.ToShortDateString());

    /// <summary>
    /// Converts the value of the current <see cref="Period" /> object to its equivalent short time string representation.
    /// </summary>
    public string ToShortTimeString() => ToString(dt => dt.ToShortTimeString());

    private string ToString(Func<DateTime, string> dateTimeToStringFunc)
    {
        if (IsInfiniteInBothDirections)
        {
            return "∞";
        }

        if (HasAlwaysStarted)
        {
            return $"∞<->{dateTimeToStringFunc(End.Value)}";
        }

        if (NeverEnds)
        {
            return $"{dateTimeToStringFunc(Start.Value)}<->∞";
        }

        return $"{dateTimeToStringFunc(Start.Value)}<->{dateTimeToStringFunc(End.Value)}";
    }
}