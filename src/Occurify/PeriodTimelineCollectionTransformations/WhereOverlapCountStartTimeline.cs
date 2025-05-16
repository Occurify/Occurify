
using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.PeriodTimelineCollectionTransformations
{
    internal class WhereOverlapCountStartTimeline : Timeline
    {
        private readonly IPeriodTimeline[] _source;
        private readonly ITimeline[] _sourceStartTimelines;
        private readonly ITimeline[] _sourceEndTimelines;
        private readonly Func<int, bool> _predicate;

        // Note: If the result is a completely overlapping period, we have to place a single start at DateTime.MinValue.
        private bool? _isOverlapOnly;

        public WhereOverlapCountStartTimeline(IPeriodTimeline[] source, Func<int, bool> predicate)
        {
            _source = source;
            _sourceStartTimelines = source.Select(s => s.StartTimeline).ToArray();
            _sourceEndTimelines = source.Select(s => s.EndTimeline).ToArray();
            _predicate = predicate;
        }

        public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
        {
            if (utcRelativeTo.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
            }

            if (IsOverlapOnly())
            {
                return utcRelativeTo == DateTime.MinValue ? null : DateTimeHelper.MinValueUtc;
            }

            // To determine an end in the overlap count, we have to look at the previous instant - 1 and compare it to the previous instant. If previous instant - 1 is false while previous is true, previous marks a start instant.
            // As we do not look for a start on the original utcRelativeTo, we also start 1 tick before it.
            var currentlyInPeriod = _predicate(_source.Count(pt => pt.ContainsInstant(utcRelativeTo - TimeSpan.FromTicks(1))));
            do
            {
                // todo: edge case
                var previousStart = _sourceStartTimelines.GetPreviousUtcInstant(utcRelativeTo);
                var previousEnd = _sourceEndTimelines.GetPreviousUtcInstant(utcRelativeTo);
                var previous = DateTimeHelper.MaxAssumingNullIsMinInfinity(previousStart, previousEnd);
                if (previous == null)
                {
                    return null;
                }

                var inPeriodBeforePrevious = _predicate(_source.Count(pt => pt.ContainsInstant(previous.Value - TimeSpan.FromTicks(1))));
                if (currentlyInPeriod && !inPeriodBeforePrevious)
                {
                    return previous;
                }
                currentlyInPeriod = inPeriodBeforePrevious;
                utcRelativeTo = previous.Value;
            } while (true);
        }

        public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
        {
            if (utcRelativeTo.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
            }

            if (IsOverlapOnly())
            {
                return null;
            }

            var currentlyInPeriod = _predicate(_source.Count(pt => pt.ContainsInstant(utcRelativeTo)));
            do
            {
                var nextStart = _sourceStartTimelines.GetNextUtcInstant(utcRelativeTo);
                var nextEnd = _sourceEndTimelines.GetNextUtcInstant(utcRelativeTo);
                var next = DateTimeHelper.MinAssumingNullIsPlusInfinity(nextStart, nextEnd);
                if (next == null)
                {
                    return null;
                }

                var inPeriodOnNext = _predicate(_source.Count(pt => pt.ContainsInstant(next.Value)));
                if (!currentlyInPeriod && inPeriodOnNext)
                {
                    return next;
                }
                currentlyInPeriod = inPeriodOnNext;
                utcRelativeTo = next.Value;
            } while (true);
        }

        public override bool IsInstant(DateTime utcDateTime)
        {
            if (IsOverlapOnly())
            {
                return utcDateTime == DateTime.MinValue;
            }

            var hasStart = _sourceStartTimelines.IsInstant(utcDateTime);
            var hasEnd = _sourceEndTimelines.IsInstant(utcDateTime);
            if (!hasStart && !hasEnd)
            {
                return false;
            }
            if (utcDateTime == DateTime.MinValue)
            {
                return hasStart && _predicate(_source.Count(pt => pt.ContainsInstant(utcDateTime))); // todo: does this make sense?
            }
            return !_predicate(_source.Count(pt => pt.ContainsInstant(utcDateTime - TimeSpan.FromTicks(1)))) &&
                   _predicate(_source.Count(pt => pt.ContainsInstant(utcDateTime)));
        }

        private bool IsOverlapOnly()
        {
            if (_isOverlapOnly != null)
            {
                return _isOverlapOnly.Value;
            }

            var instant = DateTimeHelper.MinValueUtc;
            var currentlyInPeriod = _predicate(_source.Count(pt => pt.ContainsInstant(instant)));
            if (!currentlyInPeriod)
            {
                _isOverlapOnly = false;
                return false;
            }
            
            do
            {
                var nextStart = _sourceStartTimelines.GetNextUtcInstant(instant);
                var nextEnd = _sourceEndTimelines.GetNextUtcInstant(instant);
                var next = DateTimeHelper.MinAssumingNullIsPlusInfinity(nextStart, nextEnd);
                if (next == null)
                {
                    _isOverlapOnly = true;
                    return true;
                }
                var inPeriodOnNext = _predicate(_source.Count(pt => pt.ContainsInstant(next.Value)));
                if (!inPeriodOnNext)
                {
                    _isOverlapOnly = false;
                    return false;
                }
                instant = next.Value;
            } while (true);
        }
    }
}
