using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class PlayerTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            GameUtils.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);

            // Draw Player hand
            while (game.Player.Hand.Count < 3)
            {
                Card card = game.Player.Deck.DrawCard();
                game.Player.Hand.Add(card);
            }

            // Print Player hand
            int i = 0;
            foreach (Card card in game.Player.Hand)
            {
                Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, i + 1, card.Name));
                i++;
            }
            Console.WriteLine(GameConstants.SPLITTER_TEXT);

            // Get Player input
            Console.WriteLine("What card do you want to play?");
            Console.WriteLine(GameConstants.SPLITTER_TEXT);

            bool successfulInput = false;
            while (!successfulInput)
            {
                try
                {
                    int chosenCardIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (chosenCardIndex >= 0 && chosenCardIndex <= 2)
                    {
                        successfulInput = true;

                        // Activate card's effect
                        switch (game.Player.Hand[chosenCardIndex].EffectIndex)
                        {
                            case (int)CardEffect.IncreaseHeroAttack:
                                game.Player.IncrementHeroAttack();
                                break;
                            case (int)CardEffect.IncreaseHeroHealth:
                                game.Player.IncrementHeroHealth();
                                break;
                            case (int)CardEffect.DecreaseEnemyAttack:
                                game.Enemy.DecrementHeroAttack();
                                break;
                            case (int)CardEffect.DecreaseEnemyHealth:
                                game.Enemy.DecrementHeroHealth();
                                break;
                        }
                        game.Player.Hand.RemoveAt(chosenCardIndex);

                        // Attack the Enemy hero
                        if (game.Player.HeroAttack > 0)
                        {
                            game.Enemy.DecreaseHeroHealth(game.Player.HeroAttack);
                        }

                        gameTurnStateManager.TransitionTo(new PostPlayerTurnState());

                        // End of turn
                        game.IncrementTurnNumber();
                        Console.WriteLine();
                    }
                    else
                    {
                        // Print message to tell the player they need input a number between 1 and 3
                        // Then reset the player's turn
                        Console.WriteLine("Need a number between 1 and 3");
                    }
                }
                catch (Exception)
                {
                    // Print message to tell the player they need input a number and not a letter
                    // Then reset the player's turn
                    Console.WriteLine("Need a number between 1 and 3");
                }
            }
        }
    }
}
