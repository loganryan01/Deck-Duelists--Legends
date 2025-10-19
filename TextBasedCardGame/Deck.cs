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
            for (int i = 0; i < 52; i++)
            {
                Card card = new Card((i + 1).ToString(), 0);
                cards.Add(card);
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
