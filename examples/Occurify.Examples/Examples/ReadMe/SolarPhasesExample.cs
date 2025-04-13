using Occurify.Astro;
using Occurify.Extensions;
using Occurify.TimeZones;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class SolarPhasesExample : IExample
    {
        public string Command => "readme/solar-phases";

        public void Run()
        {
            Coordinates arcticCoordinates = new Coordinates(80.45302, 54.77918, Height: 37);
            ITimeline sunsetsAndRises = AstroInstants.SunPhases(arcticCoordinates, SunPhases.Sunrise | SunPhases.Sunset);
            IPeriodTimeline daysOfCurrentYear = TimeZonePeriods.Days().Within(TimeZonePeriods.CurrentYear());
            IPeriodTimeline daysWithoutSunsetsOrRises = daysOfCurrentYear - daysOfCurrentYear.Containing(sunsetsAndRises);

            Console.WriteLine($"This year on the arctic the sun doesn't rise or set on {daysWithoutSunsetsOrRises.Count()} days.");
        }
    }
}
