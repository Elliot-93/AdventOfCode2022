namespace AoC_2022;

public class Day_10 : BaseDay
{
    private readonly List<string> _input;

    public Day_10()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(parseInstructions().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(getOutputString());
    }

    private List<string> ParseInput()
    {
        return File.ReadAllLines(InputFilePath).ToList();
    }   

    private int parseInstructions()
    {
        var x = 1;
        var cycle = 1;
        var sumX = 0;
        var inputIndex = 0;
        var addition = 0;

        do
        {
            if (addition != 0)
            {
                sumX = calcSumx(x, cycle, sumX);

                x += addition;
                addition = 0;

                inputIndex++;
                cycle++;
                continue;

            }

            var instruction = _input[inputIndex];

            if (instruction == "noop")
            {
                sumX = calcSumx(x, cycle, sumX);

                inputIndex++;
                cycle++;
                continue;
            }
            else
            {
                var v = int.Parse(instruction.Split(" ")[1]);
                addition = v;

                sumX = calcSumx(x, cycle, sumX);

                cycle++;
                continue;
            }
        } while (inputIndex < _input.Count);

        return sumX;
    }

    private static int calcSumx(int x, int cycle, int sumX)
    {
        if ((cycle+20) % 40 == 0)
        {
            return sumX + (cycle * x);
        }

        return sumX;
    }

    private string getOutputString()
    {
        var outputString = "";

        var spritePosition = 1;
        var cycle = 1;
        var inputIndex = 0;
        var addition = 0;

        do
        {
            var rowPosition = cycle % 40;
            var spriteRange = Enumerable.Range(spritePosition, 3);

            if (spriteRange.Contains(rowPosition))
            {
                outputString += "#";
            }
            else
            {
                outputString += ".";
            }

            newLineIfEndOfRow(cycle, ref outputString);

            if (addition != 0)
            {
                spritePosition += addition;
                addition = 0;

                inputIndex++;
                cycle++;
                continue;

            }

            var instruction = _input[inputIndex];

            if (instruction == "noop")
            {
                inputIndex++;
                cycle++;
                continue;
            }
            else
            {
                var v = int.Parse(instruction.Split(" ")[1]);
                addition = v;
                cycle++;
                continue;
            }
        } while (inputIndex < _input.Count);

        return outputString;
    }

    private static void newLineIfEndOfRow(int cycle, ref string outputString)
    {
        if ((cycle) % 40 == 0)
        {
            outputString += Environment.NewLine;
        }
    }
}


