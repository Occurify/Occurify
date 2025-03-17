using Cronos;

namespace Occurify.TimeZones.Helpers
{
    internal static class CronHelper
    {
        internal static CronFormat ResolveCronFormat(string cronExpression)
        {
            // Note: return Standard for everything that is certainly not IncludeSeconds. Let CronExpression.Parse throw the exception if the format is not otherwise correct.
            return !string.IsNullOrEmpty(cronExpression) && cronExpression.Split(' ').Length == 6
                ? CronFormat.IncludeSeconds
                : CronFormat.Standard;
        }
    }
}
