using System.Text.RegularExpressions;

namespace AoC_2022;

public partial class Day_05 : BaseDay
{
    private readonly List<MoveInstruction> _input;

    //                     [Q]     [P] [P]
    //                 [G] [V] [S] [Z] [F]
    //             [W] [V] [F] [Z] [W] [Q]
    //         [V] [T] [N] [J] [W] [B] [W]
    //     [Z] [L] [V] [B] [C] [R] [N] [M]
    // [C] [W] [R] [H] [H] [P] [T] [M] [B]
    // [Q] [Q] [M] [Z] [Z] [N] [G] [G] [J]
    // [B] [R] [B] [C] [D] [H] [D] [C] [N]
    //  1   2   3   4   5   6   7   8   9 
    private readonly Stack<char>[] stacks = new Stack<char>[]
    {
        new Stack<char>(new char[] { 'B', 'Q', 'C'}),
        new Stack<char>(new char[] { 'R', 'Q', 'W', 'Z'}),
        new Stack<char>(new char[] { 'B', 'M', 'R', 'L', 'V'}),
        new Stack<char>(new char[] { 'C', 'Z', 'H', 'V', 'T', 'W'}),
        new Stack<char>(new char[] { 'D', 'Z', 'H', 'B', 'N', 'V', 'G'}),
        new Stack<char>(new char[] { 'H', 'N', 'P', 'C', 'J', 'F', 'V', 'Q'}),
        new Stack<char>(new char[] { 'D', 'G', 'T', 'R', 'W', 'Z', 'S'}),
        new Stack<char>(new char[] { 'C', 'G', 'M', 'N', 'B', 'W', 'Z', 'P'}),
        new Stack<char>(new char[] { 'N', 'J', 'B', 'M', 'W', 'Q', 'F', 'P'}),

    };

    private readonly Stack<char>[] stacks2 = new Stack<char>[]
    {
        new Stack<char>(new char[] { 'B', 'Q', 'C'}),
        new Stack<char>(new char[] { 'R', 'Q', 'W', 'Z'}),
        new Stack<char>(new char[] { 'B', 'M', 'R', 'L', 'V'}),
        new Stack<char>(new char[] { 'C', 'Z', 'H', 'V', 'T', 'W'}),
        new Stack<char>(new char[] { 'D', 'Z', 'H', 'B', 'N', 'V', 'G'}),
        new Stack<char>(new char[] { 'H', 'N', 'P', 'C', 'J', 'F', 'V', 'Q'}),
        new Stack<char>(new char[] { 'D', 'G', 'T', 'R', 'W', 'Z', 'S'}),
        new Stack<char>(new char[] { 'C', 'G', 'M', 'N', 'B', 'W', 'Z', 'P'}),
        new Stack<char>(new char[] { 'N', 'J', 'B', 'M', 'W', 'Q', 'F', 'P'}),

    };

    public Day_05()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        foreach (var intruction in _input) 
        {
            var fromStack = stacks[intruction.from - 1];
            var toStack = stacks[intruction.to - 1];

            for (int i = 0; i < intruction.moveCount; i++)
            {
                var popped = fromStack.Pop();
                toStack.Push(popped);
            }
        }

        var topOfEach = "";

        foreach (var stack in stacks)
        {
            topOfEach += stack.Pop();
        }

        return new(topOfEach);
    }

    public override ValueTask<string> Solve_2()
    {
        foreach (var intruction in _input)
        {
            var fromStack = stacks2[intruction.from - 1];
            var toStack = stacks2[intruction.to - 1];

            var popped = new List<char>();
            for (int i = 0; i < intruction.moveCount; i++)
            {
                popped.Add(fromStack.Pop());
            }

            for (int pi = popped.Count - 1; pi >= 0; pi--)
            {
                toStack.Push(popped.ElementAt(pi));
            }
            popped.Clear();
        }

        var topOfEach = "";

        foreach (var stack in stacks2)
        {
            topOfEach += stack.Pop();
        }

        return new(topOfEach);
    }

    private List<MoveInstruction> ParseInput()
    {
        var regex = ParseRegex();

        var foo = File.ReadAllLines(InputFilePath)
            .Select(l => regex.Matches(l))
            .Where(mc => mc.Count > 0)
            .Select(mc => mc[0]);

        return foo.Select(m => new MoveInstruction(
            int.Parse(m.Groups[1].Value),
            int.Parse(m.Groups[2].Value),
            int.Parse(m.Groups[3].Value))).ToList();
    }

    private record struct MoveInstruction(int moveCount, int from,  int to) { }

    [GeneratedRegex("move (?<moveCount>\\d+) from (?<from>\\d+) to (?<to>\\d+)", RegexOptions.Compiled)]
    private static partial Regex ParseRegex();
}


