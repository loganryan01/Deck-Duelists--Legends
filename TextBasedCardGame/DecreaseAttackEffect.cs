namespace TextBasedCardGame
{
    /// <summary>
    /// Decrease the attack of the opponent's hero by 1.
    /// </summary>
    class DecreaseAttackEffect : ICardEffect
    {
        public void Apply(Game game, Player self, Player opponent)
        {
            opponent.DecrementHeroAttack();
        }
    }
}
