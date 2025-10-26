using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class Deck
    {
        private const string cardPrintFormat = "[{0}] {1}";
        
        private readonly List<Card> cards = new List<Card>();
        public List<Card> Cards 
        { 
            get 
            { 
                return cards; 
            } 
        }
        
        public Deck() 
        {
            CreateDeck();
        }

        private void CreateDeck()
        {
            Card? card = null;
            for (int i = 0; i < 52; i++)
            {
                if (i < 13)
                {
                    card = new Card("+1 Hero Attack", 0);
                }
                else if (i < 26)
                {
                    card = new Card("+1 Hero Health", 1);
                }
                else if (i < 39)
                {
                    card = new Card("-1 Enemy Attack", 2);
                }
                else if (i < 52)
                {
                    card = new Card("-1 Enemy Health", 3);
                }

                if (card != null)
                {
                    cards.Add(card);
                }
            }
        }

        public void PrintDeck()
        {
            int cardIndex = 0;
            foreach (var card in cards)
            {
                Console.WriteLine(string.Format(cardPrintFormat, cardIndex + 1, card.Name));
                cardIndex++;
            }
        }

        public Card DrawCard()
        {
            Random random = new Random();
            int randomIndex = random.Next(cards.Count);

            Card chosenCard = cards[randomIndex];
            cards.Remove(chosenCard);
            return chosenCard;
        }
    }
}
