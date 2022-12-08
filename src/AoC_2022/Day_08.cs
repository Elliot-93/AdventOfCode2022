using MoreLinq;
using Spectre.Console;

namespace AoC_2022;

public class Day_08 : BaseDay
{
    private readonly int[,] _input;

    public Day_08()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(GetVisbleTreeCount(_input).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(GetHighestTreeScenicScore(_input).ToString());
    }

    private int[,] ParseInput()
    {
        var input = File.ReadAllLines(InputFilePath);

        var rowCount = input.Length;
        var colsCount = input[0].Length;

        int[,] result = new int[rowCount, colsCount];
        for (int y = 0; y < rowCount; y++)
        {
            var row = input[y];
            for (int x = 0; x < colsCount; x++)
            {
                var v = row[x];
                result[y, x] = int.Parse(v.ToString());
            }
        }


        return result;
    }

    private int GetHighestTreeScenicScore(int[,] trees)
    {
        var highestScore = 0;

        var rowCount = trees.GetLength(0);
        var colCount = trees.GetLength(1);

        for (int r = 0; r < rowCount; r++)
        {
            var row = GetRow(trees, r);
            for (int c = 0; c < colCount; c++)
            {
                var col = GetColumn(trees, c);

                var treeHeight = trees[r, c];
                var leftTrees = row[0..c];
                var rightTrees = row[(c + 1)..colCount];
                var upTrees = col[0..r];
                var downTrees = col[(r + 1)..rowCount];

                var leftScore = GetScenicScore(treeHeight, leftTrees.Reverse().ToArray());
                var rightScore = GetScenicScore(treeHeight, rightTrees);
                var upScore = GetScenicScore(treeHeight, upTrees.Reverse().ToArray());
                var downScore = GetScenicScore(treeHeight, downTrees);

                var totalScore = leftScore * rightScore * upScore * downScore;

                if (totalScore > highestScore)
                {
                    highestScore = totalScore;
                }
            }
        }

        return highestScore;
    }

    private int GetScenicScore(int height, int[] view) 
    {
        var score = 0;
        if (view.Length == 0)
        {
            return score;
        }

        foreach (var v in view)
        {
            if (v < height) { score++; }
            else 
            {
                score++;
                return score; 
            }
        }

        return score;
    }

    private int GetVisbleTreeCount(int[,] trees)
    {
        var rowCount = trees.GetLength(0);
        var colCount = trees.GetLength(1);
        var visibleCount = (rowCount * 2) + (colCount * 2) - 4;

        for (int r = 1; r < rowCount - 1; r++)
        {
            var row = GetRow(trees, r);
            for (int c = 1; c < colCount - 1; c++)
            {
                var treeHeight = trees[r, c];
                var leftTrees = row[0..c];
                if (!leftTrees.Any(t => t >= treeHeight)) 
                {
                    visibleCount++;
                    continue;
                }

                var rightTrees = row[(c+1)..colCount];
                if (!rightTrees.Any(t => t >= treeHeight))
                {
                    visibleCount++;
                    continue;
                }

                var col = GetColumn(trees, c);
                var upTrees = col[0..r];
                if (!upTrees.Any(t => t >= treeHeight))
                {
                    visibleCount++;
                    continue;
                }

                var downTrees = col[(r + 1)..rowCount];
                if (!downTrees.Any(t => t >= treeHeight))
                {
                    visibleCount++;
                    continue;
                }
            }
        }

        return visibleCount;
    }

    public int[] GetColumn(int[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public int[] GetRow(int[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }
}


