using System.Collections;

namespace Occurify.Timelines;

internal class CollectionTimeline : ITimeline
{
    private readonly DateTime[] _instants;

    public CollectionTimeline(IEnumerable<DateTime> instants)
    {
        // Note: we make the array before the check so we don't iterate the enumerable twice.
        _instants = instants.OrderBy(i => i).ToArray();
        if (_instants.Any(i => i.Kind != DateTimeKind.Utc))
        {
            throw new ArgumentException($"{nameof(instants)} should be UTC time.");
        }
    }

    public CollectionTimeline(params DateTime[] instants)
    {
        if (instants.Any(i => i.Kind != DateTimeKind.Utc))
        {
            throw new ArgumentException($"{nameof(instants)} should be UTC time.");
        }
        _instants = instants.OrderBy(i => i).ToArray();
    }

    public DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        var earlierInstants = _instants.Where(i => i < utcRelativeTo).ToArray();
        if (!earlierInstants.Any())
        {
            return null;
        }

        return earlierInstants.Last();
    }

    public DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        var laterInstants = _instants.Where(i => i > utcRelativeTo).ToArray();
        if (!laterInstants.Any())
        {
            return null;
        }

        return laterInstants.First();
    }

    public bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        return _instants.Contains(utcDateTime);
    }

    public IEnumerator<DateTime> GetEnumerator()
    {
        return ((IEnumerable<DateTime>)_instants).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}