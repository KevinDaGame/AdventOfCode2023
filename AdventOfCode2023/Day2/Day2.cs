using System.Drawing;

namespace AdventOfCode2023.Day2;

[TestClass]
public class Day2 : IDay
{
    [TestMethod]
    public void Part1()
    {
        Dictionary<string, int> colors = new Dictionary<string, int>()
        {
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        };

        int sumOfIds = PuzzleInputReader.ReadInput(2).Select(x =>
        {
            var id = int.Parse(x[x.IndexOf(' ')..x.IndexOf(":", StringComparison.Ordinal)]);
            var subLine = x[(x.IndexOf(": ", StringComparison.Ordinal) + 2)..];
            var result = subLine
                .Split(';') // split each roll
                .Select(x => x.Split(',') // split each color
                    .Select(x => x.Trim())
                    .Select(x => new
                    {
                        Count = int.Parse(x.Substring(0, x.IndexOf(' '))),
                        Color = x.Substring((x.IndexOf(' ') + 1))
                    })
                    .Select(arg => colors[arg.Color] >= arg.Count)
                ).All(it => it.All(it2 => it2));
            return result ? id : 0;
        }).Sum();
        Console.WriteLine(sumOfIds);
    }

    [TestMethod]
    public void Part2()
    {
        Dictionary<string, int> colors = new Dictionary<string, int>()
        {
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        };

        int sumOfIds = PuzzleInputReader.ReadInput(2).Select(x =>
        {
            Dictionary<string, int> colors = new Dictionary<string, int>()
            {
                {"red", 12},
                {"green", 13},
                {"blue", 14}
            };
            var id = int.Parse(x[x.IndexOf(' ')..x.IndexOf(":", StringComparison.Ordinal)]);
            var subLine = x[(x.IndexOf(": ", StringComparison.Ordinal) + 2)..];
            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;
            var result = subLine
                .Split(';') // split each roll
                .Select(x => x.Split(',') // split each color
                    .Select(x => x.Trim())
                    .Select(x => new
                    {
                        Count = int.Parse(x.Substring(0, x.IndexOf(' '))),
                        Color = x.Substring((x.IndexOf(' ') + 1))
                    })
                    .Select(arg => colors[arg.Color] >= arg.Count)
                ).All(it => it.All(it2 => it2));
            return result ? id : 0;
        }).Sum();
        Console.WriteLine(sumOfIds);
    }
}