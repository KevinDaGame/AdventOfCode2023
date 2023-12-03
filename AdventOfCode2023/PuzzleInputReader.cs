namespace AdventOfCode2023;

public static class PuzzleInputReader
{
    public static IEnumerable<string> ReadInput(
        int day)
    {
        var path = $"../../../Day{day}/input";
        if (!File.Exists(path))
        {
            throw new Exception("The file is not there!");
        }

        string[] contents = File.ReadAllLines(path);
        return contents;
    }
}