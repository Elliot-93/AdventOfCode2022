//namespace AoC_2022;

//public class Day_11 : BaseDay
//{
//    private readonly Monkey[] _input;

//    public Day_11()
//    {
//        _input = ParseInput();
//    }

//    public override ValueTask<string> Solve_1()
//    {
//        var monkies = _input;

//        for (int r = 0; r < 10000; r++)
//        {
//            for (int i = 0; i < monkies.Length; i++)
//            {
//                var monkey = monkies[i];
//                monkey.ProcessItems(monkies);
//            }
//        }

//        var topTwoInspectors = monkies
//            .OrderByDescending(m => m.InspectCount).Take(2).ToList();

//        var monkeyBusiness = topTwoInspectors[0].InspectCount * topTwoInspectors[1].InspectCount;

//        return new(monkeyBusiness.ToString());
//    }

//    public override ValueTask<string> Solve_2()
//    {
//        return new("".ToString());
//    }

//    private Monkey[] ParseInput()
//    {
//        //return File.ReadAllLines(InputFilePath).ToList();
//        // Test input, expected 10605, no /3 2713310158
//        return new[]
//        {
//            new Monkey(
//               new List<int>(new[] { 79, 98 }),
//                item => item * 19,
//                item => item % 23 == 0 ? 2 : 3),
//             new Monkey(
//               new List<int>(new[] { 54, 65, 75, 74 }),
//                item => item + 6,
//                item => item % 19 == 0 ? 2 : 0),
//             new Monkey(
//               new List<int>(new[] { 79, 60, 97 }),
//                item => item * item,
//                item => item % 13 == 0 ? 1 : 3),
//             new Monkey(
//               new List<int>(new[] { 74 }),
//                item => item + 3,
//                item => item % 17 == 0 ? 0 : 1),
//        };
//        //return new[]
//        //{
//        //    new Monkey(
//        //       new List<int>(new[] { 54, 61, 97, 63, 74 }),
//        //        item => item * 7,
//        //        item => item % 17 == 0 ? 5 : 3),
//        //     new Monkey(
//        //       new List<int>(new[] { 61, 70, 97, 64, 99, 83, 52, 87 }),
//        //        item => item + 8,
//        //        item => item % 2 == 0 ? 7 : 6),
//        //     new Monkey(
//        //       new List<int>(new[] { 60, 67, 80, 65 }),
//        //        item => item * 13,
//        //        item => item % 5 == 0 ? 1 : 6),
//        //     new Monkey(
//        //       new List<int>(new[] { 61, 70, 76, 69, 82, 56 }),
//        //        item => item + 7,
//        //        item => item % 3 == 0 ? 5 : 2),
//        //     new Monkey(
//        //       new List<int>(new[] { 79, 98 }),
//        //        item => item + 2,
//        //        item => item % 7 == 0 ? 0 : 3),
//        //     new Monkey(
//        //       new List<int>(new[] { 72, 79, 55 }),
//        //        item => item + 1,
//        //        item => item % 13 == 0 ? 2 : 1),
//        //     new Monkey(
//        //       new List<int>(new[] { 63 }),
//        //        item => item + 4,
//        //        item => item % 19 == 0 ? 7 : 4),
//        //     new Monkey(
//        //       new List<int>(new[] { 72, 51, 93, 63, 80, 86, 81 }),
//        //        item => item * item,
//        //        item => item % 11 == 0 ? 0 : 4),
//        //};
//    }

//    private class Monkey 
//    {
//        private List<int> items;

//        private Func<int, double> inspectOp;

//        private Func<int, int> throwTest;

//        public int InspectCount { get; set; }

//        public Monkey(List<int> items, Func<int, double> inspectOp, Func<int, int> throwTest)
//        {
//            this.items = items;
//            this.inspectOp = inspectOp;
//            this.throwTest = throwTest;
//        }

//        public void CatchItem(int item) 
//        {
//            items.Add(item);
//        }

//        public void ProcessItems(Monkey[] allMonkeys) 
//        {
//            var monkiesToThrowTo = new List<Monkey>();

//            for (int i = 0; i < items.Count(); i++)
//            {
//                UpdateWorryLevel(i);
//                var monkeyTo = throwTest(items[i]);
//                var monkeyToThrowTo = allMonkeys[monkeyTo];
//                monkiesToThrowTo.Add(monkeyToThrowTo);
//            }


//            var item = items.ToArray();
//            items.Clear();
//            for (int i = 0; i < monkiesToThrowTo.Count; i++)
//            {
//                monkiesToThrowTo[i].CatchItem(item[i]);
//            }
//        }

