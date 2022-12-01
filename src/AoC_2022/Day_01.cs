using MoreLinq;

namespace AoC_2022;

public class Day_01 : BaseDay
{
    private readonly List<string> _input;

    public Day_01()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(getElfsTotalCalories().Max().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(getElfsTotalCalories().OrderByDescending(x => x).Take(3).Sum().ToString());
    }

    private List<string> ParseInput()
    {
        var file = new ParsedFile(InputFilePath);

        return File.ReadAllLines(InputFilePath).ToList();
    }   

    private IEnumerable<int> getElfsTotalCalories()
    {
        return _input.Split("").Select(g => g.Select(i => int.Parse(i)).Sum());
    }
}


