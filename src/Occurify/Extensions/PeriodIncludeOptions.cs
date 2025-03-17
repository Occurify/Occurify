namespace Occurify.Extensions;

public enum PeriodIncludeOptions
{
    /// <summary>
    /// Only complete periods will be included.
    /// </summary>
    CompleteOnly,
    /// <summary>
    /// The end of periods should be included. The start does not have to be.
    /// </summary>
    StartPartialAllowed,
    /// <summary>
    /// The start of the periods should be included. The end does not have to be.
    /// </summary>
    EndPartialAllowed,
    /// <summary>
    /// All periods touching will be included.
    /// </summary>
    PartialAllowed,
}