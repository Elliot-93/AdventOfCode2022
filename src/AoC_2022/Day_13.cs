using MoreLinq;
using Newtonsoft.Json.Linq;
using SheepTools.Extensions;

namespace AoC_2022;

public partial class Day_13 : BaseDay
{
    private readonly List<(JArray, JArray)> _inputOne;
    private readonly List<JArray> _inputTwo;

    public Day_13()
    {
        _inputOne = ParseInputOne();
        _inputTwo = ParseInputTwo();
    }

    public override ValueTask<string> Solve_1()
    {
        var i = 1;
        var rightOrderSum = 0;
        foreach (var pair in _inputOne)
        {
            var inOrder = PairInCorrectOrder(pair.Item1.Children(), pair.Item2.Children());
            Console.WriteLine($"Pair {i} Result: {inOrder}");
            if (inOrder == null || inOrder == true)
            {
                rightOrderSum += i;
            }

            i++;
        }

        return new(rightOrderSum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        BubbleSort(_inputTwo);

        var twoIndex = 0;
        var sixIndex = 0;

        for (int i = 1; i <= _inputTwo.Count(); i++)
        {
            var foo = _inputTwo[i - 1];
            var a = _inputTwo[i-1].Children();
            if (a.Count() == 1
                && a.ElementAt(0).Type == JTokenType.Array
                && a.ElementAt(0)?.Value<JArray>()?.Children().Count() == 1
                && a.ElementAt(0).Value<JArray>().ElementAt(0).Type == JTokenType.Integer)
            {
                if (a.ElementAt(0).Value<JArray>().ElementAt(0).Value<int>() == 2)
                {
                    twoIndex = i;
                }

                if (a.ElementAt(0).Value<JArray>().ElementAt(0).Value<int>() == 6) 
                {
                    sixIndex = i;
                }
            }
        }

        return new((twoIndex * sixIndex).ToString());
    }

    public void BubbleSort(List<JArray> arrays)
    {
        var itemMoved = false;
        do
        {
            itemMoved = false;
            for (int i = 0; i < arrays.Count() - 1; i++)
            {
                var inOrder = PairInCorrectOrder(arrays[i].Children(), arrays[i + 1].Children());

                if (inOrder == false)
                {
                    var lowerValue = arrays[i + 1];
                    arrays[i + 1] = arrays[i];
                    arrays[i] = lowerValue;
                    itemMoved = true;
                }
            }
        } while (itemMoved);
    }

    private List<(JArray, JArray)> ParseInputOne()
    {
        var pairs = File.ReadAllLines(InputFilePath).Split("");
        return pairs.Select(p =>
                (ParseElement(p.ElementAt(0)),
                ParseElement(p.ElementAt(1))))
            .ToList();
    }

    private List<JArray> ParseInputTwo()
    {
        var arrays = File.ReadAllLines(InputFilePath).Where(l => !l.IsNullOrEmpty());
        return arrays.Select(ParseElement).ToList();
    }

    private JArray ParseElement(string section)
    {
        return JArray.Parse(section);
    }

    private bool? PairInCorrectOrder(IEnumerable<JToken> left, IEnumerable<JToken> right)
    {
        var maxLen = Math.Max(left.Count(), right.Count());

        for (int i = 0; i < maxLen; i++)
        {
            var lc = left.ElementAtOrDefault(i);
            var rc = right.ElementAtOrDefault(i);

            if (lc == null && rc != null)
            {
                return true;
            }

            if (lc != null && rc == null)
            {
                return false;
            }

            if (lc?.Type == JTokenType.Integer
                && rc?.Type == JTokenType.Integer)
            {
                var lInt = lc.Value<int>();
                var rInt = rc.Value<int>();

                if (lInt < rInt)
                {
                    return true;
                }
                else if (lInt > rInt)
                {
                    return false;
                }
            }
            else if (lc?.Type == JTokenType.Array
                && rc?.Type == JTokenType.Array)
            {
                var result = PairInCorrectOrder(lc.Value<JArray>().Children(), rc.Value<JArray>().Children());
                if (result != null) { return result; }
            }
            else if (lc?.Type == JTokenType.Array
                || rc?.Type == JTokenType.Array)
            {
                var lArray = lc?.Type == JTokenType.Array
                    ? lc.Value<JArray>()
                    : new JArray(new int[] { lc.Value<int>() });

                var rArray = rc?.Type == JTokenType.Array
                    ? rc.Value<JArray>()
                    : new JArray(new int[] { rc.Value<int>() });

                var result = PairInCorrectOrder(lArray.Children(), rArray.Children());
                if (result != null) { return result; }
            }
        }

        return null;
    }
}