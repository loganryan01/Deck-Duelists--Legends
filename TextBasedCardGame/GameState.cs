using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public abstract class GameState
    {
        protected GameStateManager gameStateManager;

        public void SetManager(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        public abstract void DoAction(Game game);
    }
}
