namespace AdventOfCode2023.Day6;

[TestClass]
public class Day6 : IDay
{
    //correct 25200
    [TestMethod]
    public void Part1()
    {
        List<string> input = PuzzleInputReader.ReadInput(6).ToList();

        List<string> firstItems = input.First()[(input.First().IndexOf(':') + 1)..].Split(' ')
            .Where(num => num != string.Empty).ToList();
        List<string> secondItems = input.Last()[(input.Last().IndexOf(':') + 1)..].Split(' ')
            .Where(num => num != string.Empty).ToList();

        List<int> moreDistanceCounts = new List<int>();
        for (var i = 0; i < firstItems.Count; i++)
        {
            var time = int.Parse(firstItems[i]);
            var distance = int.Parse(secondItems[i]);
            var moreDistanceCount = (int)CalculateWinPermutations(time, distance);

            moreDistanceCounts.Add(moreDistanceCount);
        }

        Console.WriteLine(moreDistanceCounts.Aggregate((total, distance) => total * distance));
    }

    [TestMethod]
    public void Part2()
    {
        List<string> input = PuzzleInputReader.ReadInput(6).ToList();

        string firstItem = new string(input.First()[(input.First().IndexOf(':') + 1)..].ToCharArray()
            .Where(c => c != ' ').ToArray());
        string secondItem = new string(input.Last()[(input.Last().IndexOf(':') + 1)..].ToCharArray()
            .Where(c => c != ' ').ToArray());


        long time = long.Parse(firstItem);
        long distance = long.Parse(secondItem);

        var result = CalculateWinPermutations(time, distance);


        Console.WriteLine(result);
    }

    [TestMethod]
    public void TestCalculateWinPermutations()
    {
        //Arrange
        long time = 7;
        long distance = 9;
        
        //Act
        long expected = 4;
        long actual = CalculateWinPermutations(time, distance);
        //Assert
        Assert.AreEqual(expected, actual);
        
    }
    
    [TestMethod]
    public void TestCalculateDistanceForTime()
    {
        long heldTime = 42934733;
        long time = 48876981;

        var result = CalculateDistanceForTime(heldTime, time);
        
        Assert.IsTrue(result > 255128811171623);

    }
    
    private long CalculateWinPermutations(long time, long distance)
    {
        long minimumTime = MinimumTime(time, distance);
        long maximumTime = MaximumTime(time, distance);
        
        return maximumTime - minimumTime;
    }

    private long MaximumTime(long time, long distance)
    {
        long maximumTime = 0;
        long min = 0;
        long max = time;
        while (min <= max)
        {
            long mid = (min + max) / 2;
            long value = CalculateDistanceForTime(mid, time);
            if (value <= distance)
            {
                max = mid - 1;
            }
            else
            {
                maximumTime = mid + 1;
                min = mid + 1;
            }
        }

        return maximumTime;
    }

    private long MinimumTime(long time, long distance)
    {
        long minimumTime = 0;
        long min = 0;
        long max = time;
        while (min <= max)
        {
            long mid = (min + max) / 2;
            long value = CalculateDistanceForTime(mid, time);
            if (value > distance)
            {
                max = mid - 1;
                minimumTime = mid;
            }
            else
            {
                min = mid + 1;
            }
        }

        return minimumTime;
    }

    public long CalculateDistanceForTime(long timeHeld, long time)
    {
        long speed = timeHeld;
        long remainingTime = time - timeHeld;

        return speed * remainingTime;
    }
}