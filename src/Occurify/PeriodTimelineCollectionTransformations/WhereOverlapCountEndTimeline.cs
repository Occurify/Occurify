﻿
using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.PeriodTimelineCollectionTransformations
{
    internal class WhereOverlapCountEndTimeline : Timeline
    {
        private readonly IPeriodTimeline[] _source;
        private readonly ITimeline[] _sourceStartTimelines;
        private readonly ITimeline[] _sourceEndTimelines;
        private readonly Func<int, bool> _predicate;

        public WhereOverlapCountEndTimeline(IPeriodTimeline[] source, Func<int, bool> predicate)
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

            if (utcRelativeTo == DateTime.MinValue)
            {
                return null;
            }

            // To determine an end in the overlap count, we have to look at the previous instant - 1 and compare it to the previous instant. If previous instant - 1 is true while previous is not, previous marks an end instant.
            // As we do not look for an end on the original utcRelativeTo, we also start 1 tick before it.
            var currentlyInPeriod = _predicate(_source.Count(pt => pt.ContainsInstant(utcRelativeTo - TimeSpan.FromTicks(1))));
            do
            {
                var previousStart = _sourceStartTimelines.GetPreviousUtcInstant(utcRelativeTo);
                var previousEnd = _sourceEndTimelines.GetPreviousUtcInstant(utcRelativeTo);
                var previous = DateTimeHelper.MaxAssumingNullIsMinInfinity(previousStart, previousEnd);
                if (previous == null)
                {
                    return null;
                }

                if (previous == DateTime.MinValue)
                {
                    return IsInstant(previous.Value) ? DateTimeHelper.MinValueUtc : null;
                }

                var inPeriodBeforePrevious = _predicate(_source.Count(pt => pt.ContainsInstant(previous.Value - TimeSpan.FromTicks(1))));
                if (!currentlyInPeriod && inPeriodBeforePrevious)
                {
                    return previous;
                }
                currentlyInPeriod = inPeriodBeforePrevious;
                utcRelativeTo = previous.Value;
                if (utcRelativeTo == DateTime.MinValue)
                {
                    return null;
                }
            } while (true);
        }

        public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
        {
            if (utcRelativeTo.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
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
                if (currentlyInPeriod && !inPeriodOnNext)
                {
                    return next;
                }
                currentlyInPeriod = inPeriodOnNext;
                utcRelativeTo = next.Value;
            } while (true);
        }

        public override bool IsInstant(DateTime utcDateTime)
        {
            var hasStart = _sourceStartTimelines.IsInstant(utcDateTime);
            var hasEnd = _sourceEndTimelines.IsInstant(utcDateTime);
            if (!hasStart && !hasEnd)
            {
                return false;
            }
            if (utcDateTime == DateTime.MinValue)
            {
                // In case of utcDateTime being DateTime.MinValue, we have to check whether the overlap period just ended or was never started.
                var currentOverlapCount = _source.Count(pt => pt.ContainsInstant(utcDateTime));
                var inPeriod = _predicate(currentOverlapCount);
                if (inPeriod)
                {
                    return false;
                }
                var startCount = _sourceStartTimelines.Count(st => st.IsInstant(DateTimeHelper.MinValueUtc));
                var endCount = _sourceEndTimelines.Count(et => et.IsInstant(DateTimeHelper.MinValueUtc));
                var overlapCountBeforeMinValue = currentOverlapCount - startCount + endCount;
                return _predicate(overlapCountBeforeMinValue);
            }
            return _predicate(_source.Count(pt => pt.ContainsInstant(utcDateTime - TimeSpan.FromTicks(1)))) &&
                   !_predicate(_source.Count(pt => pt.ContainsInstant(utcDateTime)));
        }
    }
}
