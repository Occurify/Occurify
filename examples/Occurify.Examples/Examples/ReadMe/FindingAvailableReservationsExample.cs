using Occurify.Examples.Helpers;
using Occurify.Extensions;
using Occurify.TimeZones;
using Occurify.TimeZones.Extensions;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class FindingAvailableReservationsExample : IExample
    {
        public string Command => "readme/finding-available-reservations";

        public void Run()
        {
            var reservationsIn2025 = ExamplePeriodHelper.CreateRandomAppointments(TimeZonePeriods.Year(2025));
            var availableFreePeriodsInAugust =
                FindAvailableFreePeriods(TimeZonePeriods.Month(8, 2025), reservationsIn2025, TimeSpan.FromMinutes(90));

            Console.WriteLine("Available periods for 90 min timeslots in august:");
            foreach (var period in availableFreePeriodsInAugust)
            {
                Console.WriteLine(period.ToLocalTimeZoneString());
            }
        }

        private static Period[] FindAvailableFreePeriods(Period searchRange, Period[] reservations, TimeSpan minimumDuration)
        {
            return reservations
                .Invert() // Identify free periods by inverting the reserved ones
                .WherePeriods(p =>
                    p.Duration >= minimumDuration) // Filter periods that meet the minimum duration requirement
                .EnumeratePeriod(searchRange) // Restrict results to the given search range
                .ToArray();
        }
    }
}
