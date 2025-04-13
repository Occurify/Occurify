using Occurify.Extensions;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class PeriodCreationExample : IExample
    {
        public string Command => "readme/period-creation";

        public void Run()
        {
            DateTime utcNow = DateTime.UtcNow;

            // Using extension methods
            Period nowToOneHoursFromNow = utcNow.ToPeriodWithDuration(TimeSpan.FromHours(1));
            Period nowToTwoHoursFromNow = utcNow.To(utcNow + TimeSpan.FromHours(2));
            Period nowToNeverEnding = utcNow.To(null);

            // Using static construction
            Period nowToThreeHoursFromNow = Period.Create(utcNow, utcNow + TimeSpan.FromHours(3));
        }
    }
}
