
using Occurify.Extensions;

namespace Occurify.PeriodTimelineCollectionTransformations
{
    internal class WhereOverlapCountStartTimeline : Timeline
    {
        private readonly IPeriodTimeline[] _source;
        private readonly ITimeline[] _sourceStartTimelines;
        private readonly ITimeline[] _sourceEndTimelines;
        private readonly Func<int, bool> _predicate;

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

            var currentlyInPeriod = _predicate(_source.Count(pt => pt.ContainsInstant(utcRelativeTo)));
            do
            {
                var previousStart = _sourceStartTimelines.GetPreviousUtcInstant(utcRelativeTo);
                var previousEnd = _sourceEndTimelines.GetPreviousUtcInstant(utcRelativeTo);
                if (previousStart == null && previousEnd == null)
                {
                    return null;
                }

                if (previousEnd != null && (previousStart == null || previousEnd > previousStart))
                {
                    if (previousEnd == DateTime.MinValue)
                    {
                        return null;
                    }
                    var inPeriodBeforeEnd = _predicate(_source.Count(pt => pt.ContainsInstant(previousEnd.Value - TimeSpan.FromTicks(1))));
                    if (inPeriodBeforeEnd != currentlyInPeriod)
                    {
                        if (currentlyInPeriod)
                        {
                            return previousEnd;
                        }

                        currentlyInPeriod = true;
                    }

                    utcRelativeTo = previousEnd.Value;
                }
                else
                {
                    var inPeriodOnStart = _predicate(_source.Count(pt => pt.ContainsInstant(previousStart!.Value)));
                    if (inPeriodOnStart != currentlyInPeriod)
                    {
                        if (currentlyInPeriod)
                        {
                            return previousEnd;
                        }

                        currentlyInPeriod = true;
                    }

                    utcRelativeTo = previousStart!.Value;
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
                if (nextStart == null && nextEnd == null)
                {
                    return null;
                }

                if (nextEnd != null && (nextStart == null || nextEnd < nextStart))
                {
                    var inPeriodBeforeEnd = _predicate(_source.Count(pt => pt.ContainsInstant(nextEnd.Value - TimeSpan.FromTicks(1))));
                    if (inPeriodBeforeEnd != currentlyInPeriod)
                    {
                        if (currentlyInPeriod)
                        {
                            return nextEnd;
                        }

                        currentlyInPeriod = true;
                    }

                    utcRelativeTo = nextEnd.Value;
                }
                else
                {
                    var inPeriodOnStart = _predicate(_source.Count(pt => pt.ContainsInstant(nextStart!.Value)));
                    if (inPeriodOnStart != currentlyInPeriod)
                    {
                        if (currentlyInPeriod)
                        {
                            return nextEnd;
                        }

                        currentlyInPeriod = true;
                    }

                    utcRelativeTo = nextStart!.Value;
                }
            } while (true);
        }

        public override bool IsInstant(DateTime utcDateTime)
        {
            var hasStart = _sourceStartTimelines.IsInstant(utcDateTime);
            var hasEnd = _sourceStartTimelines.IsInstant(utcDateTime);
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
    }
}
