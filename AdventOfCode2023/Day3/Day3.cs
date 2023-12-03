namespace AdventOfCode2023.Day3;

[TestClass]
public class Day3 : IDay
{
    [TestMethod]
    public void Part1()
    {
        var sum = 0;
        var input = PuzzleInputReader.ReadInput(3).ToList();

        for (var i = 0; i < input.Count; i++)
        {
            string line = input[i];

            string number = string.Empty;
            for (var j = 0; j < line.Length; j++)
            {
                var character = line[j];

                if (char.IsDigit(character))
                {
                    number += character;
                    continue;
                }

                if (number != string.Empty)
                {
                    //a part number is a number which is adjacent to a symbol (everything but a number and '.')
                    //Adjacent means horizontally, vertically, or diagonally

                    bool isPartNumber = false;
                    for (int k = i - 1 < 0 ? 0 : i - 1; k <= Math.Min(i + 1, input.Count-1); k++)
                    {
                        for (int l = j - number.Length - 1 < 0 ? 0 : j - number.Length - 1; l < Math.Min(j + 1, line.Length-1); l++)
                        {
                            if (input[k][l] != '.')
                            {
                                isPartNumber = true;
                            }
                        }
                    }

                    if (isPartNumber)
                    {
                        sum += int.Parse(number);
                    }

                    number = string.Empty;
                }
            }
        }

        Console.WriteLine(sum);
    }

    [TestMethod]
    public void Part2()
    {
        throw new NotImplementedException();
    }
}