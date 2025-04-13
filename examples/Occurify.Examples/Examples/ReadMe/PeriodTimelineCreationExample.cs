using Occurify.Extensions;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class PeriodTimelineCreationExample : IExample
    {
        public string Command => "readme/period-timeline-creation";

        public void Run()
        {
            DateTime utcNow = DateTime.UtcNow;
            Period period = utcNow.To(utcNow + TimeSpan.FromHours(2));

            // Using extension methods on periods
            IPeriodTimeline periodTimeline1 = period.AsPeriodTimeline();
            IPeriodTimeline periodTimeline2 = new[] { period, period + TimeSpan.FromHours(2) }.AsPeriodTimeline();

            // Using extension methods on instant timelines
            ITimeline periodStartTimeline = Timeline.Periodic(TimeSpan.FromHours(1));
            ITimeline periodEndTimeline = periodStartTimeline.OffsetMinutes(10);
            IPeriodTimeline periodTimeline3 = periodStartTimeline.To(periodEndTimeline);

            // Using static methods
            IPeriodTimeline periodTimeline4 = PeriodTimeline.FromPeriods(period, period + TimeSpan.FromHours(2), period + TimeSpan.FromHours(4));
            IPeriodTimeline periodTimeline5 = PeriodTimeline.Between(periodStartTimeline, periodEndTimeline);
        }
    }
}