//        private void UpdateWorryLevel(int itemIndex)
//        { 
//            var newWorry = inspectOp(items[itemIndex]);
//            InspectCount++;
//           // var worryAfterRelief = (int)Math.Floor(newWorry / 3);
//           // items[itemIndex] = worryAfterRelief;
//            items[itemIndex] = (int)Math.Floor(newWorry);
//        }
//    }
//}


// Day 13 
//for (int i = 0; i < leftContents.Length; i++)
//{
//    var leftChar = leftContents[i];
//    var rightChar = rightContents[i];
//    var leftIsDigit = int.TryParse(leftChar.ToString(), out var leftCharNum);
//    var rightIsDigit = int.TryParse(rightChar.ToString(), out var rightCharNum);
//    if (leftIsDigit && rightIsDigit && leftCharNum > rightCharNum)
//    {
//        return false;
//    }
//}

//return true;


//private static IEnumerable<string> Nested(string value)
//{
//    if (string.IsNullOrEmpty(value))
//        yield break; // or throw exception

//    Stack<int> brackets = new Stack<int>();

//    for (int i = 0; i < value.Length; ++i)
//    {
//        char ch = value[i];

//        if (ch == '[')
//            brackets.Push(i);
//        else if (ch == ']')
//        {
//            //TODO: you may want to check if close ']' has corresponding open '['
//            // i.e. stack has values: if (!brackets.Any()) throw ...
//            int openBracket = brackets.Pop();

//            yield return value.Substring(openBracket + 1, i - openBracket - 1);
//        }
//    }

//    //TODO: you may want to check here if there're too many '['
//    // i.e. stack still has values: if (brackets.Any()) throw ... 

//    yield return value;
//}





//using MoreLinq;
//using Newtonsoft.Json.Linq;

//namespace AoC_2022;

//public partial class Day_14 : BaseDay
//{
//    private readonly List<(JArray, JArray)> _input;

//    public Day_14()
//    {
//        _input = ParseInput();
//    }

//    public override ValueTask<string> Solve_1()
//    {
//        var i = 1;
//        var rightOrderSum = 0;
//        foreach (var pair in _input)
//        {
//            var inOrder = PairInCorrectOrder(pair.Item1, pair.Item2);
//            //var inOrder = true; 
//            Console.WriteLine($"Pair {i} Result: {inOrder}");
//            if (inOrder)
//            {
//                rightOrderSum += i;
//            }

//            i++;
//        }

//        return new(rightOrderSum.ToString());
//        // 3008 too low
//        //627 too low 
//    }

//    public override ValueTask<string> Solve_2()
//    {
//        return new("");
//    }

//    private List<(JArray, JArray)> ParseInput()
//    {
//        var pairs = File.ReadAllLines(InputFilePath).Split("");
//        return pairs.Select(p =>
//                (ParseElement(p.ElementAt(0)),
//                ParseElement(p.ElementAt(1))))
//            .ToList();
//    }

//    private JArray ParseElement(string section)
//    {
//        return JArray.Parse(section);
//    }

//    private bool PairInCorrectOrder(JArray left, JArray right)
//    {
//        var maxLen = Math.Max(left.Children().Count(), right.Children().Count());

//        for (int i = 0; i < maxLen; i++)
//        {
//            var lc = left.Children().ElementAtOrDefault(i);
//            var rc = right.Children().ElementAtOrDefault(i);

//            if (lc == null && rc != null)
//            {
//                return true;
//            }

//            if (lc != null && rc == null)
//            {
//                return false;
//            }

//            if (lc?.Type == JTokenType.Integer
//                && rc?.Type == JTokenType.Integer)
//            {
//                var lInt = lc.Value<int>();
//                var rInt = rc.Value<int>();

//                if (lInt < rInt)
//                {
//                    return true;
//                }
//                else if (lInt > rInt)
//                {
//                    return false;
//                }
//            }
//            else if (lc?.Type == JTokenType.Array
//                && rc?.Type == JTokenType.Array)
//            {
//                if (!PairInCorrectOrder(lc.Value<JArray>(), rc.Value<JArray>()))
//                {
//                    return false;
//                }
//            }
//            else if (lc?.Type == JTokenType.Array
//                || rc?.Type == JTokenType.Array)
//            {
//                var lArray = lc?.Type == JTokenType.Array
//                    ? lc.Value<JArray>()
//                    : new JArray(new int[] { lc.Value<int>() });

//                var rArray = rc?.Type == JTokenType.Array
//                    ? rc.Value<JArray>()
//                    : new JArray(new int[] { rc.Value<int>() });

//                if (!PairInCorrectOrder(lArray, rArray))
//                {
//                    return false;
//                }
//            }
//        }

