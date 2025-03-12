namespace Occurify.Extensions;

internal static class PeriodIncludeOptionsExtensions
{
    internal static bool AllowsStartPartial(this PeriodIncludeOptions periodIncludeOption) =>
        periodIncludeOption is PeriodIncludeOptions.PartialAllowed or PeriodIncludeOptions.StartPartialAllowed;

    internal static bool AllowsEndPartial(this PeriodIncludeOptions periodIncludeOption) =>
        periodIncludeOption is PeriodIncludeOptions.PartialAllowed or PeriodIncludeOptions.EndPartialAllowed;
}