using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class GameTurnStateManager
    {
        private GameTurnState state = null;
        public GameTurnState State { get { return state; } }

        public GameTurnStateManager(GameTurnState state)
        {
            TransitionTo(state);
        }

        public void TransitionTo(GameTurnState state)
        {
            this.state = state;
            this.state.SetManager(this);
        }

        public void DoAction(Game game)
        {
            state.DoAction(game);
        }
    }
}