//        return true;
//    }
//}




///Day 14 Part 1 
//using MoreLinq;
//using System.Data;

//namespace AoC_2022;

//public class Day_14 : BaseDay
//{
//    private readonly Map _map;

//    public Day_14()
//    {
//        _map = ParseInput();
//    }

//    public override ValueTask<string> Solve_1()
//    {
//        _map.Play();

//        Console.WriteLine(_map.Draw());

//        var sandAtRest = _map.Pixels.Where(p => p.Type == PixelType.Sand);
//        return new(sandAtRest.Count().ToString());
//    }

//    public override ValueTask<string> Solve_2()
//    {
//        return new("");
//    }

//    private Map ParseInput()
//    {
//        var input = File.ReadAllLines(InputFilePath);

//        var walls = input
//            .Select(l => l.Split(" -> "))
//            .Select(coord => coord.Select(co => co.Split(",")))
//            .Select(s => new Wall(
//                s.Select(rnc => new WallSection(
//                    int.Parse(rnc.ElementAt(1)),
//                    int.Parse(rnc.ElementAt(0)))).ToArray()));

//        var minColumn = walls.SelectMany(w => w.sections.Select(s => s.col)).Min();
//        var maxColumn = walls.SelectMany(w => w.sections.Select(s => s.col)).Max();
//        var minRow = 0;
//        var maxRow = walls.SelectMany(w => w.sections.Select(s => s.row)).Max();

//        var map = new Map();

//        // populate array, minus (min column ) so starts from 0 with empty col at begnning
//        var colCount = maxColumn - minColumn + 3;
//        var rowCount = maxRow + 1;

//        map.Pixels = new Pixel[rowCount, colCount];
//        for (int r = 0; r < rowCount; r++)
//        {
//            for (int c = 0; c < colCount; c++)
//            {
//                map.Pixels[r, c] = new Pixel() { Row = r, Col = c, Type = PixelType.Air };
//            }
//        }

//        foreach (var wall in walls)
//        {
//            var previousSection = wall.sections[0];
//            for (int i = 1; i < wall.sections.Count(); i++)
//            {
//                var currentSection = wall.sections[i];

//                var rowDiff = Math.Abs(currentSection.row - previousSection.row);
//                if (rowDiff > 0)
//                {
//                    var startingRow = Math.Min(currentSection.row, previousSection.row);
//                    for (int r = startingRow; r < startingRow + rowDiff + 1; r++)
//                    {
//                        map.Pixels[r, previousSection.col - minColumn + 1].Type = PixelType.Wall;
//                    }
//                }

//                var colDiff = Math.Abs(currentSection.col - previousSection.col);
//                if (colDiff > 0)
//                {
//                    var startingCol = Math.Min(currentSection.col, previousSection.col);
//                    for (int c = startingCol; c < startingCol + colDiff + 1; c++)
//                    {
//                        map.Pixels[previousSection.row, c - minColumn + 1].Type = PixelType.Wall;
//                    }
//                }

//                previousSection = currentSection;
//            }
//        }

//        map.Pixels[0, 500 - minColumn + 1].Type = PixelType.Source;

//        return map;
//    }

//    public record WallSection(int row, int col);

//    public record Wall(WallSection[] sections);

//    public enum PixelType
//    {
//        Air,
//        Wall,
//        Sand,
//        Source
//    }

//    private class Pixel
//    {
//        public int Row { get; set; }
//        public int Col { get; set; }
//        public PixelType Type { get; set; }
//    }

//    private class Map
//    {
//        public Pixel[,] Pixels { get; set; } = new Pixel[,] { };

//        public Pixel? MovingSand { get; set; }

//        public bool MapFilled { get; set; }

//        public bool AddSand()
//        {
//            var source = Pixels.Where(p => p.Type == PixelType.Source).First();

//            var newSand = new Pixel()
//            {
//                Row = source.Row + 1,
//                Col = source.Col,
//                Type = PixelType.Sand
//            };

//            if (Pixels[newSand.Row, newSand.Col].Type == PixelType.Sand)
//            {
//                return false;
//            }

//            Pixels[newSand.Row, newSand.Col].Type = PixelType.Sand;
//            MovingSand = newSand;
//            return true;
//        }

//        public void Play()
//        {
//            while (!MapFilled)
//            {
//                if (AddSand())
//                {
//                    OutputMap();
//                    FallSand();
//                }
//                else
//                {
//                    OutputMap();
//                    return;
//                }
//            }
//            OutputMap();
//        }

