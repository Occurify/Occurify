using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class TimeSpanExtensions
{
    internal static Duration? ToDuration(this TimeSpan? timeSpan)
    {
        if (timeSpan == null)
        {
            return null;
        }

        return Duration.FromTimeSpan(timeSpan.Value);
    }
}