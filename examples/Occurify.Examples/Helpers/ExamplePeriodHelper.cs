namespace Occurify.Examples.Helpers
{
    internal class ExamplePeriodHelper
    {
        /// <summary>
        /// Will generate some random appointments for a given period.
        /// Appointments can go from 6 AM to 9 PM.
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public static Period[] CreateRandomAppointments(Period period)
        {
            var periods = new List<Period>();
            var random = new Random();
            var latestAppointment = new TimeOnly(21, 0);

            for (var dayOfYear = period.Start!.Value; dayOfYear < period.End; dayOfYear = dayOfYear.AddDays(1))
            {
                // Note1: We start a little early, as the demo demonstrates filtering on working time.
                // Note2: even though we don't convert this to UTC, this works as we add the time of day to the date which is in UTC (it's used relatively).
                var timeOfDay = new TimeOnly(6, 0);
                do
                {
                    timeOfDay = timeOfDay.Add(TimeSpan.FromMinutes(15 * random.Next(0, 9)));
                    if (timeOfDay > latestAppointment)
                    {
                        break;
                    }

                    if (random.NextDouble() < 0.8)
                    {
                        continue; // 80% chance to skip this appointment
                    }

                    // Random duration between 15 min and 2 hours
                    var duration = TimeSpan.FromMinutes(15 * random.Next(1, 9));

                    if (timeOfDay.Add(duration) > latestAppointment)
                    {
                        break;
                    }
                    // Create the period and add it to the list
                    periods.Add(Period.Create(dayOfYear + timeOfDay.ToTimeSpan(), duration));

                    timeOfDay = timeOfDay.Add(duration);
                }
                while (timeOfDay < latestAppointment);
            }

            return periods.ToArray();
        }
    }
}
