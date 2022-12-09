using System.Drawing;

namespace AoC_2022;

public class Day_09 : BaseDay
{
    private readonly Move[] _input;

    public Day_09()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(
            GetTailVisitedPoints(
                _input,
                new RopeSection[] {
                new RopeSection(),
                new RopeSection()})
            .ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(
            GetTailVisitedPoints(
                _input, 
                new RopeSection[] {
                new RopeSection(),
                new RopeSection(),
                new RopeSection(),
                new RopeSection(),
                new RopeSection(),
                new RopeSection(),
                new RopeSection(),
                new RopeSection(),
                new RopeSection(),
                new RopeSection()})
            .ToString());
    }

    private Move[] ParseInput()
    {
        return File.ReadAllLines(InputFilePath)
            .Select(l => new Move(
                l.Split(" ").ElementAt(0).ElementAt(0), 
                int.Parse(l.Split(" ").ElementAt(1))))
            .ToArray();
    }

    private record Move(char Direction, int Count);

    private class RopeSection
    {
        public HashSet<(int, int)> Visited { get; set; } = new HashSet<(int, int)>(new (int, int)[] { (0, 0) });

        public int X { get; set; } = 0;

        public int Y { get; set; } = 0;

        public void CalculateMove(RopeSection parentSection)
        {
            if (Touching(parentSection))
            {
                return;
            }

            var moveWithSmallestDistance = (0, 0);
            var smallestDistance = GetDistance(parentSection.X, parentSection.Y, this.X, this.Y);

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    var potentialdistance = GetDistance(parentSection.X, parentSection.Y, this.X + x, this.Y + y);

                    if (potentialdistance < smallestDistance) 
                    {
                        moveWithSmallestDistance = (x, y);
                        smallestDistance = potentialdistance;
                    }
                }
            }

            this.X += moveWithSmallestDistance.Item1;
            this.Y += moveWithSmallestDistance.Item2;

            Visited.Add(new (this.X, this.Y));
        }

        private bool Touching (RopeSection parentSection) 
        {
            var touching = false;

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    var ropeHeadBoundary = (parentSection.X + x, parentSection.Y + y);
                    if (ropeHeadBoundary.Item1 == this.X && ropeHeadBoundary.Item2 == this.Y)
                    {
                        return true;
                    }
                }
            }

            return touching;
        }

        public void Move(char direction)
        {
            switch (direction)
            {
                case 'U':
                    Y++;
                    break;
                case 'R':
                    X++;
                    break;
                case 'D':
                    Y--;
                    break;
                case 'L':
                    X--;
                    break;
                default:
                    throw new NotSupportedException($"direction {direction} not supported");
            }
        }
    }

    private int GetTailVisitedPoints(Move[] moves, RopeSection[] ropeSections) 
    {
        foreach (var m in moves) 
        {
            for (int mc = 0; mc < m.Count; mc++)
            {
                ropeSections[0].Move(m.Direction);
                for (int si = 1; si < ropeSections.Length; si++)
                {
                    ropeSections[si].CalculateMove(ropeSections[si-1]);
                }
            }
        }

        return ropeSections[ropeSections.Length-1].Visited.Count;
    }

    private static double GetDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
    }


}


