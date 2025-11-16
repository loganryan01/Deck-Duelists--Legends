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
            if (!game.IsSim)
                GameUtils.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);

            while (game.Enemy.Hand.Count < 3)
            {
                Card card = game.Enemy.Deck.DrawCard();
                game.Enemy.Hand.Add(card);
            }

            // Get card from enemy hand
            int chosenCardIndex = -1;
            List<int> priorityList = game.Enemy.HeroHealth > 5 ? new List<int>() { 0, 3, 2, 1 } : new List<int>() { 1, 2, 0, 3 };
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
                case 0:
                    game.Enemy.IncrementHeroAttack();
                    break;
                case 1:
                    game.Enemy.IncrementHeroHealth();
                    break;
                case 2:
                    game.Player.DecrementHeroAttack();
                    break;
                case 3:
                    game.Player.DecrementHeroHealth();
                    break;
            }
            if (!game.IsSim)
                Console.WriteLine(string.Format(GameConstants.ENEMY_ACTION_FORMAT, game.Enemy.Hand[chosenCardIndex].Name));
            game.Enemy.Hand.RemoveAt(chosenCardIndex);

            // Attack the Player hero
            if (game.Enemy.HeroAttack > 0)
            {
                game.Player.DecreaseHeroHealth(game.Enemy.HeroAttack);
            }

            // End of turn
            game.IncrementTurnNumber();

            gameTurnStateManager.TransitionTo(new PostEnemyTurnState());

            if (!game.IsSim)
                Console.WriteLine();
        }
    }
}
