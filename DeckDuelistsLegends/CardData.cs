namespace TextBasedCardGame
{
    /// <summary>
    /// Represents raw card data loaded from the external JSON file.
    /// 
    /// This class is NOT used directly during gameplay.
    /// It only stores card information before the CardFactory
    /// converts it into a playable Card object.
    /// </summary>
    public class CardData
    {
        /// <summary>
        /// The display name of the card.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The effect identifier used by the CardFactory
        /// to create the correct ICardEffect implementation.
        /// </summary>
        public string Effect { get; set; }
    }
}
