using System.Globalization;
using Occurify.TimeZones;
using Occurify.TimeZones.Extensions;

namespace Occurify.Examples.Examples.TimeZone
{
    internal class PeriodToStringExample : IExample
    {
        public string Command => "timezone/period-to-string";

        public void Run()
        {
            var today = TimeZonePeriods.Today();

            Console.WriteLine("`TimeZonePeriods.Today()` represented using:");

            const int padWidth = 42;
            Console.WriteLine($"{"ToString:",-padWidth}{today}");
            Console.WriteLine($"{"ToString(format):",-padWidth}{today.ToString("dddd dd MMMM hh:mm:ss.fff")}");
            Console.WriteLine($"{"ToString(provider):",-padWidth}{today.ToString(CultureInfo.CreateSpecificCulture("nl-NL"))}");
            Console.WriteLine($"{"ToString(format, provider):",-padWidth}{today.ToString("dddd dd MMMM hh:mm:ss.fff", CultureInfo.CreateSpecificCulture("nl-NL"))}");
            Console.WriteLine($"{"ToLongDateString():",-padWidth}{today.ToLongDateString()}");
            Console.WriteLine($"{"ToLongTimeString():",-padWidth}{today.ToLongTimeString()}");
            Console.WriteLine($"{"ToShortDateString():",-padWidth}{today.ToShortDateString()}");
            Console.WriteLine($"{"ToShortTimeString():",-padWidth}{today.ToShortTimeString()}");

            Console.WriteLine($"{"ToLocalTimeZoneString:",-padWidth}{today.ToLocalTimeZoneString()}");
            Console.WriteLine($"{"ToLocalTimeZoneString(format):",-padWidth}{today.ToLocalTimeZoneString("dddd dd MMMM hh:mm:ss.fff")}");
            Console.WriteLine($"{"ToLocalTimeZoneString(provider):",-padWidth}{today.ToLocalTimeZoneString(CultureInfo.CreateSpecificCulture("nl-NL"))}");
            Console.WriteLine($"{"ToLocalTimeZoneString(format, provider):",-padWidth}{today.ToLocalTimeZoneString("dddd dd MMMM hh:mm:ss.fff", CultureInfo.CreateSpecificCulture("nl-NL"))}");
            Console.WriteLine($"{"ToLocalTimeZoneLongDateString():",-padWidth}{today.ToLocalTimeZoneLongDateString()}");
            Console.WriteLine($"{"ToLocalTimeZoneLongTimeString():",-padWidth}{today.ToLocalTimeZoneLongTimeString()}");
            Console.WriteLine($"{"ToLocalTimeZoneShortDateString():",-padWidth}{today.ToLocalTimeZoneShortDateString()}");
            Console.WriteLine($"{"ToLocalTimeZoneShortTimeString():",-padWidth}{today.ToLocalTimeZoneShortTimeString()}");
        }
    }
}
