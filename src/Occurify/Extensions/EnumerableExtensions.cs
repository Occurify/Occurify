namespace Occurify.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<T> CombineOrderedEnumerables<T>(this IEnumerable<IEnumerable<T>> sources, bool descending = false) where T : IComparable<T>
        {
            var enumerators = sources
                .Select(e => e.GetEnumerator())
                .Where(e => e.MoveNext())
                .ToList();

            T? lastYielded = default;
            var hasLastYielded = false;

            while (enumerators.Count > 0)
            {
                // Find the enumerator with the smallest current value
                var minIndex = 0;
                for (var i = 1; i < enumerators.Count; i++)
                {
                    if (descending)
                    {
                        if (enumerators[i].Current.CompareTo(enumerators[minIndex].Current) > 0)
                        {
                            minIndex = i;
                        }
                    }
                    else
                    {
                        if (enumerators[i].Current.CompareTo(enumerators[minIndex].Current) < 0)
                        {
                            minIndex = i;
                        }
                    }
                }

                var nextValue = enumerators[minIndex].Current;

                if (!hasLastYielded || lastYielded!.CompareTo(nextValue) != 0)
                {
                    yield return nextValue;
                    lastYielded = nextValue;
                    hasLastYielded = true;
                }

                // Move to the next item in that enumerator
                if (!enumerators[minIndex].MoveNext())
                {
                    enumerators.RemoveAt(minIndex);
                }
            }
        }
    }
}
