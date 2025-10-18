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

        public Card(string name)
        {
            this.name = name;
        }
    }
}
