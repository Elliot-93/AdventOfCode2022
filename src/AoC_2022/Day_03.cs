namespace AoC_2022;

public class Day_03 : BaseDay
{
    private readonly List<string> _input;

    public Day_03()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(_input
            .Select(i =>
            new
            {
                c1 = i.Substring(0, i.Length / 2),
                c2 = i.Substring(i.Length / 2)
            })
            .Select(bp => bp.c1.First(c1i => bp.c2.Contains(c1i)))
            .Select(getItemPriority)
            .Sum()
            .ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = 0;

        for (int i = 0; i < _input.Count(); i += 3)
        {
            var item = _input
                .Skip(i)
                .Take(1)
                .First()
                .First(item => _input.ElementAt(i+1).Contains(item) && _input.ElementAt(i+2).Contains(item));

            sum += getItemPriority(item);
        }

        return new(sum.ToString());
    }

    private List<string> ParseInput()
    {
        return File.ReadAllLines(InputFilePath).ToList();
    }

    private int getItemPriority(char c) 
    {
        var i = char.ToUpper(c) - 'A';

        return char.IsUpper(c) ? i + 27 : i + 1;
    }
}


