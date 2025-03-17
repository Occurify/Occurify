namespace Occurify.TimeZones.Extensions
{
    internal static class DayOfWeekExtensions
    {
        internal static DayOfWeek NextDay(this DayOfWeek dayOfWeek)
        {
            return (DayOfWeek)(((int)dayOfWeek + 1) % 7);
        }
    }
}
