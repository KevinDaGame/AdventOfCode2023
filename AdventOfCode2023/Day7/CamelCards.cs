namespace AdventOfCode2023.Day7;

public class CamelCards
{
    public static List<Hand> ParseHands(IEnumerable<string> hands)
    {
        return (
            from hand in hands
            let i = hand.Split(' ')
            select new Hand { Bet = int.Parse(i[1]), Cards = i[0].ToCharArray() }
        ).ToList();
    }
}

public class Hand : IComparable<Hand>
{
    public char[] Cards { get; set; }
    public int Bet { get; set; }

    public int JokerCount => Cards.Count(c => c == 'J');
    public bool IsFiveOfAKind => Cards.GroupBy(c => c).Any(g => g.Count()  == 5);

    public bool IsFourOfAKind => Cards.GroupBy(c => c).Any(g => g.Count() == 5);

    public bool IsFullHouse => Cards.GroupBy(c => c).Any(g => g.Count() == 3) &&
                               Cards.GroupBy(c => c).Count() == 2;

    public bool IsThreeOfAKind => Cards.GroupBy(c => c).Any(g => g.Count() == 3) &&
                                  Cards.GroupBy(c => c).Count() == 3;

    public bool IsTwoPair => Cards.GroupBy(c => c).Count(g => g.Count()  == 2) == 2;

    public bool IsOnePair => Cards.GroupBy(c => c).Count(g => g.Count()  == 2) == 1;
    public bool IsHighCard => Cards.GroupBy(c => c).Count()  == 5;

    public int CompareTo(Hand? other)
    {
        if (other == null) throw new InvalidOperationException("Hand must not be null");
        //compare cards
        if (Cards.Equals(other.Cards)) return 0;

        // if one is five of a kind AND other is not
        if (IsFiveOfAKind && !other.IsFiveOfAKind) return 1;
        if (other.IsFiveOfAKind && !IsFiveOfAKind) return -1;
        if (IsFiveOfAKind && other.IsFiveOfAKind) return CompareHandValues(other);

        // if one is four of a kind AND other is not
        if (IsFourOfAKind && !other.IsFourOfAKind) return 1;
        if (other.IsFourOfAKind && !IsFourOfAKind) return -1;
        if (IsFourOfAKind && other.IsFourOfAKind) return CompareHandValues(other);

        // if one is full house AND other is not
        if (IsFullHouse && !other.IsFullHouse) return 1;
        if (other.IsFullHouse && !IsFullHouse) return -1;
        if (IsFullHouse && other.IsFullHouse) return CompareHandValues(other);

        // if one is three of a kind AND other is not
        if (IsThreeOfAKind && !other.IsThreeOfAKind) return 1;
        if (other.IsThreeOfAKind && !IsThreeOfAKind) return -1;
        if (IsThreeOfAKind && other.IsThreeOfAKind) return CompareHandValues(other);

        // if one is two pair AND other is not
        if (IsTwoPair && !other.IsTwoPair) return 1;
        if (other.IsTwoPair && !IsTwoPair) return -1;
        if (IsTwoPair && other.IsTwoPair) return CompareHandValues(other);

        // if one is one pair AND other is not
        if (IsOnePair && !other.IsOnePair) return 1;
        if (other.IsOnePair && !IsOnePair) return -1;
        if (IsOnePair && other.IsOnePair) return CompareHandValues(other);

        // if one is high card AND other is not
        if (IsHighCard && !other.IsHighCard) return 1;
        if (other.IsHighCard && !IsHighCard) return -1;
        if (IsHighCard && other.IsHighCard) return CompareHandValues(other);

        return CompareHandValues(other);
    }

    //last is least valuable
    private static List<char> _orderedCards = new List<char>()
        { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };

    private int CompareHandValues(Hand other)
    {
        for (var i = 0; i < Cards.Length; i++)
        {
            int cardIndex = _orderedCards.IndexOf(Cards[i]);
            int otherIndex = _orderedCards.IndexOf(other.Cards[i]);

            if (cardIndex < otherIndex) return 1;
            if (cardIndex > otherIndex) return -1;
        }

        return 0;
    }
}