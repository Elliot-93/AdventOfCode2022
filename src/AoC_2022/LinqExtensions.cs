using Newtonsoft.Json.Linq;

namespace AoC_2022;

public static class LinqExtensions
{
    public static IEnumerable<T> Where<T>(this T[,] source, Func<T, bool> predicate)
    {
        if (source == null) throw new ArgumentNullException("source");
        if (predicate == null) throw new ArgumentNullException("predicate");
        return WhereImpl(source, predicate);
    }

    private static IEnumerable<T> WhereImpl<T>(this T[,] source, Func<T, bool> predicate)
    {
        for (int i = 0; i < source.GetLength(0); ++i)
        {
            T value;
            for (int j = 0; j < source.GetLength(1); ++j)
            {
                value = source[i, j];

                if (predicate(value))
                {
                    yield return value;
                }
            }
        }
    }
}