//        public void FallSand()
//        {
//            while (MovingSand != null)
//            {
//                if (MovingSand.Row + 1 >= Pixels.GetLength(0)
//                    || MovingSand.Col + 1 >= Pixels.GetLength(1)
//                    || MovingSand.Col - 1 < 0)
//                {
//                    Pixels[MovingSand.Row, MovingSand.Col].Type = PixelType.Air;
//                    MovingSand = null;
//                    MapFilled = true;
//                    return;
//                }

//                var pixelBelow = Pixels[MovingSand.Row + 1, MovingSand.Col];
//                var pixelDiagL = Pixels[MovingSand.Row + 1, MovingSand.Col - 1];
//                var pixelDiagR = Pixels[MovingSand.Row + 1, MovingSand.Col + 1];

//                if (pixelBelow.Type == PixelType.Air)
//                {
//                    Pixels[MovingSand.Row, MovingSand.Col].Type = PixelType.Air;
//                    pixelBelow.Type = PixelType.Sand;
//                    MovingSand = pixelBelow;
//                }
//                else if (pixelDiagL.Type == PixelType.Air)
//                {
//                    Pixels[MovingSand.Row, MovingSand.Col].Type = PixelType.Air;
//                    pixelDiagL.Type = PixelType.Sand;
//                    MovingSand = pixelDiagL;
//                }
//                else if (pixelDiagR.Type == PixelType.Air)
//                {
//                    Pixels[MovingSand.Row, MovingSand.Col].Type = PixelType.Air;
//                    pixelDiagR.Type = PixelType.Sand;
//                    MovingSand = pixelDiagR;
//                }
//                else
//                {
//                    MovingSand = null;
//                }


//                OutputMap();
//            }
//        }

//        public void OutputMap()
//        {
//            //Thread.Sleep(5);
//            //Console.Clear();
//            //Console.WriteLine(Draw());
//        }

//        public string Draw()
//        {
//            var output = "";
//            var rowCount = Pixels.GetLength(0);
//            var colsCount = Pixels.GetLength(1);

//            for (int r = 0; r < rowCount; r++)
//            {
//                for (int c = 0; c < colsCount; c++)
//                {
//                    var p = Pixels[r, c];
//                    var draw = "";
//                    switch (p.Type)
//                    {
//                        case PixelType.Air:
//                            draw = ".";
//                            break;
//                        case PixelType.Wall:
//                            draw = "#";
//                            break;
//                        case PixelType.Sand:
//                            draw = "o";
//                            break;
//                        case PixelType.Source:
//                            draw = "+";
//                            break;
//                    }
//                    output += draw;
//                }
//                output += Environment.NewLine;
//            }

//            return output;
//        }
//    }

//    public static IEnumerable<T[]> Filter<T>(T[,] source, Func<T[], bool> predicate)
//    {
//        for (int i = 0; i < source.GetLength(0); ++i)
//        {
//            T[] values = new T[source.GetLength(1)];
//            for (int j = 0; j < values.Length; ++j)
//            {
//                values[j] = source[i, j];
//            }
//            if (predicate(values))
//            {
//                yield return values;
//            }
//        }
//    }
//}


/// Day 15
/// 
//using System.Data;
//using System.Text.RegularExpressions;

//namespace AoC_2022;

//public partial class Day_15 : BaseDay
//{
//    private readonly Map _map;

//    public Day_15()
//    {
//        _map = ParseInput();
//    }

//    public override ValueTask<string> Solve_1()
//    {
//        _map.OutputMap();
//        return new("".ToString());
//    }

//    public override ValueTask<string> Solve_2()
//    {
//        return new("");
//    }

//    private Map ParseInput()
//    {
//        var input = File.ReadAllLines(InputFilePath);

//        var matches = input.Select(i => ParseRegex().Match(i));

//        var sensorBeacons = matches.Select(m =>
//            new SensorBeacons()
//            {
//                Sensor = new Pixel()
//                {
//                    Row = int.Parse(m.Groups["sy"].Value),
//                    Col = int.Parse(m.Groups["sx"].Value),
//                    Type = PixelType.Sensor
//                },
//                Beacon = new Pixel()
//                {
//                    Row = int.Parse(m.Groups["by"].Value),
//                    Col = int.Parse(m.Groups["bx"].Value),
//                    Type = PixelType.Beacon
//                }
//            });


//        var minColumn = sensorBeacons.Select(sb => sb.Sensor.Col)
//            .Concat(sensorBeacons.Select(sb => sb.Beacon.Col)).Min();
//        var maxColumn = sensorBeacons.Select(sb => sb.Sensor.Col)
//            .Concat(sensorBeacons.Select(sb => sb.Beacon.Col)).Max();
//        var minRow = sensorBeacons.Select(sb => sb.Sensor.Row)
//            .Concat(sensorBeacons.Select(sb => sb.Beacon.Row)).Min();
//        var maxRow = sensorBeacons.Select(sb => sb.Sensor.Row)
//            .Concat(sensorBeacons.Select(sb => sb.Beacon.Row)).Max();

