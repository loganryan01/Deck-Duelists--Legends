using System;

namespace TextBasedCardGame
{
    /// <summary>
    /// Handles the enemy's turn.
    /// Responsible for drawing cards, selecting a card using AI logic,
    /// applying the effect, and attacking the player.
    /// </summary>
    public class EnemyTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            //------------------------------------------------
            // Draw game board
            //------------------------------------------------

            GameUtils.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);

            if (game.IsLogEnabled)
            {
                GameUtils.UpdateLog(game.Log);
            }

            //------------------------------------------------
            // Ensure enemy has 3 cards
            //------------------------------------------------

            while (game.Enemy.Hand.Count < GameConstants.MAX_HAND_SIZE)
            {
                Card card = game.Enemy.Deck.DrawCard();
                game.Enemy.Hand.Add(card);
            }

            //------------------------------------------------
            // Select card using AI priority logic
            //------------------------------------------------

            int chosenCardIndex = ChooseCard(game);

            Card chosenCard = game.Enemy.Hand[chosenCardIndex];

            //------------------------------------------------
            // Apply card effect
            //------------------------------------------------

            ApplyCardEffect(game, chosenCard);

            //------------------------------------------------
            // Display enemy action
            //------------------------------------------------

            GameUtils.ClearConsoleLine(GameConstants.BOX_FOUR_Y_POSITION);
            GameUtils.WriteAt(
                string.Format(GameConstants.ENEMY_ACTION_FORMAT, chosenCard.Name), 
                0,
                GameConstants.BOX_FOUR_Y_POSITION
            );

            //------------------------------------------------
            // Log enemy action
            //------------------------------------------------

            game.AddToLog(GameConstants.DEFAULT_ENEMY_NAME, game.Enemy.Hand[chosenCardIndex].Name);

            //------------------------------------------------
            // Remove card from hand
            //------------------------------------------------

            game.Enemy.Hand.RemoveAt(chosenCardIndex);

            //------------------------------------------------
            // Enemy attacks player
            //------------------------------------------------

            if (game.Enemy.HeroAttack > GameConstants.MINIMUM_ATTACK_NUMBER)
            {
                game.Player.ModifyHeroHealth(-game.Enemy.HeroAttack);
            }

            //------------------------------------------------
            // Delay before ending turn
            //------------------------------------------------

            Thread.Sleep(GameConstants.THREE_SECOND_DELAY);

            //------------------------------------------------
            // End turn
            //------------------------------------------------

            game.IncrementTurnNumber();

            gameTurnStateManager.TransitionTo(new PostEnemyTurnState());
        }

        //------------------------------------------------
        // AI Decision Logic
        //------------------------------------------------

        private int ChooseCard(Game game)
        {
            int chosenCardIndex = -1;

            var priorityList = 
                game.Enemy.HeroHealth > GameConstants.MINIMUM_ENEMY_OFFENSIVE_HEALTH 
                ? GameConstants.AI_OFFENSIVE_PRIORITY_LIST 
                : GameConstants.AI_DEFENSIVE_PRIORITY_LIST;

            // Search enemy hand for highest priority card
            for (int i = 0; i < priorityList.Count; i++)
            {
                chosenCardIndex = game.Enemy.Hand.FindIndex(
                    x => x.Effect == priorityList[i]
                );

                if (chosenCardIndex != -1)
                {
                    break;
                }
            }

            // Fallback (should never happen, but prevents crashes)
            if (chosenCardIndex == -1)
            {
                chosenCardIndex = 0;
            }

            return chosenCardIndex;
        }

        //------------------------------------------------
        // Card Effect Resolution
        //------------------------------------------------

        private void ApplyCardEffect(Game game, Card card)
        {
            switch (card.Effect)
            {
                case CardEffect.IncreaseHeroAttack:
                    game.Enemy.IncrementHeroAttack();
                    break;

                case CardEffect.IncreaseHeroHealth:
                    game.Enemy.ModifyHeroHealth(1);
                    break;

                case CardEffect.DecreaseEnemyAttack:
                    game.Player.DecrementHeroAttack();
                    break;

                case CardEffect.DecreaseEnemyHealth:
                    game.Player.ModifyHeroHealth(-1);
                    break;
            }
        }
    }
}
