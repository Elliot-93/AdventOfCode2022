using AoC_2022;

if (args.Length == 0)
{
    //var day2 = new Day_02();

    //var res = await day2.Solve_1();

    //Console.WriteLine($"Result: {res}");


    //var res2 = await day2.Solve_2();

    //Console.WriteLine($"Result: {res2}");
    await Solver.SolveLast(opt => opt.ClearConsole = false);
}
else if (args.Length == 1 && args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase))
{
    await Solver.SolveAll(opt =>
    {
        opt.ShowConstructorElapsedTime = true;
        opt.ShowTotalElapsedTimePerDay = true;
    });
}
else
{
    var indexes = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);

    await Solver.Solve(indexes.Where(i => i < uint.MaxValue));
}
