using System.Data;
using System.Numerics;

namespace AoC_2022;

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




public class Day_11 : BaseDay
{
    private readonly Monkey[] _input;

    public Day_11()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
       // var allDivisors = new int[] { 23, 19, 13, 17 };
        var allDivisors = new int[] { 2, 3, 5, 7, 11, 19, 13, 17 };
        int prod = 1;

        foreach (int value in allDivisors)
        {
            prod *= value;
        }

        var monkies = _input;

        checked
        {
            for (int r = 1; r <= 10000; r++)
            {
                var inspectCounts = new List<int>();

                for (int i = 0; i < monkies.Length; i++)
                {
                    var monkey = monkies[i];
                    monkey.ReduceWorry(prod);
                    monkey.ProcessItems(monkies);
                    inspectCounts.Add(monkey.InspectCount);
                }

                if (r % 20 == 0)
                {
                    Console.WriteLine("After Round " + r);
                    for (int x = 0; x < inspectCounts.Count(); x++)
                    {
                        Console.WriteLine($"Monkey {x} inspect items {inspectCounts[x]} times");
                    }
                    var maxItemSize = monkies.SelectMany(m => m.GetAllItems()).Max();
                    Console.WriteLine($"Max item size: {maxItemSize}");
                }


            }
        }

        var topTwoInspectors = monkies
            .OrderByDescending(m => m.InspectCount).Take(2).ToList();

        var monkeyBusiness = (BigInteger) topTwoInspectors[0].InspectCount * (BigInteger) topTwoInspectors[1].InspectCount;

        return new(monkeyBusiness.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("".ToString());
    }

    private Monkey[] ParseInput()
    {
        //return File.ReadAllLines(InputFilePath).ToList();
        // Test input, expected 10605, no /3 2713310158
        //return new[]
        //{
        //    new Monkey(
        //       new List<BigInteger>(new BigInteger[] { 79, 98 }),
        //        item => item * 19,
        //        item => item % 23 == 0 ? 2 : 3),
        //     new Monkey(
        //       new List<BigInteger>(new BigInteger[] { 54, 65, 75, 74 }),
        //        item => item + 6,
        //        item => item % 19 == 0 ? 2 : 0),
        //     new Monkey(
        //       new List<BigInteger>(new BigInteger[] { 79, 60, 97 }),
        //        item => item * item,
        //        item => item % 13 == 0 ? 1 : 3),
        //     new Monkey(
        //       new List<BigInteger>(new BigInteger[] { 74 }),
        //        item => item + 3,
        //        item => item % 17 == 0 ? 0 : 1),
        //};
        return new[]
        {
            new Monkey(
               new List<BigInteger>(new BigInteger[] { 54, 61, 97, 63, 74 }),
                item => item* 7,
                item => item % 17 == 0 ? 5 : 3),
             new Monkey(
               new List<BigInteger>(new BigInteger[] { 61, 70, 97, 64, 99, 83, 52, 87 }),
                item => item + 8,
                item => item % 2 == 0 ? 7 : 6),
             new Monkey(
               new List<BigInteger>(new BigInteger[] { 60, 67, 80, 65 }),
                item => item * 13,
                item => item % 5 == 0 ? 1 : 6),
             new Monkey(
               new List<BigInteger>(new BigInteger[] { 61, 70, 76, 69, 82, 56 }),
                item => item + 7,
                item => item % 3 == 0 ? 5 : 2),
             new Monkey(
               new List<BigInteger>(new BigInteger[] { 79, 98 }),
                item => item + 2,
                item => item % 7 == 0 ? 0 : 3),
             new Monkey(
               new List<BigInteger>(new BigInteger[] { 72, 79, 55 }),
                item => item + 1,
                item => item % 13 == 0 ? 2 : 1),
             new Monkey(
               new List<BigInteger>(new BigInteger[] { 63 }),
                item => item + 4,
                item => item % 19 == 0 ? 7 : 4),
             new Monkey(
               new List<BigInteger>(new BigInteger[] { 72, 51, 93, 63, 80, 86, 81 }),
                item => item * item,
                item => item % 11 == 0 ? 0 : 4),
        };
    }

    private class Monkey 
    {
        private List<BigInteger> items;

        private Func<BigInteger, BigInteger> inspectOp;

        private Func<BigInteger, int> throwTest;

        public int InspectCount { get; set; }

        public Monkey(List<BigInteger> items, Func<BigInteger, BigInteger> inspectOp, Func<BigInteger, int> throwTest)
        {
            this.items = items;
            this.inspectOp = inspectOp;
            this.throwTest = throwTest;
        }

        public void CatchItem(BigInteger item) 
        {
            items.Add(item);
        }

        public void ProcessItems(Monkey[] allMonkeys) 
        {
            var monkiesToThrowTo = new List<Monkey>();

            for (int i = 0; i < items.Count(); i++)
            {
                UpdateWorryLevel(i);
                var monkeyTo = throwTest(items[i]);
                var monkeyToThrowTo = allMonkeys[monkeyTo];
                monkiesToThrowTo.Add(monkeyToThrowTo);
            }


            var itemsToThrow = items.ToArray();
            items.Clear();
            for (int i = 0; i < monkiesToThrowTo.Count; i++)
            {
                monkiesToThrowTo[i].CatchItem(itemsToThrow[i]);
            }
        }

        private void UpdateWorryLevel(int itemIndex)
        {
            var newWorry = inspectOp(items[itemIndex]);
            InspectCount++;
           // items[itemIndex] = (int)Math.Floor(newWorry / 3d);
            items[itemIndex] = newWorry;
        }

        public List<BigInteger> GetAllItems()
        {
            return this.items;
        }

        public void ReduceWorry(int prod)
        {
            for (int i = 0; i < this.items.Count(); i++)
            {
                var newVal = this.items[i] % prod;
                this.items[i] = newVal;
            }
        }
    }
}