//        var map = new Map();

//        // populate array, minus (min column ) so starts from 0 with empty col at begnning
//        var colCount = maxColumn - minColumn + 1;
//        var rowCount = maxRow - minRow + 1;

//        map.Pixels = new Pixel[rowCount, colCount];
//        for (int r = 0; r < rowCount; r++)
//        {
//            for (int c = 0; c < colCount; c++)
//            {
//                map.Pixels[r, c] = new Pixel() { Row = r, Col = c, Type = PixelType.Empty };
//            }
//        }

//        foreach (var sb in sensorBeacons)
//        {
//            map.Pixels[sb.Sensor.Row, sb.Sensor.Col] = sb.Sensor;
//            map.Pixels[sb.Beacon.Row, sb.Beacon.Col] = sb.Beacon;
//        }


//        return map;
//    }

//    public enum PixelType
//    {
//        Sensor,
//        Beacon,
//        NoBeacon,
//        Empty
//    }

//    private class Pixel
//    {
//        public int Row { get; set; }
//        public int Col { get; set; }
//        public PixelType Type { get; set; }
//    }

//    private class SensorBeacons
//    {
//        public Pixel Sensor { get; set; }
//        public Pixel Beacon { get; set; }
//    }

//    private static double GetDistance(double x1, double y1, double x2, double y2)
//    {
//        return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
//    }

//    private class Map
//    {
//        public Pixel[,] Pixels { get; set; } = new Pixel[,] { };

//        public void OutputMap()
//        {
//            Console.Clear();
//            Console.WriteLine(Draw());
//        }

//        public string Draw()
//        {
//            var output = "";
//            var rowCount = Pixels.GetLength(0);
//            var colsCount = Pixels.GetLength(1);

//            for (int r = 0; r < rowCount; r++)
//            {
//                for (int c = 0; c < colsCount; c++)
//                {
//                    var p = Pixels[r, c];
//                    var draw = "";
//                    switch (p.Type)
//                    {
//                        case PixelType.Sensor:
//                            draw = "S";
//                            break;
//                        case PixelType.Beacon:
//                            draw = "B";
//                            break;
//                        case PixelType.NoBeacon:
//                            draw = "#";
//                            break;
//                        case PixelType.Empty:
//                            draw = ".";
//                            break;
//                    }
//                    output += draw;
//                }
//                output += Environment.NewLine;
//            }

//            return output;
//        }
//    }

//    public static IEnumerable<T[]> Filter<T>(T[,] source, Func<T[], bool> predicate)
//    {
//        for (int i = 0; i < source.GetLength(0); ++i)
//        {
//            T[] values = new T[source.GetLength(1)];
//            for (int j = 0; j < values.Length; ++j)
//            {
//                values[j] = source[i, j];
//            }
//            if (predicate(values))
//            {
//                yield return values;
//            }
//        }
//    }

//    [GeneratedRegex("Sensor at x=(?<sx>[-]?\\d+), y=(?<sy>[-]?\\d+): closest beacon is at x=(?<bx>[-]?\\d+), y=(?<by>[-]?\\d+)", RegexOptions.Compiled)]
//    private static partial Regex ParseRegex();
//}





//using System.Data;
//using System.Text.RegularExpressions;

//namespace AoC_2022;

//public partial class Day_15 : BaseDay
//{
//    public Day_15()
//    {
//    }

//    public override ValueTask<string> Solve_1()
//    {
//        var lineToCheck = 2000000;
//        //var lineToCheck = 10;

//        var map = ParseInput(lineToCheck);
//        map.SensorBeacons.ForEach(sb => map.MarkNoBeaconLocations(sb, lineToCheck));

//        //var map = ParseInput();
//        //map.SensorBeacons.ForEach(sb => map.MarkNoBeaconLocations(sb));

//        map.OutputMap(false);

//        var y10NoBeaconCount = map.Pixels[lineToCheck].Values.Count(t => t.Type == PixelType.NoBeacon);

//        Console.WriteLine("One answer: " + y10NoBeaconCount.ToString());

//        return new(y10NoBeaconCount.ToString());
//    }

//    public override ValueTask<string> Solve_2()
//    {
//        // Get line around all beacon radiuses and get intersection and get 1 with 4 intersections
//        var map = ParseInput(0, 4000000);
//        var count = 0;
//        foreach (var sb in map.SensorBeacons)
//        {
//            Console.WriteLine($"Checking {count}/{map.SensorBeacons.Count()}");
//            map.MarkNoBeaconPerimeter(sb);

