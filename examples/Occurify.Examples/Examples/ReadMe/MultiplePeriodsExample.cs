using Occurify.Examples.Helpers;
using Occurify.Extensions;
using Occurify.TimeZones;
using Occurify.TimeZones.Extensions;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class MultiplePeriodsExample : IExample
    {
        public string Command => "readme/multiple-periods";

        public void Run()
        {
            IPeriodTimeline workingHours = TimeZonePeriods.Between(startHour: 8, endHour: 18) - TimeZonePeriods.Weekends();

            List<Period[]> employeeAppointments = Enumerable.Range(0, 5).Select(_ => ExamplePeriodHelper.CreateRandomAppointments(TimeZonePeriods.Year(2025))).ToList();
            IPeriodTimeline[] appointmentTimelines = employeeAppointments.Select(p => p.AsPeriodTimeline()).ToArray();
            IPeriodTimeline[] invertedTimelines = appointmentTimelines.Invert().ToArray();

            IPeriodTimeline availableSlotsTimelines = invertedTimelines.IntersectPeriods() & workingHours;

            Period august = TimeZonePeriods.Month(8, 2025);
            IEnumerable<Period> periodsEveryoneIsAvailable = availableSlotsTimelines
                .EnumeratePeriod(august).Where(p => p.Duration > TimeSpan.FromHours(1));

            Console.WriteLine("Everyone is available on:");
            foreach (Period period in periodsEveryoneIsAvailable)
            {
                Console.WriteLine(period.ToLocalTimeZoneString());
            }

            DateTime timeOfInterest = new DateTime(2025, 3, 7).AsUtcInstant();
            PeriodTimelineSample[] samples = appointmentTimelines.SampleAt(timeOfInterest).ToArray();

            int appointmentPeriods = samples.Count(s => s.IsPeriod);
            int freeTimePeriods = samples.Count(s => s.IsGap);

            Console.WriteLine($"{appointmentPeriods} people had an appointment on {timeOfInterest.ToLocalTimeZoneString()}.");
            Console.WriteLine($"{freeTimePeriods} people had were free on {timeOfInterest.ToLocalTimeZoneString()}.");
        }
    }
}
