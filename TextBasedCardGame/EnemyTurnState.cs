using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class EnemyTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            GameUtils.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);
            GameUtils.DrawLog(game.Log);

            while (game.Enemy.Hand.Count < 3)
            {
                Card card = game.Enemy.Deck.DrawCard();
                game.Enemy.Hand.Add(card);
            }

            // Get card from enemy hand
            int chosenCardIndex = -1;
            var priorityList = game.Enemy.HeroHealth > 5 ? GameConstants.AI_OFFENSIVE_PRIORITY_LIST : GameConstants.AI_DEFENSIVE_PRIORITY_LIST;
            for (int i = 0; i < 4; i++)
            {
                chosenCardIndex = game.Enemy.Hand.FindIndex(x => x.EffectIndex == priorityList[i]);
                if (chosenCardIndex != -1)
                {
                    break;
                }
            }

            switch (game.Enemy.Hand[chosenCardIndex].EffectIndex)
            {
                case (int)CardEffect.IncreaseHeroAttack:
                    game.Enemy.IncrementHeroAttack();
                    break;
                case (int)CardEffect.IncreaseHeroHealth:
                    game.Enemy.IncrementHeroHealth();
                    break;
                case (int)CardEffect.DecreaseEnemyAttack:
                    game.Player.DecrementHeroAttack();
                    break;
                case (int)CardEffect.DecreaseEnemyHealth:
                    game.Player.DecrementHeroHealth();
                    break;
            }
            Console.WriteLine(string.Format(GameConstants.ENEMY_ACTION_FORMAT, game.Enemy.Hand[chosenCardIndex].Name));

            game.AddToLog("Enemy", game.Enemy.Hand[chosenCardIndex].Name);

            game.Enemy.Hand.RemoveAt(chosenCardIndex);

            // Attack the Player hero
            if (game.Enemy.HeroAttack > 0)
            {
                game.Player.DecreaseHeroHealth(game.Enemy.HeroAttack);
            }

            // Wait 3 seconds before doing end turn checks
            Thread.Sleep(3000);

            // End of turn
            game.IncrementTurnNumber();

            gameTurnStateManager.TransitionTo(new PostEnemyTurnState());

            Console.WriteLine();
        }
    }
}