//            Console.WriteLine($"Total permim count {map.Pixels.Values.Sum(v => v.Count())}");

//            count++;
//        }

//        // map.OutputMap(false);

//        var coord = getIntersectionCoords(map);

//        return new($"{coord.X},{coord.Y}. {(coord.X * 4000000) + (coord.Y)}");
//    }

//    private static Coord getIntersectionCoords(Map map)
//    {
//        var intersectionCoords = new List<Coord>();
//        foreach (var row in map.Pixels)
//        {
//            foreach (var col in map.Pixels[row.Key])
//            {
//                if (col.Value.IntersectionCount > 3)
//                {
//                    intersectionCoords.Add(new Coord(col.Key, row.Key));
//                }
//            }
//        }

//        Console.WriteLine($"Intersection count: {intersectionCoords.Count()}");

//        //Check not within any sensor boundary
//        foreach (var icoord in intersectionCoords)
//        {
//            Console.WriteLine($"Checking coord {icoord.X}/{icoord.Y}");
//            if (map.SensorBeacons.All(sb =>
//                ManhattanDistance(icoord.X, icoord.Y, sb.Sensor.X, sb.Sensor.Y) > sb.GetManhattanDistance()))
//            {
//                return icoord;
//            }
//        }


//        return new Coord(0, 0);
//    }

//    private Map ParseInput(int filterToLine)
//    {
//        var input = File.ReadAllLines(InputFilePath);

//        var matches = input.Select(i => ParseRegex().Match(i));

//        var sensorBeacons = matches.Select(m =>
//            new SensorBeacon()
//            {
//                Sensor = new Coord(
//                       int.Parse(m.Groups["sx"].Value),
//                       int.Parse(m.Groups["sy"].Value)),
//                Beacon = new Coord(
//                       int.Parse(m.Groups["bx"].Value),
//                       int.Parse(m.Groups["by"].Value))
//            }).ToList();

//        sensorBeacons = sensorBeacons
//            .Where(sb =>
//                sb.Sensor.Y + sb.GetManhattanDistance() >= filterToLine
//                && sb.Sensor.Y - sb.GetManhattanDistance() <= filterToLine).ToList();

//        var map = new Map();

//        map.SensorBeacons = sensorBeacons;

//        return map;
//    }

//    private Map ParseInput(int minRange, int MaxRange)
//    {
//        var input = File.ReadAllLines(InputFilePath);

//        var matches = input.Select(i => ParseRegex().Match(i));

//        var sensorBeacons = matches.Select(m =>
//            new SensorBeacon()
//            {
//                Sensor = new Coord(
//                       int.Parse(m.Groups["sx"].Value),
//                       int.Parse(m.Groups["sy"].Value)),
//                Beacon = new Coord(
//                       int.Parse(m.Groups["bx"].Value),
//                       int.Parse(m.Groups["by"].Value))
//            }).ToList();

//        sensorBeacons = sensorBeacons
//            .Where(sb =>
//                (sb.Sensor.Y + sb.GetManhattanDistance() >= minRange
//                && sb.Sensor.Y + sb.GetManhattanDistance() <= MaxRange)
//                || (sb.Sensor.Y - sb.GetManhattanDistance() >= minRange
//                && sb.Sensor.Y - sb.GetManhattanDistance() <= MaxRange)
//                || (sb.Sensor.X + sb.GetManhattanDistance() >= minRange
//                && sb.Sensor.X + sb.GetManhattanDistance() <= MaxRange)
//                || (sb.Sensor.X - sb.GetManhattanDistance() >= minRange
//                && sb.Sensor.X - sb.GetManhattanDistance() <= MaxRange))
//            .ToList();

//        var map = new Map();

//        map.SensorBeacons = sensorBeacons;

//        return map;
//    }

//    public enum PixelType
//    {
//        Sensor,
//        Beacon,
//        NoBeacon,
//        NoBeaconPerim,
//        Empty
//    }

//    public class Pixel
//    {
//        public PixelType Type { get; set; }
//        public int IntersectionCount { get; set; }

//        public Pixel(PixelType type)
//        {
//            Type = type;
//        }
//    }

//    private class Coord
//    {
//        public int X { get; set; }
//        public int Y { get; set; }

//        public Coord(int x, int y)
//        {
//            X = x;
//            Y = y;
//        }
//    }

//    private class SensorBeacon
//    {
//        public Coord Sensor { get; set; }
//        public Coord Beacon { get; set; }

//        private int? _manhattanDistance;

//        public int GetManhattanDistance()
//        {
//            if (_manhattanDistance != null)
//            {
//                return _manhattanDistance.Value;
//            }

