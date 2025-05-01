
using Occurify.Extensions;

namespace Occurify.TimelineTransformations
{
    internal class CachingTimeline : Timeline
    {
        private readonly IsInstantCachingMethods _cachingMethod;
        private readonly ITimeline _source;

        private readonly List<Period> _cachePeriod = new(); // Represents a full period on the source timeline. Both period.Start and period.End are an instant on the timeline.
        private readonly List<CachePeriod> _previousInstantCachePeriods = new ();
        private readonly List<CachePeriod> _nextInstantCachePeriods = new();
        private readonly ISet<DateTime> _cachedInstants = new HashSet<DateTime>();
        private readonly ISet<DateTime> _cachedNonInstants = new HashSet<DateTime>();

        public CachingTimeline(ITimeline source, IsInstantCachingMethods cachingMethod = IsInstantCachingMethods.GetPrevious)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _cachingMethod = cachingMethod;
        }

        public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
        {
            var period = _cachePeriod.FirstOrDefault(p => p.ContainsInstantInclusive(utcRelativeTo));

        }

        public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
        {
            int index;
            foreach (var period in _nextInstantCachePeriods)
            {
                if (period.Instant == null)
                {
                    continue;
                }
                if (period.Instant.Value > utcRelativeTo)
                {
                    return period.Instant;
                }
            }
        }

        public override bool IsInstant(DateTime utcDateTime)
        {
            if (_cachedInstants.Contains(utcDateTime))
            {
                return true;
            }

            if (_cachedNonInstants.Contains(utcDateTime))
            {
                return false;
            }

            if (_previousInstantCachePeriods.Any(p => p.Contains(utcDateTime)) ||
                _nextInstantCachePeriods.Any(p => p.Contains(utcDateTime)))
            {
                // As all instants of a cached period are in _cachedInstants, we know that any instance in a cache period that is not in _cachedInstants is not an instant.
                return false;
            }

            if (_cachingMethod == IsInstantCachingMethods.GetPrevious && 
                utcDateTime != DateTime.MaxValue) // We can't use GetPreviousUtcInstant on DateTime.MaxValue, so we let IsInstant handle it.
            {
                return GetPreviousUtcInstant(utcDateTime + TimeSpan.FromTicks(1)) == utcDateTime;
            }

            var isInstant = _source.IsInstant(utcDateTime);
            if (isInstant)
            {
                _cachedInstants.Add(utcDateTime);
            }
            else if (utcDateTime == DateTime.MaxValue || _cachingMethod == IsInstantCachingMethods.SaveNonInstants)
            {
                // Note: we make an exception for DateTime.MaxValue.
                _cachedNonInstants.Add(utcDateTime);
            }
            return isInstant;
        }
    }

    enum IsInstantCachingMethods
    {
        /// <summary>
        /// When IsInstant is called with a value that is not cached, the source timeline is called using GetPreviousUtcInstant or GetNextUtcInstant in an attempt to populate the cache with more information.
        /// Use this method when IsInstant is just as expensive as to call as GetPreviousUtcInstant or GetNextUtcInstant, or when you anticipate multiple calls to IsInstant within a single range of instants.
        /// </summary>
        GetPrevious,

        /// <summary>
        /// When IsInstant is called with a value that is not cached, the source timeline is called using IsInstant. If the value is not an instant, it will also be cached as such, meaning multiple calls with the same value won't call the source multiple times.
        /// Use this method if GetPreviousOrNext is not a good fit and when you anticipate calling IsInstant multiple times with the same value.
        /// </summary>
        DoNotSaveNonInstants,

        /// <summary>
        /// When IsInstant is called with a value that is not cached, the source timeline is called using IsInstant. If the value is not an instant, it will not be cached, meaning multiple calls with the same value might call the source multiple times as well.
        /// Use this method if GetPreviousOrNext is not a good fit and when you don't anticipate calling IsInstant multiple times with the same value.
        /// </summary>
        SaveNonInstants,
    }

    internal class CachePeriod
    {
        public CachePeriod(DateTime? instant, DateTime cacheStart)
        {

        }

        public bool Contains(DateTime utcDateTime)
        {
            return false;
        }
    }
}
