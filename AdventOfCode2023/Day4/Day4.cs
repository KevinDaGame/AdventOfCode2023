namespace AdventOfCode2023.Day4;

[TestClass]
public class Day4 : IDay
{
    [TestMethod]
    public void Part1()
    {
        IEnumerable<string> input = PuzzleInputReader.ReadInput(4);
        int sum = 0;
        foreach (string line in input)
        {
            int winningNumbersStart = line.IndexOf(':') + 1;
            int winningNumbersEnd = line.IndexOf('|');
            List<int> winningNumbers = line.Substring(winningNumbersStart, winningNumbersEnd - winningNumbersStart)
                .Split(' ')
                .Where(num => num != string.Empty)
                .Select(int.Parse)
                .ToList();

            List<int> numbers = line.Substring(winningNumbersEnd + 1)
                .Split(' ')
                .Where(num => num != string.Empty)
                .Select(int.Parse)
                .ToList();

            int cardValue = 0;
            foreach (int number in numbers)
            {
                if (winningNumbers.Contains(number))
                {
                    cardValue = cardValue == 0 ? 1 : cardValue * 2;
                }
            }

            sum += cardValue;
        }

        Console.WriteLine(sum);
    }

    [TestMethod]
    public void Part2()
    {
        List<string> input = PuzzleInputReader.ReadInput(4).ToList();
        var sum = 0;

        var cards = new List<Card>();
        for (var i = 0; i < input.Count; i++)
        {
            string line = input[i];
            int winningNumbersStart = line.IndexOf(':') + 1;
            int winningNumbersEnd = line.IndexOf('|');
            List<int> winningNumbers = line.Substring(winningNumbersStart, winningNumbersEnd - winningNumbersStart)
                .Split(' ')
                .Where(num => num != string.Empty)
                .Select(int.Parse)
                .ToList();

            List<int> numbers = line.Substring(winningNumbersEnd + 1)
                .Split(' ')
                .Where(num => num != string.Empty)
                .Select(int.Parse)
                .ToList();
            var card = new Card(i, winningNumbers, numbers);
            cards.Add(card);
        }
        
        foreach (Card card in cards)
        {
            sum += EvaluateCard(card, cards);
        }

        Console.WriteLine(sum);
    }

    private int EvaluateCard(Card card, IReadOnlyList<Card> cards)
    {
        var sum = 1;
        int matches = card.GetMatchCount();

        for (var i = 0; i < matches; i++)
        {
            if (card.Index + 1 + i <= cards.Count)
                sum += EvaluateCard(cards[card.Index + i + 1], cards);
        }

        return sum;
    }


    [TestMethod]
    public void TestCard()
    {
        Card card = new Card(1, new List<int>() { 1, 3 }, new List<int>() { 1, 2, 3, 4, 5 });

        var result = card.GetMatchCount();

        Assert.AreEqual(2, result);
    }
}

public record Card(int Index, List<int> WinningNumbers, List<int> Numbers)
{
    public int GetMatchCount()
    {
        return Numbers.Intersect(WinningNumbers).Count();
    }
}