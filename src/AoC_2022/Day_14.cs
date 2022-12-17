using MoreLinq;
using System.Data;

namespace AoC_2022;

public class Day_14 : BaseDay
{
    private readonly Map _map;

    public Day_14()
    {
        _map = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        _map.Play();

        Console.WriteLine(_map.Draw());

        var sandAtRest = _map.Pixels.Where(p => p.Type == PixelType.Sand);
        return new(sandAtRest.Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }

    private Map ParseInput()
    {
        var input = File.ReadAllLines(InputFilePath);
        
        var walls = input
            .Select(l => l.Split(" -> "))
            .Select(coord => coord.Select(co => co.Split(",")))
            .Select(s => new Wall(
                s.Select(rnc => new WallSection(
                    int.Parse(rnc.ElementAt(1)),
                    int.Parse(rnc.ElementAt(0)))).ToArray()));

        var minColumn = walls.SelectMany(w => w.sections.Select(s => s.col)).Min();
        var maxColumn = walls.SelectMany(w => w.sections.Select(s => s.col)).Max();
        var minRow = 0;
        var maxRow = walls.SelectMany(w => w.sections.Select(s => s.row)).Max();

        var map = new Map();

        // populate array, minus (min column ) so starts from 0 with empty col at begnning
        var colCount = maxColumn - minColumn + 3;
        var rowCount = maxRow + 1;

        map.Pixels = new Pixel[rowCount, colCount];
        for (int r = 0; r < rowCount; r++)
        {
            for (int c = 0; c < colCount; c++)
            {
                map.Pixels[r, c] = new Pixel() { Row = r, Col = c, Type = PixelType.Air };
            }
        }

        foreach (var wall in walls)
        {
            var previousSection = wall.sections[0];
            for (int i = 1; i < wall.sections.Count(); i++)
            {
                var currentSection = wall.sections[i];

                var rowDiff = Math.Abs(currentSection.row - previousSection.row);
                if (rowDiff > 0)
                {
                    var startingRow = Math.Min(currentSection.row, previousSection.row);
                    for (int r = startingRow; r < startingRow + rowDiff + 1; r++)
                    {
                        map.Pixels[r, previousSection.col - minColumn + 1].Type = PixelType.Wall;
                    }
                }

                var colDiff = Math.Abs(currentSection.col - previousSection.col);
                if (colDiff > 0)
                {
                    var startingCol = Math.Min(currentSection.col, previousSection.col);
                    for (int c = startingCol; c < startingCol + colDiff + 1; c++)
                    {
                        map.Pixels[previousSection.row, c  - minColumn + 1].Type = PixelType.Wall;
                    }
                }

                previousSection = currentSection;
            }
        }

        map.Pixels[0, 500 - minColumn + 1].Type = PixelType.Source;
        map.Source =  new Pixel()
            {
                Row = 0,
                Col = 500 - minColumn + 1,
                Type = PixelType.Source
            };

        return map;
    }

    public record WallSection(int row, int col);

    public record Wall(WallSection[] sections);

    public enum PixelType 
    { 
        Air,
        Wall,
        Sand,
        Source
    }

    private class Pixel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public PixelType Type { get; set; }
    }

    private class Map
    {
        public Pixel[,] Pixels { get; set; } = new Pixel[,] { };

        public Pixel Source { get; set; }

        public Pixel? MovingSand { get; set; }

        public bool MapFilled { get; set; }

        public bool AddSand()
        {
            //var source = Pixels.Where(p => p.Type == PixelType.Source).First();

            var newSand = new Pixel()
            {
                Row = Source.Row,
                Col = Source.Col,
                Type = PixelType.Sand
            };

            if (Pixels[newSand.Row, newSand.Col].Type == PixelType.Sand)
            {
                return false;
            }

            Pixels[newSand.Row, newSand.Col].Type = PixelType.Sand;
            MovingSand = newSand;
            return true;
        }

        public void Play()
        {
            var count = 0;

            while (!MapFilled)
            {
                if (AddSand())
                {
                    FallSand();
                }
                else {
                    MapFilled = true;
                }
                OutputMap(count, 5000);

                count++;
            }
            OutputMap(count, 5000);
        }

        public void FallSand()
        {
            while (MovingSand != null)
            {
                if (MovingSand.Row + 1 >= Pixels.GetLength(0)
                    || MovingSand.Col + 1 >= Pixels.GetLength(1)
                    || MovingSand.Col - 1 < 0)
                {
                    Pixels[MovingSand.Row, MovingSand.Col].Type = PixelType.Air;
                    MovingSand = null;
                    MapFilled = true;
                    return;
                }

                var pixelBelow = Pixels[MovingSand.Row + 1, MovingSand.Col];
                var pixelDiagL = Pixels[MovingSand.Row + 1, MovingSand.Col - 1];
                var pixelDiagR = Pixels[MovingSand.Row + 1, MovingSand.Col + 1];

                if (pixelBelow.Type == PixelType.Air)
                {
                    Pixels[MovingSand.Row, MovingSand.Col].Type = PixelType.Air;
                    pixelBelow.Type = PixelType.Sand;
                    MovingSand = pixelBelow;
                }
                else if (pixelDiagL.Type == PixelType.Air)
                {
                    Pixels[MovingSand.Row, MovingSand.Col].Type = PixelType.Air;
                    pixelDiagL.Type = PixelType.Sand;
                    MovingSand = pixelDiagL;
                }
                else if (pixelDiagR.Type == PixelType.Air)
                {
                    Pixels[MovingSand.Row, MovingSand.Col].Type = PixelType.Air;
                    pixelDiagR.Type = PixelType.Sand;
                    MovingSand = pixelDiagR;
                }
                else
                {
                    MovingSand = null;
                }
            }
        }

        public void OutputMap(int count, int mod)
        {
            if (count % mod == 0)
            {
                Console.Clear();
                Console.WriteLine(Draw());
            }
        }

        public string Draw()
        {
            var output = "";
            var rowCount = Pixels.GetLength(0);
            var colsCount = Pixels.GetLength(1);

            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colsCount; c++)
                {
                    var p = Pixels[r, c];
                    var draw = "";
                    switch (p.Type)
                    {
                        case PixelType.Air:
                            draw = ".";
                            break;
                        case PixelType.Wall:
                            draw = "#";
                            break;
                        case PixelType.Sand:
                            draw = "o";
                            break;
                        case PixelType.Source:
                            draw = "+";
                            break;
                    }
                    output += draw;
                }
                output += Environment.NewLine;
            }

            return output;
        }
    }

    public static IEnumerable<T[]> Filter<T>(T[,] source, Func<T[], bool> predicate)
    {
        for (int i = 0; i < source.GetLength(0); ++i)
        {
            T[] values = new T[source.GetLength(1)];
            for (int j = 0; j < values.Length; ++j)
            {
                values[j] = source[i, j];
            }
            if (predicate(values))
            {
                yield return values;
            }
        }
    }
}


