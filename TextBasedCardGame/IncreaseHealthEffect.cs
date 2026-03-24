namespace TextBasedCardGame
{
    /// <summary>
    /// Increase the hero's health by 1.
    /// </summary>
    class IncreaseHealthEffect : ICardEffect
    {
        public void Apply(Game game, Player self, Player opponent)
        {
            self.ModifyHeroHealth(1);
        }
    }
}
