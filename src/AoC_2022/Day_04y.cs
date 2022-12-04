using MoreLinq;
using System.Linq;

namespace AoC_2022;

public class Day_04 : BaseDay
{
    private readonly List<(int[], int[])> _input;

    public Day_04()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(_input
            .Select(i =>
            new
            {
                oneL = i.Item1.Length,
                twoL = i.Item2.Length,
                intersectL = i.Item1.Intersect(i.Item2).Count()
            })
            .Where(a => a.intersectL >= a.oneL || a.intersectL >= a.twoL)
            .Count()
            .ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(_input
            .Count(i => i.Item1.Intersect(i.Item2).Count() > 0)
            .ToString());
    }

    private List<(int[], int[])> ParseInput()
    {
        var foo = File.ReadAllLines(InputFilePath)
            .Select(l => l.Split(","));

        return foo.Select(s => (CreateRange(s[0]), CreateRange(s[1]))).ToList();
    }

    private int[] CreateRange(string range) 
    {
        var rangeStart = int.Parse(range.Split("-")[0]);
        var rangeEnd = int.Parse(range.Split("-")[1]);

        return Enumerable.Range(rangeStart, Math.Abs(rangeEnd - rangeStart)+1).ToArray();
    }
}