//            _manhattanDistance = ManhattanDistance(
//            Sensor.X,
//            Sensor.Y,
//            Beacon.X,
//            Beacon.Y);

//            return _manhattanDistance.Value;
//        }
//    }

//    public static int ManhattanDistance(double x1, double y1, double x2, double y2)
//    {
//        return (int)(Math.Abs(x1 - x2) + Math.Abs(y1 - y2));
//    }

//    private class Map
//    {
//        public int MinRow { get; set; }
//        public int MaxRow { get; set; }
//        public int MinCol { get; set; }
//        public int MaxCol { get; set; }

//        // outerkeys rows (y) inner cols (x)
//        public Dictionary<int, Dictionary<int, Pixel>> Pixels { get; set; }
//            = new Dictionary<int, Dictionary<int, Pixel>>();

//        public List<SensorBeacon> SensorBeacons { get; set; } = new List<SensorBeacon>();

//        public void OutputMap(bool draw)
//        {
//            //Console.Clear();
//            Draw(draw);
//        }

//        public string Draw(bool draw)
//        {
//            var output = "";
//            var maxCol = Math.Min(Pixels.Values.Select(y => y.Keys.Max()).Max(), 4000000);
//            var maxRow = Math.Min(Pixels.Keys.Max(), 4000000);
//            var minCol = Math.Max(Pixels.Values.Select(y => y.Keys.Min()).Min(), 0);
//            var minRow = Math.Max(Pixels.Keys.Min(), 0);

//            Console.WriteLine($"MinCol: {minCol}, MaxCol: {maxCol}");

//            for (int r = minRow; r < maxRow + 1; r++)
//            {
//                if (!Pixels.TryGetValue(r, out var row))
//                {
//                    Pixels[r] = new Dictionary<int, Pixel>();
//                    row = Pixels[r];
//                }

//                if (draw)
//                {
//                    Console.Write(r.ToString() + "\t");
//                }

//                for (int c = minCol; c < maxCol + 1; c++)
//                {
//                    if (!row.TryGetValue(c, out var pixel))
//                    {
//                        row[c] = new Pixel(PixelType.Empty);
//                    }
//                    if (draw)
//                    {
//                        switch (row[c].Type)
//                        {
//                            case PixelType.Sensor:
//                                Console.Write("S");
//                                break;
//                            case PixelType.Beacon:
//                                Console.Write("B");
//                                break;
//                            case PixelType.NoBeacon:
//                                Console.Write("#");
//                                break;
//                            case PixelType.NoBeaconPerim:
//                                Console.ForegroundColor = (ConsoleColor)row[c].IntersectionCount;
//                                Console.Write("@");
//                                Console.ResetColor();
//                                break;
//                            case PixelType.Empty:
//                                Console.Write(".");
//                                break;
//                        }
//                    }
//                }
//                if (draw)
//                {
//                    Console.WriteLine();
//                }
//            }

//            return output;
//        }

//        public void MarkNoBeaconPerimeter(SensorBeacon sb)
//        {
//            var beaconDistance = sb.GetManhattanDistance();

//            Console.WriteLine($"Sensor at {sb.Sensor.X},{sb.Sensor.Y}, distance {beaconDistance} processing");

//            var topPointRow = sb.Sensor.Y - beaconDistance - 1;
//            if (!Pixels.TryGetValue(topPointRow, out var topRow))
//            {
//                Pixels[topPointRow] = new Dictionary<int, Pixel>();
//                topRow = Pixels[topPointRow];
//            }
//            addOrUpdateNoBeaconPerim(topRow, sb.Sensor.X);

//            var perimCount = 1;
//            for (var r = Math.Max(sb.Sensor.Y - beaconDistance, 0); r <= Math.Min(sb.Sensor.Y, 4000000); r++)
//            {
//                if (!Pixels.TryGetValue(r, out var row))
//                {
//                    Pixels[r] = new Dictionary<int, Pixel>();
//                    row = Pixels[r];
//                }

//                var left = sb.Sensor.X - perimCount;
//                if (left < 0)
//                {
//                    continue;
//                }

//                var right = sb.Sensor.X + perimCount;
//                if (left > 4000000)
//                {
//                    continue;
//                }

//                addOrUpdateNoBeaconPerim(row, left);
//                addOrUpdateNoBeaconPerim(row, right);

//                perimCount++;
//            }

//            perimCount = beaconDistance;
//            for (var r = Math.Max(sb.Sensor.Y + 1, 0); r <= Math.Min(sb.Sensor.Y + beaconDistance, 4000000); r++)
//            {
//                if (!Pixels.TryGetValue(r, out var row))
//                {
//                    Pixels[r] = new Dictionary<int, Pixel>();
//                    row = Pixels[r];
//                }

