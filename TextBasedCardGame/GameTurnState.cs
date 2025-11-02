using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public abstract class GameTurnState
    {
        protected GameTurnStateManager gameTurnStateManager;

        public void SetManager(GameTurnStateManager gameTurnStateManager)
        {
            this.gameTurnStateManager = gameTurnStateManager;
        }
        
        public abstract void DoAction(Game game);
    }
}
