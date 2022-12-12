using MoreLinq;
using System.Data;

namespace AoC_2022;

public class Day_12 : BaseDay
{
    private readonly char[,] _input;

    public Day_12()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        var rowCount = _input.GetLength(0);
        var colCount = _input.GetLength(1);

        int startingR = 0;
        int startingC = 0;

        for (int r = 0; r < rowCount; r++)
        {
            for (int c = 0; c < colCount; c++)
            {
                var inputVal = _input[r, c];

                if (inputVal == 'S')
                {
                    startingR = r;
                    startingC = c;
                }
            }
        }

        var pathLength = GetShortestPath(startingR, startingC, false);

        return new(pathLength.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var rowCount = _input.GetLength(0);
        var colCount = _input.GetLength(1);

        var paths = new List<int>();
        var totalPoints = rowCount * colCount;
        var totalChecked = 0;

        for (int r = 0; r < rowCount; r++)
        {
            for (int c = 0; c < colCount; c++)
            {
                var inputVal = _input[r, c];

                Console.WriteLine($"{totalChecked} / {totalPoints} checked");

                if (inputVal == 'a')
                {
                    var pathLength = GetShortestPath(r, c, false);
                    if (pathLength > 0)
                    {
                        paths.Add(pathLength);
                        Console.WriteLine($"Lenth added: {pathLength} PathCount: " + paths.Count());
                    }
                }

                totalChecked++;
            }
        }

        return new(paths.Min().ToString());
    }

    private char[,] ParseInput()
    {
        var input = File.ReadAllLines(InputFilePath);

        var rowCount = input.Length;
        var colsCount = input[0].Length;

        char[,] result = new char[rowCount, colsCount];
        for (int r = 0; r < rowCount; r++)
        {
            var row = input[r];
            for (int c = 0; c < colsCount; c++)
            {
                var v = row[c];
                result[r, c] = v;
            }
        }


        return result;
    }

    class Tile
    {
        public int R { get; set; }
        public int C { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + Distance;
        public Tile Parent { get; set; }

        //The distance is essentially the estimated distance, ignoring walls to our target. 
        //So how many tiles left and right, up and down, ignoring walls, to get there. 
        public void SetDistance(int targetR, int targetC)
        {
            this.Distance = Math.Abs(targetR - R) + Math.Abs(targetC - C);
        }
    }

