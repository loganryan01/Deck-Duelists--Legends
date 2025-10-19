using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class Card
    {
        private readonly string name;
        public string Name
        {
            get { return name; }
        }

        private readonly int effectIndex;
        public int EffectIndex
        {
            get { return effectIndex; }
        }

        public Card(string name, int effectIndex)
        {
            this.name = name;
            this.effectIndex = effectIndex;
        }
    }
}
