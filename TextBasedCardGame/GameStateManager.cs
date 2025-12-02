using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class GameStateManager
    {
        private GameState gameState = null;

        public GameStateManager(GameState gameState) 
        {
            TransitionTo(gameState);
        }

        public void TransitionTo(GameState gameState)
        {
            this.gameState = gameState;
            this.gameState.SetManager(this);
        }

        public void DoAction(Game game)
        {
            gameState.DoAction(game);
        }
    }
}
