namespace AoC_2022;

public partial class Day_06 : BaseDay
{
    private readonly string _input;

    public Day_06()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(getStartOfMessagePosition(4).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(getStartOfMessagePosition(14).ToString());
    }

    private string ParseInput()
    {
        return File.ReadAllText(InputFilePath);
    }

    private int getStartOfMessagePosition(int uniqueCount)
    {
        for (int i = 0; i < _input.Length; i++)
        {
            var lastFour = _input.Skip(i).Take(uniqueCount);

            if (lastFour.Distinct().Count() == lastFour.Count())
            {
                return i + uniqueCount;
            }
        }

        return 0;
    }
}


