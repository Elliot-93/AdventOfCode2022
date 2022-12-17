namespace AoC_2022;

public static class StringExtentions
{
    public static List<int> AllIndexesOf(this string str, string value)
    {
        if (String.IsNullOrEmpty(value))
            return Enumerable.Empty<int>().ToList();
        List<int> indexes = new();
        for (int index = 0; ; index += value.Length)
        {
            index = str.IndexOf(value, index);
            if (index == -1)
                return indexes;
            indexes.Add(index);
        }
    }
}