    private static List<Tile> GetWalkableTiles(char[,] map, Tile currentTile, Tile targetTile)
    {
        var possibleTiles = new List<Tile>()
    {
        new Tile { R = currentTile.R, C = currentTile.C - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new Tile { R = currentTile.R, C = currentTile.C + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
        new Tile { R = currentTile.R - 1, C = currentTile.C, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new Tile { R = currentTile.R + 1, C = currentTile.C, Parent = currentTile, Cost = currentTile.Cost + 1 },
    };

        possibleTiles.ForEach(tile => tile.SetDistance(targetTile.R, targetTile.C));

        var maxR = map.GetLength(0) - 1;
        var maxC = map.GetLength(1) - 1;

        var currentTileValue = map[currentTile.R, currentTile.C];

        if (currentTileValue == 'S')
        {
            currentTileValue = 'a'; 
        }

        return possibleTiles
                .Where(tile => tile.R >= 0 && tile.R <= maxR)
                .Where(tile => tile.C >= 0 && tile.C <= maxC)
                .Where(tile => 
                   (map[tile.R, tile.C] - currentTileValue < 2 && map[tile.R, tile.C] != 'E') //height diff
                    || (map[tile.R, tile.C] == 'S')
                    || (currentTileValue == 'z' && map[tile.R, tile.C] == 'E'))
                .ToList();
    }

    private void PrintMap(char[,] map, List<Tile> tiles)
    {
        var rowCount = map.GetLength(0);
        var colCount = map.GetLength(1);

        for (int r = 0; r < rowCount; r++)
        {
            for (int c = 0; c < colCount; c++)
            {
                var tile = tiles.FirstOrDefault(t => t.R == r && t.C == c);

                if (tile != null)
                {
                    Console.Write(tile.Distance);
                }
                else 
                {
                    var inputVal = map[r, c];

                    Console.Write(inputVal);
                }
            }
            Console.WriteLine();
        }
    }

    private int GetShortestPath(int startingR, int startingC, bool printResult)
    { 
        var start = new Tile();
        var finish = new Tile();

        var rowCount = _input.GetLength(0);
        var colCount = _input.GetLength(1);

        for (int r = 0; r < rowCount; r++)
        {
            for (int c = 0; c < colCount; c++)
            {
                var inputVal = _input[r, c];

                if (inputVal == 'E')
                { 
                    finish.R = r;
                    finish.C = c;
                }
            }
        }

        start.R = startingR;
        start.C = startingC;

        start.SetDistance(finish.R, finish.C);

        var activeTiles = new List<Tile>();
        activeTiles.Add(start);
        var visitedTiles = new List<Tile>();

        var foo = 0;

        while (activeTiles.Any())
        {
            var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();
            
            //We found the destination and we can be sure (Because the the OrderBy above)
            //That it's the most low cost option. 
            var tile = checkTile;
            if (checkTile.R == finish.R && checkTile.C == finish.C)
            {
                var inputCopyToModify = (char[,])_input.Clone();

                if (!printResult) 
                {
                    var steps = 0;
                    while (true)
                    {
                        tile = tile.Parent;
                        if (tile == null) 
                        {
                            return steps;
                        }
                        steps++;
                    }
                }

                Console.WriteLine("We are at the destination!");
                Console.WriteLine("Retracing steps backwards...");

                inputCopyToModify[tile.R, tile.C] = '*';
                var stepsCount = 0;
                while (true)
                {
                    Console.WriteLine($"{tile.R} : {tile.C}");
                    // Point parent at current
                    if (tile.Parent != null)
                    {
                        if (tile.Parent.R < tile.R)
                        {
                            inputCopyToModify[tile.Parent.R, tile.Parent.C] = 'v';
                        }
                        if (tile.Parent.R > tile.R)
                        {
                            inputCopyToModify[tile.Parent.R, tile.Parent.C] = '^';
                        }
                        if (tile.Parent.C < tile.C)
                        {
                            inputCopyToModify[tile.Parent.R, tile.Parent.C] = '>';
                        }
                        if (tile.Parent.C > tile.C)
                        {
                            inputCopyToModify[tile.Parent.R, tile.Parent.C] = '<';
                        }
                    }

                    tile = tile.Parent;

                    if (tile == null)
                    {
                        Console.WriteLine("Map looks like :");

                        PrintMap(inputCopyToModify, new List<Tile>());

                        Console.WriteLine("Done!");
                        return stepsCount;
                    }

                    stepsCount++;
                }
              
            }

            visitedTiles.Add(checkTile);
            activeTiles.Remove(checkTile);

            var walkableTiles = GetWalkableTiles(_input, checkTile, finish);

            foreach (var walkableTile in walkableTiles)
            {
                //We have already visited this tile so we don't need to do so again!
                if (visitedTiles.Any(x => x.R == walkableTile.R && x.C == walkableTile.C))
                    continue;

                //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                if (activeTiles.Any(x => x.R == walkableTile.R && x.C == walkableTile.C))
                {
                    var existingTile = activeTiles.First(x => x.R == walkableTile.R && x.C == walkableTile.C);
                    if (existingTile.CostDistance > checkTile.CostDistance)
                    {
                        activeTiles.Remove(existingTile);
                        activeTiles.Add(walkableTile);
                    }
                }
                else
                {
                    //We've never seen this tile before so add it to the list. 
                    activeTiles.Add(walkableTile);
                }
            }

            foo++;

            //Console.WriteLine("Active Tiles: " + foo.ToString());
            //PrintMap(_input, activeTiles);

            //Console.WriteLine("Visited Tiles: " + foo.ToString());
            //PrintMap(_input, visitedTiles);
        }


        Console.WriteLine("No Path Found!");

        return 0;
    }
}

//          a - b   b - a  
// 4 3 y    1       -1
// 4 4 y    0       0
// 4 5 y    -1      1
// 4 6 n    -2      2



