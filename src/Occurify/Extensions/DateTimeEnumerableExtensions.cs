
namespace Occurify.Extensions
{
    internal static class DateTimeEnumerableExtensions
    {
        public static IEnumerable<DateTime?> OrderAndPutNullFirst(this IEnumerable<DateTime?> enumerable) =>
            enumerable.OrderBy(x => x);

        public static IEnumerable<DateTime?> OrderAndPutNullLast(this IEnumerable<DateTime?> enumerable) =>
            enumerable.OrderBy(x => x == null).ThenBy(x => x);
    }
}