//                var left = sb.Sensor.X - perimCount;
//                var right = sb.Sensor.X + perimCount;
//                addOrUpdateNoBeaconPerim(row, left);
//                addOrUpdateNoBeaconPerim(row, right);
//                perimCount--;
//            }

//            var bottomPointRow = sb.Sensor.Y + beaconDistance + 1;
//            if (!Pixels.TryGetValue(bottomPointRow, out var bottomRow))
//            {
//                Pixels[bottomPointRow] = new Dictionary<int, Pixel>();
//                bottomRow = Pixels[bottomPointRow];
//            }

//            addOrUpdateNoBeaconPerim(bottomRow, sb.Sensor.X);
//        }

//        private static void addOrUpdateNoBeaconPerim(Dictionary<int, Pixel> row, int col)
//        {
//            if (row.TryGetValue(col, out var pixel))
//            {
//                if (pixel.Type == PixelType.NoBeaconPerim)
//                {
//                    pixel.IntersectionCount++;
//                }
//            }
//            else
//            {
//                row[col] = new Pixel(PixelType.NoBeaconPerim);
//            }
//        }

//        public void MarkNoBeaconLocations(SensorBeacon sb, int lineToCheck)
//        {
//            var beaconDistance = sb.GetManhattanDistance();

//            Console.WriteLine($"Sensor at {sb.Sensor.X},{sb.Sensor.Y}, distance {beaconDistance} starting");

//            if (!Pixels.TryGetValue(lineToCheck, out var row))
//            {
//                Pixels[lineToCheck] = new Dictionary<int, Pixel>();
//                row = Pixels[lineToCheck];
//            }
//            for (var c = sb.Sensor.X - beaconDistance; c <= sb.Sensor.X + beaconDistance; c++)
//            {
//                if (row.TryGetValue(c, out var pixel))
//                {
//                    if (pixel.Type != PixelType.Empty)
//                    {
//                        continue;
//                    }
//                }

//                if (ManhattanDistance(sb.Sensor.X, sb.Sensor.Y, c, lineToCheck) <= beaconDistance)
//                {
//                    row[c] = new Pixel(PixelType.NoBeacon);
//                }
//            }

//            if (sb.Beacon.Y == lineToCheck)
//            {
//                Pixels[sb.Beacon.Y][sb.Beacon.X] = new Pixel(PixelType.Beacon);
//            }
//            if (sb.Sensor.Y == lineToCheck)
//            {
//                Pixels[sb.Sensor.Y][sb.Sensor.X] = new Pixel(PixelType.Sensor);
//            }
//        }

//        public void MarkNoBeaconLocations(SensorBeacon sb)
//        {
//            var beaconDistance = sb.GetManhattanDistance();

//            Console.WriteLine($"Sensor at {sb.Sensor.X},{sb.Sensor.Y}, distance {beaconDistance} starting");

//            for (var r = sb.Sensor.Y - beaconDistance; r <= sb.Sensor.Y + beaconDistance; r++)
//            {
//                if (!Pixels.TryGetValue(r, out var row))
//                {
//                    Pixels[r] = new Dictionary<int, Pixel>();
//                    row = Pixels[r];
//                }

//                for (var c = sb.Sensor.X - beaconDistance; c <= sb.Sensor.X + beaconDistance; c++)
//                {
//                    if (!row.TryGetValue(c, out var pixel))
//                    {
//                        if (pixel == null)
//                        {
//                            row[c] = new Pixel(PixelType.Empty);
//                        }
//                    }

//                    if (ManhattanDistance(sb.Sensor.X, sb.Sensor.Y, c, r) <= beaconDistance)
//                    {
//                        row[c] = new Pixel(PixelType.NoBeacon);
//                    }

//                    if (sb.Beacon.Y == r && sb.Beacon.X == c)
//                    {
//                        Pixels[sb.Beacon.Y][sb.Beacon.X] = new Pixel(PixelType.Beacon);
//                    }
//                    if (sb.Sensor.Y == r && sb.Sensor.X == c)
//                    {
//                        Pixels[sb.Sensor.Y][sb.Sensor.X] = new Pixel(PixelType.Sensor);
//                    }
//                }
//            }
//        }
//    }

//    [GeneratedRegex("Sensor at x=(?<sx>[-]?\\d+), y=(?<sy>[-]?\\d+): closest beacon is at x=(?<bx>[-]?\\d+), y=(?<by>[-]?\\d+)", RegexOptions.Compiled)]
//    private static partial Regex ParseRegex();
//}




