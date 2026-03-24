namespace TextBasedCardGame
{
    /// <summary>
    /// Increase the hero's attack by 1.
    /// </summary>
    class IncreaseAttackEffect : ICardEffect
    {
        public void Apply(Game game, Player self, Player opponent)
        {
            self.IncrementHeroAttack();
        }
    }
}
