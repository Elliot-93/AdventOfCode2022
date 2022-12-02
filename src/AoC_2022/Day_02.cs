namespace AoC_2022;

public class Day_02 : BaseDay
{
    private readonly List<(string opponentShape, string myShape)> _input;

    public Day_02()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(_input.Select(i => getRoundTotalScore(i.opponentShape, i.myShape)).Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var newInput = _input
            .Select(i => (i.opponentShape, getLetterRequiredForDesiredOutcome(i.opponentShape, i.myShape))).ToList();

        var scores = newInput
            .Select(i => getRoundTotalScore(i.opponentShape, i.Item2)).ToList();

        return new(scores.Sum().ToString());
    }

    private List<(string opponentShape, string myShape)> ParseInput()
    {
        var file = new ParsedFile(InputFilePath);

        return file.Select(l => (l.ElementAt<string>(0), l.ElementAt<string>(1))).ToList();
    }

    private int getRoundTotalScore(string opponentShape, string myShape)
    { 
        return getOutcomeScore(opponentShape, myShape) + getSelectionBonus(myShape);
    }

    private int getOutcomeScore(string opponentShape, string myShape)
    {
        switch (opponentShape.ToUpper())
        {
            case "A":
                switch (myShape.ToUpper())
                {
                    case "X":
                        return 3;
                    case "Y":
                        return 6;
                    case "Z":
                        return 0;
                    default:
                        throw new Exception($"Letter not recognised [{myShape}]");
                }
            case "B":
                switch (myShape.ToUpper())
                {
                    case "X":
                        return 0;
                    case "Y":
                        return 3;
                    case "Z":
                        return 6;
                    default:
                        throw new Exception($"Letter not recognised [{myShape}]");
                }
            case "C":
                switch (myShape.ToUpper())
                {
                    case "X":
                        return 6;
                    case "Y":
                        return 0;
                    case "Z":
                        return 3;
                    default:
                        throw new Exception($"Letter not recognised [{myShape}]");
                }
            default:
                throw new Exception($"Letter not recognised [{opponentShape}]");
        }
    }

    private int getSelectionBonus(string myShape)
    {
        switch (myShape.ToUpper())
        {
            case "X":
                return 1;
            case "Y":
                return 2;
            case "Z":
                return 3;
            default:
                throw new Exception($"Letter not recognised [{myShape}]");
        }
    }

    private string getLetterRequiredForDesiredOutcome(string opponentShape, string outcomeRequired) 
    {
        switch (opponentShape.ToUpper())
        {
            case "A":
                switch (outcomeRequired.ToUpper())
                {
                    case "X":
                        return "Z";
                    case "Y":
                        return "X";
                    case "Z":
                        return "Y";
                    default:
                        throw new Exception($"Letter not recognised [{outcomeRequired}]");
                }
            case "B":
                switch (outcomeRequired.ToUpper())
                {
                    case "X":
                        return "X";
                    case "Y":
                        return "Y";
                    case "Z":
                        return "Z";
                    default:
                        throw new Exception($"Letter not recognised [{outcomeRequired}]");
                }
            case "C":
                switch (outcomeRequired.ToUpper())
                {
                    case "X":
                        return "Y";
                    case "Y":
                        return "Z";
                    case "Z":
                        return "X";
                    default:
                        throw new Exception($"Letter not recognised [{outcomeRequired}]");
                }
            default:
                throw new Exception($"Letter not recognised [{opponentShape}]");
        }
    }
}


