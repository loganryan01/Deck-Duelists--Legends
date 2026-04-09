namespace TextBasedCardGame
{
    /// <summary>
    /// Decrease the opponent hero's health by 1.
    /// </summary>
    class DecreaseHealthEffect : ICardEffect
    {
        public void Apply(Game game, Player self, Player opponent)
        {
            opponent.ModifyHeroHealth(-1);
        }
    }
}
