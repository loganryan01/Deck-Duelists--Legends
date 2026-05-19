namespace TextBasedCardGame
{
    /// <summary>
    /// Responsible for converting raw CardData loaded from JSON
    /// into fully playable Card objects.
    /// 
    /// This acts as the bridge between:
    /// JSON data -> gameplay objects
    /// </summary>
    public static class CardFactory
    {
        /// <summary>
        /// Creates a playable Card using the provided card data.
        /// </summary>
        public static Card CreateCard(CardData data)
        {
            return new Card(
                data.Name, 
                CreateEffect(data.Effect)
                );
        }

        /// <summary>
        /// Creates the correct gameplay effect based on
        /// the effect identifier stored in the JSON data.
        /// </summary>
        private static ICardEffect CreateEffect(string effectName)
        {
            switch (effectName)
            {
                case "IncreaseAttack":
                    return new IncreaseAttackEffect();

                case "IncreaseHealth":
                    return new IncreaseHealthEffect();

                case "DecreaseAttack":
                    return new DecreaseAttackEffect();

                case "DecreaseHealth":
                    return new DecreaseHealthEffect();

                default:
                    throw new Exception($"Unknown card effect: {effectName}");
            }
        }
    }
}
