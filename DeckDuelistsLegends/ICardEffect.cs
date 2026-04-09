namespace TextBasedCardGame
{
    /// <summary>
    /// Interface for a card effect.
    /// </summary>
    public interface ICardEffect
    {
        /// <summary>
        /// Apply the card effect.
        /// </summary>
        void Apply(Game game, Player self, Player opponent);
    }
}
