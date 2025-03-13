using System.Text;
using Occurify.Extensions;

namespace Occurify.Tests.StringHelper;

internal class StringTimelineHelper
{
    private readonly DateTime _origin;

    public StringTimelineHelper()
    {
        _origin = DateTime.SpecifyKind(new DateTime(DateTime.MaxValue.Ticks / 2), DateTimeKind.Utc)
            .AddTicks(1); // Adding 1 tick will make it start on 0 ticks, which makes for debugging easier.
    }

    public StringTimelineHelper(DateTime origin)
    {
        _origin = origin;
    }

    public DateTime Origin => _origin;

    public Period GetSinglePeriod(string periodTimeline)
    {
        var dateTime = _origin;
        DateTime? start = null, end = null;

        foreach (var character in periodTimeline)
        {
            switch (character)
            {
                case '<':
                    if (start != null)
                    {
                        throw new InvalidOperationException(
                            "Multiple start characters not allowed for building period.");
                    }
                    if (end != null)
                    {
                        throw new InvalidOperationException(
                            "Period cannot start after end.");
                    }

                    start = dateTime;

                    break;
                case '>':
                    if (end != null)
                    {
                        throw new InvalidOperationException(
                            "Multiple end characters not allowed for building period.");
                    }

                    end = dateTime;
                    break;
                case ' ':
                    break;
                default:
                    throw new InvalidOperationException($"Timeline character {character} is not supported.");
            }
            dateTime = dateTime.AddTicks(1);
        }

        return new Period(start, end);
    }

    public DateTime GetSingleInstant(string instantTimeline) => GetInstants(instantTimeline).Single();

    public IEnumerable<DateTime> GetInstants(string instantsTimeline)
    {
        var dateTime = _origin;
        var instants = new List<DateTime>();

        foreach (var character in instantsTimeline)
        {
            switch (character)
            {
                case '|':
                    instants.Add(dateTime);
                    break;
                case 'X':
                    instants.Add(dateTime);
                    instants.Add(dateTime);
                    break;
                case ' ':
                    break;
                default:
                    throw new InvalidOperationException($"Timeline character {character} is not supported.");
            }
            dateTime = dateTime.AddTicks(1);
        }

        return instants;
    }

    public ITimeline CreateTimeline(string instantTimeline)
    {
        var dateTime = _origin;
        List<DateTime> instants = new();

        foreach (var character in instantTimeline)
        {
            switch (character)
            {
                case '|':
                    instants.Add(dateTime);
                    break;
                case ' ':
                    break;
                default:
                    throw new InvalidOperationException($"Timeline character {character} is not supported.");
            }

            dateTime = dateTime.AddTicks(1);
        }

        return instants.AsTimeline();
    }

    public IPeriodTimeline CreatePeriodTimeline(string periodTimeline)
    {
        var dateTime = _origin;
        List<DateTime> startInstants = new();
        List<DateTime> endInstants = new();

        foreach (var character in periodTimeline)
        {
            switch (character)
            {
                case '<':
                    startInstants.Add(dateTime);
                    break;
                case '>':
                    endInstants.Add(dateTime);
                    break;
                case 'x':
                case 'X':
                    startInstants.Add(dateTime);
                    endInstants.Add(dateTime);
                    break;
                case ' ':
                    break;
                default:
                    throw new InvalidOperationException($"Timeline character {character} is not supported.");
            }

            dateTime = dateTime.AddTicks(1);
        }

        return startInstants.To(endInstants);
    }

