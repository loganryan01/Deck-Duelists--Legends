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
            switch (game.Enemy.Hand[0].EffectIndex)
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
                Console.WriteLine(string.Format(GameConstants.ENEMY_ACTION_FORMAT, game.Enemy.Hand[0].Name));
            game.Enemy.Hand.RemoveAt(0);

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
