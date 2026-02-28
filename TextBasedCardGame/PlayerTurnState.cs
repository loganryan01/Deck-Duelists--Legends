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
            if (game.IsLogEnabled)
            {
                GameUtils.UpdateLog(game.Log);
            }

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
                GameUtils.ClearConsoleLine(12 + i);
                GameUtils.WriteAt(string.Format(GameConstants.CARD_PRINT_FORMAT, i + 1, card.Name), 0, 12 + i);
                i++;
            }

            // Get Player input
            GameUtils.ClearConsoleLine(16);
            GameUtils.WriteAt("What card do you want to play?", 0, 16);

            bool successfulInput = false;
            while (!successfulInput)
            {
                try
                {
                    Console.CursorVisible = true;
                    int chosenCardIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (chosenCardIndex >= 0 && chosenCardIndex <= 2)
                    {
                        Console.CursorVisible = false;
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
                        game.AddToLog("Player", game.Player.Hand[chosenCardIndex].Name);

                        game.Player.Hand.RemoveAt(chosenCardIndex);

                        // Attack the Enemy hero
                        if (game.Player.HeroAttack > 0)
                        {
                            game.Enemy.DecreaseHeroHealth(game.Player.HeroAttack);
                        }

                        for (int j = 0; j < 3; j++)
                        {
                            GameUtils.ClearConsoleLine(12 + j);
                            if (j <= 1)
                            {
                                Card card = game.Player.Hand[j];
                                GameUtils.WriteAt(string.Format(GameConstants.CARD_PRINT_FORMAT, j + 1, card.Name), 0, 12 + j);
                            }
                        }

                        GameUtils.ClearConsoleLine(16);
                        GameUtils.ClearConsoleLine(17);

                        gameTurnStateManager.TransitionTo(new PostPlayerTurnState());

                        // End of turn
                        game.IncrementTurnNumber();
                    }
                    else
                    {
                        // Print message to tell the player they need input a number between 1 and 3
                        // Then reset the player's turn
                        GameUtils.ClearConsoleLine(16);
                        GameUtils.ClearConsoleLine(17);
                        GameUtils.WriteAt(GameConstants.INVAILD_INPUT_MESSAGE, 0, 16);
                    }
                }
                catch (Exception)
                {
                    // Print message to tell the player they need input a number and not a letter
                    // Then reset the player's turn
                    GameUtils.ClearConsoleLine(16);
                    GameUtils.ClearConsoleLine(17);
                    GameUtils.WriteAt(GameConstants.INVAILD_INPUT_MESSAGE, 0, 16);
                }
            }
        }
    }
}