    public string TimelineToString(ITimeline timeline, int convertLength,
        TimelineMethods method)
    {
        return method switch
        {
            TimelineMethods.GetPreviousUtcInstant =>
                TimelineToStringUsingGetPreviousUtcInstant(timeline, convertLength),
            TimelineMethods.GetNextUtcInstant => TimelineToStringUsingGetNextUtcInstant(
                timeline, convertLength),
            TimelineMethods.IsInstant => TimelineToStringUsingIsInstant(
                timeline, convertLength),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }

    public string PeriodTimelineToString(IPeriodTimeline periodTimeline, int convertLength, TimelineMethods method)
    {
        return method switch
        {
            TimelineMethods.GetPreviousUtcInstant =>
                PeriodTimelineToStringUsingGetPreviousUtcInstant(periodTimeline, convertLength),
            TimelineMethods.GetNextUtcInstant => PeriodTimelineToStringUsingGetNextUtcInstant(
                periodTimeline, convertLength),
            TimelineMethods.IsInstant => PeriodTimelineToStringUsingIsInstant(
                periodTimeline, convertLength),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }

    private string TimelineToStringUsingGetPreviousUtcInstant(ITimeline timeline, int convertLength)
    {
        if ((DateTime.MaxValue - _origin).Ticks <= convertLength)
        {
            throw new ArgumentOutOfRangeException(nameof(_origin), "Origin + convertLength cannot be larger or equal to DateTime.MaxValue as we need to start one tick after the origin + convertLength to use GetPreviousUtcInstant.");
        }

        // Start at origin + convertLength
        var dateTime = _origin.AddTicks(convertLength);

        StringBuilder sb = new();

        while (dateTime >= _origin)
        {
            var previous = timeline.GetPreviousUtcInstant(dateTime);

            if (previous == null)
            {
                return sb.ToString().PadLeft(convertLength);
            }

            var distance = dateTime.Ticks - previous.Value.Ticks;
            if (sb.Length + distance > convertLength)
            {
                return $"|…{sb.ToString().PadLeft(convertLength)}";
            }

            if (distance > 1)
            {
                sb.Insert(0, new string(' ', (int)distance - 1));
            }

            sb.Insert(0, '|');
            dateTime = previous.Value;
        }

        return sb.ToString();
    }

    private string TimelineToStringUsingGetNextUtcInstant(ITimeline timeline, int convertLength)
    {
        if (_origin == DateTime.MinValue)
        {
            throw new ArgumentOutOfRangeException(nameof(_origin), "Origin cannot be DateTime.MinValue as we need to start one tick before the origin to use GetNextUtcInstant.");
        }

        if ((DateTime.MaxValue - _origin).Ticks < convertLength)
        {
            throw new ArgumentOutOfRangeException(nameof(_origin), "Origin + convertLength cannot be larger than DateTime.MaxValue.");
        }

        var dateTime = _origin;
        var end = dateTime.AddTicks(convertLength);
        // Start before origin
        dateTime = dateTime.AddTicks(-1);

        StringBuilder sb = new();

        while (dateTime < end)
        {
            var next = timeline.GetNextUtcInstant(dateTime);

            if (next == null)
            {
                return sb.ToString().PadRight(convertLength);
            }

            var distance = next.Value.Ticks - dateTime.Ticks;
            if (sb.Length + distance > convertLength)
            {
                return $"{sb.ToString().PadRight(convertLength)}…|";
            }

            if (distance > 1)
            {
                sb.Append(new string(' ', (int)distance - 1));
            }

            sb.Append('|');
            dateTime = next.Value;
        }

        return sb.ToString();
    }

    private string TimelineToStringUsingIsInstant(ITimeline timeline, int convertLength)
    {
        var dateTime = _origin;
        StringBuilder sb = new();

        for (int i = 0; i < convertLength; i++)
        {
            sb.Append(timeline.IsInstant(dateTime) ? '|' : ' ');

            dateTime = dateTime.AddTicks(1);
        }

        return sb.ToString();
    }

    private string PeriodTimelineToStringUsingGetPreviousUtcInstant(IPeriodTimeline periodTimeline,
        int convertLength)
    {
        if ((DateTime.MaxValue - _origin).Ticks <= convertLength)
        {
            throw new ArgumentOutOfRangeException(nameof(_origin), "Origin + convertLength cannot be larger or equal to DateTime.MaxValue as we need to start one tick after the origin + convertLength to use GetPreviousUtcInstant.");
        }

        // Start at origin + convertLength
        var dateTime = _origin.AddTicks(convertLength);

        StringBuilder sb = new();

        while (dateTime >= _origin)
        {
            var previousStart = periodTimeline.StartTimeline.GetPreviousUtcInstant(dateTime);
            var previousEnd = periodTimeline.EndTimeline.GetPreviousUtcInstant(dateTime);

            if (previousStart == null && previousEnd == null)
            {
                return sb.ToString().PadLeft(convertLength);
            }

            long? distanceToPreviousStart = previousStart == null ? null : dateTime.Ticks - previousStart.Value.Ticks;
            long? distanceToPreviousEnd = previousEnd == null ? null : dateTime.Ticks - previousEnd.Value.Ticks;

            var (periodChar, distance) = GetNextPeriodCharAndDistance(distanceToPreviousStart, distanceToPreviousEnd);

            if (sb.Length + distance > convertLength)
            {
                return $"{periodChar}…{sb.ToString().PadLeft(convertLength)}";
            }

            if (distance > 1)
            {
                sb.Insert(0, new string(' ', (int)distance - 1));
            }

            sb.Insert(0, periodChar);
            dateTime = dateTime.AddTicks(-distance);
        }

        return sb.ToString();
    }

    private string PeriodTimelineToStringUsingGetNextUtcInstant(IPeriodTimeline periodTimeline, int convertLength)
    {
        if (_origin == DateTime.MinValue)
        {
            throw new ArgumentOutOfRangeException(nameof(_origin), "Origin cannot be DateTime.MinValue as we need to start one tick before the origin to use GetNextUtcInstant.");
        }

        if ((DateTime.MaxValue - _origin).Ticks < convertLength)
        {
            throw new ArgumentOutOfRangeException(nameof(_origin), "Origin + convertLength cannot be larger than DateTime.MaxValue.");
        }

        var dateTime = _origin;
        var end = dateTime.AddTicks(convertLength);
        // Start before origin
        dateTime = dateTime.AddTicks(-1);

        StringBuilder sb = new();

        while (dateTime < end)
        {
            var nextStart = periodTimeline.StartTimeline.GetNextUtcInstant(dateTime);
            var nextEnd = periodTimeline.EndTimeline.GetNextUtcInstant(dateTime);

            if (nextStart == null && nextEnd == null)
            {
                return sb.ToString().PadRight(convertLength);
            }

            long? distanceToNextStart = nextStart == null ? null : nextStart.Value.Ticks - dateTime.Ticks;
            long? distanceToNextEnd = nextEnd == null ? null : nextEnd.Value.Ticks - dateTime.Ticks;

            var (periodChar, distance) = GetNextPeriodCharAndDistance(distanceToNextStart, distanceToNextEnd);
                
            if (sb.Length + distance > convertLength)
            {
                return $"{sb.ToString().PadRight(convertLength)}…{periodChar}";
            }

            if (distance > 1)
            {
                sb.Append(new string(' ', (int)distance - 1));
            }

            sb.Append(periodChar);
            dateTime = dateTime.AddTicks(distance);
        }

        return sb.ToString();
    }

    private string PeriodTimelineToStringUsingIsInstant(IPeriodTimeline periodTimeline, int convertLength)
    {
        var dateTime = _origin;
        StringBuilder sb = new();

        for (int i = 0; i < convertLength; i++)
        {
            var start = periodTimeline.StartTimeline.IsInstant(dateTime);
            var end = periodTimeline.EndTimeline.IsInstant(dateTime);

            sb.Append(GetPeriodTimelineChar(start, end));

            dateTime = dateTime.AddTicks(1);
        }

        return sb.ToString();
    }

    private static char GetPeriodTimelineChar(bool start, bool end)
    {
        if (start && end)
        {
            return 'X';
        }
        if (start)
        {
            return '<';
        }
        if (end)
        {
            return '>';
        }

        return ' ';
    }

    private static (char periodChar, long distance) GetNextPeriodCharAndDistance(
        long? distanceToStart,
        long? distanceToEnd)
    {
        if (distanceToStart == null && distanceToEnd == null)
        {
            return (' ', long.MaxValue);
        }
        if (distanceToStart == null || distanceToEnd < distanceToStart)
        {
            return ('>', distanceToEnd!.Value);
        }
        if (distanceToEnd == null || distanceToStart < distanceToEnd)
        {
            return ('<', distanceToStart.Value);
        }

        return ('X', distanceToStart.Value);
    }
}