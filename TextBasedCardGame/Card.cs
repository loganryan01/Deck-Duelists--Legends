namespace TextBasedCardGame
{
    /// <summary>
    /// Represents a single card in the game.
    /// 
    /// Each card has a name and an associated effect.
    /// </summary>
    public class Card
    {
        //------------------------------------------------
        // FIELDS & PROPERTIES
        //------------------------------------------------

        private readonly string name;
        public string Name => name;

        private readonly CardEffect effect;
        public CardEffect Effect => effect;

        //------------------------------------------------
        // CONSTRUCTOR
        //------------------------------------------------

        public Card(string name, CardEffect effect)
        {
            this.name = name;
            this.effect = effect;
        }

        //------------------------------------------------
        // METHODS
        //------------------------------------------------

        /// <summary>
        /// Returns the card name when printed.
        /// </summary>
        public override string ToString()
        {
            return name;
        }
    }
}
