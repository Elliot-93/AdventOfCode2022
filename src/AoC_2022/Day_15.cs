using System.Data;
using System.Text.RegularExpressions;

namespace AoC_2022;

public partial class Day_15 : BaseDay
{
    public Day_15()
    {
    }

    public override ValueTask<string> Solve_1()
    {
        var lineToCheck = 2000000;
        //var lineToCheck = 10;

        var map = ParseInput(lineToCheck);
        map.SensorBeacons.ForEach(sb => map.MarkNoBeaconLocations(sb, lineToCheck));

        //var map = ParseInput();
        //map.SensorBeacons.ForEach(sb => map.MarkNoBeaconLocations(sb));

        //map.OutputMap(true);

        var y10NoBeaconCount = map.Pixels[lineToCheck].Values.Count(t => t.Type == PixelType.NoBeacon);

        Console.WriteLine("One answer: " + y10NoBeaconCount.ToString());

        return new(y10NoBeaconCount.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        // Get line around all beacon radiuses and get intersection and get 1 with 4 intersections
        int minBoundary, maxBoundary;
        minBoundary = int.MinValue;
        maxBoundary = int.MaxValue;
        //minBoundary = 0;
        //maxBoundary = 4000000;


        var map = ParseInput(minBoundary, maxBoundary);
        var count = 0;
        foreach (var sb in map.SensorBeacons)
        {
            Console.WriteLine($"Checking {count}/{map.SensorBeacons.Count()}");
            map.MarkNoBeaconPerimeter(sb, minBoundary, maxBoundary);

            Console.WriteLine($"Total permim count {map.Pixels.Values.Sum(v => v.Count())}");

            count++;
        }

        //map.OutputMap(true);

        var coord = getIntersectionCoords(map);

        return new($"{coord.X},{coord.Y}. {(coord.X * 4000000) +(coord.Y)}");
    }

    private static Coord getIntersectionCoords(Map map)
    {
        var intersectionCoords = new List<Coord>();
        foreach (var row in map.Pixels)
        {
            foreach (var col in map.Pixels[row.Key])
            {
                if (col.Value.IntersectionCount > 3 )
                {
                    intersectionCoords.Add(new Coord(col.Key, row.Key));
                }
            }
        }

        Console.WriteLine($"Intersection count: {intersectionCoords.Count()}");

        //Check not within any sensor boundary
        foreach (var icoord in intersectionCoords)
        {
            Console.WriteLine($"Checking coord {icoord.X}/{icoord.Y}");
            if (map.SensorBeacons.All(sb => 
                ManhattanDistance(icoord.X, icoord.Y, sb.Sensor.X, sb.Sensor.Y) > sb.GetManhattanDistance()))
            {
                return icoord;
            }
        }


        return new Coord(0, 0);
    }

    private Map ParseInput(int filterToLine)
    {
        var input = File.ReadAllLines(InputFilePath);

        var matches = input.Select(i => ParseRegex().Match(i));

        var sensorBeacons = matches.Select(m => 
            new SensorBeacon() 
            { 
               Sensor = new Coord(
                       int.Parse(m.Groups["sx"].Value), 
                       int.Parse(m.Groups["sy"].Value)), 
               Beacon = new Coord(
                       int.Parse(m.Groups["bx"].Value),
                       int.Parse(m.Groups["by"].Value))
            }).ToList();

        sensorBeacons = sensorBeacons
            .Where(sb =>
                sb.Sensor.Y + sb.GetManhattanDistance() >= filterToLine
                && sb.Sensor.Y - sb.GetManhattanDistance() <= filterToLine).ToList();

        var map = new Map();

        map.SensorBeacons = sensorBeacons;

        return map;
    }

    private Map ParseInput(int minRange, int MaxRange)
    {
        var input = File.ReadAllLines(InputFilePath);

        var matches = input.Select(i => ParseRegex().Match(i));

        var sensorBeacons = matches.Select(m =>
            new SensorBeacon()
            {
                Sensor = new Coord(
                       int.Parse(m.Groups["sx"].Value),
                       int.Parse(m.Groups["sy"].Value)),
                Beacon = new Coord(
                       int.Parse(m.Groups["bx"].Value),
                       int.Parse(m.Groups["by"].Value))
            }).ToList();

        sensorBeacons = sensorBeacons
            .Where(sb =>
                (sb.Sensor.Y + sb.GetManhattanDistance() >= minRange
                && sb.Sensor.Y + sb.GetManhattanDistance() <= MaxRange)
                || (sb.Sensor.Y - sb.GetManhattanDistance() >= minRange
                && sb.Sensor.Y - sb.GetManhattanDistance() <= MaxRange)
                || (sb.Sensor.X + sb.GetManhattanDistance() >= minRange
                && sb.Sensor.X + sb.GetManhattanDistance() <= MaxRange)
                || (sb.Sensor.X - sb.GetManhattanDistance() >= minRange
                && sb.Sensor.X - sb.GetManhattanDistance() <= MaxRange))
            .ToList();

        var map = new Map();

        map.SensorBeacons = sensorBeacons;

        return map;
    }

    public enum PixelType 
    { 
        Sensor,
        Beacon,
        NoBeacon,
        NoBeaconPerim,
        Empty
    }

    public class Pixel
    {
        public PixelType Type { get; set; }
        public int IntersectionCount { get; set; }

        public Pixel(PixelType type)
        {
            Type = type;
        }
    }

    private class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    private class SensorBeacon
    {
        public Coord Sensor { get; set; }
        public Coord Beacon { get; set; }

        private int? _manhattanDistance;

        public int GetManhattanDistance()
        {
            if (_manhattanDistance != null)
            {
                return _manhattanDistance.Value;
            }

            _manhattanDistance = ManhattanDistance(
            Sensor.X,
            Sensor.Y,
            Beacon.X,
            Beacon.Y);

            return _manhattanDistance.Value;
        }
    }

    public static int ManhattanDistance(double x1, double y1, double x2, double y2)
    {
        return (int)(Math.Abs(x1 - x2) + Math.Abs(y1 - y2));
    }

    private class Map
    {
        public int MinRow { get; set; }
        public int MaxRow { get; set; }
        public int MinCol { get; set; }
        public int MaxCol { get; set; }

        // outerkeys rows (y) inner cols (x)
        public Dictionary<int, Dictionary<int, Pixel>> Pixels { get; set; } 
            = new Dictionary<int, Dictionary<int, Pixel>>();

        public List<SensorBeacon> SensorBeacons { get; set; } = new List<SensorBeacon>();

        public void OutputMap(bool draw)
        {
            //Console.Clear();
            Draw(draw);
        }

        public string Draw(bool draw)
        {
            var output = "";
            var maxCol = Pixels.Values.Select(c => c.Keys.Any() ? c.Keys.Max() : 0).Max();
            var maxRow = Pixels.Keys.Max();
            var minCol = Pixels.Values.Select(c => c.Keys.Any() ? c.Keys.Min() : 0).Min();
            var minRow = Pixels.Keys.Min();

            Console.WriteLine($"MinCol: {minCol}, MaxCol: {maxCol}");

            for (int r = minRow; r < maxRow + 1; r++)
            {
                if (!Pixels.TryGetValue(r, out var row))
                {
                    Pixels[r] = new Dictionary<int, Pixel>();
                    row = Pixels[r];
                }

                if (draw)
                {
                    Console.Write(r.ToString() + "\t");
                }

                for (int c = minCol ; c < maxCol + 1; c++)
                {
                    if (!row.TryGetValue(c, out var pixel))
                    {
                        row[c] = new Pixel(PixelType.Empty);
                    }
                    if (draw)
                    {
                        switch (row[c].Type)
                        {
                            case PixelType.Sensor:
                               Console.Write("S");
                                break;
                            case PixelType.Beacon:
                                Console.Write("B");
                                break;
                            case PixelType.NoBeacon:
                                Console.Write("#");
                                break;
                            case PixelType.NoBeaconPerim:
                                Console.ForegroundColor = (ConsoleColor)row[c].IntersectionCount;
                                Console.Write("@");
                                Console.ResetColor();
                                break;
                            case PixelType.Empty:
                                Console.Write(".");
                                break;
                        }
                    }
                }
                if(draw)
                {
                    Console.WriteLine();
                }
            }

            return output;
        }

        public void MarkNoBeaconPerimeter(SensorBeacon sb, int minBoundary, int maxBoundary)
        {
            var beaconDistance = sb.GetManhattanDistance();

            Console.WriteLine($"Sensor at {sb.Sensor.X},{sb.Sensor.Y}, distance {beaconDistance} processing");

            var topPointRow = sb.Sensor.Y - beaconDistance - 1;
            if (topPointRow > minBoundary && topPointRow < maxBoundary)
            {
                if (!Pixels.TryGetValue(topPointRow, out var topRow))
                {
                    Pixels[topPointRow] = new Dictionary<int, Pixel>();
                    topRow = Pixels[topPointRow];
                }
                addOrUpdateNoBeaconPerim(topRow, sb.Sensor.X);
            }

            var perimCount = 1;
            for (var r = sb.Sensor.Y - beaconDistance; r <= sb.Sensor.Y; r++)
            {
                if (!Pixels.TryGetValue(r, out var row))
                {
                    Pixels[r] = new Dictionary<int, Pixel>();
                    row = Pixels[r];
                }

                var left = sb.Sensor.X - perimCount;
                if (left < minBoundary || left > maxBoundary)
                {
                    perimCount++;
                    continue;
                }

                addOrUpdateNoBeaconPerim(row, left);

                var right = sb.Sensor.X + perimCount;
                if (right < minBoundary || right > maxBoundary)
                {
                    perimCount++;
                    continue;
                }
                addOrUpdateNoBeaconPerim(row, right);

                perimCount++;
            }

            perimCount=beaconDistance;
            for (var r = sb.Sensor.Y + 1; r <= sb.Sensor.Y + beaconDistance; r++)
            {
                if (!Pixels.TryGetValue(r, out var row))
                {
                    Pixels[r] = new Dictionary<int, Pixel>();
                    row = Pixels[r];
                }

                var left = sb.Sensor.X - perimCount;
                if (left < minBoundary || left > maxBoundary)
                {
                    perimCount--;
                    continue;
                }
                addOrUpdateNoBeaconPerim(row, left);

                var right = sb.Sensor.X + perimCount;
                if (right < minBoundary || right > maxBoundary)
                {
                    perimCount--;
                    continue;
                }
                addOrUpdateNoBeaconPerim(row, right);
                perimCount--;
            }

            var bottomPointRow = sb.Sensor.Y + beaconDistance + 1;
            if (bottomPointRow < minBoundary || bottomPointRow > maxBoundary)
            {
                return;
            }
            if (!Pixels.TryGetValue(bottomPointRow, out var bottomRow))
            {
                Pixels[bottomPointRow] = new Dictionary<int, Pixel>();
                bottomRow = Pixels[bottomPointRow];
            }

            addOrUpdateNoBeaconPerim(bottomRow, sb.Sensor.X);
        }

        private static void addOrUpdateNoBeaconPerim(Dictionary<int, Pixel> row, int col)
        {
            if (row.TryGetValue(col, out var pixel))
            {
                if (pixel.Type == PixelType.NoBeaconPerim)
                {
                    pixel.IntersectionCount++;
                }
            }
            else
            {
                row[col] = new Pixel(PixelType.NoBeaconPerim);
            }
        }

        public void MarkNoBeaconLocations(SensorBeacon sb, int lineToCheck)
        {
            var beaconDistance = sb.GetManhattanDistance();

            Console.WriteLine($"Sensor at {sb.Sensor.X},{sb.Sensor.Y}, distance {beaconDistance} starting");

            if (!Pixels.TryGetValue(lineToCheck, out var row))
            {
                Pixels[lineToCheck] = new Dictionary<int, Pixel>();
                row = Pixels[lineToCheck];
            }
            for (var c = sb.Sensor.X - beaconDistance; c <= sb.Sensor.X + beaconDistance; c++)
            {
                if (row.TryGetValue(c, out var pixel))
                {
                    if (pixel.Type != PixelType.Empty)
                    {
                        continue;
                    }
                }

                if (ManhattanDistance(sb.Sensor.X, sb.Sensor.Y, c, lineToCheck) <= beaconDistance)
                {
                    row[c] = new Pixel(PixelType.NoBeacon);
                }
            }

            if (sb.Beacon.Y == lineToCheck)
            {
                Pixels[sb.Beacon.Y][sb.Beacon.X] = new Pixel(PixelType.Beacon);
            }
            if (sb.Sensor.Y == lineToCheck)
            {
                Pixels[sb.Sensor.Y][sb.Sensor.X] = new Pixel(PixelType.Sensor);
            }
        }

        public void MarkNoBeaconLocations(SensorBeacon sb)
        {
            var beaconDistance = sb.GetManhattanDistance();

            Console.WriteLine($"Sensor at {sb.Sensor.X},{sb.Sensor.Y}, distance {beaconDistance} starting");

            for (var r = sb.Sensor.Y - beaconDistance; r <= sb.Sensor.Y + beaconDistance; r++)
            {
                if (!Pixels.TryGetValue(r, out var row))
                {
                    Pixels[r] = new Dictionary<int, Pixel>();
                    row = Pixels[r];
                }

                for (var c = sb.Sensor.X - beaconDistance; c <= sb.Sensor.X + beaconDistance; c++)
                {
                    if (!row.TryGetValue(c, out var pixel))
                    {
                        if (pixel == null)
                        {
                            row[c] = new Pixel(PixelType.Empty);
                        }
                    }

                    if (ManhattanDistance(sb.Sensor.X, sb.Sensor.Y, c, r) <= beaconDistance)
                    {
                        row[c] = new Pixel(PixelType.NoBeacon);
                    }

                    if (sb.Beacon.Y == r && sb.Beacon.X == c)
                    {
                        Pixels[sb.Beacon.Y][sb.Beacon.X] = new Pixel(PixelType.Beacon);
                    }
                    if (sb.Sensor.Y == r && sb.Sensor.X == c)
                    {
                        Pixels[sb.Sensor.Y][sb.Sensor.X] = new Pixel(PixelType.Sensor);
                    }
                }
            }
        }
    }

    [GeneratedRegex("Sensor at x=(?<sx>[-]?\\d+), y=(?<sy>[-]?\\d+): closest beacon is at x=(?<bx>[-]?\\d+), y=(?<by>[-]?\\d+)", RegexOptions.Compiled)]
    private static partial Regex ParseRegex();
}


