using Occurify.Extensions;
using Occurify.Helpers;

namespace Occurify.PeriodTimelineTransformations;

internal class RandomizedStartTimeline : Timeline
{
    private readonly IPeriodTimeline _source;
    private readonly int _seed;
    private readonly TimeSpan _maxDeviationBefore;
    private readonly TimeSpan _maxDeviationAfter;
    private readonly Func<int, double> _randomFunc;

    public RandomizedStartTimeline(
        IPeriodTimeline source, 
        int seed, 
        TimeSpan maxDeviationBefore, 
        TimeSpan maxDeviationAfter, 
        Func<int, double> randomFunc)
    {
        _seed = seed;
        _maxDeviationBefore = maxDeviationBefore;
        _maxDeviationAfter = maxDeviationAfter;
        _randomFunc = randomFunc;
        _source = source ?? throw new ArgumentNullException(nameof(source));
    }

    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        /*
         * There are three possible cases:
         * 1. The random instant calculated from the next instant is smaller than the provided instant.
         * 2. If 1 is not the case, the random instant calculated from the previous/current instant is smaller than the provided instant.
         * 3. if 2 is not the case, the random instant calculated from the instant before that has to be smaller than the provided instant.
         */

        var next = _source.StartTimeline.GetNextUtcInstant(utcRelativeTo);
        // If next is null, we are at the end and can skip this.
        if (next != null)
        {
            var randomOfNext = GetRandomizedInstant(next.Value);
            if (randomOfNext < utcRelativeTo)
            {
                return randomOfNext;
            }
        }

        var previousOrCurrent = _source.StartTimeline.GetCurrentOrPreviousUtcInstant(utcRelativeTo);
        if (previousOrCurrent == null)
        {
            return null;
        }

        var randomOfPreviousOrCurrent = GetRandomizedInstant(previousOrCurrent.Value);
        if (randomOfPreviousOrCurrent < utcRelativeTo)
        {
            return randomOfPreviousOrCurrent;
        }

        var beforePreviousOrCurrent = _source.StartTimeline.GetPreviousUtcInstant(previousOrCurrent.Value);
        if (beforePreviousOrCurrent == null)
        {
            return null;
        }
        return GetRandomizedInstant(beforePreviousOrCurrent.Value);
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        /*
         * There are three possible cases:
         * 1. The random instant calculated from the previous instant is larger than the provided instant.
         * 2. If 1 is not the case, the random instant calculated from the next/current instant is larger than the provided instant.
         * 3. if 2 is not the case, the random instant calculated from the instant after that has to be larger than the provided instant.
         */

        var previous = _source.StartTimeline.GetPreviousUtcInstant(utcRelativeTo);
        // If previous is null, we are at the beginning and can skip this.
        if (previous != null)
        {
            var randomOfPrevious = GetRandomizedInstant(previous.Value);
            if (randomOfPrevious > utcRelativeTo)
            {
                return randomOfPrevious;
            }
        }

        var nextOrCurrent = _source.StartTimeline.GetCurrentOrNextUtcInstant(utcRelativeTo);
        if (nextOrCurrent == null)
        {
            return null;
        }

        var randomOfNextOrCurrent = GetRandomizedInstant(nextOrCurrent.Value);
        if (randomOfNextOrCurrent > utcRelativeTo)
        {
            return randomOfNextOrCurrent;
        }

        var afterNextOrCurrent = _source.StartTimeline.GetNextUtcInstant(nextOrCurrent.Value);
        if (afterNextOrCurrent == null)
        {
            return null;
        }
        return GetRandomizedInstant(afterNextOrCurrent.Value);
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
        }

        var next = _source.StartTimeline.GetNextUtcInstant(utcDateTime);
        // If next is null, we are at the end and can skip this.
        if (next != null)
        {
            var randomOfNext = GetRandomizedInstant(next.Value);
            if (randomOfNext == utcDateTime)
            {
                return true;
            }
        }

        var currentOrPrevious = _source.StartTimeline.GetCurrentOrPreviousUtcInstant(utcDateTime);
        // If previous is null, we are at the beginning and can skip this.
        if (currentOrPrevious != null)
        {
            var randomOfPrevious = GetRandomizedInstant(currentOrPrevious.Value);
            if (randomOfPrevious == utcDateTime)
            {
                return true;
            }
        }

        return false;
    }

    private DateTime? GetRandomizedInstant(DateTime instant)
    {
        var boundaryBefore = _source.EndTimeline.GetCurrentOrPreviousUtcInstant(instant);
        var next = _source.EndTimeline.GetNextUtcInstant(instant);
        var boundaryAfter = next == null ? DateTimeHelper.MaxValueUtc : next - TimeSpan.FromTicks(1);
        return DateTimeHelper.GetRandomDateTimeBetweenBoundaries(
            instant, 
            _maxDeviationBefore,
            _maxDeviationAfter, 
            boundaryBefore, 
            boundaryAfter, 
            _seed, 
            _randomFunc);
    }
}