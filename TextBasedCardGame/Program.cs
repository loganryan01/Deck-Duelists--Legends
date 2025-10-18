namespace TextBasedCardGame
{
    static class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            Card card = deck.DrawCard();

            Console.WriteLine(card.Name);
        }
    }
}
