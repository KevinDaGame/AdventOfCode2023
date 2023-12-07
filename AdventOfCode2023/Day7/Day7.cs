namespace AdventOfCode2023.Day7;

[TestClass]
public class Day7 : IDay
{
    [TestMethod]
    public void Part1()
    {
        List<string> input = PuzzleInputReader.ReadInput(7).ToList();
        List<Hand> hands = CamelCards.ParseHands(input);
        List<Hand> orderedHands = hands.OrderBy(h => h).ToList();
        var total = 0;
        for (var i = 0; i < orderedHands.Count; i++)
        {
            var hand = orderedHands[i];
            total += (i + 1) * hand.Bet;
            
        }

        Console.WriteLine(total);
    }

    [TestMethod]
    public void Part1_Test_IComperable()
    {
        var bestHand = new Hand { Bet = 1, Cards = new[] { 'A', 'A', 'A', 'A', 'A' } };
        var worstHand = new Hand { Bet = 1, Cards = new[] { '2', '2', '2', '2', '1' } };
        
        Assert.AreEqual(1, bestHand.CompareTo(worstHand));
    }
    [TestMethod]
    public void Part1_Test_IComperable_Equal()
    {
        var bestHand = new Hand { Bet = 1, Cards = new[] { 'A', 'A', 'A', 'A', 'A' } };
        var worstHand = new Hand { Bet = 1, Cards = new[] { 'A', 'A', 'A', 'A', 'A' } };
        
        Assert.AreEqual(0, bestHand.CompareTo(worstHand));
    }


    [TestMethod]
    public void Part2()
    {
        List<string> input = PuzzleInputReader.ReadInput(7).ToList();
        List<Hand> hands = CamelCards.ParseHands(input);
        List<Hand> orderedHands = hands.OrderBy(h => h).ToList();
        var total = 0;
        for (var i = 0; i < orderedHands.Count; i++)
        {
            var hand = orderedHands[i];
            total += (i + 1) * hand.Bet;
            
        }

        Console.WriteLine(total);
    }
}